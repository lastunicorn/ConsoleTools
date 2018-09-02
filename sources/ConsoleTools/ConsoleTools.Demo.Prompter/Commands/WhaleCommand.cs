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
using DustInTheWind.ConsoleTools.CommandProviders;
using DustInTheWind.ConsoleTools.Demo.Prompter.Ocean;
using DustInTheWind.ConsoleTools.TabularData;

namespace DustInTheWind.ConsoleTools.Demo.Prompter.Controllers
{
    internal class WhaleCommand : IPrompterCommand
    {
        public bool IsActive { get; } = true;

        public void Execute(CliCommand cliCommand)
        {
            DisplayWhales();
        }

        private static void DisplayWhales()
        {
            WhaleProvider whaleProvider = new WhaleProvider();
            IEnumerable<Whale> whales = whaleProvider.CreateWhales();

            DataGrid dataGrid = CreateTable(whales);
            dataGrid.Display();
        }

        private static DataGrid CreateTable(IEnumerable<Whale> whales)
        {
            DataGrid dataGrid = new DataGrid("Whales");

            dataGrid.Columns.Add(new Column("Name"));
            dataGrid.Columns.Add(new Column("Population"));
            dataGrid.Columns.Add(new Column("Weight"));

            foreach (Whale whale in whales)
                dataGrid.Rows.Add(whale.Name, whale.Count, whale.Weight);

            return dataGrid;
        }
    }
}