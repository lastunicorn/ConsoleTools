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
        /// Writes the specified string value to the Console
        /// using the "Warning" foreground and background colors.
        /// </summary>
        /// <param name="text">The value to write.</param>
        public static void WriteWarning(string text)
        {
            ConsoleColor initialForegroundColor = Console.ForegroundColor;
            ConsoleColor initialBackgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = WarningColor;
            if (WarningBackgroundColor.HasValue)
                Console.BackgroundColor = WarningBackgroundColor.Value;

            Console.Write(text);

            Console.ForegroundColor = initialForegroundColor;
            Console.BackgroundColor = initialBackgroundColor;
        }

        /// <summary>
        /// Writes the text representation of the specified array of objects to the Console,
        /// using the specified format information and the "Warning" foreground and background colors.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg">An array of objects to write using format.</param>
        public static void WriteWarning(string format, params object[] arg)
        {
            ConsoleColor initialForegroundColor = Console.ForegroundColor;
            ConsoleColor initialBackgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = WarningColor;
            if (WarningBackgroundColor.HasValue)
                Console.BackgroundColor = WarningBackgroundColor.Value;

            Console.Write(format, arg);

            Console.ForegroundColor = initialForegroundColor;
            Console.BackgroundColor = initialBackgroundColor;
        }

        /// <summary>
        /// Writes the text representation of the specified object to the Console
        /// using the "Warning" foreground and background colors.
        /// </summary>
        /// <param name="o">The value to write.</param>
        public static void WriteWarning(object o)
        {
            ConsoleColor initialForegroundColor = Console.ForegroundColor;
            ConsoleColor initialBackgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = WarningColor;
            if (WarningBackgroundColor.HasValue)
                Console.BackgroundColor = WarningBackgroundColor.Value;

            Console.Write(o);

            Console.ForegroundColor = initialForegroundColor;
            Console.BackgroundColor = initialBackgroundColor;
        }

        /// <summary>
        /// Writes the specified string value to the Console, followed by the current line terminator
        /// using the "Warning" foreground and background colors.
        /// </summary>
        /// <param name="text">The value to write.</param>
        public static void WriteLineWarning(string text)
        {
            ConsoleColor initialForegroundColor = Console.ForegroundColor;
            ConsoleColor initialBackgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = WarningColor;
            if (WarningBackgroundColor.HasValue)
                Console.BackgroundColor = WarningBackgroundColor.Value;

            Console.WriteLine(text);

            Console.ForegroundColor = initialForegroundColor;
            Console.BackgroundColor = initialBackgroundColor;
        }

        /// <summary>
        /// Writes the text representation of the specified array of objects to the Console,
        /// using the specified format information, followed by the current line terminator,
        /// using the "Warning" foreground and background colors.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg">An array of objects to write using format.</param>
        public static void WriteLineWarning(string format, params object[] arg)
        {
            ConsoleColor initialForegroundColor = Console.ForegroundColor;
            ConsoleColor initialBackgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = WarningColor;
            if (WarningBackgroundColor.HasValue)
                Console.BackgroundColor = WarningBackgroundColor.Value;

            Console.WriteLine(format, arg);

            Console.ForegroundColor = initialForegroundColor;
            Console.BackgroundColor = initialBackgroundColor;
        }

        /// <summary>
        /// Writes the text representation of the specified object to the Console, followed by the current line terminator
        /// using the "Warning" foreground and background colors.
        /// </summary>
        /// <param name="o">The value to write.</param>
        public static void WriteLineWarning(object o)
        {
            ConsoleColor initialForegroundColor = Console.ForegroundColor;
            ConsoleColor initialBackgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = WarningColor;
            if (WarningBackgroundColor.HasValue)
                Console.BackgroundColor = WarningBackgroundColor.Value;

            Console.WriteLine(o);

            Console.ForegroundColor = initialForegroundColor;
            Console.BackgroundColor = initialBackgroundColor;
        }

        /// <summary>
        /// Executes the specified action while the foreground and background colors
        /// are changed to "Warning" colors.
        /// </summary>
        public static void WithWarningColors(Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            ConsoleColor initialForegroundColor = Console.ForegroundColor;
            ConsoleColor initialBackgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = WarningColor;
            if (WarningBackgroundColor.HasValue)
                Console.BackgroundColor = WarningBackgroundColor.Value;

            try
            {
                action();
            }
            finally
            {
                Console.ForegroundColor = initialForegroundColor;
                Console.BackgroundColor = initialBackgroundColor;
            }
        }

        /// <summary>
        /// Executes the specified function while the foreground and background colors
        /// are changed to "Warning" colors.
        /// </summary>
        public static T WithWarningColors<T>(Func<T> func)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));

            ConsoleColor initialForegroundColor = Console.ForegroundColor;
            ConsoleColor initialBackgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = WarningColor;

            if (WarningBackgroundColor.HasValue)
                Console.BackgroundColor = WarningBackgroundColor.Value;

            try
            {
                return func();
            }
            finally
            {
                Console.ForegroundColor = initialForegroundColor;
                Console.BackgroundColor = initialBackgroundColor;
            }
        }
    }
}