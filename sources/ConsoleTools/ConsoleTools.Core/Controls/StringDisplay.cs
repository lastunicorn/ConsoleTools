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
using System.Text;

namespace DustInTheWind.ConsoleTools.Controls
{
    /// <summary>
    /// Collects the rendered parts of a <see cref="Control"/> instance as a plain text that is later
    /// returned by the <see cref="ToString"/> method.
    /// </summary>
    public class StringDisplay : IDisplay
    {
        private readonly StringBuilder sb;

        /// <summary>
        /// Gets the number of rows written in the internal buffer.
        /// </summary>
        public int DisplayedRowCount { get; private set; }

        public ControlLayout Layout { get; set; }

        /// <summary>
        /// This value is not used.
        /// </summary>
        public ConsoleColor? ForegroundColor { get; set; }

        /// <summary>
        /// This value is not used.
        /// </summary>
        public ConsoleColor? BackgroundColor { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringDisplay"/> class.
        /// </summary>
        public StringDisplay()
        {
            sb = new StringBuilder();
        }

        public void WriteRow(string text)
        {
            sb.AppendLine(text);
            DisplayedRowCount++;
        }

        public void WriteRow(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, string text)
        {
            sb.AppendLine(text);
            DisplayedRowCount++;
        }

        /// <summary>
        /// Writes the line terminator in the internal buffer.
        /// </summary>
        public void WriteRow()
        {
            sb.AppendLine();
            DisplayedRowCount++;
        }

        public void StartRow()
        {
        }

        public void StartRow(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor)
        {
        }

        public void EndRow()
        {
            sb.AppendLine();
            DisplayedRowCount++;
        }

        /// <summary>
        /// Stores the specified text in the internal buffer.
        /// </summary>
        /// <param name="text">The text that is written into the internal buffer.</param>
        public void Write(string text)
        {
            sb.Append(text);
        }

        /// <summary>
        /// Stores the specified text in the internal buffer.
        /// The foreground and background values are ignored.
        /// </summary>
        /// <param name="foregroundColor">This parameter is ignored</param>
        /// <param name="backgroundColor">This parameter is ignored</param>
        /// <param name="text">The text that is written into the internal buffer.</param>
        public void Write(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, string text)
        {
            sb.Append(text);
        }

        /// <summary>
        /// Stores the specified character in the internal buffer.
        /// The foreground and background values are ignored.
        /// </summary>
        /// <param name="foregroundColor">This parameter is ignored</param>
        /// <param name="backgroundColor">This parameter is ignored</param>
        /// <param name="c">The character that is written into the internal buffer.</param>
        public void Write(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, char c)
        {
            sb.Append(c);
        }

        /// <summary>
        /// Returns the <see cref="string"/> built until now.
        /// </summary>
        public override string ToString()
        {
            return sb.ToString();
        }
    }
}