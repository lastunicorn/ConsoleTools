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

using System;
using DustInTheWind.ConsoleTools.InputControls;
using DustInTheWind.ConsoleTools.MenuControl;

namespace DustInTheWind.ConsoleTools.Demo.InputControls.Commands
{
    internal class ValueWriteCommand : ICommand
    {
        public bool IsActive => true;

        public void Execute()
        {
            StringOutput firstNameOutput = new StringOutput("First Name:") { LabelForegroundColor = ConsoleColor.DarkGreen };
            StringOutput lastNameOutput = new StringOutput("Last Name:") { LabelForegroundColor = ConsoleColor.DarkGreen };
            Int32Output ageOutput = new Int32Output("Age:") { LabelForegroundColor = ConsoleColor.DarkGreen };
            
            firstNameOutput.Value = "Joe";
            firstNameOutput.Display();

            lastNameOutput.Value = "Doe";
            lastNameOutput.Display();

            ageOutput.Value = 25;
            ageOutput.Display();
        }
    }
}