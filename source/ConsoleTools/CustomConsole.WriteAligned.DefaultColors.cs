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
    public static partial class CustomConsole
    {
        /// <summary>
        /// Writes the specified text to the Console, horizontally aligned relative to the Console's buffer.
        /// </summary>
        /// <param name="horizontalAlignment">The horizontal alignment in the Console's buffer.</param>
        /// <param name="text">The text to be written to the Console.</param>
        public static void Write(HorizontalAlignment horizontalAlignment, string text)
        {
            Console.CursorLeft = CalculateStartPosition(text, horizontalAlignment);
            Console.Write(text);
        }

        /// <summary>
        /// Writes the text representation of the specified array of objects to the Console,
        /// using the specified format information and
        /// horizontally aligned relative to the Console's buffer.
        /// </summary>
        /// <param name="horizontalAlignment">The horizontal alignment in the Console's buffer.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg">An array of objects to write using format.</param>
        public static void Write(HorizontalAlignment horizontalAlignment, string format, params object[] arg)
        {
            string text = string.Format(format, arg);
            Console.CursorLeft = CalculateStartPosition(text, horizontalAlignment);
            Console.Write(text);
        }

        /// <summary>
        /// Writes the text representation of the specified object to the Console,
        /// horizontally aligned relative to the Console's buffer.
        /// </summary>
        /// <param name="horizontalAlignment">The horizontal alignment in the Console's buffer.</param>
        /// <param name="o">The value to write.</param>
        public static void Write(HorizontalAlignment horizontalAlignment, object o)
        {
            string text = o?.ToString() ?? string.Empty;
            Console.CursorLeft = CalculateStartPosition(text, horizontalAlignment);
            Console.Write(text);
        }

        /// <summary>
        /// Writes the specified text to the Console, horizontally aligned relative to the Console's buffer,
        /// followed by the current line terminator.
        /// </summary>
        /// <param name="horizontalAlignment">The horizontal alignment in the Console's buffer.</param>
        /// <param name="text">The text to be written to the Console.</param>
        public static void WriteLine(HorizontalAlignment horizontalAlignment, string text)
        {
            Console.CursorLeft = CalculateStartPosition(text, horizontalAlignment);
            Console.WriteLine(text);
        }

        /// <summary>
        /// Writes the text representation of the specified array of objects to the Console,
        /// using the specified format information, and
        /// horizontally aligned relative to the Console's buffer, followed by the current line terminator.
        /// </summary>
        /// <param name="horizontalAlignment">The horizontal alignment in the Console's buffer.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg">An array of objects to write using format.</param>
        public static void WriteLine(HorizontalAlignment horizontalAlignment, string format, params object[] arg)
        {
            string text = string.Format(format, arg);
            Console.CursorLeft = CalculateStartPosition(text, horizontalAlignment);
            Console.WriteLine(text);
        }

        /// <summary>
        /// Writes the text representation of the specified object to the Console,
        /// horizontally aligned relative to the Console's buffer, followed by the current line terminator.
        /// </summary>
        /// <param name="horizontalAlignment">The horizontal alignment in the Console's buffer.</param>
        /// <param name="o">The value to write.</param>
        public static void WriteLine(HorizontalAlignment horizontalAlignment, object o)
        {
            string text = o?.ToString() ?? string.Empty;
            Console.CursorLeft = CalculateStartPosition(text, horizontalAlignment);
            Console.WriteLine(text);
        }

        private static int CalculateStartPosition(string text, HorizontalAlignment horizontalAlignment)
        {
            int startPosition;

            switch (horizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    startPosition = 0;
                    break;

                case HorizontalAlignment.Center:
                    int bufferCenter = Console.BufferWidth / 2 - 2;
                    startPosition = bufferCenter - text.Length / 2;
                    break;

                case HorizontalAlignment.Right:
                    int bufferRight = Console.BufferWidth - 1;
                    startPosition = bufferRight - text.Length;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(horizontalAlignment), horizontalAlignment, null);
            }
            return startPosition;
        }
    }
}