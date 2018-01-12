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
    internal class ValueReadQuickCommand : ICommand
    {
        public bool IsActive => true;

        public void Execute()
        {
            RunExample();
        }

        /// <summary>
        /// The QuickRead static method is used to read values from the console.
        /// </summary>
        private static void RunExample()
        {
            string firstName = ValueInput<string>.QuickRead("First Name:");
            string lastName = ValueInput<string>.QuickRead("Last Name:");
            int age = ValueInput<int>.QuickRead("Age:");
            DateTime birthday = ValueInput<DateTime>.QuickRead("Birthday:");
            float height = ValueInput<float>.QuickRead("Height (float):");

            // Display th read values.
            CustomConsole.WriteLine();
            CustomConsole.WriteLine("Hi, {0} {1}!", firstName, lastName);
            CustomConsole.WriteLine("You are {0} years old.", age);
            CustomConsole.WriteLine("Your birthday is {0}.", birthday);
            CustomConsole.WriteLine("Your height is {0}.", height);
        }
    }
}