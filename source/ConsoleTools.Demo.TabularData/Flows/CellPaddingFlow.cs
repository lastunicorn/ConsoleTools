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
    internal class CellPaddingFlow : IFlow
    {
        public void Execute()
        {
            DisplayDefaultPaddingExample();
            DisplayPaddingExample();
            DisplayPaddingLeftExample();
            DisplayPaddingRightExample();
        }

        private static void DisplayDefaultPaddingExample()
        {
            Table table = new Table("Default Padding is 1");

            table.AddRow(new[] { "First item", 1.ToString() });
            table.AddRow(new[] { "Second item", 2.ToString() });
            table.AddRow(new[] { "Third item", 3.ToString() });
            table.AddRow(new[] { "Forth item", 4.ToString() });
            table.AddRow(new[] { "Fifth item", 5.ToString() });

            CustomConsole.WriteLine(table.ToString());
        }

        private static void DisplayPaddingExample()
        {
            Table table = new Table("Padding = 3");

            table.AddRow(new[] { "First item", 1.ToString() });
            table.AddRow(new[] { "Second item", 2.ToString() });
            table.AddRow(new[] { "Third item", 3.ToString() });
            table.AddRow(new[] { "Forth item", 4.ToString() });
            table.AddRow(new[] { "Fifth item", 5.ToString() });

            table.Padding = 3;

            CustomConsole.WriteLine(table.ToString());
        }

        private static void DisplayPaddingLeftExample()
        {
            Table table = new Table("Padding left = 3");

            table.AddRow(new[] { "First item", 1.ToString() });
            table.AddRow(new[] { "Second item", 2.ToString() });
            table.AddRow(new[] { "Third item", 3.ToString() });
            table.AddRow(new[] { "Forth item", 4.ToString() });
            table.AddRow(new[] { "Fifth item", 5.ToString() });

            table.PaddingLeft = 3;

            CustomConsole.WriteLine(table.ToString());
        }

        private static void DisplayPaddingRightExample()
        {
            Table table = new Table("Padding right = 3");

            table.AddRow(new[] { "First item", 1.ToString() });
            table.AddRow(new[] { "Second item", 2.ToString() });
            table.AddRow(new[] { "Third item", 3.ToString() });
            table.AddRow(new[] { "Forth item", 4.ToString() });
            table.AddRow(new[] { "Fifth item", 5.ToString() });

            table.PaddingRight = 3;

            CustomConsole.WriteLine(table.ToString());
        }
    }
}