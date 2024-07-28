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
        public ControlLayout ControlLayout { get; set; }

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

        public void AdvanceCursor(int value)
        {
            CursorPosition += value;
            Parent?.AdvanceCursor(value);
        }

        /// <summary>
        /// Gets a value specifying if the cursor is at the start of a line.
        /// </summary>
        public bool IsBeginOfLine => CursorPosition == 0;

        public abstract bool IsCursorVisible { get; set; }

        public abstract int AvailableWidth { get; }

        public IDisplay Parent { get; set; }

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
            CursorPosition = 0;

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
            if (ControlLayout != null)
            {
                FillContentEmptySpace();
                WriteRightPadding();

                ResetRowForegroundColor();
                ResetRowBackgroundColor();

                WriteRightMargin();
                WriteOuterRightEmptySpace();
            }

            DisplayedRowCount++;
        }

        private void FillContentEmptySpace()
        {
            if (ControlLayout == null)
                return;

            int cursorPosition = CursorPosition;
            int outerEmptySpaceLeft = ControlLayout.OuterEmptySpaceLeft;
            int marginLeft = ControlLayout.MarginLeft;
            int paddingLeft = ControlLayout.PaddingLeft;
            int contentLength = cursorPosition - outerEmptySpaceLeft - marginLeft - paddingLeft;
            int emptySpaceRight = ControlLayout.ActualClientWidth - contentLength;

            if (emptySpaceRight <= 0)
                return;

            string rightContentEmptySpace = new string(' ', emptySpaceRight);
            Write(rightContentEmptySpace);
        }

        private void WriteOuterLeftEmptySpace()
        {
            int spacesCount = ControlLayout.OuterEmptySpaceLeft;

            if (spacesCount > 0)
            {
                string text = new string(' ', spacesCount);
                Write(text);
            }
        }

        private void WriteOuterRightEmptySpace()
        {
            int spacesCount = ControlLayout.OuterEmptySpaceRight;

            if (spacesCount > 0)
            {
                string text = new string(' ', spacesCount);
                Write(text);
            }
        }

        private void WriteLeftMargin()
        {
            if (ControlLayout.MarginLeft <= 0)
                return;

            string text = new string(' ', ControlLayout.MarginLeft);
            Write(text);
        }

        private void WriteRightMargin()
        {
            if (ControlLayout.MarginRight <= 0)
                return;

            string text = new string(' ', ControlLayout.MarginRight);
            Write(text);
        }

        private void WriteLeftPadding()
        {
            if (ControlLayout.PaddingLeft <= 0)
                return;

            string text = new string(' ', ControlLayout.PaddingLeft);

            if (BackgroundColor.HasValue)
                Write(null, BackgroundColor.Value, text);
            else
                Write(text);
        }

        private void WriteRightPadding()
        {
            if (ControlLayout.PaddingRight <= 0)
                return;

            string text = new string(' ', ControlLayout.PaddingRight);

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

            DisplayedRowCount++;
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
            AdvanceCursor(text.Length);
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
            AdvanceCursor(text.Length);
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
            AdvanceCursor(1);
        }

        public abstract IDisplay CreateChild();

        public Line CreateNewLine()
        {
            Line line = new Line();

            if (ControlLayout.MarginLeft > 0)
            {
                string text = new string(' ', ControlLayout.MarginLeft);
                LineSection lineSection = new LineSection { Text = text };
                line.PrefixSections.Add(lineSection);
            }

            if (ControlLayout.PaddingLeft > 0)
            {
                string text = new string(' ', ControlLayout.PaddingLeft);

                LineSection lineSection = new LineSection
                {
                    ForegroundColor = ForegroundColor,
                    BackgroundColor = BackgroundColor,
                    Text = text
                };
                line.PrefixSections.Add(lineSection);
            }

            if (ControlLayout.PaddingRight > 0)
            {
                string text = new string(' ', ControlLayout.PaddingRight);

                LineSection lineSection = new LineSection
                {
                    ForegroundColor = ForegroundColor,
                    BackgroundColor = BackgroundColor,
                    Text = text
                };
                line.PostfixSection.Add(lineSection);
            }

            if (ControlLayout.MarginRight > 0)
            {
                string text = new string(' ', ControlLayout.MarginRight);
                LineSection lineSection = new LineSection { Text = text };
                line.PostfixSection.Add(lineSection);
            }

            return line;
        }

        public Line CreateNewLine(string text)
        {
            Line line = CreateNewLine();

            line.ContentSections.Add(new LineSection
            {
                Text = text
            });

            return line;
        }

        public Line CreateNewLine(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, string text)
        {
            Line line = CreateNewLine();

            line.ContentSections.Add(new LineSection
            {
                ForegroundColor = foregroundColor,
                BackgroundColor = backgroundColor,
                Text = text
            });

            return line;
        }

        protected abstract void WriteNewLineInternal();

        protected abstract void WriteInternal(string text);

        protected abstract void WriteInternal(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, string text);

        protected abstract void WriteInternal(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, char c);

    }
}