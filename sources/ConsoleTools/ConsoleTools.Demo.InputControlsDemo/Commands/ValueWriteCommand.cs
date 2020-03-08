// ConsoleTools
// Copyright (C) 2017-2020 Dust in the Wind
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
using DustInTheWind.ConsoleTools.Controls.Menus;
using DustInTheWind.ConsoleTools.InputControls;

namespace DustInTheWind.ConsoleTools.Demo.InputControlsDemo.Commands
{
    internal class ValueWriteCommand : ICommand
    {
        public bool IsActive => true;

        public void Execute()
        {
            StringView firstNameView = new StringView("First Name:");
            firstNameView.Label.ForegroundColor = ConsoleColor.DarkGreen;

            StringView lastNameView = new StringView("Last Name:");
            lastNameView.Label.ForegroundColor = ConsoleColor.DarkGreen;

            Int32View ageView = new Int32View("Age:");
            ageView.Label.ForegroundColor = ConsoleColor.DarkGreen;


            firstNameView.Value = "Joe";
            firstNameView.Write();

            lastNameView.Value = "Doe";
            lastNameView.Write();

            ageView.Value = 25;
            ageView.Write();
        }
    }
}