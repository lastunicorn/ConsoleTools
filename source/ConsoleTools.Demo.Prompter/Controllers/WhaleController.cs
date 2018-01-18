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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using DustInTheWind.ConsoleTools.CommandProviders;
using DustInTheWind.ConsoleTools.Demo.Prompter.Ocean;
using DustInTheWind.ConsoleTools.TabularData;

namespace DustInTheWind.ConsoleTools.Demo.Prompter.Controllers
{
    internal class WhaleController : IController
    {
        public void Execute(ReadOnlyCollection<UserCommandParameter> parameters)
        {
            DisplayWhales();
        }

        private static void DisplayWhales()
        {
            WhaleProvider whaleProvider = new WhaleProvider();
            IEnumerable<Whale> whales = whaleProvider.CreateWhales();

            Table table = CreateTable(whales);
            CustomConsole.WriteLine(table.ToString());
        }

        private static Table CreateTable(IEnumerable<Whale> whales)
        {
            Table table = new Table("Whales");

            table.Columns.Add(new Column("Name"));
            table.Columns.Add(new Column("Population"));
            table.Columns.Add(new Column("Weight"));

            foreach (Whale whale in whales)
            {
                table.Rows.Add(new[]
                {
                    whale.Name,
                    whale.Count,
                    whale.Weight
                });
            }

            return table;
        }
    }
}