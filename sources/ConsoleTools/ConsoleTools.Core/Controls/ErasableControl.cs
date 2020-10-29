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
    /// Provides functionality of erasing the control after it is displayed.
    /// It is sometime useful for the controls that wait for an user input
    /// and then must get themselves out of the way.
    /// </summary>
    /// <remarks>
    /// In order to be able to successfully erase the control, the inheritor must
    /// calculate and set the <see cref="InnerSize"/> of the control until the end
    /// of the <see cref="BlockControl.DoDisplayContent"/> method.
    /// </remarks>
    public abstract class ErasableControl : BlockControl
    {
        /// <summary>
        /// Gets the size of the control after it was displayed.
        /// Does not include the margins
        /// </summary>
        public Size InnerSize { get; protected set; }

        /// <summary>
        /// Gets or sets a value that specifies if the control is erased from the Console
        /// after it was displayed.
        /// </summary>
        public bool EraseAfterClose { get; set; }

        /// <summary>
        /// When implemented by an inheritor it displays the content of the control to the console.
        /// The inheritor must also calculate and set the <see cref="InnerSize"/> property.
        /// </summary>
        protected abstract override void DoDisplayContent(ControlDisplay display);

        /// <summary>
        /// Method called at the very end, after all the control was displayed.
        /// It Erases the control if requested.
        /// </summary>
        protected override void OnAfterDisplay()
        {
            if (EraseAfterClose && ControlDisplay.RowCount > 0)
                EraseControl();

            base.OnAfterDisplay();
        }

        private void EraseControl()
        {
            string emptyLine = new string(' ', Console.BufferWidth);

            int outerHeight = Margin.Top + ControlDisplay.RowCount + Margin.Bottom;

            Console.SetCursorPosition(0, Console.CursorTop - outerHeight);

            for (int i = 0; i < outerHeight; i++)
                Console.Write(emptyLine);

            Console.SetCursorPosition(0, Console.CursorTop - outerHeight);
        }
    }
}