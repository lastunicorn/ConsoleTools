//// ConsoleTools
//// Copyright (C) 2017-2024 Dust in the Wind
//// 
//// This program is free software: you can redistribute it and/or modify
//// it under the terms of the GNU General Public License as published by
//// the Free Software Foundation, either version 3 of the License, or
//// (at your option) any later version.
//// 
//// This program is distributed in the hope that it will be useful,
//// but WITHOUT ANY WARRANTY; without even the implied warranty of
//// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//// GNU General Public License for more details.
//// 
//// You should have received a copy of the GNU General Public License
//// along with this program.  If not, see <http://www.gnu.org/licenses/>.

//using System;

//namespace DustInTheWind.ConsoleTools.Controls;

//internal class ControlLayoutBuilder
//{
//    private readonly Control control;
//    private readonly Size availableSpace;

//    private ControlLayout2 controlLayout;
//    private Size maxAllowedSize; // including margins and paddings
//    private Size actualSpace; // including margins and paddings
//    private HorizontalAlignment calculatedHorizontalAlignment;

//    public ControlLayoutBuilder(Control control, Size availableSpace)
//    {
//        this.control = control ?? throw new ArgumentNullException(nameof(control));
//        this.availableSpace = availableSpace;
//    }

//    public ControlLayout2 Build()
//    {
//        controlLayout = new ControlLayout2();

//        maxAllowedSize = CalculateMaxAllowedWidth();
//        CalculateMargins();
//        CalculatePaddings();
//        calculatedHorizontalAlignment = ComputeHorizontalAlignment();
//        CalculateContentSize();

//        return controlLayout;
//    }

//    private Size CalculateMaxAllowedWidth()
//    {
//        switch (control)
//        {
//            case BlockControl blockControl:
//                int width = blockControl.MaxWidth == null
//                    ? availableSpace.Width
//                    : Math.Min(availableSpace.Width, blockControl.MaxWidth.Value);
//                return new Size(width, availableSpace.Height);

//            case InlineControl:
//                return availableSpace;

//            default:
//                return availableSpace;
//        }
//    }

//    private void CalculateMargins()
//    {
//        Size remainingAllowedSpace = maxAllowedSize - actualSpace;
//        ControlActualMargins controlActualMargins = new(control, remainingAllowedSpace);
//        controlLayout.Margin = controlActualMargins.Compute();
//        actualSpace += controlLayout.Margin;
//    }

//    private void CalculatePaddings()
//    {
//        Size remainingAllowedSpace = maxAllowedSize - actualSpace;
//        ControlActualPaddings controlActualPaddings = new(control, remainingAllowedSpace);
//        controlLayout.Padding = controlActualPaddings.Compute();
//        actualSpace += controlLayout.Padding;
//    }

//    private HorizontalAlignment ComputeHorizontalAlignment()
//    {
//        if (control is BlockControl blockControl)
//        {
//            bool widthIsProvided = blockControl.Width != null || blockControl.MinWidth != null || blockControl.MaxWidth != null;

//            return widthIsProvided
//                ? ComputeHorizontalAlignment_WhenWidthIsProvided(blockControl)
//                : ComputeHorizontalAlignment_WhenNoWidthProvided(blockControl);
//        }

//        return HorizontalAlignment.Left;
//    }

//    private HorizontalAlignment ComputeHorizontalAlignment_WhenWidthIsProvided(BlockControl blockControl)
//    {
//        switch (blockControl.HorizontalAlignment)
//        {
//            case HorizontalAlignment.Default:
//            case HorizontalAlignment.Left:
//                return HorizontalAlignment.Left;

//            case HorizontalAlignment.Center:
//                return HorizontalAlignment.Center;

//            case HorizontalAlignment.Right:
//                return HorizontalAlignment.Right;

//            case HorizontalAlignment.Stretch:
//                return HorizontalAlignment.Left;

//            case null:
//                return HorizontalAlignment.Left;

//            default:
//                throw new ArgumentOutOfRangeException();
//        }
//    }

