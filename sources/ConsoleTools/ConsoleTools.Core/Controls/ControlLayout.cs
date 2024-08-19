// ConsoleTools
// Copyright (C) 2017-2024 Dust in the Wind
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

namespace DustInTheWind.ConsoleTools.Controls;

/// <summary>
/// Calculates the space needed for each part of the control: margins, paddings, content size,
/// empty spaces, etc.
/// </summary>
public class ControlLayout
{
    private Size maxAllowedSize; // including margins and paddings
    private Size actualSize; // including margins and paddings

    private HorizontalAlignment calculatedHorizontalAlignment;

    /// <summary>
    /// Gets or sets the control for which the layout is calculated.
    /// </summary>
    public BlockControl Control { get; set; }

    /// <summary>
    /// Gets or sets the available width in which the control can be displayed.
    /// </summary>
    public int? AvailableWidth { get; set; }

    public int? AvailableHeight { get; set; }

    /// <summary>
    /// Gets or sets the horizontal alignment of the content.
    /// It is used to calculate the left and right empty space around the content.
    /// </summary>
    public HorizontalAlignment ContentHorizontalAlignment { get; set; }

    /// <summary>
    /// Gets or sets the width of the content when the parent control does not enforce any restrictions.
    /// </summary>
    public int? DesiredContentWidth { get; set; }

    public Thickness EmptySpace { get; set; }

    /// <summary>
    /// Gets the calculated margin.
    /// </summary>
    public Thickness Margin { get; private set; }

    /// <summary>
    /// Gets the calculated padding.
    /// </summary>
    public Thickness Padding { get; private set; }

    public Size ContentSize { get; set; }

    /// <summary>
    /// Gets the actual calculated width of the control including the left and right margins.
    /// </summary>
    /// <remarks>
    /// This value is equal to the available width if the control is stretched.
    /// </remarks>
    public int ActualFullWidth => ContentSize.Width + Margin.Left + Margin.Right + Padding.Left + Padding.Right;

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

    ///// <summary>
    ///// Gets the empty space at the left of the content.
    ///// </summary>
    //public int InnerEmptySpaceLeft { get; private set; }

    ///// <summary>
    ///// Gets the empty space at the right of the content.
    ///// </summary>
    //public int InnerEmptySpaceRight { get; private set; }

    ///// <summary>
    ///// Gets the empty space at the left of the control.
    ///// This may be greater than 0 when the control is centered on the screen or aligned to the right.
    ///// </summary>
    //public int OuterEmptySpaceLeft { get; private set; }

    ///// <summary>
    ///// Gets the empty space at the right of the control.
    ///// This may be greater than 0 when the control is centered on the screen or aligned to the left.
    ///// </summary>
    //public int OuterEmptySpaceRight { get; private set; }

    /// <summary>
    /// Calculates the position and dimensions of all the parts that must be displayed.
    /// </summary>
    public void Calculate()
    {
        maxAllowedSize = CalculateMaxAllowedSize();
        CalculateMargins();
        CalculatePaddings();
        calculatedHorizontalAlignment = ComputeHorizontalAlignment();
        CalculateActualWidths();
        //CalculateInnerEmptySpace();
        CalculateOuterEmptySpace();
    }

    /// <remarks>
    /// If the control has a MaxWidth restriction, than the maximum allowed width is not the whole
    /// available space.
    /// </remarks>>
    private Size CalculateMaxAllowedSize()
    {
        int width;

        if (Control.MaxWidth == null)
        {
            width = AvailableWidth ?? int.MaxValue;
        }
        else
        {
            width = AvailableWidth == null
                ? Control.MaxWidth.Value
                : Math.Min(AvailableWidth.Value, Control.MaxWidth.Value);
        }

        int height = AvailableHeight ?? int.MaxValue;

        return new Size(width, height);
    }

    private void CalculateMargins()
    {
        Size remainingAllowedSize = maxAllowedSize - actualSize;
        ControlActualMargins controlActualMargins = new(Control, remainingAllowedSize);
        Margin = controlActualMargins.Compute();
        actualSize += Margin;
    }

    private void CalculatePaddings()
    {
        Size remainingAllowedSize = maxAllowedSize - actualSize;
        ControlActualPaddings controlActualPaddings = new(Control, remainingAllowedSize);
        Padding = controlActualPaddings.Compute();
        actualSize += Padding;
    }

