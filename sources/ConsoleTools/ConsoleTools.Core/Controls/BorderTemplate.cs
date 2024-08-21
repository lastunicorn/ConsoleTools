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
using System.Collections.Generic;
using System.Text;

namespace DustInTheWind.ConsoleTools.Controls;

/// <summary>
/// Represents a border template that can be applied on a <see cref="DataGrid"/> instance.
/// </summary>
public class BorderTemplate
{
    /// <summary>
    /// Gets an instance of the <see cref="BorderTemplate"/> that uses spaces to render the border.
    /// </summary>
    public static BorderTemplate WhiteTemplate { get; } = new("               ");

    /// <summary>
    /// Gets an instance of the <see cref="BorderTemplate"/> that uses '+' and '-' characters to render the border.
    /// </summary>
    public static BorderTemplate PlusMinusBorderTemplate { get; } = new("+-+|+-+|+++++|-");

    /// <summary>
    /// Gets an instance of the <see cref="BorderTemplate"/> that uses single line ASCII characters to render the border.
    /// </summary>
    public static BorderTemplate SingleLineBorderTemplate { get; } = new("┌─┐│┘─└│┬┤┴├┼│─");

    /// <summary>
    /// Gets an instance of the <see cref="BorderTemplate"/> that uses double line ASCII characters to render the border.
    /// </summary>
    public static BorderTemplate DoubleLineBorderTemplate { get; } = new("╔═╗║╝═╚║╦╣╩╠╬║═");

    private readonly bool isEmpty;

    /// <summary>
    /// Gets the character used to render the top left corner.
    /// </summary>
    public char TopLeft { get; }

    /// <summary>
    /// Gets the character used to render the top border.
    /// </summary>
    public char Top { get; }

    /// <summary>
    /// Gets the character used to render the top right corner.
    /// </summary>
    public char TopRight { get; }

    /// <summary>
    /// Gets the character used to render the right border.
    /// </summary>
    public char Right { get; }

    /// <summary>
    /// Gets the character used to render the bottom right corner.
    /// </summary>
    public char BottomRight { get; }

    /// <summary>
    /// Gets the character used to render the bottom border.
    /// </summary>
    public char Bottom { get; }

    /// <summary>
    /// Gets the character used to render the bottom left corner.
    /// </summary>
    public char BottomLeft { get; }

    /// <summary>
    /// Gets the character used to render the left border.
    /// </summary>
    public char Left { get; }

    /// <summary>
    /// Gets the character used to render the intersection between the top outer border and an internal vertical border.
    /// </summary>
    public char TopIntersection { get; }

    /// <summary>
    /// Gets the character used to render the intersection between the right outer border and an internal horizontal border.
    /// </summary>
    public char RightIntersection { get; }

    /// <summary>
    /// Gets the character used to render the intersection between the bottom outer border and an internal vertical border.
    /// </summary>
    public char BottomIntersection { get; }

    /// <summary>
    /// Gets the character used to render the intersection between the left outer border and an internal horizontal border.
    /// </summary>
    public char LeftIntersection { get; }

    /// <summary>
    /// Gets the character used to render the intersection between two internal borders: a vertical and a horizontal one.
    /// </summary>
    public char MiddleIntersection { get; }

    /// <summary>
    /// Gets the character used to render an internal vertical border.
    /// </summary>
    public char Vertical { get; }

    /// <summary>
    /// Gets the character used to render an internal horizontal border.
    /// </summary>
    public char Horizontal { get; }

