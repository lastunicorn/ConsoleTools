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

using System.Collections.Generic;
using DustInTheWind.ConsoleTools.Controls.InputControls;
using DustInTheWind.ConsoleTools.Controls.Menus;

namespace DustInTheWind.ConsoleTools.Demo.InputControlsDemo.Commands
{
    internal class ListReadNumbersCommand : ICommand
    {
        public bool IsActive => true;

        public void Execute()
        {
            CustomConsole.WriteLine();

            IEnumerable<int> numbers = ReadNumbers();

            CustomConsole.WriteLine();

            CustomConsole.Write("Your lucky numbers: ");
            CustomConsole.WriteLineEmphasized(string.Join(", ", numbers));
        }

        private static IEnumerable<int> ReadNumbers()
        {
            ValueList<int> luckyNumbersRead = new ValueList<int>("What are your lucky number?");
            luckyNumbersRead.Read();
            return luckyNumbersRead.Values;
        }
    }
}