    private HorizontalAlignment ComputeHorizontalAlignment()
    {
        switch (Control.HorizontalAlignment)
        {
            case null:
            case HorizontalAlignment.Default:
            case HorizontalAlignment.Left:
                return HorizontalAlignment.Left;

            case HorizontalAlignment.Center:
                return HorizontalAlignment.Center;

            case HorizontalAlignment.Right:
                return HorizontalAlignment.Right;

            case HorizontalAlignment.Stretch:
                return HorizontalAlignment.Stretch;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void CalculateActualWidths()
    {
        Size remainingAllowedSize = maxAllowedSize - actualSize;

        if (remainingAllowedSize.Width <= 0)
        {
            ContentSize = Size.Empty;
            return;
        }

        if (calculatedHorizontalAlignment == HorizontalAlignment.Stretch && AvailableWidth.HasValue)
        {
            int width = maxAllowedSize.Width - Margin.Left - Padding.Left - Padding.Right - Margin.Right;
            int height = maxAllowedSize.Height - Margin.Top - Padding.Top - Padding.Bottom - Margin.Bottom;

            ContentSize = new Size(width, height);
        }
        else
        {
            int calculatedMaxFullWidth = AvailableWidth ?? DesiredContentWidth ?? 0;
            int calculatedMaxWidth = calculatedMaxFullWidth - Control.Margin.Left - Control.Margin.Right;
            int calculatedMaxContentWidth = calculatedMaxWidth - Control.Padding.Left - Control.Padding.Right;

            int? calculatedDesiredContentWidth = CalculateDesiredContentWidth();

            int width = calculatedDesiredContentWidth.HasValue
                ? Math.Min(calculatedDesiredContentWidth.Value, calculatedMaxContentWidth)
                : calculatedMaxContentWidth;

            int height = maxAllowedSize.Height - Margin.Top - Padding.Top - Padding.Bottom - Margin.Bottom;

            ContentSize = new Size(width, height);
        }



        // -----------------


        //// Calculate max widths.

        //int calculatedMaxFullWidth = AvailableWidth ?? DesiredContentWidth ?? 0;
        //int calculatedMaxWidth = calculatedMaxFullWidth - Control.Margin.Left - Control.Margin.Right;
        //int calculatedMaxContentWidth = calculatedMaxWidth - Control.Padding.Left - Control.Padding.Right;

        //// Calculate actual widths.

        //if (calculatedHorizontalAlignment == HorizontalAlignment.Stretch && AvailableWidth.HasValue)
        //{
        //    ActualFullWidth = calculatedMaxFullWidth;
        //    ActualWidth = calculatedMaxWidth;
        //    ActualClientWidth = calculatedMaxContentWidth;

        //    int? calculatedDesiredContentWidth = DesiredContentWidth ?? int.MaxValue;
        //    ActualContentWidth = Math.Min(calculatedDesiredContentWidth.Value, calculatedMaxContentWidth);
        //}
        //else
        //{
        //    int? calculatedDesiredContentWidth = CalculateDesiredContentWidth();

        //    ActualContentWidth = calculatedDesiredContentWidth.HasValue
        //        ? Math.Min(calculatedDesiredContentWidth.Value, calculatedMaxContentWidth)
        //        : calculatedMaxContentWidth;

        //    ActualClientWidth = ActualContentWidth;
        //    ActualWidth = ActualClientWidth + Control.Padding.Left + Control.Padding.Right;
        //    ActualFullWidth = ActualWidth + Control.Margin.Left + Control.Margin.Right;
        //}
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
            int clientMaxWidth = Control.MaxWidth.Value - Control.Padding.Left - Control.Padding.Right;

            return DesiredContentWidth == null
                ? clientMaxWidth
                : Math.Min(clientMaxWidth, DesiredContentWidth.Value);
        }
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

    //private void CalculateInnerEmptySpace()
    //{
    //    int innerEmptySpaceTotal = ActualClientWidth - ActualContentWidth;

    //    switch (ContentHorizontalAlignment)
    //    {
    //        case HorizontalAlignment.Default:
    //        case HorizontalAlignment.Left:
    //            InnerEmptySpaceLeft = 0;
    //            InnerEmptySpaceRight = innerEmptySpaceTotal;
    //            break;

    //        case HorizontalAlignment.Center:
    //            double emptySpaceHalf = (double)innerEmptySpaceTotal / 2;
    //            InnerEmptySpaceLeft = (int)Math.Floor(emptySpaceHalf);
    //            InnerEmptySpaceRight = (int)Math.Ceiling(emptySpaceHalf);
    //            break;

    //        case HorizontalAlignment.Right:
    //            InnerEmptySpaceLeft = innerEmptySpaceTotal;
    //            InnerEmptySpaceRight = 0;
    //            break;

    //        case HorizontalAlignment.Stretch:
    //            InnerEmptySpaceLeft = 0;
    //            InnerEmptySpaceRight = 0;
    //            break;

    //        default:
    //            throw new ArgumentOutOfRangeException();
    //    }
    //}

    private void CalculateOuterEmptySpace()
    {
        int outerEmptySpaceTotal = AvailableWidth.HasValue
            ? AvailableWidth.Value - actualSize.Width
            : 0;

        switch (calculatedHorizontalAlignment)
        {
            case HorizontalAlignment.Default:
            case HorizontalAlignment.Left:
                EmptySpace = new Thickness(0, 0, outerEmptySpaceTotal, 0);
                break;

            case HorizontalAlignment.Center:
                double emptySpaceHalf = (double)outerEmptySpaceTotal / 2;
                int left = (int)Math.Floor(emptySpaceHalf);
                int right = (int)Math.Ceiling(emptySpaceHalf);
                EmptySpace = new Thickness(left, 0, right, 0);
                break;

            case HorizontalAlignment.Right:
                EmptySpace = new Thickness(outerEmptySpaceTotal, 0, 0, 0);
                break;

            case HorizontalAlignment.Stretch:
                EmptySpace = new Thickness(0, 0, 0, 0);
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}