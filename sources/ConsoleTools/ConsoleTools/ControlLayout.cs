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
        private bool isCalculated;

        private Thickness margin;
        private Thickness padding;
        private int? width;
        private int? minWidth;
        private int? maxWidth;
        private HorizontalAlignment? horizontalAlignment;
        private int availableWidth;
        private HorizontalAlignment contentHorizontalAlignment;
        private int? desiredContentWidth;

        public Thickness Margin
        {
            get => margin;
            set
            {
                margin = value;
                isCalculated = false;
            }
        }

        public Thickness Padding
        {
            get => padding;
            set
            {
                padding = value;
                isCalculated = false;
            }
        }

        public int? Width
        {
            get => width;
            set
            {
                width = value;
                isCalculated = false;
            }
        }

        public int? MinWidth
        {
            get => minWidth;
            set
            {
                minWidth = value;
                isCalculated = false;
            }
        }

        public int? MaxWidth
        {
            get => maxWidth;
            set
            {
                maxWidth = value;
                isCalculated = false;
            }
        }

        public HorizontalAlignment? HorizontalAlignment
        {
            get => horizontalAlignment;
            set
            {
                horizontalAlignment = value;
                isCalculated = false;
            }
        }

        public int AvailableWidth
        {
            get => availableWidth;
            set
            {
                availableWidth = value;
                isCalculated = false;
            }
        }

        public HorizontalAlignment ContentHorizontalAlignment
        {
            get => contentHorizontalAlignment;
            set
            {
                contentHorizontalAlignment = value;
                isCalculated = false;
            }
        }

        public int? DesiredContentWidth
        {
            get => desiredContentWidth;
            set
            {
                desiredContentWidth = value;
                isCalculated = false;
            }
        }

        public int MarginLeft => Margin.Left;
        public int MarginRight => Margin.Right;
        public int MarginTop => Margin.Top;
        public int MarginBottom => Margin.Bottom;

        public int PaddingLeft => Padding.Left;
        public int PaddingRight => Padding.Right;
        public int PaddingTop => Padding.Top;
        public int PaddingBottom => Padding.Bottom;

        public int ActualFullWidth { get; private set; }

        public int ActualWidth { get; private set; }

        public int ActualClientWidth { get; private set; }

        public int ActualContentWidth { get; private set; }

        public int EmptySpaceLeft { get; private set; }

        public int EmptySpaceRight { get; private set; }

        public void Calculate()
        {
            HorizontalAlignment calculatedHorizontalAlignment = CalculateHorizontalAlignment();
            CalculateActualWidths(calculatedHorizontalAlignment);
            CalculateEmptySpace();

            isCalculated = true;
        }

        private HorizontalAlignment CalculateHorizontalAlignment()
        {
            if (Width == null && MinWidth == null && MaxWidth == null)
            {
                switch (HorizontalAlignment)
                {
                    case ConsoleTools.HorizontalAlignment.Default:
                        return ConsoleTools.HorizontalAlignment.Left;

                    case ConsoleTools.HorizontalAlignment.Left:
                    case ConsoleTools.HorizontalAlignment.Center:
                    case ConsoleTools.HorizontalAlignment.Right:
                    case ConsoleTools.HorizontalAlignment.Stretch:
                        return HorizontalAlignment.Value;

                    case null:
                        return ConsoleTools.HorizontalAlignment.Stretch;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                switch (HorizontalAlignment)
                {
                    case ConsoleTools.HorizontalAlignment.Default:
                    case ConsoleTools.HorizontalAlignment.Left:
                        return ConsoleTools.HorizontalAlignment.Left;

                    case ConsoleTools.HorizontalAlignment.Center:
                        return ConsoleTools.HorizontalAlignment.Center;

                    case ConsoleTools.HorizontalAlignment.Right:
                        return ConsoleTools.HorizontalAlignment.Right;

                    case ConsoleTools.HorizontalAlignment.Stretch:
                        return ConsoleTools.HorizontalAlignment.Left;

                    case null:
                        return ConsoleTools.HorizontalAlignment.Left;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void CalculateActualWidths(HorizontalAlignment calculatedHorizontalAlignment)
        {
            // Calculate max widths.

            int calculatedMaxFullWidth = AvailableWidth;
            int calculatedMaxWidth = calculatedMaxFullWidth - Margin.Left - Margin.Right;
            int calculatedMaxClientWidth = calculatedMaxWidth - Padding.Left - padding.Right;

            // Calculate actual widths.

            if (calculatedHorizontalAlignment == ConsoleTools.HorizontalAlignment.Stretch)
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
                ActualWidth = ActualClientWidth + Padding.Left + Padding.Right;
                ActualFullWidth = ActualWidth + Margin.Left + Margin.Right;
            }
        }

        private int? CalculateDesiredContentWidth()
        {
            if (Width.HasValue)
                return Width.Value;

            if (MinWidth == null)
            {
                if (MaxWidth == null)
                {
                    return DesiredContentWidth;
                }
                else
                {
                    int clientMaxWidth = MaxWidth.Value - Padding.Left - Padding.Right;

                    return DesiredContentWidth == null
                        ? clientMaxWidth
                        : Math.Min(clientMaxWidth, DesiredContentWidth.Value);
                }
            }
            else
            {
                if (MaxWidth == null)
                {
                    int clientMinWidth = MinWidth.Value - Padding.Left - Padding.Right;

                    return DesiredContentWidth == null
                        ? clientMinWidth
                        : Math.Max(clientMinWidth, DesiredContentWidth.Value);
                }
                else
                {
                    int clientMinWidth = MinWidth.Value - Padding.Left - Padding.Right;
                    int clientMaxWidth = MaxWidth.Value - Padding.Left - Padding.Right;

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
                case ConsoleTools.HorizontalAlignment.Default:
                case ConsoleTools.HorizontalAlignment.Left:
                    EmptySpaceLeft = 0;
                    EmptySpaceRight = emptySpaceTotal;
                    break;

                case ConsoleTools.HorizontalAlignment.Center:
                    double emptySpaceHalf = (double)emptySpaceTotal / 2;
                    EmptySpaceLeft = (int)Math.Floor(emptySpaceHalf);
                    EmptySpaceRight = (int)Math.Ceiling(emptySpaceHalf);
                    break;

                case ConsoleTools.HorizontalAlignment.Right:
                    EmptySpaceLeft = emptySpaceTotal;
                    EmptySpaceRight = 0;
                    break;

                case ConsoleTools.HorizontalAlignment.Stretch:
                    EmptySpaceLeft = 0;
                    EmptySpaceRight = 0;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}