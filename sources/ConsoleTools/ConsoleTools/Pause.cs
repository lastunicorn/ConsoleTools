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
    /// A control that displays a message and waits for the user to press any key.
    /// </summary>
    public class Pause : ErasableControl
    {
        private int lastLineLength;

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
            Margin = "0 1";
        }

        /// <summary>
        /// Displays the pause text and waits for the user to press a key.
        /// </summary>
        protected override void DoDisplayContent()
        {
            if (Text == null)
                return;

            lastLineLength = 0;

            IEnumerable<string> lines = Text.GetLines(ActualContentWidth);

            foreach (string line in lines)
            {
                lastLineLength = line.Length;
                //string text = line;
                
                //if(text.Length < ActualContentWidth)
                //    text += new string(' ', ActualContentWidth - lastLineLength);

                WriteTextLine(line);
                InnerSize = InnerSize.InflateHeight(1);
            }
        }

        protected override void OnAfterDisplay()
        {
            int oldCursorLeft = Console.CursorLeft;
            int oldCursorTop = Console.CursorTop;

            int cursorLeft = Margin.Left + Padding.Left + lastLineLength;
            int cursorTop = Console.CursorTop - 1 - Margin.Bottom - Padding.Bottom;
            Console.SetCursorPosition(cursorLeft, cursorTop);

            WaitForUnlockKey();

            Console.SetCursorPosition(oldCursorLeft, oldCursorTop);

            base.OnAfterDisplay();
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