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
    partial class BlockControl
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
        /// Gets the width available for the control to render itself.
        /// </summary>
        /// <remarks>
        /// The parent's control is deciding this space.
        /// </remarks>
        protected int AvailableWidth
        {
            get
            {
                switch (DefaultParent)
                {
                    case DefaultParent.ConsoleBuffer:
                        return Console.BufferWidth;

                    case DefaultParent.ConsoleWindow:
                        return Console.WindowWidth;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        ///// <summary>
        ///// Gets the actual width of the content.
        ///// </summary>
        //protected int ActualContentWidth => Layout.ActualContentWidth;

        ///// <summary>
        ///// When implemented by an inheritor, gets the actual height of the content calculated
        ///// taking in account the <see cref="ActualContentWidth"/>.
        ///// </summary>
        //protected virtual int ActualContentHeight { get; }

        //public int ActualHeight => ActualContentHeight + Padding.Top + Padding.Bottom;

        //public int ActualFullHeight => ActualContentHeight + Margin.Top + Margin.Bottom;

        /// <summary>
        /// When implemented by an inheritor, gets the width of the content when there are no other restrictions applied to the control.
        /// If the value is not provided, <see cref="int.MaxValue"/> is assumed.
        /// </summary>
        protected virtual int DesiredContentWidth { get; }
    }
}