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
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new

using System;

namespace DustInTheWind.ConsoleTools
{
    public partial class CustomConsole
    {
        public static void Write(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string text)
        {
            ConsoleColor initialForegroundColor = Console.ForegroundColor;
            ConsoleColor initialBackgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;

            Console.Write(text);

            Console.ForegroundColor = initialForegroundColor;
            Console.BackgroundColor = initialBackgroundColor;
        }

        public static void Write(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string format, params object[] arg)
        {
            ConsoleColor initialForegroundColor = Console.ForegroundColor;
            ConsoleColor initialBackgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;

            Console.Write(format, arg);

            Console.ForegroundColor = initialForegroundColor;
            Console.BackgroundColor = initialBackgroundColor;
        }

        public static void Write(ConsoleColor foregroundColor, ConsoleColor backgroundColor, object o)
        {
            ConsoleColor initialForegroundColor = Console.ForegroundColor;
            ConsoleColor initialBackgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;

            Console.Write(o);

            Console.ForegroundColor = initialForegroundColor;
            Console.BackgroundColor = initialBackgroundColor;
        }

        public static void WriteLine(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string text)
        {
            ConsoleColor initialForegroundColor = Console.ForegroundColor;
            ConsoleColor initialBackgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;

            Console.WriteLine(text);

            Console.ForegroundColor = initialForegroundColor;
            Console.BackgroundColor = initialBackgroundColor;
        }

        public static void WriteLine(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string format, params object[] arg)
        {
            ConsoleColor initialForegroundColor = Console.ForegroundColor;
            ConsoleColor initialBackgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;

            Console.WriteLine(format, arg);

            Console.ForegroundColor = initialForegroundColor;
            Console.BackgroundColor = initialBackgroundColor;
        }

        public static void WriteLine(ConsoleColor foregroundColor, ConsoleColor backgroundColor, object o)
        {
            ConsoleColor initialForegroundColor = Console.ForegroundColor;
            ConsoleColor initialBackgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;

            Console.WriteLine(o);

            Console.ForegroundColor = initialForegroundColor;
            Console.BackgroundColor = initialBackgroundColor;
        }

        public static void WithColors(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            if (foregroundColor.HasValue)
            {
                if (backgroundColor.HasValue)
                    WithColors(foregroundColor.Value, backgroundColor.Value, action);
                else
                    WithForegroundColor(foregroundColor.Value, action);
            }
            else
            {
                if (backgroundColor.HasValue)
                    WithBackgroundColor(backgroundColor.Value, action);
                else
                    action();
            }
        }

        /// <summary>
        /// Executes the specified action while the foreground and background colors
        /// are set to the specified values.
        /// </summary>
        public static void WithColors(ConsoleColor foregroundColor, ConsoleColor backgroundColor, Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            ConsoleColor initialForegroundColor = Console.ForegroundColor;
            ConsoleColor initialBackgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;

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

        public static T WithColors<T>(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, Func<T> func)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));

            if (foregroundColor.HasValue)
            {
                if (backgroundColor.HasValue)
                    return WithColors(foregroundColor.Value, backgroundColor.Value, func);
                else
                    return WithForegroundColor(foregroundColor.Value, func);
            }
            else
            {
                if (backgroundColor.HasValue)
                    return WithBackgroundColor(backgroundColor.Value, func);
                else
                    return func();
            }
        }

        /// <summary>
        /// Executes the specified function while the foreground and background colors
        /// are set to the specified values.
        /// </summary>
        public static T WithColors<T>(ConsoleColor foregroundColor, ConsoleColor backgroundColor, Func<T> func)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));

            ConsoleColor initialForegroundColor = Console.ForegroundColor;
            ConsoleColor initialBackgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;

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