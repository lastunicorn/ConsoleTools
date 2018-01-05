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
        public string Text { get; } = "Press any key to continue...";

        /// <summary>
        /// Gets or sets a value that specifies if the cursor is hidden while waiting for the user to press a key.
        /// </summary>
        public bool HideCursor { get; set; }

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
                RunWithoutCursor(DisplayInternal);
            else
                DisplayInternal();
        }

        private static void RunWithoutCursor(Action action)
        {
            bool initialCursorVisible = Console.CursorVisible;
            Console.CursorVisible = false;

            try
            {
                action();
            }
            finally
            {
                Console.CursorVisible = initialCursorVisible;
            }
        }

        private void DisplayInternal()
        {
            Console.WriteLine();
            Console.Write(Text);

            while (true)
            {
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);

                if (!UnlockKey.HasValue || consoleKeyInfo.Key == UnlockKey)
                    break;
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Displays the pause with default settings.
        /// </summary>
        public static void DisplayDefault()
        {
            new Pause().Display();
        }
    }
}