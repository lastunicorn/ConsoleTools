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
using System.Collections.Generic;

namespace DustInTheWind.ConsoleTools
{
    /// <summary>
    /// This control displays a multiline text to the console.
    /// </summary>
    public class TextBlock : BlockControl
    {
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public MultilineText Text { get; set; }

        /// <summary>
        /// Gets or sets the maximum width allowed including the margins.
        /// Negative value means the limit is the console's width.
        /// </summary>
        public int MaxWidth { get; set; } = -1;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextBlock"/> class.
        /// </summary>
        public TextBlock()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextBlock"/> class
        /// with the text to be displayed.
        /// </summary>
        public TextBlock(MultilineText text)
        {
            Text = text;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextBlock"/> class
        /// with the text to be displayed.
        /// </summary>
        public TextBlock(string text)
        {
            Text = text;
        }

        /// <summary>
        /// Displays the lines of text together with the left and right margins.
        /// </summary>
        protected override void DoDisplayContent()
        {
            if (Text == null)
                return;

            int consoleWidth = Console.BufferWidth;

            Size innerSize = Text.CalculateSize(consoleWidth);
            Size outerSize = innerSize + new Size(Margin.Left + Margin.Right, Margin.Top + Margin.Bottom);

            bool isBlockEqualToConsoleWidth = outerSize.Width == consoleWidth;

            string marginLeftText = Margin.Left > 0
                ? new string(' ', Margin.Left)
                : string.Empty;

            string marginRightText = Margin.Right > 0
                ? new string(' ', Margin.Right)
                : string.Empty;

            IEnumerable<string> chunks = Text.GetLines(innerSize.Width);

            foreach (string chunk in chunks)
            {
                Console.Write(marginLeftText);
                WriteText(chunk);
                Console.Write(marginRightText);

                if (!isBlockEqualToConsoleWidth)
                    Console.WriteLine();
            }
        }

        /// <summary>
        /// Calculates the size of the current instance including the margins.
        /// </summary>
        /// <returns>A <see cref="Size"/> instance representing the size of the control.</returns>
        public Size CalculateOuterSize()
        {
            if (Text == null)
                return Size.Empty;

            int outerMaxWidth = MaxWidth < 0
                ? Console.BufferWidth
                : MaxWidth;

            int innerMaxWidth = outerMaxWidth - Margin.Left - Margin.Right;

            Size contentSize = Text.CalculateSize(innerMaxWidth);

            int totalWidth = Margin.Left + contentSize.Width + Margin.Right;
            int totalHeight = Margin.Top + contentSize.Height + Margin.Bottom;

            return new Size(totalWidth, totalHeight);
        }

        /// <summary>
        /// Calculates the size of the current instance's content. Margins are not included.
        /// </summary>
        /// <returns>A <see cref="Size"/> instance representing the size of the control.</returns>
        public Size CalculateInnerSize()
        {
            if (Text == null)
                return Size.Empty;

            int outerMaxWidth = MaxWidth < 0
                ? Console.BufferWidth
                : MaxWidth;

            int innerMaxWidth = outerMaxWidth - Margin.Left - Margin.Right;

            return Text.CalculateSize(innerMaxWidth);
        }

        /// <summary>
        /// Displays the specified text into the console.
        /// </summary>
        /// <param name="text">The text to be displayed to the console.</param>
        private static void QuickDisplay(string text)
        {
            TextBlock textBlock = new TextBlock
            {
                Text = text
            };
            textBlock.Display();
        }
    }
}