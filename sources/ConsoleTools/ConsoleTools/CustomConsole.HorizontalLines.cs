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

// --------------------------------------------------------------------------------
// Bugs or feature requests
// --------------------------------------------------------------------------------
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new

using System;

namespace DustInTheWind.ConsoleTools
{
    /// <summary>
    /// Contains a set of methods that help to write text to the Console.
    /// </summary>
    public static partial class CustomConsole
    {
        /// <summary>
        /// Builds a string repeating the specified character and having the leangth equal to the width of the console's window.
        /// </summary>
        public static string BuildHorizontalWindowLine(char c = '-') => new string(c, Console.WindowWidth);

        public static void WriteHorizontalWindowLine(char c = '-')
        {
            string line = new string(c, Console.WindowWidth);
            WriteLine(line);
        }

        /// <summary>
        /// Builds a string repeating the specified character and having the leangth equal to the width of the console's buffer.
        /// </summary>
        public static string BuildHorizontalBufferLine(char c = '-') => new string(' ', Console.BufferWidth);

        public static void WriteHorizontalBufferLine(char c = '-')
        {
            string line = new string(c, Console.BufferWidth);
            WriteLine(line);
        }
    }
}