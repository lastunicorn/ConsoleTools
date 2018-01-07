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
    internal class TextInputCommand : ICommand
    {
        public bool IsActive => true;

        public void Execute()
        {
            RunExample_Instance();
            // or
            //RunExample_Quick();

            // Specify default value
            //RunExample_DefaultValue();
        }

        /// <summary>
        /// This example creates instances for each input value and sets different label colors.
        /// Each instance reads a different type of value (string, int, DateTime, float)
        /// </summary>
        private static void RunExample_Instance()
        {
            // Create the input controls
            TextInput<string> firstNameInput = new TextInput<string>("First Name:");
            firstNameInput.LabelForegroundColor = ConsoleColor.Cyan;
            TextInput<string> lastNameInput = new TextInput<string>("Last Name:");
            lastNameInput.LabelForegroundColor = ConsoleColor.Cyan;
            TextInput<int> ageInput = new TextInput<int>("Age:");
            ageInput.LabelForegroundColor = ConsoleColor.DarkGreen;
            TextInput<DateTime> birthdayInput = new TextInput<DateTime>("Birthday:");
            birthdayInput.LabelForegroundColor = ConsoleColor.DarkGreen;
            TextInput<float> heightInput = new TextInput<float>("Height (float):");
            heightInput.LabelForegroundColor = ConsoleColor.DarkGreen;

            // Read values using the input controls
            string firstName = firstNameInput.Read();
            string lastName = lastNameInput.Read();
            int age = ageInput.Read();
            DateTime birthday = birthdayInput.Read();
            float height = heightInput.Read();

            // Display th read values.
            CustomConsole.WriteLine();
            CustomConsole.WriteLine("Hi, {0} {1}!", firstName, lastName);
            CustomConsole.WriteLine("You are {0} years old.", age);
            CustomConsole.WriteLine("Your birthday is {0}.", birthday);
            CustomConsole.WriteLine("Your height is {0}.", height);
        }

        /// <summary>
        /// The QuickRead static method is used to read values from the console.
        /// </summary>
        private static void RunExample_Quick()
        {
            string firstName = TextInput<string>.QuickRead("First Name:");
            string lastName = TextInput<string>.QuickRead("Last Name:");
            int age = TextInput<int>.QuickRead("Age:");
            DateTime birthday = TextInput<DateTime>.QuickRead("Birthday:");
            float height = TextInput<float>.QuickRead("Height (float):");

            // Display th read values.
            CustomConsole.WriteLine();
            CustomConsole.WriteLine("Hi, {0} {1}!", firstName, lastName);
            CustomConsole.WriteLine("You are {0} years old.", age);
            CustomConsole.WriteLine("Your birthday is {0}.", birthday);
            CustomConsole.WriteLine("Your height is {0}.", height);
        }

        private static void RunExample_DefaultValue()
        {
            TextInput<int> numberInput = new TextInput<int>("Number:");
            numberInput.AcceptDefaultValue = true;
            numberInput.DefaultValue = 42;

            int number = numberInput.Read();

            CustomConsole.WriteLine();
            CustomConsole.WriteLine("You selected {0}.", number);
        }
    }
}