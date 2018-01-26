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
using DustInTheWind.ConsoleTools.TabularData;

namespace DustInTheWind.ConsoleTools
{
    /// <summary>
    /// A control that displays a message and waits for the user to press any key.
    /// </summary>
    public class Pause : ErasableControl
    {
        //public MultilineText Text2 { get; set; }
        /// <summary>
        /// Gets or sets the text to be displayed to the user while witing for the user to press a key.
        /// </summary>
        public MultilineText Text { get; set; } = PauseResources.PauseText;

        /// <summary>
        /// Gets or sets the <see cref="ConsoleKey"/> the user must press to break the pause.
        /// If <c>null</c> is provided, any key unbreaks the pause.
        /// Default value: null
        /// </summary>
        public ConsoleKey? UnlockKey { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pause"/> class.
        /// </summary>
        public Pause()
        {
            MarginTop = 1;
            MarginBottom = 1;
            EnsureBeginOfLine = true;
        }

        /// <summary>
        /// Displays the pause text and waits for the user to press a key.
        /// </summary>
        protected override void DoDisplayContent()
        {
            int consoleWidth = Console.BufferWidth;
            int lastLineLength = 0;

            for (int i = 0; i < Text.Lines.Count; i++)
            {
                string line = Text.Lines[i];
                lastLineLength = line.Length;

                if (lastLineLength % consoleWidth == 0 || i == Text.Lines.Count - 1)
                    Console.Write(line);
                else
                    Console.WriteLine(line);
            }

            WaitForUnlockKey();

            if (lastLineLength != consoleWidth)
                Console.WriteLine();
        }

        /// <summary>
        /// Calculates and returns the size of the current instance in characters, including the top and bottom margins.
        /// The size is calculated as the control will be displayed starting from the current locatio of the cursor in the console.
        /// </summary>
        protected override Size CalculateControlSize()
        {
            int textWidth = 0;
            int textHeight = 0;

            bool isFirstLine = true;

            foreach (string line in Text.Lines)
            {
                int lineFill;

                if (isFirstLine)
                {
                    lineFill = Console.CursorLeft;
                    isFirstLine = false;
                }
                else
                {
                    lineFill = 0;
                }

                textWidth = Math.Max(textWidth, Math.Min(line.Length, Console.BufferWidth - lineFill));
                textHeight += (int)Math.Ceiling(line.Length / (double)(Console.BufferWidth - lineFill));
            }

            int totalHeight = MarginTop + textHeight + MarginBottom;
            return new Size(textWidth, totalHeight);
        }

        private void WaitForUnlockKey()
        {
            while (true)
            {
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);

                if (!UnlockKey.HasValue || consoleKeyInfo.Key == UnlockKey)
                    break;
            }
        }

        /// <summary>
        /// Displays the pause with default settings.
        /// </summary>
        public static void QuickDisplay()
        {
            new Pause().Display();
        }

        /// <summary>
        /// Displays the pause with default settings.
        /// </summary>
        /// <param name="text">The text to be displayed by the <see cref="Pause"/> control.</param>
        public static void QuickDisplay(string text)
        {
            Pause pause = new Pause { Text = text };
            pause.Display();
        }

        /// <summary>
        /// Displays the pause with default settings.
        /// </summary>
        /// <param name="text">The text to be displayed by the <see cref="Pause"/> control.</param>
        /// <param name="unlockKey">The key thatthe user has to press to unlock the pause.</param>
        public static void QuickDisplay(string text, ConsoleKey unlockKey)
        {
            Pause pause = new Pause
            {
                Text = text,
                UnlockKey = unlockKey
            };
            pause.Display();
        }
    }
}