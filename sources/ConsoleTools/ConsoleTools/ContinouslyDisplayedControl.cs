﻿// ConsoleTools
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
    /// This control is displayed continously until the <see cref="RequestClose"/> method is called.
    /// After each display, the <see cref="AfterSingleDisplay"/> event is raised.
    /// </summary>
    public abstract class ContinouslyDisplayedControl : Control
    {
        private volatile bool closeWasRequested;

        /// <summary>
        /// Gets a value that specifies if the control was requested to close.
        /// </summary>
        protected bool CloseWasRequested => closeWasRequested;

        /// <summary>
        /// Gets or sets the mode in which the control should be rendered: continously or just once.
        /// </summary>
        public DisplayMode DisplayMode { get; set; } = DisplayMode.Continous;

        /// <summary>
        /// Event raised after each display of the control.
        /// </summary>
        public event EventHandler AfterSingleDisplay;

        /// <summary>
        /// Displays the control once.
        /// While the control is displayed, the <see cref="DisplayMode"/> is changed to <see cref="ConsoleTools.DisplayMode.Once"/> value,
        /// but it is restored to the old value after the method is finished.
        /// </summary>
        public void DisplayOnce()
        {
            DisplayMode oldDisplayMode = DisplayMode;
            DisplayMode = DisplayMode.Once;

            try
            {
                Display();
            }
            finally
            {
                DisplayMode = oldDisplayMode;
            }
        }

        protected override void DoDisplay()
        {
            if (DisplayMode == DisplayMode.Once)
            {
                base.DoDisplay();
                OnAfterSingleDisplay();
            }
            else
            {
                DisplayContinous();
            }
        }

        private void DisplayContinous()
        {
            closeWasRequested = false;

            while (!closeWasRequested)
            {
                base.DoDisplay();
                OnAfterSingleDisplay();
            }
        }

        /// <summary>
        /// Sets the <see cref="CloseWasRequested"/> flag.
        /// The control will exit next time when it checks the flag.
        /// </summary>
        public void RequestClose()
        {
            closeWasRequested = true;
        }

        protected virtual void OnAfterSingleDisplay()
        {
            AfterSingleDisplay?.Invoke(this, EventArgs.Empty);
        }
    }
}