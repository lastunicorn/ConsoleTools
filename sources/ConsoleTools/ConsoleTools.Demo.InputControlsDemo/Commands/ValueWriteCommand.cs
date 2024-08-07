﻿// ConsoleTools
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

using System;
using DustInTheWind.ConsoleTools.Controls.InputControls;
using DustInTheWind.ConsoleTools.Controls.Menus;

namespace DustInTheWind.ConsoleTools.Demo.InputControlsDemo.Commands
{
    internal class ValueWriteCommand : ICommand
    {
        public bool IsActive => true;

        public void Execute()
        {
            StringValue firstNameControl = new StringValue("First Name:");
            firstNameControl.Label.ForegroundColor = ConsoleColor.DarkGreen;

            StringValue lastNameControl = new StringValue("Last Name:");
            lastNameControl.Label.ForegroundColor = ConsoleColor.DarkGreen;

            Int32Value ageControl = new Int32Value("Age:");
            ageControl.Label.ForegroundColor = ConsoleColor.DarkGreen;

            firstNameControl.Value = "Joe";
            firstNameControl.Write();

            lastNameControl.Value = "Doe";
            lastNameControl.Write();

            ageControl.Value = 25;
            ageControl.Write();
        }
    }
}