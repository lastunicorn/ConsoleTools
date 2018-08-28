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
    internal class ValueReadStringCommand : ICommand
    {
        public bool IsActive => true;

        public void Execute()
        {
            RunExample();
        }

        /// <summary>
        /// This example creates instances for each input value and sets different label colors.
        /// Each instance reads a different type of value (string, int, DateTime, float)
        /// </summary>
        private static void RunExample()
        {
            // Create the input controls
            ValueView<string> firstNameView = new ValueView<string>("First Name:");
            firstNameView.Label.ForegroundColor = ConsoleColor.Cyan;

            ValueView<string> lastNameView = new ValueView<string>("Last Name:");
            lastNameView.Label.ForegroundColor = ConsoleColor.Cyan;

            // Read values using the input controls
            firstNameView.Display();
            string firstName = firstNameView.Value;

            lastNameView.Display();
            string lastName = lastNameView.Value;

            // Display th read values.
            CustomConsole.WriteLine();
            CustomConsole.WriteLine("Hi, {0} {1}!", firstName, lastName);
        }
    }
}