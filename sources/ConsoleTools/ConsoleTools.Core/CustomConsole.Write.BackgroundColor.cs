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

// --------------------------------------------------------------------------------
// Bugs or feature requests
// --------------------------------------------------------------------------------
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new/choose

using System;

namespace DustInTheWind.ConsoleTools
{
    public partial class CustomConsole
    {
        /// <summary>
        /// Writes the specified text to the Console using the specified background color.
        /// </summary>
        /// <param name="backgroundColor">The background color used to write the text.</param>
        /// <param name="text">The text to be written to the Console.</param>
        public static void WriteBackgroundColor(ConsoleColor backgroundColor, string text)
        {
            ConsoleColor oldColor = Console.BackgroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Write(text);
            Console.BackgroundColor = oldColor;
        }

        /// <summary>
        /// Writes the text representation of the specified array of objects to the Console
        /// using the specified format information and background color.
        /// </summary>
        /// <param name="backgroundColor">The background color used to write the text.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg">An array of objects to write using format.</param>
        public static void WriteBackgroundColor(ConsoleColor backgroundColor, string format, params object[] arg)
        {
            ConsoleColor oldColor = Console.BackgroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Write(format, arg);
            Console.BackgroundColor = oldColor;
        }

        /// <summary>
        /// Writes the text representation of the specified object to the Console.
        /// An additional parameter can be specified for the background color used to write the text.
        /// </summary>
        /// <param name="backgroundColor">The background color used to write the text.</param>
        /// <param name="o">The value to write.</param>
        public static void WriteBackgroundColor(ConsoleColor backgroundColor, object o)
        {
            ConsoleColor oldColor = Console.BackgroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Write(o);
            Console.BackgroundColor = oldColor;
        }

        /// <summary>
        ///  Writes the specified string value, followed by the current line terminator, to the Console.
        /// An additional parameter can be specified for the background color used to write the text.
        /// </summary>
        /// <param name="backgroundColor">The background color used to write the text.</param>
        /// <param name="text">The text to write.</param>
        public static void WriteLineBackgroundColor(ConsoleColor backgroundColor, string text)
        {
            ConsoleColor oldColor = Console.BackgroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine(text);
            Console.BackgroundColor = oldColor;
        }

        /// <summary>
        /// Writes the text representation of the specified array of objects,
        /// followed by the current line terminator, to the Console
        /// using the specified format information and background color.
        /// </summary>
        /// <param name="backgroundColor">The background color used to write the text.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg">An array of objects to write using format.</param>
        public static void WriteLineBackgroundColor(ConsoleColor backgroundColor, string format, params object[] arg)
        {
            ConsoleColor oldColor = Console.BackgroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine(format, arg);
            Console.BackgroundColor = oldColor;
        }

        /// <summary>
        /// Writes the text representation of the specified object, followed by the current line terminator, to the Console.
        /// An additional parameter can be specified for the background color used to write the text.
        /// </summary>
        /// <param name="backgroundColor">The background color used to write the text.</param>
        /// <param name="o">The value to write.</param>
        public static void WriteLineBackgroundColor(ConsoleColor backgroundColor, object o)
        {
            ConsoleColor oldColor = Console.BackgroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine(o);
            Console.BackgroundColor = oldColor;
        }

        /// <summary>
        /// Executes the specified action while the background color is set to the specified value.
        /// </summary>
        public static void WithBackgroundColor(ConsoleColor backgroundColor, Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            ConsoleColor initialBackgroundColor = Console.BackgroundColor;
            Console.BackgroundColor = backgroundColor;

            try
            {
                action();
            }
            finally
            {
                Console.BackgroundColor = initialBackgroundColor;
            }
        }

        /// <summary>
        /// Executes the specified function while the background color is set to the specified value.
        /// </summary>
        public static T WithBackgroundColor<T>(ConsoleColor backgroundColor, Func<T> func)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));

            ConsoleColor initialBackgroundColor = Console.BackgroundColor;
            Console.BackgroundColor = backgroundColor;

            try
            {
                return func();
            }
            finally
            {
                Console.BackgroundColor = initialBackgroundColor;
            }
        }
    }
}