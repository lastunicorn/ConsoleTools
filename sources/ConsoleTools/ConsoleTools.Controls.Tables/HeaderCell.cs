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
    /// Gets a value that specifies if the current instance of the <see cref="HeaderCell"/> has
    /// a content to be displayed.
    /// </summary>
    public override bool HasVisibleContent =>
        Content?.IsEmpty == false ||
        DefaultContent?.IsEmpty == false ||
        ParentRow?.CellDefaultContent?.IsEmpty == false;

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

    /// <inheritdoc />
    [Obsolete("Intended for internal usage only.")]
    public override ConsoleColor? CalculateForegroundColor()
    {
        return ForegroundColor
               ?? ParentRow?.ForegroundColor
               ?? ParentColumn.ForegroundColor
               ?? ParentRow?.ParentDataGrid?.ForegroundColor;
    }

    /// <inheritdoc />
    [Obsolete("Intended for internal usage only.")]
    public override ConsoleColor? CalculateBackgroundColor()
    {
        return BackgroundColor
               ?? ParentRow?.BackgroundColor
               ?? ParentColumn.BackgroundColor
               ?? ParentRow?.ParentDataGrid?.BackgroundColor;
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

        paddingLeft = ParentRow?.CellPaddingLeft;
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

        alignment = ParentRow?.CellHorizontalAlignment ?? HorizontalAlignment.Default;
        if (alignment != HorizontalAlignment.Default)
            return alignment;

        alignment = ParentColumn?.CellHorizontalAlignment ?? HorizontalAlignment.Default;
        if (alignment != HorizontalAlignment.Default)
            return alignment;

        alignment = ParentRow?.ParentDataGrid?.CellHorizontalAlignment ?? HorizontalAlignment.Default;
        if (alignment != HorizontalAlignment.Default)
            return alignment;

        alignment = DefaultHorizontalAlignment;

        return alignment;
    }

    internal override MultilineText ComputeContent()
    {
        MultilineText content = Content;

        if (content == null || content.IsEmpty)
            content = ParentRow?.CellDefaultContent;

        if (content == null || content.IsEmpty)
            content = DefaultContent;

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
        return 1;
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