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
using System.Collections.Generic;
using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Controls.InputControls;
using DustInTheWind.ConsoleTools.Controls.Menus;

namespace DustInTheWind.ConsoleTools.Demo.InputControlsDemo.Commands
{
    internal class ListReadStringsCommand : ICommand
    {
        public bool IsActive => true;

        public void Execute()
        {
            CustomConsole.WriteLine("  This will demonstrates how to read a list of values using the");
            CustomConsole.WriteLine("  ListRead<T> control.");
            CustomConsole.WriteLine();
            CustomConsole.WriteLine("  By creating an instance of the ListRead<string>, it offers the possibility");
            CustomConsole.WriteLine("  to adjust different properties like the item's bullet and the label's color.");
            CustomConsole.WriteLine();

            IEnumerable<string> beverages = ReadBeverages();

            CustomConsole.WriteLine();

            CustomConsole.Write("Beverages you like: ");
            CustomConsole.WriteLineEmphasized(string.Join(", ", beverages));
        }

        /// <summary>
        /// By creating an instance of the <see cref="ListRead{T}"/>, additional properties can be set.
        /// </summary>
        private static List<string> ReadBeverages()
        {
            ValueList<string> beveragesRead = new ValueList<string>
            {
                Label = new TextBlock
                {
                    Text = "What are your prefered beverages?",
                    ForegroundColor = ConsoleColor.Cyan
                },
                Bullet = "#"
                // etc...
            };

            return beveragesRead.Read();
        }
    }
}