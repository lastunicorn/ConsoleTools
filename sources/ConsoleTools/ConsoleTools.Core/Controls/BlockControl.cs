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
    /// Provides base functionality for a block control like top and bottom margins, paddings, etc.
    /// A block control does not accept other controls on the same horizontal space.
    /// It also force the rendering to start from the beginning of the next line if the cursor is
    /// in the middle of a line.
    /// </summary>
    public abstract partial class BlockControl : Control
    {
        /// <summary>
        /// Gets an instance that represents the display available for the control to write on.
        /// It also provides helper methods to write partial or entire rows.
        /// </summary>
        protected ControlDisplay ControlDisplay { get; private set; }

        /// <summary>
        /// Gets the calculated layout for the current instance.
        /// This value is calculated at the beginning of the display process and it is available throughout
        /// the entire display process.
        /// Before and after the display has unknown value.
        /// </summary>
        protected ControlLayout Layout { get; private set; }

        /// <summary>
        /// Gets or sets a value that specifies who should be considered the parent if none is specified.
        /// This is useful when calculating the alignment.
        /// Default value: ConsoleWindow
        /// </summary>
        public DefaultParent DefaultParent { get; set; } = DefaultParent.ConsoleWindow;

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
        /// Displays the margins and the content of the control.
        /// It also ensures that the control is displayed starting from a new line.
        /// </summary>
        protected override void DoDisplay()
        {
            MoveToNextLineIfNecessary();

            CalculateLayout();
            CreateControlDisplay();

            WriteTopMargin();
            WriteTopPadding();

            DoDisplayContent(ControlDisplay);

            WriteBottomPadding();
            WriteBottomMargin();
        }

        private void CreateControlDisplay()
        {
            ControlDisplay = new ControlDisplay
            {
                Layout = Layout,
                ForegroundColor = ForegroundColor,
                BackgroundColor = BackgroundColor
            };
        }

        private static void MoveToNextLineIfNecessary()
        {
            if (Console.CursorLeft != 0)
                Console.WriteLine();
        }

        private void CalculateLayout()
        {
            Layout = new ControlLayout
            {
                Control = this,
                AvailableWidth = AvailableWidth,
                DesiredContentWidth = DesiredContentWidth
            };

            Layout.Calculate();
        }

        /// <summary>
        /// When implemented by an inheritor, it displays the content of the control to the console.
        /// </summary>
        protected abstract void DoDisplayContent(ControlDisplay display);
    }
}