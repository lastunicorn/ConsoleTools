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
    /// Calculates the space needed for each component of the control: each margin, padding, content dimensions, empty spaces, etc.
    /// </summary>
    public class ControlLayout
    {
        private HorizontalAlignment calculatedHorizontalAlignment;

        /// <summary>
        /// Gets or sets the control for which the layout is calculated.
        /// </summary>
        public BlockControl Control { get; set; }

        /// <summary>
        /// Gets or sets the available width in which the control can be displayed.
        /// </summary>
        public int AvailableWidth { get; set; }

        /// <summary>
        /// Gets or sets the horizontal alignment of the content.
        /// It is used to calculate the left and right empty space around the content.
        /// </summary>
        public HorizontalAlignment ContentHorizontalAlignment { get; set; }

        /// <summary>
        /// Gets or sets the width of the content when the parent control does not enforce any restrictions.
        /// </summary>
        public int? DesiredContentWidth { get; set; }

        /// <summary>
        /// Gets the calculated thickness of the left margin.
        /// </summary>
        public int MarginLeft { get; private set; }

        /// <summary>
        /// Gets the calculated thickness of the right margin.
        /// </summary>
        public int MarginRight { get; private set; }

        /// <summary>
        /// Gets the calculated thickness of the top margin.
        /// </summary>
        public int MarginTop { get; private set; }

        /// <summary>
        /// Gets the calculated thickness of the bottom margin.
        /// </summary>
        public int MarginBottom { get; private set; }

        /// <summary>
        /// Gets the calculated thickness of the left padding.
        /// </summary>
        public int PaddingLeft { get; private set; }

        /// <summary>
        /// Gets the calculated thickness of the right padding.
        /// </summary>
        public int PaddingRight { get; private set; }

        /// <summary>
        /// Gets the calculated thickness of the top padding.
        /// </summary>
        public int PaddingTop { get; private set; }

        /// <summary>
        /// Gets the calculated thickness of the bottom padding.
        /// </summary>
        public int PaddingBottom { get; private set; }

        /// <summary>
        /// Gets the actual calculated width of the control including the left and right margins.
        /// </summary>
        /// <remarks>
        /// This value is equal to the available width if the control is stretched.
        /// </remarks>
        public int ActualFullWidth { get; private set; }

        /// <summary>
        /// Gets the actual calculated width of the control without the left and right margins.
        /// </summary>
        public int ActualWidth { get; private set; }

        /// <summary>
        /// Gets the actual calculated width of the client area (without the left and right paddings).
        /// </summary>
        public int ActualClientWidth { get; private set; }

        /// <summary>
        /// Gets the calculated width of the content.
        /// </summary>
        public int ActualContentWidth { get; private set; }

        /// <summary>
        /// Gets the empty space at the left of the content.
        /// </summary>
        public int InnerEmptySpaceLeft { get; private set; }

        /// <summary>
        /// Gets the empty space at the right of the content.
        /// </summary>
        public int InnerEmptySpaceRight { get; private set; }

        /// <summary>
        /// Gets the empty space at the left of the control.
        /// This may be greater than 0 when the control is centered on the screen or aligned to the right.
        /// </summary>
        public int OuterEmptySpaceLeft { get; private set; }

        /// <summary>
        /// Gets the empty space at the right of the control.
        /// This may be greater than 0 when the control is centered on the screen or aligned to the left.
        /// </summary>
        public int OuterEmptySpaceRight { get; private set; }

        /// <summary>
        /// Calculates the position and dimensions of all the parts that must be displayed.
        /// </summary>
        public void Calculate()
        {
            CalculateMargins();
            CalculatePaddings();
            calculatedHorizontalAlignment = CalculateHorizontalAlignment();
            CalculateActualWidths();
            CalculateInnerEmptySpace();
            CalculateOuterEmptySpace();
        }

        private void CalculateMargins()
        {
            MarginLeft = Control.Margin.Left;
            MarginTop = Control.Margin.Top;
            MarginRight = Control.Margin.Right;
            MarginBottom = Control.Margin.Bottom;
        }

        private void CalculatePaddings()
        {
            PaddingLeft = Control.Padding.Left;
            PaddingTop = Control.Padding.Top;
            PaddingRight = Control.Padding.Right;
            PaddingBottom = Control.Padding.Bottom;
        }

        private HorizontalAlignment CalculateHorizontalAlignment()
        {
            bool widthIsProvided = Control.Width != null || Control.MinWidth != null || Control.MaxWidth != null;

            if (!widthIsProvided)
            {
                switch (Control.HorizontalAlignment)
                {
                    case HorizontalAlignment.Default:
                        return HorizontalAlignment.Left;

                    case HorizontalAlignment.Left:
                    case HorizontalAlignment.Center:
                    case HorizontalAlignment.Right:
                    case HorizontalAlignment.Stretch:
                        return Control.HorizontalAlignment.Value;

                    case null:
                        return HorizontalAlignment.Stretch;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                switch (Control.HorizontalAlignment)
                {
                    case HorizontalAlignment.Default:
                    case HorizontalAlignment.Left:
                        return HorizontalAlignment.Left;

                    case HorizontalAlignment.Center:
                        return HorizontalAlignment.Center;

                    case HorizontalAlignment.Right:
                        return HorizontalAlignment.Right;

                    case HorizontalAlignment.Stretch:
                        return HorizontalAlignment.Left;

                    case null:
                        return HorizontalAlignment.Left;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void CalculateActualWidths()
        {
            // Calculate max widths.

            int calculatedMaxFullWidth = AvailableWidth;
            int calculatedMaxWidth = calculatedMaxFullWidth - Control.Margin.Left - Control.Margin.Right;
            int calculatedMaxClientWidth = calculatedMaxWidth - Control.Padding.Left - Control.Padding.Right;

            // Calculate actual widths.

            if (calculatedHorizontalAlignment == HorizontalAlignment.Stretch)
            {
                ActualFullWidth = calculatedMaxFullWidth;
                ActualWidth = calculatedMaxWidth;
                ActualClientWidth = calculatedMaxClientWidth;

                int? calculatedDesiredContentWidth = DesiredContentWidth ?? int.MaxValue;
                ActualContentWidth = Math.Min(calculatedDesiredContentWidth.Value, calculatedMaxClientWidth);
            }
            else
            {
                int? calculatedDesiredContentWidth = CalculateDesiredContentWidth();

                ActualContentWidth = calculatedDesiredContentWidth.HasValue
                    ? Math.Min(calculatedDesiredContentWidth.Value, calculatedMaxClientWidth)
                    : calculatedMaxClientWidth;

                ActualClientWidth = ActualContentWidth;
                ActualWidth = ActualClientWidth + Control.Padding.Left + Control.Padding.Right;
                ActualFullWidth = ActualWidth + Control.Margin.Left + Control.Margin.Right;
            }
        }

        private int? CalculateDesiredContentWidth()
        {
            if (Control.Width != null)
                return Control.Width.Value - Control.Padding.Left - Control.Padding.Right;

            if (Control.MinWidth == null)
            {
                if (Control.MaxWidth == null)
                {
                    return DesiredContentWidth;
                }
                else
                {
                    int clientMaxWidth = Control.MaxWidth.Value - Control.Padding.Left - Control.Padding.Right;

                    return DesiredContentWidth == null
                        ? clientMaxWidth
                        : Math.Min(clientMaxWidth, DesiredContentWidth.Value);
                }
            }
            else
            {
                if (Control.MaxWidth == null)
                {
                    int clientMinWidth = Control.MinWidth.Value - Control.Padding.Left - Control.Padding.Right;

                    return DesiredContentWidth == null
                        ? clientMinWidth
                        : Math.Max(clientMinWidth, DesiredContentWidth.Value);
                }
                else
                {
                    int clientMinWidth = Control.MinWidth.Value - Control.Padding.Left - Control.Padding.Right;
                    int clientMaxWidth = Control.MaxWidth.Value - Control.Padding.Left - Control.Padding.Right;

                    return DesiredContentWidth == null
                        ? clientMinWidth
                        : Math.Min(Math.Max(clientMinWidth, DesiredContentWidth.Value), clientMaxWidth);
                }
            }
        }

        private void CalculateInnerEmptySpace()
        {
            int innerEmptySpaceTotal = ActualClientWidth - ActualContentWidth;

            switch (ContentHorizontalAlignment)
            {
                case HorizontalAlignment.Default:
                case HorizontalAlignment.Left:
                    InnerEmptySpaceLeft = 0;
                    InnerEmptySpaceRight = innerEmptySpaceTotal;
                    break;

                case HorizontalAlignment.Center:
                    double emptySpaceHalf = (double)innerEmptySpaceTotal / 2;
                    InnerEmptySpaceLeft = (int)Math.Floor(emptySpaceHalf);
                    InnerEmptySpaceRight = (int)Math.Ceiling(emptySpaceHalf);
                    break;

                case HorizontalAlignment.Right:
                    InnerEmptySpaceLeft = innerEmptySpaceTotal;
                    InnerEmptySpaceRight = 0;
                    break;

                case HorizontalAlignment.Stretch:
                    InnerEmptySpaceLeft = 0;
                    InnerEmptySpaceRight = 0;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void CalculateOuterEmptySpace()
        {
            int outerEmptySpaceTotal = AvailableWidth - ActualFullWidth;

            switch (calculatedHorizontalAlignment)
            {
                case HorizontalAlignment.Default:
                case HorizontalAlignment.Left:
                    OuterEmptySpaceLeft = 0;
                    OuterEmptySpaceRight = outerEmptySpaceTotal;
                    break;

                case HorizontalAlignment.Center:
                    double emptySpaceHalf = (double)outerEmptySpaceTotal / 2;
                    OuterEmptySpaceLeft = (int)Math.Floor(emptySpaceHalf);
                    OuterEmptySpaceRight = (int)Math.Ceiling(emptySpaceHalf);
                    break;

                case HorizontalAlignment.Right:
                    OuterEmptySpaceLeft = outerEmptySpaceTotal;
                    OuterEmptySpaceRight = 0;
                    break;

                case HorizontalAlignment.Stretch:
                    OuterEmptySpaceLeft = 0;
                    OuterEmptySpaceRight = 0;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}