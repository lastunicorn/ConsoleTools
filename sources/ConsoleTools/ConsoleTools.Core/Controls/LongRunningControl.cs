// ConsoleTools
// Copyright (C) 2017-2020 Dust in the Wind
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
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new/choose

using System;

namespace DustInTheWind.ConsoleTools
{
    /// <summary>
    /// Provides base functionality for a control that continues to run after it is displayed, until it is explicitly closed.
    /// <para>
    /// The provided functionality is:
    /// <list type="bullet">
    ///     <item><description>top/bottom margin,</description></item>
    ///     <item><description>optionally hides cursor while displaying,</description></item>
    ///     <item><description>optionally ensures the display of the control on a new line.</description></item>
    /// </list>
    /// </para>
    /// </summary>
    public abstract class LongRunningControl
    {
        private bool initialCursorVisible;

        /// <summary>
        /// Gets a value that specifies if the control is still active.
        /// Active means that it will automatically refresh itself when its state is changed.
        /// It become active when the <see cref="Display"/> method is called.
        /// It is inactivated when the <see cref="Close"/> method is called.
        /// </summary>
        protected bool IsActive { get; private set; }

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
        /// Displays the control and changes its status to "running".
        /// </summary>
        public virtual void Display()
        {
            OnBeforeDisplay();

            if (IsActive)
                return;

            initialCursorVisible = Console.CursorVisible;
            if (!ShowCursor)
                Console.CursorVisible = false;

            MoveToNextLineIfNecessary();

            WriteTopMargin();

            DoDisplayContent();

            IsActive = true;

            Refresh();
        }

        /// <summary>
        /// Method called at the beginning of the <see cref="Display"/> method, before doing anything else.
        /// </summary>
        protected virtual void OnBeforeDisplay()
        {
        }

        /// <summary>
        /// Method called immediately before writing the top margin.
        /// </summary>
        protected virtual void OnBeforeTopMargin()
        {
        }

        /// <summary>
        /// Method called immediately after writing the bottom margin.
        /// </summary>
        protected virtual void OnAfterBottomMargin()
        {
        }

        private void MoveToNextLineIfNecessary()
        {
            if (Console.CursorLeft != 0 && (EnsureBeginOfLine || MarginTop > 0))
                Console.WriteLine();
        }

        /// <summary>
        /// When implemented by an inheritor, it displays the content of the control to the console.
        /// </summary>
        protected abstract void DoDisplayContent();

        private void WriteTopMargin()
        {
            OnBeforeTopMargin();

            for (int i = 0; i < MarginTop; i++)
                Console.WriteLine();
        }

        private void WriteBottomMargin()
        {
            for (int i = 0; i < MarginBottom; i++)
                Console.WriteLine();

            OnAfterBottomMargin();
        }

        /// <summary>
        /// Displays again the control in the console.
        /// This is done only if the control is active. 
        /// </summary>
        protected void Refresh()
        {
            if (IsActive)
                DoRefresh();
        }

        /// <summary>
        /// When implemented by an inheritor, it performs the necessary actions to show the control in the console.
        /// </summary>
        protected abstract void DoRefresh();

        /// <summary>
        /// Changes the status of the control to "not running" and ends its display.
        /// </summary>
        public void Close()
        {
            OnClosing();

            if (!IsActive)
                return;

            DoClose();

            WriteBottomMargin();

            if (!ShowCursor)
                Console.CursorVisible = initialCursorVisible;

            IsActive = false;

            OnClosed();
        }

        /// <summary>
        /// Method called at the very beginning of the <see cref="Close"/> method.
        /// </summary>
        protected virtual void OnClosing()
        {
        }

        /// <summary>
        /// When implemented by an inheritor, it performs the necessary actions to close the control.
        /// </summary>
        protected abstract void DoClose();

        /// <summary>
        /// Method called at the very end of the <see cref="Close"/> method, before returning.
        /// </summary>
        protected virtual void OnClosed()
        {
        }
    }
}