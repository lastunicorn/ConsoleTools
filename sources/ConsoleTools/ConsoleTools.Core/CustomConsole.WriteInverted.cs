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
        public static void WriteInverted(string text)
        {
            ConsoleColor initialForegroundColor = Console.ForegroundColor;
            ConsoleColor initialBackgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = initialBackgroundColor;
            Console.BackgroundColor = initialForegroundColor;

            Console.Write(text);

            Console.ForegroundColor = initialForegroundColor;
            Console.BackgroundColor = initialBackgroundColor;
        }

        public static void WriteInverted(string format, params object[] arg)
        {
            ConsoleColor initialForegroundColor = Console.ForegroundColor;
            ConsoleColor initialBackgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = initialBackgroundColor;
            Console.BackgroundColor = initialForegroundColor;

            Console.Write(format, arg);

            Console.ForegroundColor = initialForegroundColor;
            Console.BackgroundColor = initialBackgroundColor;
        }

        public static void WriteInverted(object o)
        {
            ConsoleColor initialForegroundColor = Console.ForegroundColor;
            ConsoleColor initialBackgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = initialBackgroundColor;
            Console.BackgroundColor = initialForegroundColor;

            Console.Write(o);

            Console.ForegroundColor = initialForegroundColor;
            Console.BackgroundColor = initialBackgroundColor;
        }

        public static void WriteLineInverted(string text)
        {
            ConsoleColor initialForegroundColor = Console.ForegroundColor;
            ConsoleColor initialBackgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = initialBackgroundColor;
            Console.BackgroundColor = initialForegroundColor;

            Console.WriteLine(text);

            Console.ForegroundColor = initialForegroundColor;
            Console.BackgroundColor = initialBackgroundColor;
        }

        public static void WriteLineInverted(string text, params object[] arg)
        {
            ConsoleColor initialForegroundColor = Console.ForegroundColor;
            ConsoleColor initialBackgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = initialBackgroundColor;
            Console.BackgroundColor = initialForegroundColor;

            Console.WriteLine(text, arg);

            Console.ForegroundColor = initialForegroundColor;
            Console.BackgroundColor = initialBackgroundColor;
        }

        public static void WriteLineInverted(object o)
        {
            ConsoleColor initialForegroundColor = Console.ForegroundColor;
            ConsoleColor initialBackgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = initialBackgroundColor;
            Console.BackgroundColor = initialForegroundColor;

            Console.WriteLine(o);

            Console.ForegroundColor = initialForegroundColor;
            Console.BackgroundColor = initialBackgroundColor;
        }

        /// <summary>
        /// Executes the specified action while the foreground and background colors
        /// are switched between them.
        /// </summary>
        public static void WithInvertedColors(Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            ConsoleColor initialForegroundColor = Console.ForegroundColor;
            ConsoleColor initialBackgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = initialBackgroundColor;
            Console.BackgroundColor = initialForegroundColor;

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
        /// are switched between them.
        /// </summary>
        public static T WithInvertedColors<T>(Func<T> func)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));

            ConsoleColor initialForegroundColor = Console.ForegroundColor;
            ConsoleColor initialBackgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = initialBackgroundColor;
            Console.BackgroundColor = initialForegroundColor;

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