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

namespace DustInTheWind.ConsoleTools.Controls.Tables;

/// <summary>
/// Represents the single cell displayed in the title row.
/// </summary>
public class TitleCell : CellBase
{
    /// <summary>
    /// Gets or sets the <see cref="TitleRow"/> instance that owns the current <see cref="TitleCell"/> instance.
    /// </summary>
    public TitleRow ParentRow { get; internal set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TitleCell" /> class with
    /// empty content.
    /// </summary>
    public TitleCell()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TitleCell" /> class with
    /// the text contained by it.
    /// </summary>
    /// <param name="text">The text displayed in the cell.</param>
    public TitleCell(string text)
        : base(text)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TitleCell" /> class with
    /// the text contained by it and its horizontal alignment.
    /// </summary>
    /// <param name="text">The text displayed in the cell.</param>
    /// <param name="horizontalAlignment">The horizontal alignment of the content of the new cell.</param>
    public TitleCell(string text, HorizontalAlignment horizontalAlignment)
        : base(text, horizontalAlignment)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TitleCell" /> class with
    /// the text contained by it.
    /// </summary>
    /// <param name="text"></param>
    public TitleCell(MultilineText text)
        : base(text)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TitleCell" /> class with
    /// the text contained by it and its horizontal alignment.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="horizontalAlignment">The horizontal alignment of the content of the new cell.</param>
    public TitleCell(MultilineText text, HorizontalAlignment horizontalAlignment)
        : base(text, horizontalAlignment)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TitleCell" /> class with
    /// an object representing the content.
    /// </summary>
    public TitleCell(object content)
        : base(content)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TitleCell" /> class with
    /// an object representing the content and its horizontal alignment.
    /// </summary>
    public TitleCell(object content, HorizontalAlignment horizontalAlignment)
        : base(content, horizontalAlignment)
    {
    }

    /// <summary>
    /// Calculates and returns the left padding for the content displayed in the cell.
    /// The value is calculated taking into account also the parent row and parent table.
    /// </summary>
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

    /// <summary>
    /// Calculates and returns the right padding for the content displayed in the cell.
    /// The value is calculated taking into account also the parent row and parent table.
    /// </summary>
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

    /// <inheritdoc />
    public override ConsoleColor? CalculateForegroundColor()
    {
        return ForegroundColor
               ?? ParentRow?.ForegroundColor
               ?? ParentRow?.ParentDataGrid?.ForegroundColor;
    }

    /// <inheritdoc />
    public override ConsoleColor? CalculateBackgroundColor()
    {
        return BackgroundColor
               ?? ParentRow?.BackgroundColor
               ?? ParentRow?.ParentDataGrid?.BackgroundColor;
    }

    /// <summary>
    /// Calculates and returns the horizontal alignment of the content displayed in the cell.
    /// The value is calculated taking into account also the parent row and parent table.
    /// </summary>
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
}