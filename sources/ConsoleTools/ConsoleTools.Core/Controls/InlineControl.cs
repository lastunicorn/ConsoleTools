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
    /// <summary>
    /// Base class for a control displayed inline.
    /// It provides default functionality for margins (only left and right), colors, etc.
    /// </summary>
    public abstract class InlineControl : Control
    {
        /// <summary>
        /// Gets or sets the number of spaces to be written before the content (to the left).
        /// Default value: 0
        /// </summary>
        public int MarginLeft { get; set; }

        /// <summary>
        /// Gets or sets the number of spaces to be written after the content (to the right).
        /// Default value: 0
        /// </summary>
        public int MarginRight { get; set; }

        /// <summary>
        /// Gets or sets the number of spaces to be written before the content (to the left).
        /// Default value: 0
        /// </summary>
        public int PaddingLeft { get; set; }

        /// <summary>
        /// Gets or sets the number of spaces to be written after the content (to the right).
        /// Default value: 0
        /// </summary>
        public int PaddingRight { get; set; }

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
        /// Displays the margins and the content of the control.
        /// </summary>
        protected override void DoDisplay()
        {
            if (MarginLeft > 0)
                DisplayLeftMargin();

            if (PaddingLeft > 0)
                DisplayPaddingLeft();

            DoDisplayContent();

            if (PaddingRight > 0)
                DisplayPaddingRight();

            if (MarginRight > 0)
                DisplayRightMargin();
        }

        private void DisplayLeftMargin()
        {
            string space = new string(' ', MarginLeft);
            Console.Write(space);
        }

        private void DisplayRightMargin()
        {
            string space = new string(' ', MarginRight);
            Console.Write(space);
        }

        private void DisplayPaddingLeft()
        {
            string paddingLeft = new string(' ', PaddingLeft);
            WriteText(paddingLeft);
        }

        private void DisplayPaddingRight()
        {
            string paddingRight = new string(' ', PaddingRight);
            WriteText(paddingRight);
        }

        /// <summary>
        /// When implemented in a derived class, it displays the content of the control.
        /// This method is not responsible to display the margins.
        /// </summary>
        protected abstract void DoDisplayContent();

        /// <summary>
        /// Helper method that writes the specified text to the console using the
        /// <see cref="ForegroundColor"/> and <see cref="BackgroundColor"/> values.
        /// </summary>
        /// <param name="text">The text to be written to the console.</param>
        protected void WriteText(string text)
        {
            if (!ForegroundColor.HasValue && !BackgroundColor.HasValue)
                CustomConsole.Write(text);
            else if (ForegroundColor.HasValue && BackgroundColor.HasValue)
                CustomConsole.Write(ForegroundColor.Value, BackgroundColor.Value, text);
            else if (ForegroundColor.HasValue)
                CustomConsole.Write(ForegroundColor.Value, text);
            else
                CustomConsole.WriteBackgroundColor(BackgroundColor.Value, text);
        }
    }
}