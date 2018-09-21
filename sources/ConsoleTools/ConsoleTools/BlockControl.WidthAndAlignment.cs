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
    partial class BlockControl
    {
        /// <summary>
        /// Gets or sets the width of the control. It does not including the left and right margins.
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

        /// <summary>
        /// Gets the actual width of the content.
        /// </summary>
        protected int ActualContentWidth => controlLayout.ActualContentWidth;

        /// <summary>
        /// When implemented by an inheritor, gets the actual height of the content calculated
        /// taking in account the <see cref="ActualContentWidth"/>.
        /// </summary>
        protected virtual int ActualContentHeight { get; }

        public int ActualHeight => ActualContentHeight + Padding.Top + Padding.Bottom;

        public int ActualFullHeight => ActualContentHeight + Margin.Top + Margin.Bottom;

        /// <summary>
        /// Gets the desired width of the control calculated by honoring the
        /// <see cref="Width"/>, <see cref="MinWidth"/> and <see cref="MaxWidth"/>.
        /// </summary>
        protected int CalculatedContentWidth
        {
            get
            {
                if (Width == null)
                {
                    if (MinWidth == null)
                    {
                        if (MaxWidth == null)
                        {
                            return DesiredContentWidth;
                        }
                        else
                        {
                            int contentMaxWidth = MaxWidth.Value - Padding.Left - Padding.Right;
                            return Math.Min(DesiredContentWidth, contentMaxWidth);
                        }
                    }
                    else
                    {
                        if (MaxWidth == null)
                        {
                            int contentMinWidth = MinWidth.Value - Padding.Left - Padding.Right;
                            return Math.Max(DesiredContentWidth, contentMinWidth);
                        }
                        else
                        {
                            int contentMinWidth = MinWidth.Value - Padding.Left - Padding.Right;
                            int contentMaxWidth = MaxWidth.Value - Padding.Left - Padding.Right;
                            return Math.Max(Math.Min(DesiredContentWidth, contentMaxWidth), contentMinWidth);
                        }
                    }
                }
                else
                {
                    if (MinWidth == null)
                    {
                        if (MaxWidth == null)
                        {
                            return Width.Value;
                        }
                        else
                        {
                            int contentMaxWidth = MaxWidth.Value - Padding.Left - Padding.Right;
                            return Math.Min(Width.Value, contentMaxWidth);
                        }
                    }
                    else
                    {
                        if (MaxWidth == null)
                        {
                            int contentMinWidth = MinWidth.Value - Padding.Left - Padding.Right;
                            return Math.Max(Width.Value, contentMinWidth);
                        }
                        else
                        {
                            int contentMinWidth = MinWidth.Value - Padding.Left - Padding.Right;
                            int contentMaxWidth = MaxWidth.Value - Padding.Left - Padding.Right;
                            return Math.Max(Math.Min(Width.Value, contentMaxWidth), contentMinWidth);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// When implemented by an inheritor, gets the width of the content when no restrictions are applied of any kind.
        /// Not event the Width, MinWidth and MaxWidth are honored.
        /// </summary>
        protected virtual int DesiredContentWidth { get; }
    }
}