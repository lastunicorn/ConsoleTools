// ConsoleTools
// Copyright (C) 2017-2022 Dust in the Wind
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

// --------------------------------------------------------------------------------
// Bugs or feature requests
// --------------------------------------------------------------------------------
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new/choose

using System;

namespace DustInTheWind.ConsoleTools.Controls
{
    public class MultiColor : BlockControl
    {
        protected override void DoDisplayContent(IDisplay display)
        {
            DisplayLine(display, ConsoleColor.Black, "Black");
            DisplayLine(display, ConsoleColor.White, "White");
            DisplayLine(display, ConsoleColor.Gray, "Gray");
            DisplayLine(display, ConsoleColor.DarkGray, "DarkGray");
            display.WriteRow();
            DisplayLine(display, ConsoleColor.Blue, "Blue");
            DisplayLine(display, ConsoleColor.Green, "Green");
            DisplayLine(display, ConsoleColor.Cyan, "Cyan");
            DisplayLine(display, ConsoleColor.Red, "Red");
            DisplayLine(display, ConsoleColor.Magenta, "Magenta");
            DisplayLine(display, ConsoleColor.Yellow, "Yellow");
            display.WriteRow();
            DisplayLine(display, ConsoleColor.DarkBlue, "DarkBlue");
            DisplayLine(display, ConsoleColor.DarkGreen, "DarkGreen");
            DisplayLine(display, ConsoleColor.DarkCyan, "DarkCyan");
            DisplayLine(display, ConsoleColor.DarkRed, "DarkRed");
            DisplayLine(display, ConsoleColor.DarkMagenta, "DarkMagenta");
            DisplayLine(display, ConsoleColor.DarkYellow, "DarkYellow");
        }

        private static void DisplayLine(IDisplay display, ConsoleColor foregroundColor, string text)
        {
            display.StartRow();
            display.Write("» ");
            display.Write(foregroundColor, null, text);
            display.EndRow();
        }
    }
}
