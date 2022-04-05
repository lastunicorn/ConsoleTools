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
using System.Collections.Generic;

namespace DustInTheWind.ConsoleTools.Controls
{
    public abstract class DisplayBase : IDisplay
    {
        private List<ConsoleColor> foregroundColors = new List<ConsoleColor>();
        
        private List<ConsoleColor> backgroundColors = new List<ConsoleColor>();

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

        protected abstract void SetRowForegroundColor(ConsoleColor? foregroundColor);

        protected abstract void SetRowBackgroundColor(ConsoleColor? backgroundColor);

        /// <summary>
        /// Writes the ending of a row.
        /// It includes the right margin and padding.
        /// </summary>
        public void EndRow()
        {
            bool isConsoleRowFull = FillEmptySpace();
            WriteRightPadding();

            RestoreForegroundColor();
            RestoreBackgroundColor();

            WriteRightMargin();
            WriteOuterRightEmptySpace();

            if (!isConsoleRowFull)
                Console.WriteLine();

            DisplayedRowCount++;
        }

        private bool FillEmptySpace()
        {
            if (Layout == null)
                return false;

            int cursorLeft = Console.CursorLeft;

            if (cursorLeft >= Layout.ActualFullWidth)
                return false;

            int marginRight = Layout.MarginRight;
            int paddingRight = Layout.PaddingRight;
            int emptySpaceRight = Layout.ActualFullWidth - cursorLeft - paddingRight - marginRight;

            if (emptySpaceRight <= 0)
                return false;

            string rightContentEmptySpace = new string(' ', emptySpaceRight);
            WriteInternal(rightContentEmptySpace);

            int currentCursorPosition = cursorLeft + emptySpaceRight + paddingRight + marginRight;
            bool isConsoleRowFull = currentCursorPosition == Console.BufferWidth;
            return isConsoleRowFull;
        }

        protected abstract void RestoreForegroundColor();

        protected abstract void RestoreBackgroundColor();

        public void Write(string text)
        {
            if (text == null)
                return;

            CustomConsole.Write(text);

            //if (text == null)
            //{
            //    if (Layout != null)
            //    {
            //        string rightContentEmptySpace = new string(' ', Layout.ActualContentWidth);
            //        CustomConsole.Write(rightContentEmptySpace);
            //    }

            //    return;
            //}

            //if (Layout == null)
            //{
            //    CustomConsole.Write(text);
            //    return;
            //}

            //if (text.Length <= Layout.ActualContentWidth)
            //{
            //    CustomConsole.Write(text);

            //    if (text.Length < Layout.ActualContentWidth)
            //    {
            //        string rightContentEmptySpace = new string(' ', Layout.ActualContentWidth - text.Length);
            //        CustomConsole.Write(rightContentEmptySpace);
            //    }
            //}
            //else
            //{
            //    CustomConsole.Write(text.Substring(0, Layout.ActualContentWidth));
            //}
        }

        public void Write(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, string text)
        {
            if (text == null)
                return;

            if (foregroundColor.HasValue)
            {
                if (backgroundColor.HasValue)
                    CustomConsole.Write(foregroundColor.Value, backgroundColor.Value, text);
                else
                    CustomConsole.Write(foregroundColor.Value, text);
            }
            else
            {
                if (backgroundColor.HasValue)
                    CustomConsole.WriteBackgroundColor(backgroundColor.Value, text);
                else
                    CustomConsole.Write(text);
            }
        }

        public void Write(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, char c)
        {
            if (foregroundColor.HasValue)
            {
                if (backgroundColor.HasValue)
                    CustomConsole.Write(foregroundColor.Value, backgroundColor.Value, c);
                else
                    CustomConsole.Write(foregroundColor.Value, c);
            }
            else
            {
                if (backgroundColor.HasValue)
                    CustomConsole.WriteBackgroundColor(backgroundColor.Value, c);
                else
                    CustomConsole.Write(c);
            }
        }

        protected abstract void WriteInternal(string text);
        
        protected abstract void WriteInternal(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string text);
        
        protected abstract void WriteInternal(ConsoleColor foregroundColor, ConsoleColor backgroundColor, char c);

        private void WriteOuterLeftEmptySpace()
        {
            int spacesCount = Layout.OuterEmptySpaceLeft;

            if (spacesCount > 0)
                Console.Write(new string(' ', spacesCount));
        }

        private void WriteOuterRightEmptySpace()
        {
            int spacesCount = Layout.OuterEmptySpaceRight;

            if (spacesCount > 0)
                Console.Write(new string(' ', spacesCount));
        }

        private void WriteLeftMargin()
        {
            if (Layout.MarginLeft <= 0)
                return;

            string text = new string(' ', Layout.MarginLeft);
            CustomConsole.Write(text);
        }

        private void WriteRightMargin()
        {
            if (Layout.MarginRight <= 0)
                return;

            string text = new string(' ', Layout.MarginRight);
            CustomConsole.Write(text);
        }

        private void WriteLeftPadding()
        {
            if (Layout.PaddingLeft <= 0)
                return;

            string text = new string(' ', Layout.PaddingLeft);

            if (BackgroundColor.HasValue)
                CustomConsole.WithBackgroundColor(BackgroundColor.Value, () => CustomConsole.Write(text));
            else
                CustomConsole.Write(text);
        }

        private void WriteRightPadding()
        {
            if (Layout.PaddingRight <= 0)
                return;

            string text = new string(' ', Layout.PaddingRight);

            if (BackgroundColor.HasValue)
                CustomConsole.WithBackgroundColor(BackgroundColor.Value, () => CustomConsole.Write(text));
            else
                CustomConsole.Write(text);
        }
    }
}