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
/// Represents a cell that contains data.
/// </summary>
public class ContentCell : CellBase
{
    private int columnSpan = 1;

    /// <summary>
    /// Gets or sets a value that specifies across how many columns should the cell be displayed.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Exception thrown when column span is set to a negative or zero value.</exception>
    public virtual int ColumnSpan
    {
        get => columnSpan;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(ColumnSpan), "The column span value must be a positive value.");

            columnSpan = value;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ContentCell" /> class with
    /// empty content.
    /// </summary>
    public ContentCell()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ContentCell" /> class with
    /// the text contained by it.
    /// </summary>
    /// <param name="text">The text displayed in the cell.</param>
    public ContentCell(string text)
        : base(text)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ContentCell" /> class with
    /// the text contained by it and its horizontal alignment.
    /// </summary>
    /// <param name="text">The text displayed in the cell.</param>
    /// <param name="horizontalAlignment">The horizontal alignment of the content of the new cell.</param>
    public ContentCell(string text, HorizontalAlignment horizontalAlignment)
        : base(text, horizontalAlignment)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ContentCell" /> class with
    /// the text contained by it.
    /// </summary>
    /// <param name="text"></param>
    public ContentCell(MultilineText text)
        : base(text)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ContentCell" /> class with
    /// the text contained by it and its horizontal alignment.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="horizontalAlignment">The horizontal alignment of the content of the new cell.</param>
    public ContentCell(MultilineText text, HorizontalAlignment horizontalAlignment)
        : base(text, horizontalAlignment)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ContentCell" /> class with
    /// an object representing the content.
    /// </summary>
    public ContentCell(object content)
        : base(content)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ContentCell" /> class with
    /// an object representing the content and its horizontal alignment.
    /// </summary>
    public ContentCell(object content, HorizontalAlignment horizontalAlignment)
        : base(content, horizontalAlignment)
    {
    }

    /// <summary>
    /// Calculates and returns the left padding for the content displayed in the cell.
    /// The value is calculated taking into account also the parent row, parent column and parent table.
    /// </summary>
    [Obsolete("Intended for internal usage only.")]
    public override int CalculatePaddingLeft()
    {
        int? paddingLeft = PaddingLeft;

        if (paddingLeft != null)
            return paddingLeft.Value;

        paddingLeft = ParentRow?.CellPaddingLeft;

        if (paddingLeft != null)
            return paddingLeft.Value;

        Column column = GetColumn();
        paddingLeft = column?.CellPaddingLeft;

        if (paddingLeft != null)
            return paddingLeft.Value;

        paddingLeft = ParentRow?.ParentDataGrid?.CellPaddingLeft;

        if (paddingLeft != null)
            return paddingLeft.Value;

        paddingLeft = DefaultPaddingLeft;

        return paddingLeft.Value;
    }

    /// <summary>
    /// Calculates and returns the right padding for the content displayed in the cell.
    /// The value is calculated taking into account also the parent row, parent column and parent table.
    /// </summary>
    [Obsolete("Intended for internal usage only.")]
    public override int CalculatePaddingRight()
    {
        int? paddingRight = PaddingRight;
        if (paddingRight != null)
            return paddingRight.Value;

        paddingRight = ParentRow?.CellPaddingRight;
        if (paddingRight != null)
            return paddingRight.Value;

        Column column = GetColumn();
        paddingRight = column?.CellPaddingRight;
        if (paddingRight != null)
            return paddingRight.Value;

        paddingRight = ParentRow?.ParentDataGrid?.CellPaddingRight;
        if (paddingRight != null)
            return paddingRight.Value;

        paddingRight = DefaultPaddingRight;

        return paddingRight.Value;
    }

    /// <summary>
    /// Calculates and returns the foreground color for the content displayed in the cell.
    /// The value is calculated taking into account also the parent row, parent column and parent table.
    /// </summary>
    [Obsolete("Intended for internal usage only.")]
    public override ConsoleColor? CalculateForegroundColor()
    {
        return ForegroundColor
               ?? ParentRow?.ForegroundColor
               ?? GetColumn()?.ForegroundColor
               ?? ParentRow?.ParentDataGrid?.ForegroundColor;
    }

