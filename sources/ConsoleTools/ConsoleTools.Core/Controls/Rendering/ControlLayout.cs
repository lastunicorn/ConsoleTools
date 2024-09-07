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

namespace DustInTheWind.ConsoleTools.Controls.Rendering;

/// <summary>
/// Calculates the space needed for each part of the control: margins, paddings, content size,
/// empty spaces, etc.
/// </summary>
public class ControlLayout
{
    /// <summary>
    /// It includes margins and paddings.
    /// </summary>
    private Size maxAllowedSize;

    /// <summary>
    /// It includes margins and paddings.
    /// </summary>
    private Size actualSize;

    private HorizontalAlignment actualHorizontalAlignment;

    /// <summary>
    /// Gets or sets the control for which the layout is calculated.
    /// </summary>
    public BlockControl Control { get; set; }

    /// <summary>
    /// Gets or sets the width allocated for the control being rendered.
    /// If this value is provided, all this space must be filled exactly by the control, no more,
    /// no less.
    /// If the control is smaller than the allocated width, the remaining space must be filled
    /// with empty spaces.
    /// </summary>
    public int? AllocatedWidth { get; set; }

    /// <summary>
    /// Gets or sets the height allocated for the control being rendered.
    /// If this value is provided, all this space must be filled exactly by the control, no more,
    /// no less.
    /// If the control is smaller than the allocated height, the remaining space must be filled
    /// with empty spaces.
    /// </summary>
    public int? AllocatedHeight { get; set; }

    /// <summary>
    /// Gets the the empty space, outside the calculated margins, that must be rendered around the
    /// control.
    /// </summary>
    public Thickness EmptySpace { get; private set; }

    /// <summary>
    /// Gets the calculated margins that must be rendered around the control.
    /// </summary>
    public Thickness Margin { get; private set; }

    /// <summary>
    /// Gets the calculated padding that must be rendered around the control.
    /// </summary>
    public Thickness Padding { get; private set; }

    /// <summary>
    /// Gets the calculated content size.
    /// </summary>
    public Size ContentSize { get; private set; }

    /// <summary>
    /// Gets the actual calculated width of the control including the left and right margins.
    /// </summary>
    /// 
    /// <remarks>
    /// This value is equal to the available width if the control is stretched.
    /// </remarks>
    public int ActualFullWidth => ContentSize.Width + Padding.Left + Padding.Right + Margin.Left + Margin.Right + EmptySpace.Left + EmptySpace.Right;

    /// <summary>
    /// Gets the actual calculated width of the control without the left and right margins.
    /// </summary>
    public int ActualWidth => ContentSize.Width + Padding.Left + Padding.Right;

    /// <summary>
    /// Gets the calculated width of the content.
    /// </summary>
    public int ActualContentWidth => ContentSize.Width;

    /// <summary>
    /// Calculates the position and dimensions of all the parts that must be displayed.
    /// </summary>
    public void Calculate()
    {
        maxAllowedSize = CalculateMaxAllowedSize();
        CalculateMargins();
        CalculatePaddings();
        actualHorizontalAlignment = ComputeHorizontalAlignment();
        CalculateContentSize();
        CalculateOuterEmptySpace();
    }

    private Size CalculateMaxAllowedSize()
    {
        int width;

        if (Control.MaxWidth == null)
        {
            width = AllocatedWidth ?? int.MaxValue;
        }
        else
        {
            width = AllocatedWidth == null
                ? Control.MaxWidth.Value
                : Math.Min(AllocatedWidth.Value, Control.MaxWidth.Value);
        }

        int height = AllocatedHeight ?? int.MaxValue;

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

    private void CalculateContentSize()
    {
        Size remainingAllowedSize = maxAllowedSize - actualSize;

        if (remainingAllowedSize.Width <= 0)
        {
            ContentSize = Size.Empty;
            return;
        }

        if (actualHorizontalAlignment == HorizontalAlignment.Stretch && AllocatedWidth.HasValue)
        {
            int contentWidth = maxAllowedSize.Width - Margin.Left - Padding.Left - Padding.Right - Margin.Right;
            int contentHeight = maxAllowedSize.Height - Margin.Top - Padding.Top - Padding.Bottom - Margin.Bottom;

            ContentSize = new Size(contentWidth, contentHeight);
        }
        else
        {
            int contentWidth = Control.CalculateNaturalWidth(false, false);

            if (AllocatedWidth != null)
            {
                int allocatedContentWidth = AllocatedWidth.Value - Control.Padding.Left - Control.Padding.Right - Control.Margin.Left - Control.Margin.Right;

                if (allocatedContentWidth < contentWidth)
                    contentWidth = allocatedContentWidth;
            }

            int contentHeight = maxAllowedSize.Height - Margin.Top - Padding.Top - Padding.Bottom - Margin.Bottom;

            ContentSize = new Size(contentWidth, contentHeight);
        }

        actualSize += ContentSize;
    }

    private void CalculateOuterEmptySpace()
    {
        int outerEmptySpaceTotal = AllocatedWidth.HasValue
            ? AllocatedWidth.Value - actualSize.Width
            : 0;

        switch (actualHorizontalAlignment)
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