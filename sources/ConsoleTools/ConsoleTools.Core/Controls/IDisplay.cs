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

namespace DustInTheWind.ConsoleTools.Controls
{
    /// <summary>
    /// Represents a display where a control is rendered.
    /// </summary>
    public interface IDisplay
    {
        /// <summary>
        /// Gets the number of rows already displayed.
        /// </summary>
        int DisplayedRowCount { get; }

        /// <summary>
        /// Gets or sets the calculated layout for the current instance.
        /// Some details like margin and padding are displayed based on the values provided by this instance.
        /// </summary>
        ControlLayout Layout { get; set; }

        /// <summary>
        /// Gets or sets the foreground color used to write the text.
        /// Default value: <c>null</c>
        /// </summary>
        ConsoleColor? ForegroundColor { get; set; }

        /// <summary>
        /// Gets or sets the background color used to write the text.
        /// Default value: <c>null</c>
        /// </summary>
        ConsoleColor? BackgroundColor { get; set; }

        /// <summary>
        /// Writes an entire row using the default <see cref="ControlDisplay.ForegroundColor"/>
        /// and <see cref="ControlDisplay.BackgroundColor"/> values.
        /// The left and right margins and paddings are added automatically.
        /// </summary>
        /// <param name="text">The text to be written as the content of th row.</param>
        void WriteRow(string text);

        /// <summary>
        /// Writes an entire row using the specified foreground and background values.
        /// The left and right margins and paddings are added automatically.
        /// </summary>
        /// <param name="backgroundColor">The background color to be used for the content of the row.</param>
        /// <param name="foregroundColor">The foreground color to be used for the content of the row.</param>
        /// <param name="text">The text representing the content of th row.</param>
        void WriteRow(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, string text);

        /// <summary>
        /// Writes an empty row.
        /// The left and right margins and paddings are added automatically.
        /// </summary>
        void WriteRow();

        /// <summary>
        /// Writes the starting of a row using the default <see cref="ControlDisplay.ForegroundColor"/>
        /// and <see cref="ControlDisplay.BackgroundColor"/> values.
        /// It includes the left margin and padding.
        /// </summary>
        void StartRow();

        /// <summary>
        /// Writes the starting of a row using the specified foreground and background values.
        /// It includes the left margin and padding.
        /// </summary>
        void StartRow(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor);

        /// <summary>
        /// Writes the ending of a row.
        /// It includes the right margin and padding.
        /// </summary>
        void EndRow();

        /// <summary>
        /// Writes the specified text using the default foreground and background colors.
        /// </summary>
        /// <param name="text">The text to be displayed.</param>
        void Write(string text);

        /// <summary>
        /// Writes the specified text using the specified foreground and background colors.
        /// </summary>
        /// <param name="backgroundColor">The background color to be used for the text.</param>
        /// <param name="foregroundColor">The foreground color to be used for the content of the row.</param>
        /// <param name="text">The text to be displayed.</param>
        void Write(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, string text);

        /// <summary>
        /// Writes the specified character using the specified foreground and background colors.
        /// </summary>
        /// <param name="backgroundColor">The background color to be used for the text.</param>
        /// <param name="foregroundColor">The foreground color to be used for the content of the row.</param>
        /// <param name="c">The character to be displayed.</param>
        void Write(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, char c);
    }
}