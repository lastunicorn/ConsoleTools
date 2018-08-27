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
    /// Represents a text to be displayed in the console.
    /// </summary>
    public class InlineTextBlock : InlineControl
    {
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text { get; set; }

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

        protected override void DoDisplay()
        {
            if (!ForegroundColor.HasValue && !BackgroundColor.HasValue)
                CustomConsole.Write(Text);
            else if (ForegroundColor.HasValue && BackgroundColor.HasValue)
                CustomConsole.Write(ForegroundColor.Value, BackgroundColor.Value, Text);
            else if (ForegroundColor.HasValue)
                CustomConsole.Write(ForegroundColor.Value, Text);
            else
                CustomConsole.WriteBackgroundColor(BackgroundColor.Value, Text);
        }

        public static implicit operator InlineTextBlock(string text)
        {
            return new InlineTextBlock(text);
        }

        public static implicit operator string(InlineTextBlock inlineTextBlock)
        {
            return inlineTextBlock.Text;
        }
    }
}