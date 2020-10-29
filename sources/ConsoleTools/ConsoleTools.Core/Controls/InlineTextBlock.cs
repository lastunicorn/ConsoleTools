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
    /// Represents a text to be displayed in the console.
    /// </summary>
    public class InlineTextBlock : InlineControl
    {
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets a format string for the <see cref="Text"/> value.
        /// </summary>
        public string TextFormat { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineTextBlock"/> class.
        /// </summary>
        public InlineTextBlock()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineTextBlock"/> class with
        /// the text.
        /// </summary>
        public InlineTextBlock(string text)
        {
            Text = text;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineTextBlock"/> class with
        /// the text and the foreground color.
        /// </summary>
        public InlineTextBlock(string text, ConsoleColor foregroundColor)
        {
            Text = text;
            ForegroundColor = foregroundColor;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineTextBlock"/> class with
        /// the text, the foreground color and the background color.
        /// </summary>
        public InlineTextBlock(string text, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            Text = text;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }

        /// <summary>
        /// Displays the <see cref="Text"/> value.
        /// </summary>
        protected override void DoDisplayContent()
        {
            if (Text == null)
                return;

            string formattedText = TextFormat == null
                ? Text
                : string.Format(TextFormat, Text);

            WriteText(formattedText);
        }

        public int CalculateOuterLength()
        {
            int length = MarginLeft;

            if (Text != null)
                length += Text.Length;

            length += MarginRight;

            return length;
        }

        /// <summary>
        /// Converts a simple string into a <see cref="InlineTextBlock"/> object containing that string.
        /// </summary>
        /// <param name="text">The text to be converted.</param>
        public static implicit operator InlineTextBlock(string text)
        {
            return new InlineTextBlock(text);
        }

        /// <summary>
        /// Converts a <see cref="InlineTextBlock"/> object int a string by returning its text.
        /// </summary>
        /// <param name="inlineTextBlock">The <see cref="InlineTextBlock"/> object to be converted.</param>
        public static implicit operator string(InlineTextBlock inlineTextBlock)
        {
            return inlineTextBlock.Text;
        }
    }
}