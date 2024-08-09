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
/// Represents the cell that contains a column header.
/// </summary>
public class HeaderCell : CellBase
{
    /// <summary>
    /// Gets or sets the column that contains the current cell.
    /// </summary>
    public Column ParentColumn { get; internal set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="HeaderCell" /> class with
    /// empty content.
    /// </summary>
    public HeaderCell()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HeaderCell" /> class with
    /// the text contained by it.
    /// </summary>
    /// <param name="text">The text displayed in the cell.</param>
    public HeaderCell(string text)
        : base(text)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HeaderCell" /> class with
    /// the text contained by it and its horizontal alignment.
    /// </summary>
    /// <param name="text">The text displayed in the cell.</param>
    /// <param name="horizontalAlignment">The horizontal alignment of the content of the new cell.</param>
    public HeaderCell(string text, HorizontalAlignment horizontalAlignment)
        : base(text, horizontalAlignment)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HeaderCell" /> class with
    /// the text contained by it.
    /// </summary>
    /// <param name="text"></param>
    public HeaderCell(MultilineText text)
        : base(text)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HeaderCell" /> class with
    /// the text contained by it and its horizontal alignment.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="horizontalAlignment">The horizontal alignment of the content of the new cell.</param>
    public HeaderCell(MultilineText text, HorizontalAlignment horizontalAlignment)
        : base(text, horizontalAlignment)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HeaderCell" /> class with
    /// an object representing the content.
    /// </summary>
    public HeaderCell(object content)
        : base(content)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HeaderCell" /> class with
    /// an object representing the content and its horizontal alignment.
    /// </summary>
    protected HeaderCell(object content, HorizontalAlignment horizontalAlignment)
        : base(content, horizontalAlignment)
    {
    }

    /// <summary>
    /// Calculates and returns the left padding for the content displayed in the cell.
    /// The value is calculated taking into account also the parent column and parent table.
    /// </summary>
    [Obsolete("Intended for internal usage only.")]
    public override int CalculatePaddingLeft()
    {
        int? paddingLeft = PaddingLeft;
        if (paddingLeft != null)
            return paddingLeft.Value;

        paddingLeft = ParentColumn?.ParentDataGrid?.HeaderRow?.CellPaddingLeft;
        if (paddingLeft != null)
            return paddingLeft.Value;

        paddingLeft = ParentColumn?.CellPaddingLeft;
        if (paddingLeft != null)
            return paddingLeft.Value;

        paddingLeft = ParentColumn?.ParentDataGrid?.CellPaddingLeft;
        if (paddingLeft != null)
            return paddingLeft.Value;

        paddingLeft = DefaultPaddingLeft;

        return paddingLeft.Value;
    }

    /// <summary>
    /// Calculates and returns the right padding for the content displayed in the cell.
    /// The value is calculated taking into account also the parent column and parent table.
    /// </summary>
    [Obsolete("Intended for internal usage only.")]
    public override int CalculatePaddingRight()
    {
        int? paddingRight = PaddingRight;
        if (paddingRight != null)
            return paddingRight.Value;

        paddingRight = ParentColumn?.ParentDataGrid?.HeaderRow?.CellPaddingRight;
        if (paddingRight != null)
            return paddingRight.Value;

        paddingRight = ParentColumn?.CellPaddingRight;
        if (paddingRight != null)
            return paddingRight.Value;

        paddingRight = ParentColumn?.ParentDataGrid?.CellPaddingRight;
        if (paddingRight != null)
            return paddingRight.Value;

        paddingRight = DefaultPaddingRight;

        return paddingRight.Value;
    }

    /// <inheritdoc />
    [Obsolete("Intended for internal usage only.")]
    public override ConsoleColor? CalculateForegroundColor()
    {
        ConsoleColor? color = ForegroundColor;
        if (color != null)
            return color;

        color = ParentColumn?.ParentDataGrid?.HeaderRow?.ForegroundColor;
        if (color != null)
            return color;

        color = ParentColumn?.ForegroundColor;
        if (color != null)
            return color;

        color = ParentColumn?.ParentDataGrid?.ForegroundColor;

        return color;
    }

    /// <inheritdoc />
    [Obsolete("Intended for internal usage only.")]
    public override ConsoleColor? CalculateBackgroundColor()
    {
        ConsoleColor? color = BackgroundColor;
        if (color != null)
            return color;

        color = ParentColumn?.ParentDataGrid?.HeaderRow?.BackgroundColor;
        if (color != null)
            return color;

        color = ParentColumn?.BackgroundColor;
        if (color != null)
            return color;

        color = ParentColumn?.ParentDataGrid?.BackgroundColor;

        return color;
    }

    /// <summary>
    /// Calculates and returns the horizontal alignment of the content displayed in the cell.
    /// The value is calculated taking into account also the parent row and parent table.
    /// </summary>
    [Obsolete("Intended for internal usage only.")]
    public override HorizontalAlignment CalculateHorizontalAlignment()
    {
        HorizontalAlignment alignment = HorizontalAlignment;
        if (alignment != HorizontalAlignment.Default)
            return alignment;

        alignment = CalculateHorizontalAlignmentAtHeaderRowLevel();
        if (alignment != HorizontalAlignment.Default)
            return alignment;

        alignment = CalculateHorizontalAlignmentAtColumnLevel();
        if (alignment != HorizontalAlignment.Default)
            return alignment;

        alignment = CalculateHorizontalAlignmentAtTableLevel();
        if (alignment != HorizontalAlignment.Default)
            return alignment;

        alignment = DefaultHorizontalAlignment;

        return alignment;
    }

    private HorizontalAlignment CalculateHorizontalAlignmentAtHeaderRowLevel()
    {
        return ParentColumn?.ParentDataGrid?.HeaderRow?.CellHorizontalAlignment ?? HorizontalAlignment.Default;
    }

    private HorizontalAlignment CalculateHorizontalAlignmentAtColumnLevel()
    {
        return ParentColumn?.CellHorizontalAlignment ?? HorizontalAlignment.Default;
    }

    private HorizontalAlignment CalculateHorizontalAlignmentAtTableLevel()
    {
        return ParentColumn?.ParentDataGrid?.CellHorizontalAlignment ?? HorizontalAlignment.Default;
    }

    /// <summary>
    /// Converts the specified text into a <see cref="HeaderCell"/> instance.
    /// The text is used as the content for the cell.
    /// </summary>
    public static implicit operator HeaderCell(string text)
    {
        MultilineText multilineText = new(text);
        return new HeaderCell(multilineText);
    }

    /// <summary>
    /// Converts the specified <see cref="HeaderCell"/> instance into a text.
    /// The text representation of the content is returned.
    /// </summary>
    public static implicit operator string(HeaderCell cell)
    {
        return cell.Content?.ToString() ?? string.Empty;
    }
}