// ConsoleTools
// Copyright (C) 2017 Dust in the Wind
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

using DustInTheWind.ConsoleTools.TabularData;

namespace DustInTheWind.ConsoleTools.Demo.TabularData.Flows
{
    internal class LongShortTitleFlow : IFlow
    {
        public void Execute()
        {
            DisplayTableWithLongTitle();
            DisplayTableWithShortTitle();
        }

        private static void DisplayTableWithLongTitle()
        {
            Table table1 = new Table("Long title - longer than the content of the table");

            table1.AddRow(new[] { "First item", 1.ToString() });
            table1.AddRow(new[] { "Second item", 2.ToString() });
            table1.AddRow(new[] { "Third item", 3.ToString() });
            table1.AddRow(new[] { "Forth item", 4.ToString() });
            table1.AddRow(new[] { "Fifth item", 5.ToString() });

            CustomConsole.WriteLine(table1.ToString());
        }

        private static void DisplayTableWithShortTitle()
        {
            Table table2 = new Table("Short title");

            table2.AddRow(new[] { "First item", 1.ToString() });
            table2.AddRow(new[] { "Second item", 2.ToString() });
            table2.AddRow(new[] { "Third item", 3.ToString() });
            table2.AddRow(new[] { "Forth item", 4.ToString() });
            table2.AddRow(new[] { "Fifth item", 5.ToString() });

            CustomConsole.WriteLine(table2.ToString());
        }
    }
}