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
using DustInTheWind.ConsoleTools.Controls.InputControls;
using DustInTheWind.ConsoleTools.Controls.Menus;

namespace DustInTheWind.ConsoleTools.Demo.InputControlsDemo.Commands
{
    internal class ListReadWithCustomParserCommand : ICommand
    {
        public bool IsActive => true;

        public void Execute()
        {
            IReadOnlyList<ConsoleColor> colors = ReadColors();
            CustomConsole.WriteLine();

            DisplayColors(colors);
        }

        private static void DisplayColors(IReadOnlyList<ConsoleColor> colors)
        {
            CustomConsole.Write("Your prefered colors: ");

            for (int i = 0; i < colors.Count; i++)
            {
                CustomConsole.Write(colors[i], colors[i].ToString());

                if (i < colors.Count - 1)
                    CustomConsole.Write(", ");
            }

            CustomConsole.WriteLine();
        }

        private static IReadOnlyList<ConsoleColor> ReadColors()
        {
            ListView<ConsoleColor> colorsRead = new ListView<ConsoleColor>("What are your prefered colors?");
            colorsRead.CustomParser = value => (ConsoleColor)Enum.Parse(typeof(ConsoleColor), value, true);

            return colorsRead.Read();
        }
    }
}