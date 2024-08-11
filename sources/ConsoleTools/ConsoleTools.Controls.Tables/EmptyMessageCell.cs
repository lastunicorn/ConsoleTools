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
/// Represents the sing;e cell displayed in the <see cref="EmptyMessageRow"/>.
/// It contains a message to be displayed to the user in the content area when the
/// <see cref="DataGrid"/> contains no content rows.
/// </summary>
public class EmptyMessageCell : CellBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EmptyMessageCell" /> class with
    /// empty content.
    /// </summary>
    public EmptyMessageCell()
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

        paddingRight = DefaultPaddingRight;

        return paddingRight.Value;
    }

    /// <inheritdoc />
    [Obsolete("Intended for internal usage only.")]
    public override HorizontalAlignment CalculateHorizontalAlignment()
    {
        HorizontalAlignment alignment = HorizontalAlignment;
        if (alignment != HorizontalAlignment.Default)
            return alignment;

        alignment = CalculateHorizontalAlignmentAtRowLevel();
        if (alignment != HorizontalAlignment.Default)
            return alignment;

        alignment = DefaultHorizontalAlignment;

        return alignment;
    }

    private HorizontalAlignment CalculateHorizontalAlignmentAtRowLevel()
    {
        return ParentRow?.CellHorizontalAlignment ?? HorizontalAlignment.Default;
    }

    internal override CellContentOverflow ComputeContentOverflow()
    {
        CellContentOverflow contentOverflow = ContentOverflow;
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