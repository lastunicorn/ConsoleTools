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

namespace DustInTheWind.ConsoleTools
{
    /// <summary>
    /// Displays a horizontal line by repeating a specific character.
    /// Multiple aspects can be configured like width, horizontal alignment, etc.
    /// </summary>
    /// <remarks>
    /// The content of the control is filled with the <see cref="Character"/> character.
    /// The control will always be one line height and, by default, it will stretch to fill the parent's client width.
    /// The <see cref="BlockControl.Width"/> property can be used to specify a smaller width if necessary.
    /// </remarks>
    public class HorizontalLine : BlockControl
    {
        /// <summary>
        /// Gets or sets the character to be used to fill the content of the control.
        /// </summary>
        public char Character { get; set; } = '-';

        /// <summary>
        /// Initializes a new instance of the <see cref="HorizontalLine"/> class.
        /// </summary>
        public HorizontalLine()
        {
            Margin = "0 1";
        }

        /// <summary>
        /// Displays the top padding, the content and the bottom padding.
        /// </summary>
        protected override void DoDisplayContent()
        {
            string text = GenerateText();
            WriteTextLine(text);
        }

        private MultilineText GenerateText()
        {
            return new string(Character, ActualContentWidth);
        }

        public static void QuickDisplay()
        {
            HorizontalLine horizontalLine = new HorizontalLine();
            horizontalLine.Display();
        }

        public static void QuickDisplay(char character)
        {
            HorizontalLine horizontalLine = new HorizontalLine
            {
                Character = character
            };
            horizontalLine.Display();
        }
    }
}