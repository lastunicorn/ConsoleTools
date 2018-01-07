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
            TextInput<string> firstNameInput = new TextInput<string>("First Name:");
            TextInput<string> lastNameInput = new TextInput<string>("Last Name:");
            TextInput<int> ageInput = new TextInput<int>("Age:");
            TextInput<DateTime> birthdayInput = new TextInput<DateTime>("Birthday:");
            TextInput<float> heightInput = new TextInput<float>("Height (float):");
            
            string firstName = firstNameInput.Read();
            string lastName = lastNameInput.Read();
            int age = ageInput.Read();
            DateTime birthday = birthdayInput.Read();
            float height = heightInput.Read();

            CustomConsole.WriteLine();
            CustomConsole.WriteLine("Hi, {0} {1}!", firstName, lastName);
            CustomConsole.WriteLine("You are {0} years old.", age);
            CustomConsole.WriteLine("Your birthday is {0}.", birthday);
            CustomConsole.WriteLine("Your height is {0}.", height);
        }
    }
}