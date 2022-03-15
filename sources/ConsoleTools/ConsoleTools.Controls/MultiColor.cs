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
    public class MultiColor : Control
    {
        protected override void DoDisplay()
        {
            CustomConsole.WriteLine("Player", ConsoleColor.Black);
            CustomConsole.WriteLine("Player", ConsoleColor.White);
            CustomConsole.WriteLine("Player", ConsoleColor.Gray);
            CustomConsole.WriteLine("Player", ConsoleColor.DarkGray);

            CustomConsole.WriteLine("Player", ConsoleColor.Blue);
            CustomConsole.WriteLine("Player", ConsoleColor.Green);
            CustomConsole.WriteLine("Player", ConsoleColor.Cyan);
            CustomConsole.WriteLine("Player", ConsoleColor.Red);
            CustomConsole.WriteLine("Player", ConsoleColor.Magenta);
            CustomConsole.WriteLine("Player", ConsoleColor.Yellow);
            
            CustomConsole.WriteLine("Player", ConsoleColor.DarkBlue);
            CustomConsole.WriteLine("Player", ConsoleColor.DarkGreen);
            CustomConsole.WriteLine("Player", ConsoleColor.DarkCyan);
            CustomConsole.WriteLine("Player", ConsoleColor.DarkRed);
            CustomConsole.WriteLine("Player", ConsoleColor.DarkMagenta);
            CustomConsole.WriteLine("Player", ConsoleColor.DarkYellow);
        }
    }
}
