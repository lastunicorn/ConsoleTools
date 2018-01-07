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

namespace DustInTheWind.ConsoleTools
{
    public partial class CustomConsole
    {
        /// <summary>
        /// Writes the specified text to the Console using the specified background color.
        /// </summary>
        /// <param name="color">The background color used to write the text.</param>
        /// <param name="text">The text to be written to the Console.</param>
        public static void WriteBackgroundColor(ConsoleColor color, string text)
        {
            ConsoleColor oldColor = Console.BackgroundColor;
            Console.BackgroundColor = color;
            Console.Write(text);
            Console.BackgroundColor = oldColor;
        }

        /// <summary>
        /// Writes the text representation of the specified array of objects to the Console
        /// using the specified format information and background color.
        /// </summary>
        /// <param name="color">The background color used to write the text.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg">An array of objects to write using format.</param>
        public static void WriteBackgroundColor(ConsoleColor color, string format, params object[] arg)
        {
            ConsoleColor oldColor = Console.BackgroundColor;
            Console.BackgroundColor = color;
            Console.Write(format, arg);
            Console.BackgroundColor = oldColor;
        }

        /// <summary>
        /// Writes the text representation of the specified object to the Console.
        /// An additional parameter can be specified for the background color used to write the text.
        /// </summary>
        /// <param name="color">The background color used to write the text.</param>
        /// <param name="o">The value to write.</param>
        public static void WriteBackgroundColor(ConsoleColor color, object o)
        {
            ConsoleColor oldColor = Console.BackgroundColor;
            Console.BackgroundColor = color;
            Console.Write(o);
            Console.BackgroundColor = oldColor;
        }

        /// <summary>
        ///  Writes the specified string value, followed by the current line terminator, to the Console.
        /// An additional parameter can be specified for the background color used to write the text.
        /// </summary>
        /// <param name="color">The background color used to write the text.</param>
        /// <param name="text">The text to write.</param>
        public static void WriteLineBackgroundColor(ConsoleColor color, string text)
        {
            ConsoleColor oldColor = Console.BackgroundColor;
            Console.BackgroundColor = color;
            Console.WriteLine(text);
            Console.BackgroundColor = oldColor;
        }

        /// <summary>
        /// Writes the text representation of the specified array of objects,
        /// followed by the current line terminator, to the Console
        /// using the specified format information and background color.
        /// </summary>
        /// <param name="color">The background color used to write the text.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg">An array of objects to write using format.</param>
        public static void WriteLineBackgroundColor(ConsoleColor color, string format, params object[] arg)
        {
            ConsoleColor oldColor = Console.BackgroundColor;
            Console.BackgroundColor = color;
            Console.WriteLine(format, arg);
            Console.BackgroundColor = oldColor;
        }

        /// <summary>
        /// Writes the text representation of the specified object, followed by the current line terminator, to the Console.
        /// An additional parameter can be specified for the background color used to write the text.
        /// </summary>
        /// <param name="color">The background color used to write the text.</param>
        /// <param name="o">The value to write.</param>
        public static void WriteLineBackgroundColor(ConsoleColor color, object o)
        {
            ConsoleColor oldColor = Console.BackgroundColor;
            Console.BackgroundColor = color;
            Console.WriteLine(o);
            Console.BackgroundColor = oldColor;
        }
    }
}