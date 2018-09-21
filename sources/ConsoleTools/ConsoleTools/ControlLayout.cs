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
    internal class ControlLayout
    {
        public BlockControl Control { get; set; }

        public int AvailableWidth { get; set; }

        public HorizontalAlignment ContentHorizontalAlignment { get; set; }

        public int? DesiredContentWidth { get; set; }

        public int MarginLeft { get; private set; }
        public int MarginRight { get; private set; }
        public int MarginTop { get; private set; }
        public int MarginBottom { get; private set; }

        public int PaddingLeft { get; private set; }
        public int PaddingRight { get; private set; }
        public int PaddingTop { get; private set; }
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
        /// Gets the actual calculated width of the content.
        /// </summary>
        public int ActualContentWidth { get; private set; }

        public int EmptySpaceLeft { get; private set; }

        public int EmptySpaceRight { get; private set; }

        public void Calculate()
        {
            CalculateMargins();
            CalculatePaddings();
            HorizontalAlignment calculatedHorizontalAlignment = CalculateHorizontalAlignment();
            CalculateActualWidths(calculatedHorizontalAlignment);
            CalculateEmptySpace();
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
            if (Control.Width == null && Control.MinWidth == null && Control.MaxWidth == null)
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
                        return ConsoleTools.HorizontalAlignment.Stretch;

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

        private void CalculateActualWidths(HorizontalAlignment calculatedHorizontalAlignment)
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

                int? calculatedDesiredContentWidth = DesiredContentWidth;

                ActualContentWidth = calculatedDesiredContentWidth.HasValue
                    ? Math.Min(calculatedDesiredContentWidth.Value, calculatedMaxClientWidth)
                    : calculatedMaxClientWidth;
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
            if (Control.Width.HasValue)
                return Control.Width.Value;

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

        private void CalculateEmptySpace()
        {
            int emptySpaceTotal = ActualClientWidth - ActualContentWidth;

            switch (ContentHorizontalAlignment)
            {
                case HorizontalAlignment.Default:
                case HorizontalAlignment.Left:
                    EmptySpaceLeft = 0;
                    EmptySpaceRight = emptySpaceTotal;
                    break;

                case HorizontalAlignment.Center:
                    double emptySpaceHalf = (double)emptySpaceTotal / 2;
                    EmptySpaceLeft = (int)Math.Floor(emptySpaceHalf);
                    EmptySpaceRight = (int)Math.Ceiling(emptySpaceHalf);
                    break;

                case HorizontalAlignment.Right:
                    EmptySpaceLeft = emptySpaceTotal;
                    EmptySpaceRight = 0;
                    break;

                case HorizontalAlignment.Stretch:
                    EmptySpaceLeft = 0;
                    EmptySpaceRight = 0;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}