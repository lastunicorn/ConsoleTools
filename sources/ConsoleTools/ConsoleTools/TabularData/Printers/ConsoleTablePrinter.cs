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

namespace DustInTheWind.ConsoleTools.TabularData.Printers
{
    /// <summary>
    /// Writes the parts of a <see cref="DataGrid"/> instance to the <see cref="Console"/>,
    /// using different colors for each type of part.
    /// </summary>
    public class ConsoleTablePrinter : ITablePrinter
    {
        /// <summary>
        /// Gets or sets the foreground color for the borders.
        /// Default value: Gray
        /// </summary>
        public ConsoleColor? BorderColor { get; set; }

        /// <summary>
        /// Gets or sets the foreground color for the title.
        /// Default value: White
        /// </summary>
        public ConsoleColor? TitleColor { get; set; }

        /// <summary>
        /// Gets or sets the foreground color for the column headers.
        /// Default value: White
        /// </summary>
        public ConsoleColor? HeaderColor { get; set; }

        /// <summary>
        /// Gets or sets the default foreground color.
        /// Default value: Gray
        /// </summary>
        public ConsoleColor? NormalColor { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleTablePrinter"/> class.
        /// </summary>
        public ConsoleTablePrinter()
        {
            BorderColor = ConsoleColor.Gray;
            TitleColor = ConsoleColor.White;
            HeaderColor = ConsoleColor.White;
            NormalColor = ConsoleColor.Gray;
        }

        /// <summary>
        /// Writes the specified text to the <see cref="Console"/> using the <see cref="BorderColor"/>.
        /// </summary>
        public void WriteBorder(string text)
        {
            ConsoleColor? color = BorderColor ?? NormalColor;

            if (color.HasValue)
                CustomConsole.Write(color.Value, text);
            else
                CustomConsole.Write(text);
        }

        /// <summary>
        /// Writes the specified character to the <see cref="Console"/> using the <see cref="BorderColor"/>.
        /// </summary>
        public void WriteBorder(char c)
        {
            ConsoleColor? color = BorderColor ?? NormalColor;

            if (color.HasValue)
                CustomConsole.Write(color.Value, c);
            else
                CustomConsole.Write(c);
        }

        /// <summary>
        /// Writes the specified text to the <see cref="Console"/> using the <see cref="BorderColor"/>,
        /// followed by a line terminator.
        /// </summary>
        public void WriteLineBorder(string text)
        {
            ConsoleColor? color = BorderColor ?? NormalColor;

            if (color.HasValue)
                CustomConsole.WriteLine(color.Value, text);
            else
                CustomConsole.WriteLine(text);
        }

        /// <summary>
        /// Writes the specified character to the <see cref="Console"/> using the <see cref="BorderColor"/>,
        /// followed by a line terminator.
        /// </summary>
        public void WriteLineBorder(char c)
        {
            ConsoleColor? color = BorderColor ?? NormalColor;

            if (color.HasValue)
                CustomConsole.WriteLine(color.Value, c);
            else
                CustomConsole.WriteLine(c);
        }

        /// <summary>
        /// Writes the specified text to the <see cref="Console"/> using the <see cref="TitleColor"/>.
        /// </summary>
        public void WriteTitle(string text)
        {
            ConsoleColor? color = TitleColor ?? NormalColor;

            if (color.HasValue)
                CustomConsole.Write(color.Value, text);
            else
                CustomConsole.Write(text);
        }

        /// <summary>
        /// Writes the specified text to the <see cref="Console"/> using the <see cref="HeaderColor"/>.
        /// </summary>
        public void WriteHeader(string text)
        {
            ConsoleColor? color = HeaderColor ?? NormalColor;

            if (color.HasValue)
                CustomConsole.Write(color.Value, text);
            else
                CustomConsole.Write(text);
        }

        /// <summary>
        /// Writes the specified text to the <see cref="Console"/> using the <see cref="NormalColor"/>.
        /// </summary>
        public void WriteNormal(string text)
        {
            ConsoleColor? color = NormalColor;

            if (color.HasValue)
                CustomConsole.Write(color.Value, text);
            else
                CustomConsole.Write(text);
        }

        /// <summary>
        /// Writes the line terminator to the <see cref="Console"/>.
        /// </summary>
        public void WriteLine()
        {
            Console.WriteLine();
        }
    }
}