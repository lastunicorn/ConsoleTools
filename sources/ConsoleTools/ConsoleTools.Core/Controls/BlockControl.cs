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
    /// Provides base functionality for a block control like top and bottom margins, paddings, etc.
    /// A block control does not accept other controls on the same horizontal space.
    /// It also force the rendering to start from the beginning of the next line if the cursor is
    /// in the middle of a line.
    /// </summary>
    public abstract partial class BlockControl : Control
    {
        /// <summary>
        /// Gets or sets the width of the control. The margins are not included.
        /// </summary>
        public int? Width { get; set; }

        /// <summary>
        /// Gets or sets the minimum width allowed for the control.
        /// </summary>
        public int? MinWidth { get; set; }

        /// <summary>
        /// Gets or sets the maximum width allowed for the control.
        /// </summary>
        public int? MaxWidth { get; set; }

        /// <summary>
        /// Gets or sets a value that specifies the horizontal position of the control in respect to its parent container.
        /// </summary>
        public HorizontalAlignment? HorizontalAlignment { get; set; }

        /// <summary>
        /// Gets or sets the amount of space that should be empty outside the control.
        /// </summary>
        public Thickness Margin { get; set; }

        /// <summary>
        /// Gets or sets the amount of space between the content and the margin of the control.
        /// </summary>
        public Thickness Padding { get; set; }

        /// <summary>
        /// Event raised immediately before writing the top margin.
        /// </summary>
        public event EventHandler BeforeTopMargin;

        /// <summary>
        /// Event raised immediately after writing the bottom margin.
        /// </summary>
        public event EventHandler AfterBottomMargin;

        /// <summary>
        /// Displays the margins and the content of the control.
        /// It also ensures that the control is displayed starting from a new line.
        /// </summary>
        /// <param name="display"></param>
        protected override void DoDisplay(IDisplay display)
        {
            OnBeforeTopMargin();

            IControlRenderer controlRenderer = GetRenderer(display);

            if (controlRenderer != null)
            {
                while (controlRenderer.HasMoreRows)
                {
                    bool allowNewLine = AllowNewLine(controlRenderer);

                    controlRenderer.RenderNextRow();

                    if (allowNewLine)
                        display.WriteNewLine();
                }
            }

            OnAfterBottomMargin();
        }

        private static bool AllowNewLine(IControlRenderer controlRenderer)
        {
            if (controlRenderer is Renderer legacyControlRenderer)
                return legacyControlRenderer.RenderingState != ControlRenderingState.Content;

            return true;
        }

        /// <summary>
        /// When implemented by an inheritor, it displays the content of the control to the specified <see cref="IDisplay"/> instance.
        /// </summary>
        protected virtual void DoDisplayContent(IDisplay display)
        {
        }

        public virtual IControlRenderer GetRenderer(IDisplay display)
        {
            return new Renderer(this, display);
        }

        /// <summary>
        /// Method called immediately before writing the top margin.
        /// </summary>
        protected virtual void OnBeforeTopMargin()
        {
            BeforeTopMargin?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Method called immediately after writing the bottom margin.
        /// </summary>
        protected virtual void OnAfterBottomMargin()
        {
            AfterBottomMargin?.Invoke(this, EventArgs.Empty);
        }
    }
}