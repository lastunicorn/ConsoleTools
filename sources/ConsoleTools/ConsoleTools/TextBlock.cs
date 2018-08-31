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
    /// This control displays a multiline text to the console.
    /// </summary>
    public class TextBlock : Control
    {
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public MultilineText Text { get; set; }

        /// <summary>
        /// Gets or sets the number of spaces to be written before the text (to the left).
        /// Default value: 0
        /// </summary>
        public int MarginLeft { get; set; }

        /// <summary>
        /// Gets or sets the number of spaces to be written after the text (to the right).
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

        protected override void DoDisplayContent()
        {
            if (Text == null)
                return;

            MultilineText multilineText = Text;

            int consoleWidth = Console.BufferWidth;
            int lastLineLength = 0;

            for (int i = 0; i < multilineText.Lines.Count; i++)
            {
                string line = multilineText.Lines[i];
                lastLineLength = line.Length;

                bool isLineEqualToConsoleWidth = lastLineLength % consoleWidth == 0;

                if (isLineEqualToConsoleWidth)
                {
                    Console.Write(line);
                }
                else
                {
                    bool isLastLine = i == multilineText.Lines.Count - 1;

                    if (isLastLine)
                        Console.Write(line);
                    else
                        Console.WriteLine(line);
                }
            }

            if (lastLineLength != consoleWidth)
                Console.WriteLine();
        }

        /// <summary>
        /// Calculates the size of the current instance including the margins.
        /// </summary>
        /// <param name="maxWidth">The maximum width allowed. Negative value means no limit.</param>
        /// <returns>A <see cref="Size"/> instance representing the size of the control.</returns>
        public Size CalculateSize(int maxWidth = -1)
        {
            if (Text == null)
                return Size.Empty;

            if (maxWidth < 0)
                maxWidth = Console.BufferWidth;

            Size contentSize = Text.CalculateSize(maxWidth);

            int totalWidth = MarginLeft + contentSize.Width + MarginRight;
            int totalHeight = MarginTop + contentSize.Height + MarginBottom;

            return new Size(totalWidth, totalHeight);
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