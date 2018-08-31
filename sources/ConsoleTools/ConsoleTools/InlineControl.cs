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
    /// Base class for a control displayed inline.
    /// It provides default functionality for margins (only left and right), colors, etc.
    /// </summary>
    public abstract class InlineControl
    {
        /// <summary>
        /// Gets or sets the number of spaces to be written before the content (to the left).
        /// Default value: 0
        /// </summary>
        public int MarginLeft { get; set; }

        /// <summary>
        /// Gets or sets the number of spaces to be written after the content (to the right).
        /// Default value: 0
        /// </summary>
        public int MarginRight { get; set; }

        /// <summary>
        /// Gets or sets a value that specifies if the cursor is visible while the control is displayed.
        /// Default value: <c>true</c>
        /// </summary>
        public bool ShowCursor { get; set; } = true;

        /// <summary>
        /// Event raised at the begining of the <see cref="Display"/> method, before doing anything else.
        /// </summary>
        public event EventHandler BeforeDisplay;

        /// <summary>
        /// Event raised at the very end of the <see cref="Display"/> method, before returning.
        /// </summary>
        public event EventHandler AfterDisplay;

        /// <summary>
        /// Displays the control in the console.
        /// </summary>
        public void Display()
        {
            OnBeforeDisplay();

            if (ShowCursor)
                DoDisplay();
            else
                CustomConsole.RunWithoutCursor(DoDisplay);

            OnAfterDisplay();
        }

        /// <summary>
        /// Displays the margins and the content of the control.
        /// </summary>
        protected virtual void DoDisplay()
        {
            if (MarginLeft > 0)
                DisplayLeftMargin();

            DoDisplayContent();

            if (MarginRight > 0)
                DisplayRightMargin();
        }

        private void DisplayLeftMargin()
        {
            string space = new string(' ', MarginLeft);
            Console.Write(space);
        }

        private void DisplayRightMargin()
        {
            string space = new string(' ', MarginRight);
            Console.Write(space);
        }

        protected abstract void DoDisplayContent();

        /// <summary>
        /// Method called at the begining of the <see cref="Display"/> method, before doing anything else.
        /// </summary>
        protected virtual void OnBeforeDisplay()
        {
            BeforeDisplay?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Method called at the very end of the <see cref="Display"/> method, before returning.
        /// </summary>
        protected virtual void OnAfterDisplay()
        {
            AfterDisplay?.Invoke(this, EventArgs.Empty);
        }
    }
}