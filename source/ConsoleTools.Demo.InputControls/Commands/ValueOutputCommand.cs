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
    internal class ValueOutputCommand : ICommand
    {
        public bool IsActive => true;

        public void Execute()
        {
            DisplayData();
            // or
            //DisplayDataQuick();
        }

        private static void DisplayData()
        {
            ValueOutput valueOutput = new ValueOutput();

            valueOutput.LabelForegroundColor = ConsoleColor.DarkGreen;

            valueOutput.Write("First Name:", "John");
            valueOutput.Write("Last Name:", "Doe");
            valueOutput.Write("Age:", 25);
        }

        private void DisplayDataQuick()
        {
            ValueOutput.QuickWrite("First Name:", "John");
            ValueOutput.QuickWrite("Last Name:", "Doe");
            ValueOutput.QuickWrite("Age:", 25);
        }
    }
}