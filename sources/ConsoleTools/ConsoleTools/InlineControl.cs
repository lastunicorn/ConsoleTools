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

namespace DustInTheWind.ConsoleTools
{
    /// <summary>
    /// Base class for a control displayed inline.
    /// It provides default functionality for margins (only left and right), colors, etc.
    /// </summary>
    public abstract class InlineControl
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
        /// Displays the control in the Console.
        /// </summary>
        public void Display()
        {
            if (MarginLeft > 0)
                DisplayLeftMargin();

            DoDisplay();

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

        protected abstract void DoDisplay();
    }
}