//    private HorizontalAlignment ComputeHorizontalAlignment_WhenNoWidthProvided(BlockControl blockControl)
//    {
//        switch (blockControl.HorizontalAlignment)
//        {
//            case HorizontalAlignment.Default:
//                return HorizontalAlignment.Left;

//            case HorizontalAlignment.Left:
//            case HorizontalAlignment.Center:
//            case HorizontalAlignment.Right:
//            case HorizontalAlignment.Stretch:
//                return blockControl.HorizontalAlignment.Value;

//            case null:
//                return HorizontalAlignment.Stretch;

//            default:
//                throw new ArgumentOutOfRangeException();
//        }
//    }

//    private void CalculateContentSize()
//    {
//        if (control is BlockControl blockControl)
//        {
//            Size availableSize = maxAllowedSize - actualSpace;
//            Size naturalSize = blockControl.CalculateNaturalSize();

//            int width = calculatedHorizontalAlignment == HorizontalAlignment.Stretch
//                ? availableSize.Width
//                : Math.Min(naturalSize.Width, availableSize.Width);

//            if (blockControl.MaxWidth != null && width > blockControl.MaxWidth)
//                width = blockControl.MaxWidth.Value;

//            int height = Math.Min(naturalSize.Height, availableSize.Height);

//            controlLayout.ContentSize = new Size(width, height);
//        }
//        else if (control is InlineControl inlineControl)
//        {
//            controlLayout.ContentSize = inlineControl.CalculateNaturalSize();
//        }
//        else
//        {
//            controlLayout.ContentSize = Size.Empty;
//        }
//    }

//    private void CalculateInnerEmptySpace()
//    {
//        int innerEmptySpaceTotal = ActualClientWidth - ActualContentWidth;

//        switch (ContentHorizontalAlignment)
//        {
//            case HorizontalAlignment.Default:
//            case HorizontalAlignment.Left:
//                InnerEmptySpaceLeft = 0;
//                InnerEmptySpaceRight = innerEmptySpaceTotal;
//                break;

//            case HorizontalAlignment.Center:
//                double emptySpaceHalf = (double)innerEmptySpaceTotal / 2;
//                InnerEmptySpaceLeft = (int)Math.Floor(emptySpaceHalf);
//                InnerEmptySpaceRight = (int)Math.Ceiling(emptySpaceHalf);
//                break;

//            case HorizontalAlignment.Right:
//                InnerEmptySpaceLeft = innerEmptySpaceTotal;
//                InnerEmptySpaceRight = 0;
//                break;

//            case HorizontalAlignment.Stretch:
//                InnerEmptySpaceLeft = 0;
//                InnerEmptySpaceRight = 0;
//                break;

//            default:
//                throw new ArgumentOutOfRangeException();
//        }
//    }

//    private void CalculateOuterEmptySpace()
//    {
//        int outerEmptySpaceTotal = AvailableWidth - ActualFullWidth;

//        switch (calculatedHorizontalAlignment)
//        {
//            case HorizontalAlignment.Default:
//            case HorizontalAlignment.Left:
//                OuterEmptySpaceLeft = 0;
//                OuterEmptySpaceRight = outerEmptySpaceTotal;
//                break;

//            case HorizontalAlignment.Center:
//                double emptySpaceHalf = (double)outerEmptySpaceTotal / 2;
//                OuterEmptySpaceLeft = (int)Math.Floor(emptySpaceHalf);
//                OuterEmptySpaceRight = (int)Math.Ceiling(emptySpaceHalf);
//                break;

//            case HorizontalAlignment.Right:
//                OuterEmptySpaceLeft = outerEmptySpaceTotal;
//                OuterEmptySpaceRight = 0;
//                break;

//            case HorizontalAlignment.Stretch:
//                OuterEmptySpaceLeft = 0;
//                OuterEmptySpaceRight = 0;
//                break;

//            default:
//                throw new ArgumentOutOfRangeException();
//        }
//    }
//}