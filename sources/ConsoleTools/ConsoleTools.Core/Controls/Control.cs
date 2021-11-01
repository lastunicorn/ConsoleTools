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
    /// Provides base functionality for a control.
    /// </summary>
    public abstract class Control
    {
        private bool originalCursorVisibility;

        /// <summary>
        /// Gets or sets a value that specifies if the cursor is visible while the control is displayed.
        /// Default value: <c>true</c>
        /// </summary>
        public bool? CursorVisibility { get; set; }

        /// <summary>
        /// Gets or sets a value that specifies if the visibility of the cursor should be set back
        /// to the value it was before displaying the control.
        /// </summary>
        protected bool RestoreCursorVisibilityAfterDisplay { get; set; } = true;

        /// <summary>
        /// Event raised at the beginning of the <see cref="Display"/> method, before doing anything else.
        /// </summary>
        public virtual event EventHandler BeforeDisplay;

        /// <summary>
        /// Event raised at the very end of the <see cref="Display"/> method, before returning.
        /// </summary>
        public virtual event EventHandler AfterDisplay;

        /// <summary>
        /// Displays the control in the console.
        /// </summary>
        public void Display()
        {
            OnBeforeDisplay();

            if (CursorVisibility.HasValue)
            {
                originalCursorVisibility = Console.CursorVisible;
                Console.CursorVisible = CursorVisibility.Value;

                try
                {
                    DoDisplay();
                }
                finally
                {
                    if (RestoreCursorVisibilityAfterDisplay)
                        Console.CursorVisible = originalCursorVisibility;
                }
            }
            else
            {
                DoDisplay();
            }

            OnAfterDisplay();
        }

        /// <summary>
        /// When implemented by an inheritor, displays the margins and the content of the control.
        /// </summary>
        protected abstract void DoDisplay();

        /// <summary>
        /// Method called at the beginning of the <see cref="Display"/> method, before doing anything else
        /// to raise the <see cref="BeforeDisplay"/> event.
        /// When overwritten, the base method must be called in order to allow the event to be raised.
        /// </summary>
        protected virtual void OnBeforeDisplay()
        {
            BeforeDisplay?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Method called at the very end of the <see cref="Display"/> method, before returning
        /// to raise the <see cref="AfterDisplay"/> event.
        /// When overwritten, the base method must be called in order to allow the event to be raised.
        /// </summary>
        protected virtual void OnAfterDisplay()
        {
            AfterDisplay?.Invoke(this, EventArgs.Empty);
        }
    }
}