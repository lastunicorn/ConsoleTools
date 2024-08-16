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

using System;

namespace DustInTheWind.ConsoleTools.Controls.Tables;

/// <summary>
/// Represents the sing;e cell displayed in the <see cref="EmptyGridRow"/>.
/// It contains a message to be displayed to the user in the content area when the
/// <see cref="DataGrid"/> contains no content rows.
/// </summary>
public class EmptyGridCell : CellBase
{
    /// <summary>
    /// Gets a value that specifies if the current instance of the <see cref="EmptyGridCell"/> has
    /// a content to be displayed.
    /// </summary>
    public override bool HasVisibleContent =>
        Content?.IsEmpty == false ||
        DefaultContent?.IsEmpty == false ||
        ParentRow?.CellDefaultContent?.IsEmpty == false;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmptyGridCell" /> class with
    /// empty content.
    /// </summary>
    public EmptyGridCell()
    {
        PaddingLeft = 10;
        PaddingTop = 1;
        PaddingRight = 10;
        PaddingBottom = 1;
        HorizontalAlignment = HorizontalAlignment.Center;
    }

    /// <inheritdoc />
    [Obsolete("Intended for internal usage only.")]
    public override ConsoleColor? CalculateForegroundColor()
    {
        return ForegroundColor
               ?? ParentRow?.ForegroundColor
               ?? ParentRow?.ParentDataGrid?.ForegroundColor;
    }

    /// <inheritdoc />
    [Obsolete("Intended for internal usage only.")]
    public override ConsoleColor? CalculateBackgroundColor()
    {
        return BackgroundColor
               ?? ParentRow?.BackgroundColor
               ?? ParentRow?.ParentDataGrid?.BackgroundColor;
    }

    /// <inheritdoc />
    [Obsolete("Intended for internal usage only.")]
    public override int CalculatePaddingLeft()
    {
        int? paddingLeft = PaddingLeft;
        if (paddingLeft != null)
            return paddingLeft.Value;

        paddingLeft = ParentRow?.CellPaddingLeft;
        if (paddingLeft != null)
            return paddingLeft.Value;

        paddingLeft = DefaultPaddingLeft;

        return paddingLeft.Value;
    }

    /// <inheritdoc />
    [Obsolete("Intended for internal usage only.")]
    public override int CalculatePaddingRight()
    {
        int? paddingRight = PaddingRight;
        if (paddingRight != null)
            return paddingRight.Value;

        paddingRight = ParentRow?.CellPaddingRight;
        if (paddingRight != null)
            return paddingRight.Value;

        paddingRight = DefaultPaddingRight;

        return paddingRight.Value;
    }

    internal override int ComputePaddingTop()
    {
        int? paddingTop = PaddingTop;
        if (paddingTop != null)
            return paddingTop.Value;

        paddingTop = ParentRow?.CellPaddingTop;
        if (paddingTop != null)
            return paddingTop.Value;

        paddingTop = DefaultPaddingTop;

        return paddingTop.Value;
    }

    internal override int ComputePaddingBottom()
    {
        int? paddingBottom = PaddingBottom;
        if (paddingBottom != null)
            return paddingBottom.Value;

        paddingBottom = ParentRow?.CellPaddingBottom;
        if (paddingBottom != null)
            return paddingBottom.Value;

        paddingBottom = DefaultPaddingBottom;

        return paddingBottom.Value;
    }

    /// <inheritdoc />
    [Obsolete("Intended for internal usage only.")]
    public override HorizontalAlignment CalculateHorizontalAlignment()
    {
        HorizontalAlignment alignment = HorizontalAlignment;
        if (alignment != HorizontalAlignment.Default)
            return alignment;

        alignment = ParentRow?.CellHorizontalAlignment ?? HorizontalAlignment.Default;
        if (alignment != HorizontalAlignment.Default)
            return alignment;

        alignment = DefaultHorizontalAlignment;

        return alignment;
    }

    internal override MultilineText ComputeContent()
    {
        MultilineText content = Content;

        if (content == null || content.IsEmpty)
            content = DefaultContent;

        if (content == null || content.IsEmpty)
            content = ParentRow?.CellDefaultContent;

        return content;
    }

    internal override CellContentOverflow ComputeContentOverflow()
    {
        CellContentOverflow contentOverflow = ContentOverflow;
        if (contentOverflow != CellContentOverflow.Default)
            return contentOverflow;

        contentOverflow = ParentRow?.CellContentOverflow ?? CellContentOverflow.Default;
        if (contentOverflow != CellContentOverflow.Default)
            return contentOverflow;

        contentOverflow = DefaultContentOverflow;

        return contentOverflow;
    }

    internal override int ComputeColumnSpan()
    {
        return int.MaxValue;
    }
}