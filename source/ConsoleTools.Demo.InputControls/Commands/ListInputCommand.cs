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
using DustInTheWind.ConsoleTools.InputControls;
using DustInTheWind.ConsoleTools.MenuControl;

namespace DustInTheWind.ConsoleTools.Demo.InputControls.Commands
{
    internal class ListInputCommand : ICommand
    {
        public bool IsActive => true;

        public void Execute()
        {
            ListInput<string> beveragesInput = new ListInput<string>("What are your prefered beverages?");
            ListInput<int> luckyNumbersInput = new ListInput<int>("What are your lucky number?");

            CustomConsole.WriteLine();
            List<string> beverages = beveragesInput.Read();
            List<int> luckyNumbers = luckyNumbersInput.Read();

            CustomConsole.WriteLine();
            CustomConsole.Write("Beverages you like: ");
            CustomConsole.WriteLineEmphasies(string.Join(", ", beverages));
            CustomConsole.Write("Your lucky numbers: ");
            CustomConsole.WriteLineEmphasies(string.Join(", ", luckyNumbers));
        }
    }
}