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
        /// When implemented by an inheritor, gets the number of rows already displayed.
        /// </summary>
        int DisplayedRowCount { get; }

        /// <summary>
        /// When implemented by an inheritor, gets or sets the calculated layout for the current instance.
        /// Some details like margin and padding are displayed based on the values provided by this instance.
        /// </summary>
        ControlLayout ControlLayout { get; set; }

        /// <summary>
        /// When implemented by an inheritor, gts or sets the foreground color used to write the text.
        /// Default value: <c>null</c>
        /// </summary>
        ConsoleColor? ForegroundColor { get; set; }

        /// <summary>
        /// When implemented by an inheritor, gets or sets the background color used to write the text.
        /// Default value: <c>null</c>
        /// </summary>
        ConsoleColor? BackgroundColor { get; set; }

        /// <summary>
        /// When implemented by an inheritor, gets the horizontal location of the cursor, starting from
        /// the beginning of the current line.
        /// </summary>
        int CursorPosition { get; }

        /// <summary>
        /// When implemented by an inheritor, gets a value specifying if the cursor is at the start of a line.
        /// </summary>
        bool IsBeginOfLine { get; }

        bool IsCursorVisible { get; set; }

        int AvailableWidth { get; }
        
        IDisplay Parent { get; set; }

        /// <summary>
        /// When implemented by an inheritor, writes an entire row using the default <see cref="ForegroundColor"/>
        /// and <see cref="BackgroundColor"/> values.
        /// The left and right margins and paddings are added automatically.
        /// </summary>
        /// <param name="text">The text to be written as the content of th row.</param>
        void WriteRow(string text);

        /// <summary>
        /// When implemented by an inheritor, writes an entire row using the specified foreground and background values.
        /// The left and right margins and paddings are added automatically.
        /// </summary>
        /// <param name="backgroundColor">The background color to be used for the content of the row.</param>
        /// <param name="foregroundColor">The foreground color to be used for the content of the row.</param>
        /// <param name="text">The text representing the content of th row.</param>
        void WriteRow(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, string text);

        /// <summary>
        /// When implemented by an inheritor, writes an empty row using the default <see cref="BackgroundColor"/>.
        /// The left and right margins and paddings are added automatically.
        /// </summary>
        void WriteRow();

        /// <summary>
        /// When implemented by an inheritor, writes the starting of a row using the default <see cref="ForegroundColor"/>
        /// and <see cref="BackgroundColor"/> values.
        /// It includes the left margin and padding.
        /// </summary>
        void StartRow();

        /// <summary>
        /// When implemented by an inheritor, writes the starting of a row using the specified foreground and background colors.
        /// It includes the left margin and padding.
        /// </summary>
        void StartRow(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor);

        /// <summary>
        /// When implemented by an inheritor, writes the ending of a row.
        /// It includes the right margin and padding.
        /// </summary>
        void EndRow();

        /// <summary>
        /// When implemented by an inheritor, writes the newline character and moves the cursor at the beginning of the next line.
        /// </summary>
        void WriteNewLine();

        /// <summary>
        /// When implemented by an inheritor, writes the specified text using the default foreground and background colors.
        /// </summary>
        /// <param name="text">The text to be displayed.</param>
        void Write(string text);

        /// <summary>
        /// When implemented by an inheritor, writes the specified text using the specified foreground and background colors.
        /// </summary>
        /// <param name="backgroundColor">The background color to be used for the text.</param>
        /// <param name="foregroundColor">The foreground color to be used for the content of the row.</param>
        /// <param name="text">The text to be displayed.</param>
        void Write(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, string text);

        /// <summary>
        /// When implemented by an inheritor, writes the specified character using the specified foreground and background colors.
        /// </summary>
        /// <param name="backgroundColor">The background color to be used for the text.</param>
        /// <param name="foregroundColor">The foreground color to be used for the content of the row.</param>
        /// <param name="c">The character to be displayed.</param>
        void Write(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, char c);

        void AdvanceCursor(int value);

        IDisplay CreateChild();

        Line CreateNewLine();
        
        Line CreateNewLine(string text);
        Line CreateNewLine(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, string text);
    }
}