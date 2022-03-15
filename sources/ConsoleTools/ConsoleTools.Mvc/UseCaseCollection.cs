// ConsoleTools
// Copyright (C) 2017-2022 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

// --------------------------------------------------------------------------------
// Bugs or feature requests
// --------------------------------------------------------------------------------
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new/choose

using System;
using System.Collections.ObjectModel;
using System.Linq;
using DustInTheWind.ConsoleTools.Mvc.UseCases;

namespace DustInTheWind.ConsoleTools.Mvc
{
    public class UseCaseCollection : Collection<UseCaseCollectionItem>
    {
        public void Add(string key, IUseCase useCase)
        {
            Add(new UseCaseCollectionItem(key, useCase));
        }

        protected override void InsertItem(int index, UseCaseCollectionItem item)
        {
            if (this.Any(x => x.Key == item.Key))
                throw new ArgumentException("There is another use case with the same key.", nameof(item.Key));

            base.InsertItem(index, item);
        }

        public bool Contains(string commandKey)
        {
            return commandKey != null && this.Any(x => x.Key == commandKey);
        }

        public bool Contains(IUseCase useCase)
        {
            return useCase != null && this.Any(x => x.UseCase == useCase);
        }

        public IUseCase SelectCommand(string commandName)
        {
            IUseCase useCase;

            if (string.IsNullOrEmpty(commandName))
            {
                useCase = Items
                    .Select(x => x.UseCase)
                    .OfType<HelpUseCase>()
                    .FirstOrDefault();

                if (useCase == null)
                    throw new ConsoleFrameworkException("Please provide a use case name to execute.");
            }
            else
            {
                if (!Contains(commandName))
                    throw new ConsoleFrameworkException("Invalid use case.");

                useCase = this[commandName];
            }

            return useCase;
        }

        public IUseCase this[string commandKey]
        {
            get
            {
                if (commandKey == null)
                    return null;

                return Items
                    .Where(x => x.Key == commandKey)
                    .Select(x => x.UseCase)
                    .FirstOrDefault();
            }
        }
    }
}