    private BorderTemplate()
    {
        isEmpty = true;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BorderTemplate"/> class with
    /// a string of length 15 that containing all the characters needed to render the border.
    /// </summary>
    /// <param name="values">
    /// The string of length 15 that containing all the characters needed to render the border.
    /// The string must contain the characters in the following order:
    /// top-left, top, top-right, right, bottom-right, bottom, bottom-left, left,
    /// top-intersection, right-intersection, bottom-intersection, left-intersection, middle-intersection,
    /// vertical, horizontal.
    /// </param>
    public BorderTemplate(string values)
    {
        if (values == null) throw new ArgumentNullException(nameof(values));

        if (values.Length != 15)
        {
            throw new ArgumentException(
                "Please provide 15 characters for the border in the following order: top-left, top, top-right, right, bottom-right, bottom, bottom-left, left, top-intersection, right-intersection, bottom-intersection, left-intersection, middle-intersection, vertical, horizontal.",
                nameof(values));
        }

        TopLeft = values[0];
        Top = values[1];
        TopRight = values[2];
        Right = values[3];
        BottomRight = values[4];
        Bottom = values[5];
        BottomLeft = values[6];
        Left = values[7];

        TopIntersection = values[8];
        RightIntersection = values[9];
        BottomIntersection = values[10];
        LeftIntersection = values[11];
        MiddleIntersection = values[12];

        Vertical = values[13];
        Horizontal = values[14];
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BorderTemplate"/> class with
    /// all the characters needed to render the border.
    /// </summary>
    public BorderTemplate(char topLeft, char top, char topRight, char right, char bottomRight, char bottom, char bottomLeft, char left,
        char topIntersection, char rightIntersection, char bottomIntersection, char leftIntersection, char middleIntersection,
        char vertical, char horizontal)
    {
        TopLeft = topLeft;
        Top = top;
        TopRight = topRight;
        Right = right;
        BottomRight = bottomRight;
        Bottom = bottom;
        BottomLeft = bottomLeft;
        Left = left;

        TopIntersection = topIntersection;
        RightIntersection = rightIntersection;
        BottomIntersection = bottomIntersection;
        LeftIntersection = leftIntersection;
        MiddleIntersection = middleIntersection;

        Vertical = vertical;
        Horizontal = horizontal;
    }

    /// <summary>
    /// Generates the border displayed at the top of the column header row.
    /// This border is used only when title is hidden and the column header row is visible, being the first row of the table.
    /// </summary>
    public string GenerateTopBorder(params int[] cellWidths)
    {
        if (isEmpty)
            return string.Empty;

        return GenerateTopBorder((IList<int>)cellWidths);
    }

    /// <summary>
    /// Generates the border displayed at the top of the column header row.
    /// This border is used only when title is hidden and the column header row is visible, being the first row of the table.
    /// </summary>
    public string GenerateTopBorder(IList<int> cellWidths)
    {
        if (isEmpty)
            return string.Empty;

        if (cellWidths == null || cellWidths.Count == 0)
            return string.Empty;

        StringBuilder sb = new();

        sb.Append(TopLeft);

        for (int cellIndex = 0; cellIndex < cellWidths.Count; cellIndex++)
        {
            int cellWidth = cellWidths[cellIndex];
            sb.Append(new string(Top, cellWidth));

            char cellBorderRight = cellIndex < cellWidths.Count - 1
                ? TopIntersection
                : TopRight;

            sb.Append(cellBorderRight);
        }

        return sb.ToString();
    }

    /// <summary>
    /// Generates the border displayed between title and column header rows.
    /// This border is used only when both title and column header rows are visible.
    /// </summary>
    public string GenerateHorizontalSeparator(params int[] cellWidths)
    {
        if (isEmpty)
            return string.Empty;

        return GenerateHorizontalSeparator((IList<int>)cellWidths);
    }

    /// <summary>
    /// Generates the border displayed between title and column header rows.
    /// This border is used only when both title and column header rows are visible.
    /// </summary>
    public string GenerateHorizontalSeparator(IList<int> cellWidths)
    {
        if (isEmpty)
            return string.Empty;

        if (cellWidths == null || cellWidths.Count == 0)
            return string.Empty;

        StringBuilder sb = new();

        sb.Append(LeftIntersection);

        for (int cellIndex = 0; cellIndex < cellWidths.Count; cellIndex++)
        {
            int columnWidth = cellWidths[cellIndex];
            sb.Append(new string(Horizontal, columnWidth));

            char cellBorderRight = cellIndex < cellWidths.Count - 1
                ? MiddleIntersection
                : RightIntersection;

            sb.Append(cellBorderRight);
        }

        return sb.ToString();
    }

    /// <summary>
    /// Generates the border displayed between title and column header rows.
    /// This border is used only when both title and column header rows are visible.
    /// </summary>
    public string GenerateHorizontalSeparator(IList<int> topCellWidths, IList<int> bottomCellWidths)
    {
        if (isEmpty)
            return string.Empty;

        if (topCellWidths == null && bottomCellWidths == null)
            return string.Empty;

        if (topCellWidths == null)
            return GenerateTopBorder(bottomCellWidths);

        if (bottomCellWidths == null)
            return GenerateBottomBorder(topCellWidths);

        HorizontalSeparatorBuilder builder = new()
        {
            Horizontal = Horizontal,
            LeftIntersection = LeftIntersection,
            RightIntersection = RightIntersection,
            TopIntersection = TopIntersection,
            BottomIntersection = BottomIntersection,
            MiddleIntersection = MiddleIntersection,
            TopRight = TopRight,
            BottomRight = BottomRight,
            TopCellWidths = topCellWidths,
            BottomCellWidths = bottomCellWidths
        };

        return builder.Build();
    }

    /// <summary>
    /// Generates the border displayed at the bottom of the last data row.
    /// </summary>
    public string GenerateBottomBorder(params int[] cellWidths)
    {
        if (isEmpty)
            return string.Empty;

        return GenerateBottomBorder((IList<int>)cellWidths);
    }

    /// <summary>
    /// Generates the border displayed at the bottom of the last data row.
    /// </summary>
    public string GenerateBottomBorder(IList<int> cellWidths)
    {
        if (isEmpty)
            return string.Empty;

        if (cellWidths == null || cellWidths.Count == 0)
            return string.Empty;

        StringBuilder sb = new();

        sb.Append(BottomLeft);

        for (int cellIndex = 0; cellIndex < cellWidths.Count; cellIndex++)
        {
            int cellWidth = cellWidths[cellIndex];
            sb.Append(new string(Bottom, cellWidth));

            char cellBorderRight = cellIndex < cellWidths.Count - 1
                ? BottomIntersection
                : BottomRight;

            sb.Append(cellBorderRight);
        }

        return sb.ToString();
    }
}