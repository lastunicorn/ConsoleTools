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
// Bugs or fearure requests
// --------------------------------------------------------------------------------
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new

using System;

namespace DustInTheWind.ConsoleTools
{
    /// <summary>
    /// A contyrol that displays a message and waits for the user to press any key.
    /// </summary>
    public class Pause
    {
        /// <summary>
        /// Gets or sets the text to be displayed to the user while witing for the user to press a key.
        /// </summary>
        public string Text { get; set; } = PauseResources.PauseText;

        /// <summary>
        /// Gets or sets a value that specifies if the cursor is hidden while waiting for the user to press a key.
        /// </summary>
        public bool HideCursor { get; set; }

        /// <summary>
        /// Gets or sets the number of empty lines displayed before the pause text.
        /// Default value: 1
        /// </summary>
        public int MarginTop { get; set; } = 1;

        /// <summary>
        /// Gets or sets the number of empty lines displayed after the pause text, after the pause was ended.
        /// Default value: 1
        /// </summary>
        public int MarginBottom { get; set; } = 1;

        /// <summary>
        /// Gets or sets a value that specifies if the <see cref="Text"/> is erased from the Console
        /// after the user press the <see cref="UnlockKey"/>.
        /// </summary>
        public bool EraseAfterClose { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ConsoleKey"/> the user must press to break the pause.
        /// If <c>null</c> is provided, any key unbreaks the pause.
        /// Default value: null
        /// </summary>
        public ConsoleKey? UnlockKey { get; set; }

        /// <summary>
        /// Displays the pause text and waits for the user to press a key.
        /// </summary>
        public void Display()
        {
            if (HideCursor)
                CustomConsole.WithoutCursor(DisplayInternal);
            else
                DisplayInternal();
        }

        private void DisplayInternal()
        {
            // Ensure new line

            if (Console.CursorLeft != 0)
                Console.WriteLine();

            // Write empty space

            Size size = CalculateDimensions();

            for (int i = 0; i < size.Height; i++)
                Console.WriteLine();

            Console.SetCursorPosition(0, Console.CursorTop - size.Height);

            // Display the control

            int initialCursorTop = Console.CursorTop;
            int initialCursorLeft = Console.CursorLeft;

            WriteTopMargin();

            int textCursorTop = Console.CursorTop;
            int textCursorLeft = Console.CursorLeft;
            int textLength = Text?.Length ?? 0;

            Console.Write(Text);
            WaitForUnlockKey();

            if (EraseAfterClose)
            {
                Console.SetCursorPosition(textCursorLeft, textCursorTop);
                Console.Write(new string(' ', textLength));
                Console.SetCursorPosition(initialCursorLeft, initialCursorTop);
            }
            else
            {
                Console.WriteLine();
                WriteBottomMargin();
            }
        }

        private Size CalculateDimensions()
        {
            int textWidth = Text.Length < Console.BufferWidth
                ? Text.Length
                : Console.BufferWidth;

            int textHeight = (int)Math.Ceiling(Text.Length / (double)Console.BufferWidth);

            int totalHeight = MarginTop + textHeight + MarginBottom;

            return new Size(textWidth, totalHeight);
        }

        private void WriteTopMargin()
        {
            for (int i = 0; i < MarginTop; i++)
                Console.WriteLine();
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

        private void WriteBottomMargin()
        {
            for (int i = 0; i < MarginBottom; i++)
                Console.WriteLine();
        }

        /// <summary>
        /// Displays the pause with default settings.
        /// </summary>
        public static void QuickPause()
        {
            new Pause().Display();
        }

        /// <summary>
        /// Displays the pause with default settings.
        /// </summary>
        /// <param name="text">The text to be displayed by the <see cref="Pause"/> control.</param>
        public static void QuickPause(string text)
        {
            Pause pause = new Pause { Text = text };
            pause.Display();
        }

        /// <summary>
        /// Displays the pause with default settings.
        /// </summary>
        /// <param name="text">The text to be displayed by the <see cref="Pause"/> control.</param>
        /// <param name="unlockKey">The key thatthe user has to press to unlock the pause.</param>
        public static void QuickPause(string text, ConsoleKey unlockKey)
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