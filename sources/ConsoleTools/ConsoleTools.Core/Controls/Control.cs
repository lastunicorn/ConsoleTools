// ConsoleTools
// Copyright (C) 2017-2022 Dust in the Wind
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
        /// Gets or sets the foreground color used to write the text.
        /// Default value: <c>null</c>
        /// </summary>
        public ConsoleColor? ForegroundColor { get; set; }

        /// <summary>
        /// Gets or sets the background color used to write the text.
        /// Default value: <c>null</c>
        /// </summary>
        public ConsoleColor? BackgroundColor { get; set; }

        /// <summary>
        /// When implemented by an inheritor, gets the desired width of the content when there are no other restrictions applied to the control.
        /// If the value is not provided, <see cref="int.MaxValue"/> is assumed.
        /// </summary>
        protected virtual int? DesiredContentWidth { get; }

        /// <summary>
        /// Event raised at the beginning of the <see cref="Display()"/> method, before doing anything else.
        /// </summary>
        public virtual event EventHandler<DisplayEventArgs> BeforeDisplay;

        /// <summary>
        /// Event raised at the very end of the <see cref="Display()"/> method, before returning.
        /// </summary>
        public virtual event EventHandler<DisplayEventArgs> AfterDisplay;

        /// <summary>
        /// Displays the control in the console.
        /// </summary>
        public void Display()
        {
            IDisplay display = CreateDisplay();
            Display(display);
        }

        public IDisplay CreateDisplay(IDisplay parent = null)
        {
            IDisplay childDisplay = parent == null
                ? new ControlDisplay()
                : parent.CreateChild();

            childDisplay.ForegroundColor = ForegroundColor;
            childDisplay.BackgroundColor = BackgroundColor;

            childDisplay.ControlLayout = CalculateLayout(childDisplay);

            return childDisplay;
        }

        private ControlLayout CalculateLayout(IDisplay display)
        {
            ControlLayout controlLayout = new ControlLayout
            {
                Control = this,
                AvailableWidth = display.AvailableWidth,
                DesiredContentWidth = DesiredContentWidth
            };

            controlLayout.Calculate();

            return controlLayout;
        }

        /// <summary>
        /// Displays the control using the specified display.
        /// </summary>
        public void Display(IDisplay display)
        {
            if (display == null) throw new ArgumentNullException(nameof(display));

            DisplayEventArgs beforeDisplayArgs = new DisplayEventArgs(display);
            OnBeforeDisplay(beforeDisplayArgs);

            if (CursorVisibility.HasValue)
            {
                originalCursorVisibility = display.IsCursorVisible;
                display.IsCursorVisible = CursorVisibility.Value;

                try
                {
                    DoDisplay(display);
                }
                finally
                {
                    if (RestoreCursorVisibilityAfterDisplay)
                        display.IsCursorVisible = originalCursorVisibility;
                }
            }
            else
            {
                DoDisplay(display);
            }

            DisplayEventArgs afterDisplayArgs = new DisplayEventArgs(display);
            OnAfterDisplay(afterDisplayArgs);
        }

        /// <summary>
        /// When implemented by an inheritor, displays the margins and the content of the control.
        /// </summary>
        /// <param name="display"></param>
        protected abstract void DoDisplay(IDisplay display);

        /// <summary>
        /// Method called at the beginning of the <see cref="Display()"/> method, before doing anything else
        /// to raise the <see cref="BeforeDisplay"/> event.
        /// When overwritten, the base method must be called in order to allow the event to be raised.
        /// </summary>
        protected virtual void OnBeforeDisplay(DisplayEventArgs e)
        {
            BeforeDisplay?.Invoke(this, e);
        }

        /// <summary>
        /// Method called at the very end of the <see cref="Display()"/> method, before returning
        /// to raise the <see cref="AfterDisplay"/> event.
        /// When overwritten, the base method must be called in order to allow the event to be raised.
        /// </summary>
        protected virtual void OnAfterDisplay(DisplayEventArgs e)
        {
            AfterDisplay?.Invoke(this, e);
        }
    }
}