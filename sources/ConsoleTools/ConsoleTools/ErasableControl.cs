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
        protected Location InitialLocation { get; set; }

        protected Size Size { get; private set; }

        /// <summary>
        /// Gets or sets a value that specifies if the control is erased from the Console
        /// after it was displayed.
        /// </summary>
        public bool EraseAfterClose { get; set; }

        protected override void OnBeforeTopMargin()
        {
            Size = CalculateControlSize();

            EnsureVerticalSpace();

            InitialLocation = new Location(Console.CursorLeft, Console.CursorTop);

            base.OnBeforeTopMargin();
        }

        private void EnsureVerticalSpace()
        {
            for (int i = 0; i < Size.Height; i++)
                Console.WriteLine();

            Console.SetCursorPosition(0, Console.CursorTop - Size.Height);
        }

        /// <summary>
        /// Before displaying the control, this method is called in order to calculate the size of the control.
        /// The inheritors must returns e valid size if they need to have a preallocated vertical space in the console,
        /// before starting to display the content.
        /// If this method returns <see cref="Size.Empty"/>, no vertical space is preallocated.
        /// </summary>
        protected abstract Size CalculateControlSize();

        protected override void OnAfterDisplay()
        {
            if (EraseAfterClose && Size.Height > 0)
                EraseControl();

            base.OnAfterDisplay();
        }

        private void EraseControl()
        {
            string emptyLine = new string(' ', Console.BufferWidth);

            Console.SetCursorPosition(InitialLocation.Left, InitialLocation.Top);

            for (int i = 0; i < Size.Height; i++)
                Console.Write(emptyLine);

            Console.SetCursorPosition(InitialLocation.Left, InitialLocation.Top);
        }
    }
}