    /// <summary>
    /// Calculates and returns the background color for the content displayed in the cell.
    /// The value is calculated taking into account also the parent row, parent column and parent table.
    /// </summary>
    [Obsolete("Intended for internal usage only.")]
    public override ConsoleColor? CalculateBackgroundColor()
    {
        return BackgroundColor
               ?? ParentRow?.BackgroundColor
               ?? GetColumn()?.BackgroundColor
               ?? ParentRow?.ParentDataGrid?.BackgroundColor;
    }

    /// <summary>
    /// Returns the calculated horizontal alignment for the content of the current data cell.
    /// The value is calculated based on the <see cref="HorizontalAlignment"/> property of the current data cell,
    /// and the values specified by the parent row, parent column and parent table.
    /// It never returns <see cref="HorizontalAlignment.Default"/>.
    /// </summary>
    [Obsolete("Intended for internal usage only.")]
    public override HorizontalAlignment CalculateHorizontalAlignment()
    {
        HorizontalAlignment alignment = HorizontalAlignment;
        if (alignment != HorizontalAlignment.Default)
            return alignment;

        alignment = CalculateHorizontalAlignmentAtRowLevel();
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

    private HorizontalAlignment CalculateHorizontalAlignmentAtRowLevel()
    {
        return ParentRow?.CellHorizontalAlignment ?? HorizontalAlignment.Default;
    }

    private HorizontalAlignment CalculateHorizontalAlignmentAtColumnLevel()
    {
        Column column = GetColumn();
        return column?.CellHorizontalAlignment ?? HorizontalAlignment.Default;
    }

    private Column GetColumn()
    {
        ColumnList columns = ParentRow?.ParentDataGrid?.Columns;
        int? columnIndex = ParentRow?.IndexOfCell(this);

        return columns != null && columnIndex.HasValue
            ? ParentRow?.ParentDataGrid?.Columns[columnIndex.Value]
            : null;
    }

    private HorizontalAlignment CalculateHorizontalAlignmentAtTableLevel()
    {
        DataGrid dataGrid = ParentRow?.ParentDataGrid;
        return dataGrid?.CellHorizontalAlignment ?? HorizontalAlignment.Default;
    }

    internal override CellContentOverflow ComputeContentOverflow()
    {
        CellContentOverflow contentOverflow = ContentOverflow;
        if (contentOverflow != CellContentOverflow.Default)
            return contentOverflow;

        contentOverflow = ComputeContentOverflowAtColumnLevel();
        if (contentOverflow != CellContentOverflow.Default)
            return contentOverflow;

        contentOverflow = ComputeContentOverflowAtTableLevel();
        if (contentOverflow != CellContentOverflow.Default)
            return contentOverflow;

        contentOverflow = DefaultContentOverflow;

        return contentOverflow;
    }

    private CellContentOverflow ComputeContentOverflowAtColumnLevel()
    {
        Column column = GetColumn();
        return column?.CellContentOverflow ?? CellContentOverflow.Default;
    }

    private CellContentOverflow ComputeContentOverflowAtTableLevel()
    {
        DataGrid dataGrid = ParentRow?.ParentDataGrid;
        return dataGrid?.CellContentOverflow ?? CellContentOverflow.Default;
    }

    /// <summary>
    /// Converts a <see cref="string"/> into a <see cref="ContentCell"/> instance.
    /// </summary>
    /// <param name="text">The text to be converted.</param>
    public static implicit operator ContentCell(string text)
    {
        MultilineText multilineText = new(text);
        return new ContentCell(multilineText);
    }

    /// <summary>
    /// Converts a <see cref="ContentCell"/> into its <see cref="string"/> representation.
    /// </summary>
    /// <param name="cell">The <see cref="ContentCell"/> to be converted.</param>
    public static implicit operator string(ContentCell cell)
    {
        return cell.Content?.ToString() ?? string.Empty;
    }
}