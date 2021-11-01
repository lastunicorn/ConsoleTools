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
        /// Writes the specified text to the Console using the specified foreground color.
        /// </summary>
        /// <param name="foregroundColor">The foreground color used to write the text.</param>
        /// <param name="text">The text to be written to the Console.</param>
        public static void Write(ConsoleColor foregroundColor, string text)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.Write(text);
            Console.ForegroundColor = oldColor;
        }

        /// <summary>
        /// Writes the text representation of the specified array of objects to the Console
        /// using the specified format information and foreground color.
        /// </summary>
        /// <param name="foregroundColor">The foreground color used to write the text.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg">An array of objects to write using format.</param>
        public static void Write(ConsoleColor foregroundColor, string format, params object[] arg)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.Write(format, arg);
            Console.ForegroundColor = oldColor;
        }

        /// <summary>
        /// Writes the text representation of the specified object to the Console.
        /// An additional parameter can be specified for the foreground color used to write the text.
        /// </summary>
        /// <param name="foregroundColor">The foreground color used to write the text.</param>
        /// <param name="o">The value to write.</param>
        public static void Write(ConsoleColor foregroundColor, object o)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.Write(o);
            Console.ForegroundColor = oldColor;
        }

        /// <summary>
        /// Writes the specified string value, followed by the current line terminator, to the Console.
        /// An additional parameter can be specified for the foreground color used to write the text.
        /// </summary>
        /// <param name="foregroundColor">The foreground color used to write the text.</param>
        /// <param name="text">The text to write.</param>
        public static void WriteLine(ConsoleColor foregroundColor, string text)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(text);
            Console.ForegroundColor = oldColor;
        }

        /// <summary>
        /// Writes the text representation of the specified array of objects,
        /// followed by the current line terminator, to the Console
        /// using the specified format information and foreground color.
        /// </summary>
        /// <param name="foregroundColor">The foreground color used to write the text.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg">An array of objects to write using format.</param>
        public static void WriteLine(ConsoleColor foregroundColor, string format, params object[] arg)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(format, arg);
            Console.ForegroundColor = oldColor;
        }

        /// <summary>
        /// Writes the text representation of the specified object, followed by the current line terminator, to the Console.
        /// An additional parameter can be specified for the foreground color used to write the text.
        /// </summary>
        /// <param name="foregroundColor">The foreground color used to write the text.</param>
        /// <param name="o">The value to write.</param>
        public static void WriteLine(ConsoleColor foregroundColor, object o)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(o);
            Console.ForegroundColor = oldColor;
        }

        /// <summary>
        /// Executes the specified action while the foreground color is set to the specified value.
        /// </summary>
        public static void WithForegroundColor(ConsoleColor foregroundColor, Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            ConsoleColor initialForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;

            try
            {
                action();
            }
            finally
            {
                Console.ForegroundColor = initialForegroundColor;
            }
        }

        /// <summary>
        /// Executes the specified function while the foreground color is set to the specified value.
        /// </summary>
        public static T WithForegroundColor<T>(ConsoleColor foregroundColor, Func<T> func)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));

            ConsoleColor initialForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;

            try
            {
                return func();
            }
            finally
            {
                Console.ForegroundColor = initialForegroundColor;
            }
        }
    }
}