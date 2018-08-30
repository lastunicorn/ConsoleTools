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
    /// Provides functionality of erassing the control after it is displayed.
    /// It is sometime useful for the controls that wait for an user input
    /// and then must get themselves out of the way.
    /// </summary>
    /// <remarks>
    /// In order to be able to successfully erase the control, the inheritor must
    /// provide in advance the size of the control.
    /// If the size of the control cannot be known in advance, there are some cases when
    /// the control cannot be successfully erased. For example when the console's buffer is full
    /// and new lines are inserted, the text displayed by the control is shifted up and
    /// the control has no way to know this. It will erase only part of the displayed text.
    /// </remarks>
    public abstract class ErasableControl : Control
    {
        private Location initialLocation;

        /// <summary>
        /// Gets the size of the control after it was displayed.
        /// </summary>
        protected Size Size { get; private set; }

        /// <summary>
        /// Gets or sets a value that specifies if the control is erased from the Console
        /// after it was displayed.
        /// </summary>
        public bool EraseAfterClose { get; set; }

        /// <summary>
        /// Method called immediately before writting the top margin.
        /// It calculates and stores the size of the control for later usage and
        /// writes enough empty lines to later write the content.
        /// </summary>
        protected override void OnBeforeTopMargin()
        {
            Size = CalculateControlSize();

            EnsureVerticalSpace();

            initialLocation = new Location(Console.CursorLeft, Console.CursorTop);

            base.OnBeforeTopMargin();
        }

        private void EnsureVerticalSpace()
        {
            int initialLeft = Console.CursorLeft;

            int totalHeight = MarginTop + Size.Height + MarginBottom;
            totalHeight = Math.Min(Console.BufferHeight - 1, totalHeight);

            for (int i = 0; i < totalHeight; i++)
                Console.WriteLine();

            Console.SetCursorPosition(initialLeft, Console.CursorTop - totalHeight);
        }

        /// <summary>
        /// Before displaying the control, this method is called in order to calculate the size of the control.
        /// The inheritors must returns e valid size if they need to have a preallocated vertical space in the console,
        /// before starting to display the content.
        /// If this method returns <see cref="Size.Empty"/>, no vertical space is preallocated.
        /// </summary>
        protected abstract Size CalculateControlSize();

        /// <summary>
        /// Method called at the very end, after all the control was displayed.
        /// It Erases the control if requested.
        /// </summary>
        protected override void OnAfterDisplay()
        {
            if (EraseAfterClose && Size.Height > 0)
                EraseControl();

            base.OnAfterDisplay();
        }

        private void EraseControl()
        {
            string emptyLine = new string(' ', Console.BufferWidth);

            Console.SetCursorPosition(initialLocation.Left, initialLocation.Top);

            int totalHeight = MarginTop + Size.Height + MarginBottom;

            for (int i = 0; i < totalHeight; i++)
                Console.Write(emptyLine);

            Console.SetCursorPosition(initialLocation.Left, initialLocation.Top);
        }
    }
}