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

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DustInTheWind.ConsoleTools.Controls.Tables.RenderingModel;

internal class ColumnsLayout : IEnumerable<int>
{
    private readonly List<int> columnsWidth = new();
    private readonly List<ColumnSpanX> columnsSpan = new();

    public bool HasBorders { get; set; }

    public int MinWidth { get; set; }

    public int MaxWidth { get; set; }

    public int ActualWidth { get; private set; }

    public int this[int index] => columnsWidth[index];

    public int Count => columnsWidth.Count;

    public void AddColumn(int initialWidth)
    {
        columnsWidth.Add(initialWidth);
    }

    /// <summary>
    /// Increase the column width if necessary.
    /// </summary>
    public void UpdateColumnWidth(int index, int minWidth, int span)
    {
        while (columnsWidth.Count <= index)
            columnsWidth.Add(0);

        if (span > 1)
        {
            ColumnSpanX columnSpanX = new()
            {
                StartIndex = index,
                Span = span,
                MinWidth = minWidth
            };
            columnsSpan.Add(columnSpanX);
        }
        else
        {
            if (minWidth > columnsWidth[index])
                columnsWidth[index] = minWidth;
        }
    }

    /// <summary>
    /// At the end, the columns need to be adjusted to accomodate
    /// cell's column spans and the grid min width constraints.
    /// </summary>
    public void FinalizeLayout()
    {
        ActualWidth = CalculateTotalWidth();
    }

    private int CalculateTotalWidth()
    {
        if (columnsWidth.Count <= 0)
            return MinWidth;

        // Distribute column span spaces

        foreach (ColumnSpanX columnSpan in columnsSpan)
        {
            int startIndex = columnSpan.StartIndex;
            int span = columnSpan.Span ?? int.MaxValue;
            int minWidth = columnSpan.MinWidth;

            InflateColumns(startIndex, span, minWidth);
        }

        // Distribute space to reach min width.

        InflateColumns(0, int.MaxValue, MinWidth);

        int totalWidth = columnsWidth
            .Sum();

        if (HasBorders)
            totalWidth += columnsWidth.Count + 1;

        return totalWidth;
    }

    private void InflateColumns(int startColumnIndex, int columnCount, int desiredWidth)
    {
        int[] spanColumns = columnsWidth
            .Skip(startColumnIndex)
            .Take(columnCount)
            .ToArray();

        int actualWidth = spanColumns
            .Sum();

        if (HasBorders)
            actualWidth += spanColumns.Length - 1;

        if (actualWidth < desiredWidth)
        {
            int diffWidth = desiredWidth - actualWidth;

            int smallIncreaseWidth = diffWidth / spanColumns.Length;
            int bigIncreaseWidth = smallIncreaseWidth + 1;

            int bigColumnCount = diffWidth % spanColumns.Length;

            for (int i = 0; i < bigColumnCount; i++)
                columnsWidth[i] += bigIncreaseWidth;

            for (int i = bigColumnCount; i < spanColumns.Length; i++)
                columnsWidth[i] += smallIncreaseWidth;
        }
    }

    public IEnumerator<int> GetEnumerator()
    {
        return columnsWidth.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}