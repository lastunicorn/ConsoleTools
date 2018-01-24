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
    /// Provides base functionality for a control like top/bottom margin,
    /// optionally hides cucursor while displaying,
    /// optionally ensures the display of the control on a new line,
    /// optionally erasses the control after it is displayed.
    /// </summary>
    public abstract class Control
    {
        protected Location InitialLocation { get; set; }

        protected Size Size { get; private set; }

        /// <summary>
        /// Gets or sets the number of empty lines displayed before the pause text.
        /// Default value: 0
        /// </summary>
        public int MarginTop { get; set; }

        /// <summary>
        /// Gets or sets the number of empty lines displayed after the pause text, after the pause was ended.
        /// Default value: 0
        /// </summary>
        public int MarginBottom { get; set; }

        /// <summary>
        /// Gets or sets a value that specifies if the cursor is visible while the control is displayed.
        /// Default value: <c>true</c>
        /// </summary>
        public bool ShowCursor { get; set; } = true;

        /// <summary>
        /// Gets or sets a value that specifies if the control should always be displayed at the beginning of the line.
        /// If this value is <c>true</c> and the cursor is not at the beginning of the line, a new line is written before displaying the control.
        /// </summary>
        public bool EnsureBeginOfLine { get; set; }

        /// <summary>
        /// Gets or sets a value that specifies if the control is erased from the Console
        /// after it was displayed.
        /// </summary>
        public bool EraseAfterClose { get; set; }

        /// <summary>
        /// Displays the pause text and waits for the user to press a key.
        /// </summary>
        public void Display()
        {
            OnBeforeDisplay();

            if (ShowCursor)
                DisplayInternal();
            else
                CustomConsole.WithoutCursor(DisplayInternal);

            OnAfterDisplay();
        }

        private void DisplayInternal()
        {
            MoveToNextLineIfNecessary();

            Size = CalculateControlSize();

            EnsureVerticalSpace();

            InitialLocation = new Location(Console.CursorLeft, Console.CursorTop);

            WriteTopMargin();

            DoDisplayContent();

            WriteBottomMargin();

            if (EraseAfterClose && Size.Height > 0)
                EraseControl();
        }

        private void MoveToNextLineIfNecessary()
        {
            if (EnsureBeginOfLine && Console.CursorLeft != 0)
                Console.WriteLine();
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
        protected virtual Size CalculateControlSize()
        {
            return Size.Empty;
        }

        /// <summary>
        /// Method called at the begining of the <see cref="Display"/> method before doing anything else.
        /// </summary>
        protected virtual void OnBeforeDisplay()
        {
        }

        /// <summary>
        /// When implemented by an inheritor it displays the content of the control to the console.
        /// </summary>
        protected abstract void DoDisplayContent();

        private void EraseControl()
        {
            string emptyLine = new string(' ', Console.BufferWidth);

            Console.SetCursorPosition(InitialLocation.Left, InitialLocation.Top);

            for (int i = 0; i < Size.Height; i++)
                Console.Write(emptyLine);

            Console.SetCursorPosition(InitialLocation.Left, InitialLocation.Top);
        }

        /// <summary>
        /// Method called at the very end of the <see cref="Display"/> method before returning.
        /// </summary>
        protected virtual void OnAfterDisplay()
        {
        }

        private void WriteTopMargin()
        {
            for (int i = 0; i < MarginTop; i++)
                Console.WriteLine();
        }

        private void WriteBottomMargin()
        {
            for (int i = 0; i < MarginBottom; i++)
                Console.WriteLine();
        }
    }
}