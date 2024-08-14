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
/// Represents a table cell that contains data.
/// </summary>
public abstract class CellBase
{
    private int? paddingLeft;
    private int? paddingRight;
    private int? paddingTop;
    private int? paddingBottom;

    /// <summary>
    /// Gets the default horizontal alignment for a cell.
    /// </summary>
    public static HorizontalAlignment DefaultHorizontalAlignment => HorizontalAlignment.Left;

    /// <summary>
    /// Gets the default content overflow for a cell.
    /// </summary>
    public static CellContentOverflow DefaultContentOverflow => CellContentOverflow.WrapWord;

    /// <summary>
    /// Gets the default left padding applied to a cell's content.
    /// </summary>
    public static int DefaultPaddingLeft => 1;

    /// <summary>
    /// Gets the default left padding applied to a cell's content.
    /// </summary>
    public static int DefaultPaddingRight => 1;

    /// <summary>
    /// Gets the default top padding applied to a cell's content.
    /// </summary>
    public static int DefaultPaddingTop => 0;

    /// <summary>
    /// Gets the default bottom padding applied to a cell's content.
    /// </summary>
    public static int DefaultPaddingBottom => 0;

    /// <summary>
    /// Gets or sets the row that contains the current cell.
    /// </summary>
    public RowBase ParentRow { get; internal set; }

    /// <summary>
    /// Gets or sets the content of the cell.
    /// </summary>
    public MultilineText Content { get; set; }

    /// <summary>
    /// Gets or sets the text to be displayed when the <see cref="Content"/> is empty.
    /// </summary>
    public MultilineText DefaultContent { get; set; }

    /// <summary>
    /// Gets a value that specified if the cell contains no data.
    /// </summary>
    public bool IsEmpty => Content == null || Content.IsEmpty;

    /// <summary>
    /// Gets or sets the foreground color for the cell.
    /// Default value: <c>null</c>
    /// </summary>
    public ConsoleColor? ForegroundColor { get; set; }

    /// <summary>
    /// Gets or sets the background color for the cell.
    /// Default value: <c>null</c>
    /// </summary>
    public ConsoleColor? BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the horizontal alignment of the content displayed in the cell.
    /// </summary>
    public HorizontalAlignment HorizontalAlignment { get; set; }

    /// <summary>
    /// Gets or sets the number of spaces to be applied between the content and the left border of
    /// the cell.
    /// </summary>
    public int? PaddingLeft
    {
        get => paddingLeft;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(PaddingLeft), "Padding value must be positive or zero.");

            paddingLeft = value;
        }
    }

    /// <summary>
    /// Gets or sets the number of spaces to be applied between the content and the right border of
    /// the cell.
    /// </summary>
    public int? PaddingRight
    {
        get => paddingRight;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(PaddingRight), "Padding value must be positive or zero.");

            paddingRight = value;
        }
    }

    /// <summary>
    /// Gets or sets the number of empty lines to be applied between the content and the top border
    /// of the cell.
    /// </summary>
    public int? PaddingTop
    {
        get => paddingTop;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(PaddingTop), "Padding value must be positive or zero.");

            paddingTop = value;
        }
    }

    /// <summary>
    /// Gets or sets the number of empty lines to be applied between the content and the bottom
    /// border of the cell.
    /// </summary>
    public int? PaddingBottom
    {
        get => paddingBottom;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(PaddingBottom), "Padding value must be positive or zero.");

            paddingBottom = value;
        }
    }

    /// <summary>
    /// Gets or sets a value specifying how the overflow text is handled when the cell has the
    /// width too small for displaying it.
    /// </summary>
    public CellContentOverflow ContentOverflow { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CellBase" /> class with
    /// empty content.
    /// </summary>
    protected CellBase(HorizontalAlignment horizontalAlignment = HorizontalAlignment.Default)
    {
        Content = MultilineText.Empty;
        HorizontalAlignment = horizontalAlignment;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CellBase" /> class with
    /// the text contained by it and its horizontal alignment.
    /// </summary>
    /// <param name="text">The text displayed in the cell.</param>
    /// <param name="horizontalAlignment">The horizontal alignment of the content of the new cell.</param>
    protected CellBase(string text, HorizontalAlignment horizontalAlignment = HorizontalAlignment.Default)
    {
        Content = new MultilineText(text);
        HorizontalAlignment = horizontalAlignment;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CellBase" /> class with
    /// the text contained by it and its horizontal alignment.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="horizontalAlignment">The horizontal alignment of the content of the new cell.</param>
    protected CellBase(MultilineText text, HorizontalAlignment horizontalAlignment = HorizontalAlignment.Default)
    {
        Content = text;
        HorizontalAlignment = horizontalAlignment;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CellBase" /> class with
    /// an object representing the content and its horizontal alignment.
    /// </summary>
    protected CellBase(object content, HorizontalAlignment horizontalAlignment = HorizontalAlignment.Default)
    {
        Content = new MultilineText(content?.ToString());
        HorizontalAlignment = horizontalAlignment;
    }

    /// <summary>
    /// Returns the size of the cell, including the padding.
    /// </summary>
    [Obsolete("Intended for internal usage only.")]
    public Size CalculatePreferredSize()
    {
        int cellWidth;
        int cellHeight;

        int preferredPaddingLeft = CalculatePaddingLeft();
        int preferredPaddingRight = CalculatePaddingRight();

        if (IsEmpty)
        {
            cellWidth = preferredPaddingLeft + preferredPaddingRight;
            cellHeight = 0;
        }
        else
        {
            cellWidth = preferredPaddingLeft + Content.Size.Width + preferredPaddingRight;
            cellHeight = Content.Size.Height;
        }

        return new Size(cellWidth, cellHeight);
    }

    /// <summary>
    /// Returns the foreground color calculated based on the hierarchy from which the current cell is part of.
    /// </summary>
    [Obsolete("Intended for internal usage only.")]
    public abstract ConsoleColor? CalculateForegroundColor();

    /// <summary>
    /// Returns the background color calculated based on the hierarchy from which the current cell is part of.
    /// </summary>
    [Obsolete("Intended for internal usage only.")]
    public abstract ConsoleColor? CalculateBackgroundColor();

    /// <summary>
    /// Returns the number of spaces representing the left padding.
    /// </summary>
    [Obsolete("Intended for internal usage only.")]
    public abstract int CalculatePaddingLeft();

    /// <summary>
    /// Returns the number of spaces representing the right padding.
    /// </summary>
    [Obsolete("Intended for internal usage only.")]
    public abstract int CalculatePaddingRight();

    internal abstract int ComputePaddingTop();

    internal abstract int ComputePaddingBottom();

    /// <summary>
    /// Returns the calculated horizontal alignment for the content of the current instance.
    /// The value is calculated based on the <see cref="HorizontalAlignment"/> property of the current instance,
    /// and the values specified by the parents.
    /// It should never return <see cref="HorizontalAlignment.Default"/>.
    /// </summary>
    [Obsolete("Intended for internal usage only.")]
    public abstract HorizontalAlignment CalculateHorizontalAlignment();

    internal abstract MultilineText ComputeContent();

    internal abstract CellContentOverflow ComputeContentOverflow();

    internal abstract int ComputeColumnSpan();

    /// <summary>
    /// Returns the string representation of the content of the cell.
    /// </summary>
    public override string ToString()
    {
        return Content?.ToString() ?? string.Empty;
    }
}