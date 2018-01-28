// ConsoleTools
// Copyright (C) 2017-2018 Dust in the Wind
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
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new

using System;
using DustIntheWind.ConsoleTools.Tutorial.AddressBookModel;
using DustIntheWind.ConsoleTools.Tutorial.CustomControls;
using DustInTheWind.ConsoleTools.MenuControl;

namespace DustIntheWind.ConsoleTools.Tutorial.Commands
{
    internal class DisplayPersonsCommand : ICommand
    {
        private readonly AddressBook addressBook;

        public bool IsActive { get; } = true;

        public DisplayPersonsCommand(AddressBook addressBook)
        {
            if (addressBook == null) throw new ArgumentNullException(nameof(addressBook));
            this.addressBook = addressBook;
        }

        public void Execute()
        {
            PersonsView personsView = new PersonsView
            {
                Persons = addressBook
            };

            personsView.Display();
        }
    }
}