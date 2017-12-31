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

using DustInTheWind.ConsoleTools.MenuControl;
using DustInTheWind.ConsoleTools.TabularData;

namespace DustInTheWind.ConsoleTools.Demo.TabularData.Commands
{
    internal class MultilineTitleCommand : ICommand
    {
        public bool IsActive => true;

        public void Execute()
        {
            Table table = new Table("If you need to write the title\ron more then one line you can do that.\nMark the new line with one of the following:\r\nCR (\\r), LF (\\n) or CRLF (\\r\\n)");

            table.AddRow(new[] { "First item", 1.ToString() });
            table.AddRow(new[] { "Second item", 2.ToString() });
            table.AddRow(new[] { "Third item", 3.ToString() });
            table.AddRow(new[] { "Forth item", 4.ToString() });
            table.AddRow(new[] { "Fifth item", 5.ToString() });

            CustomConsole.WriteLine(table.ToString());
        }
    }
}