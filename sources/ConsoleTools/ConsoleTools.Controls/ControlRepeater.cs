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

namespace DustInTheWind.ConsoleTools.Controls
{
    /// <summary>
    /// Displays a control repeatedly until the <see cref="RequestClose"/> method is called.
    /// </summary>
    public class ControlRepeater : BlockControl
    {
        private volatile bool closeWasRequested;
        private BlockControl control;

        private bool isRunning;

        /// <summary>
        /// Gets or sets the control that is to be displayed repeatedly.
        /// </summary>
        public BlockControl Control
        {
            get => control;
            set
            {
                if (control is IRepeatableSupport repeatableControl1)
                    repeatableControl1.Closed -= HandleControlClosed;

                if (isRunning)
                    throw new Exception("The control cannot be changed while the Display method is running.");

                control = value;

                if (control is IRepeatableSupport repeatableControl2)
                    repeatableControl2.Closed += HandleControlClosed;
            }
        }

        private void HandleControlClosed(object sender, EventArgs e)
        {
            closeWasRequested = true;
        }

        /// <summary>
        /// Gets a value that specifies if the control was requested to close.
        /// </summary>
        protected bool CloseWasRequested => closeWasRequested;

        /// <summary>
        /// Runs a loop in which the <see cref="Control"/> is displayed repeatedly
        /// until the <see cref="RequestClose"/> method is called.
        /// </summary>
        protected override void DoDisplayContent(ControlDisplay display)
        {
            isRunning = true;
            try
            {
                if (Control == null)
                    return;

                closeWasRequested = false;

                while (!closeWasRequested)
                    Control.Display();
            }
            finally
            {
                isRunning = false;
            }
        }

        /// <summary>
        /// Sets the <see cref="CloseWasRequested"/> flag.
        /// The control will exit next time when it checks the flag.
        /// </summary>
        public void RequestClose()
        {
            closeWasRequested = true;

            if (Control is IRepeatableSupport repeatableControl)
                repeatableControl.RequestClose();
        }
    }
}