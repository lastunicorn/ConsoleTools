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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DustInTheWind.ConsoleTools.Controls.Tables.RenderingModel;

internal class ColumnsLayout
{
    private readonly List<int> columnsWidth = new();
    private readonly List<ColumnSpanX> columnsSpan = new();

    public bool HasBorders { get; set; }

    public int MinWidth { get; set; }

    public int MaxWidth { get; set; }

    public int ActualWidth { get; private set; }

    public ReadOnlyCollection<int> Columns => columnsWidth.AsReadOnly();

    public void AddRow(RowX rowX)
    {
        UpdateColumnsWidths(rowX);
    }

    private void UpdateColumnsWidths(RowX rowX)
    {
        for (int i = 0; i < rowX.Cells.Count; i++)
        {
            CellX cellX = rowX.Cells[i];
            UpdateColumnWidth(cellX, i);
        }
    }

    private void UpdateColumnWidth(CellX cellX, int i)
    {
        while (columnsWidth.Count <= i)
            columnsWidth.Add(0);

        Size cellSize = cellX.Size;

        if (cellX.HorizontalMerge > 1)
        {
            ColumnSpanX columnSpanX = new()
            {
                StartColumnIndex = i,
                EndColumnIndex = i + cellX.HorizontalMerge - 1,
                MinContentWidth = cellSize.Width
            };
            columnsSpan.Add(columnSpanX);
        }
        else
        {
            if (cellSize.Width > columnsWidth[i])
                columnsWidth[i] = cellSize.Width;
        }
    }

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
            InflateColumns(columnSpan.StartColumnIndex, columnSpan.SpanValue ?? int.MaxValue, columnSpan.MinContentWidth);

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
            int diff = desiredWidth - actualWidth;

            for (int i = 0; i < diff; i++)
            {
                int columnIndex = i % spanColumns.Length;
                columnsWidth[columnIndex]++;
            }
        }
    }
}