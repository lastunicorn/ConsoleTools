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

namespace DustInTheWind.ConsoleTools.InputControls
{
    /// <summary>
    /// Represents a multiline text to be displayed to the console.
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

        protected override void DoDisplayContent()
        {
            if (Text != null)
                DisplayMultilineText(Text);
        }

        private static void DisplayMultilineText(MultilineText multilineText)
        {
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
    }
}