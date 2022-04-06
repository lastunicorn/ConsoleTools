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
        /// Displays the margins and the content of the control.
        /// It also ensures that the control is displayed starting from a new line.
        /// </summary>
        /// <param name="display"></param>
        protected override void DoDisplay(IDisplay display)
        {
            MoveToNextLineIfNecessary(display);

            WriteTopMargin(display);
            WriteTopPadding(display);
            
            DoDisplayContent(display);

            WriteBottomPadding(display);
            WriteBottomMargin(display);
        }

        private void MoveToNextLineIfNecessary(IDisplay display)
        {
            if (!display.IsBeginOfLine)
                display.WriteNewLine();
        }
        
        /// <summary>
        /// When implemented by an inheritor, it displays the content of the control to the specified <see cref="IDisplay"/> instance.
        /// </summary>
        protected abstract void DoDisplayContent(IDisplay display);
    }
}