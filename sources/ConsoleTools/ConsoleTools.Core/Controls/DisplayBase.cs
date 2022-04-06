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
    /// Implements base functionality for a display class that is used by a control to display itself.
    /// </summary>
    public abstract class DisplayBase : IDisplay
    {
        /// <summary>
        /// Gets the number of rows already displayed.
        /// </summary>
        public int DisplayedRowCount { get; private set; }
        
        /// <summary>
        /// Gets or sets the calculated layout for the current instance.
        /// Some details like margin and padding are displayed based on the values provided by this instance.
        /// </summary>
        public ControlLayout Layout { get; set; }

        /// <summary>
        /// Gets or sets the foreground color used to write the text.
        /// Default value: <c>null</c>
        /// </summary>
        public ConsoleColor? ForegroundColor { get; set; }

        /// <summary>
        /// Gets or sets the background color used to write the text.
        /// Default value: <c>null</c>
        /// </summary>
        public ConsoleColor? BackgroundColor { get; set; }

        /// <summary>
        /// Gets the horizontal location of the cursor, starting from the beginning of the current line.
        /// </summary>
        public int CursorPosition { get; private set; }

        /// <summary>
        /// Gets a value specifying if the cursor is at the start of a line.
        /// </summary>
        public bool IsBeginOfLine => CursorPosition == 0;

        public abstract bool IsCursorVisible { get; set; }
        
        public abstract int AvailableWidth { get; }

        /// <summary>
        /// Writes an entire row using the default <see cref="ForegroundColor"/>
        /// and <see cref="BackgroundColor"/> values.
        /// The left and right margins and paddings are added automatically.
        /// </summary>
        /// <param name="text">The text to be written as the content of th row.</param>
        public void WriteRow(string text)
        {
            WriteRow(ForegroundColor, BackgroundColor, text);
        }

        /// <summary>
        /// Writes an entire row using the specified foreground and background values.
        /// The left and right margins and paddings are added automatically.
        /// </summary>
        /// <param name="backgroundColor">The background color to be used for the content of the row.</param>
        /// <param name="foregroundColor">The foreground color to be used for the content of the row.</param>
        /// <param name="text">The text representing the content of th row.</param>
        public void WriteRow(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, string text)
        {
            StartRow(foregroundColor, backgroundColor);
            Write(text);
            EndRow();
        }

        /// <summary>
        /// Writes an empty row.
        /// The left and right margins and paddings are added automatically.
        /// </summary>
        public void WriteRow()
        {
            StartRow();
            EndRow();
        }

        /// <summary>
        /// Writes the starting of a row using the default <see cref="ForegroundColor"/>
        /// and <see cref="BackgroundColor"/> values.
        /// It includes the left margin and padding.
        /// </summary>
        public void StartRow()
        {
            StartRow(ForegroundColor, BackgroundColor);
        }

        /// <summary>
        /// Writes the starting of a row using the specified foreground and background values.
        /// It includes the left margin and padding.
        /// </summary>
        public void StartRow(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor)
        {
            WriteOuterLeftEmptySpace();
            WriteLeftMargin();

            SetRowForegroundColor(foregroundColor);
            SetRowBackgroundColor(backgroundColor);

            WriteLeftPadding();
        }

        /// <summary>
        /// Writes the ending of a row.
        /// It includes the right margin and padding.
        /// </summary>
        public void EndRow()
        {
            FillContentEmptySpace();
            WriteRightPadding();

            ResetRowForegroundColor();
            ResetRowBackgroundColor();

            WriteRightMargin();
            WriteOuterRightEmptySpace();

            WriteNewLine();

            DisplayedRowCount++;
        }

        private void FillContentEmptySpace()
        {
            if (Layout == null)
                return;

            int cursorLeft = CursorPosition;

            if (cursorLeft >= Layout.ActualFullWidth)
                return;

            int marginRight = Layout.MarginRight;
            int paddingRight = Layout.PaddingRight;
            int emptySpaceRight = Layout.ActualFullWidth - cursorLeft - paddingRight - marginRight;

            if (emptySpaceRight <= 0)
                return;

            string rightContentEmptySpace = new string(' ', emptySpaceRight);
            Write(rightContentEmptySpace);
        }

        private void WriteOuterLeftEmptySpace()
        {
            int spacesCount = Layout.OuterEmptySpaceLeft;

            if (spacesCount > 0)
            {
                string text = new string(' ', spacesCount);
                Write(text);
            }
        }

        private void WriteOuterRightEmptySpace()
        {
            int spacesCount = Layout.OuterEmptySpaceRight;

            if (spacesCount > 0)
            {
                string text = new string(' ', spacesCount);
                Write(text);
            }
        }

        private void WriteLeftMargin()
        {
            if (Layout.MarginLeft <= 0)
                return;

            string text = new string(' ', Layout.MarginLeft);
            Write(text);
        }

        private void WriteRightMargin()
        {
            if (Layout.MarginRight <= 0)
                return;

            string text = new string(' ', Layout.MarginRight);
            Write(text);
        }

        private void WriteLeftPadding()
        {
            if (Layout.PaddingLeft <= 0)
                return;

            string text = new string(' ', Layout.PaddingLeft);

            if (BackgroundColor.HasValue)
                Write(null, BackgroundColor.Value, text);
            else
                Write(text);
        }

        private void WriteRightPadding()
        {
            if (Layout.PaddingRight <= 0)
                return;

            string text = new string(' ', Layout.PaddingRight);

            if (BackgroundColor.HasValue)
                Write(null, BackgroundColor.Value, text);
            else
                Write(text);
        }

        protected abstract void SetRowForegroundColor(ConsoleColor? foregroundColor);

        protected abstract void SetRowBackgroundColor(ConsoleColor? backgroundColor);

        protected abstract void ResetRowForegroundColor();

        protected abstract void ResetRowBackgroundColor();

        /// <summary>
        /// Writes the newline character and moves the cursor at the beginning of the next line.
        /// </summary>
        public void WriteNewLine()
        {
            WriteNewLineInternal();
            CursorPosition = 0;
        }

        /// <summary>
        /// Writes the specified text using the default foreground and background colors.
        /// </summary>
        /// <param name="text">The text to be displayed.</param>
        public void Write(string text)
        {
            if (string.IsNullOrEmpty(text))
                return;

            WriteInternal(text);
            CursorPosition += text.Length;
        }

        /// <summary>
        /// Writes the specified text using the specified foreground and background colors.
        /// </summary>
        /// <param name="backgroundColor">The background color to be used for the text.</param>
        /// <param name="foregroundColor">The foreground color to be used for the content of the row.</param>
        /// <param name="text">The text to be displayed.</param>
        public void Write(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, string text)
        {
            if (string.IsNullOrEmpty(text))
                return;

            WriteInternal(foregroundColor, backgroundColor, text);
            CursorPosition += text.Length;
        }

        /// <summary>
        /// Writes the specified character using the specified foreground and background colors.
        /// </summary>
        /// <param name="backgroundColor">The background color to be used for the text.</param>
        /// <param name="foregroundColor">The foreground color to be used for the content of the row.</param>
        /// <param name="c">The character to be displayed.</param>
        public void Write(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, char c)
        {
            WriteInternal(foregroundColor, backgroundColor, c);
            CursorPosition += 1;
        }

        protected abstract void WriteNewLineInternal();

        protected abstract void WriteInternal(string text);

        protected abstract void WriteInternal(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, string text);

        protected abstract void WriteInternal(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, char c);

    }
}