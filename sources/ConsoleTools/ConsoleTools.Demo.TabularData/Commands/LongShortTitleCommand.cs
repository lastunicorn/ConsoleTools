// ConsoleTools
// Copyright (C) 2017-2024 Dust in the Wind
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

using DustInTheWind.ConsoleTools.Controls.Menus;
using DustInTheWind.ConsoleTools.Controls.Tables;

namespace DustInTheWind.ConsoleTools.Demo.TabularData.Commands
{
    internal class LongShortTitleCommand : ICommand
    {
        public bool IsActive => true;

        public void Execute()
        {
            DisplayTableWithLongTitle();
            DisplayTableWithShortTitle();
        }

        private static void DisplayTableWithLongTitle()
        {
            DataGrid dataGrid = new DataGrid("Long title - longer than the content of the table");

            dataGrid.Rows.Add("First item", 1.ToString());
            dataGrid.Rows.Add("Second item", 2.ToString());
            dataGrid.Rows.Add("Third item", 3.ToString());
            dataGrid.Rows.Add("Forth item", 4.ToString());
            dataGrid.Rows.Add("Fifth item", 5.ToString());

            dataGrid.Display();
        }

        private static void DisplayTableWithShortTitle()
        {
            DataGrid dataGrid = new DataGrid("Short title");

            dataGrid.Rows.Add("First item", 1.ToString());
            dataGrid.Rows.Add("Second item", 2.ToString());
            dataGrid.Rows.Add("Third item", 3.ToString());
            dataGrid.Rows.Add("Forth item", 4.ToString());
            dataGrid.Rows.Add("Fifth item", 5.ToString());

            dataGrid.Display();
        }
    }
}