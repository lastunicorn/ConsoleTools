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

    public int MaxWidth { get; set; } = int.MaxValue;

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
        if (columnsWidth.Count <= 0)
            return;

        // Distribute column span spaces

        foreach (ColumnSpanX columnSpan in columnsSpan)
        {
            int startIndex = columnSpan.StartIndex;
            int span = columnSpan.Span ?? int.MaxValue;
            int minWidth = columnSpan.MinWidth;

            InflateColumns(startIndex, span, minWidth);
        }

        // Distribute space to reach min width.

        int totalWidth = CalculateTotalWidth();

        if (totalWidth < MinWidth)
        {
            int delta = MinWidth - totalWidth;
            InflateEntireGrid(delta);

            totalWidth = CalculateTotalWidth();
        }
        else if (totalWidth > MaxWidth)
        {
            int delta = totalWidth - MaxWidth;
            DeflateEntireGrid(delta);

            totalWidth = CalculateTotalWidth();
        }

        ActualWidth = totalWidth;
    }

    private int CalculateTotalWidth()
    {
        int totalWidth = columnsWidth
            .Sum();

        if (HasBorders)
            totalWidth += columnsWidth.Count + 1;

        return totalWidth;
    }

    private void InflateColumns(int startColumnIndex, int columnCount, int desiredInnerWidth)
    {
        int[] spanColumns = columnsWidth
            .Skip(startColumnIndex)
            .Take(columnCount)
            .ToArray();

        int actualWidth = spanColumns
            .Sum();

        if (HasBorders)
            actualWidth += spanColumns.Length - 1;

        if (actualWidth < desiredInnerWidth)
        {
            int diffWidth = desiredInnerWidth - actualWidth;

            int smallIncreaseWidth = diffWidth / spanColumns.Length;
            int bigIncreaseWidth = smallIncreaseWidth + 1;

            int bigColumnCount = diffWidth % spanColumns.Length;

            for (int i = startColumnIndex; i < startColumnIndex + bigColumnCount; i++)
                columnsWidth[i] += bigIncreaseWidth;

            for (int i = startColumnIndex + bigColumnCount; i < startColumnIndex + spanColumns.Length; i++)
                columnsWidth[i] += smallIncreaseWidth;
        }
    }

    public int GetCellWidth(int cellIndex, int columnSpan = 1)
    {
        if (columnSpan == 1)
            return columnsWidth[cellIndex];

        int[] spannedColumns = columnsWidth
            .Skip(cellIndex)
            .Take(columnSpan)
            .ToArray();

        int contentWidth = spannedColumns
            .Sum();

        bool shouldAddBorders = HasBorders && spannedColumns.Length > 0;

        return shouldAddBorders
            ? contentWidth + spannedColumns.Length - 1
            : contentWidth;
    }

    private void InflateEntireGrid(int deltaWidth)
    {
        int smallIncreaseWidth = deltaWidth / columnsWidth.Count;
        int bigIncreaseWidth = smallIncreaseWidth + 1;

        int bigColumnCount = deltaWidth % columnsWidth.Count;

        for (int i = 0; i < bigColumnCount; i++)
            columnsWidth[i] += bigIncreaseWidth;

        for (int i = bigColumnCount; i < columnsWidth.Count; i++)
            columnsWidth[i] += smallIncreaseWidth;
    }

    private void DeflateEntireGrid(int deltaWidth)
    {
        int smallDecreaseWidth = deltaWidth / columnsWidth.Count;
        int bigDecreaseWidth = smallDecreaseWidth + 1;

        int bigColumnCount = deltaWidth % columnsWidth.Count;

        for (int i = 0; i < bigColumnCount; i++)
            columnsWidth[i] -= bigDecreaseWidth;

        for (int i = bigColumnCount; i < columnsWidth.Count; i++)
            columnsWidth[i] -= smallDecreaseWidth;
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