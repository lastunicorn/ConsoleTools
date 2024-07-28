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

internal class DataGridLayout
{
    private readonly List<ColumnX> columns = new();
    private readonly List<ColumnSpanX> columnSpans = new();

    public bool BorderVisibility { get; set; }

    public int MinWidth { get; set; }

    public int MaxWidth { get; set; }

    public int ActualWidth { get; private set; }

    public ReadOnlyCollection<ColumnX> Columns => columns.AsReadOnly();

    public void AddRow(RowX rowX)
    {
        UpdateColumnsWidths(rowX);
    }

    private void UpdateColumnsWidths(RowX rowX)
    {
        for (int i = 0; i < rowX.Cells.Count; i++)
        {
            while (columns.Count <= i)
                columns.Add(new ColumnX());

            CellX cellX = rowX.Cells[i];
            Size cellSize = cellX.Size;

            if (cellX.HorizontalMerge > 1)
            {
                ColumnSpanX columnSpanX = new()
                {
                    StartColumnIndex = i,
                    EndColumnIndex = i + cellX.HorizontalMerge - 1,
                    MinContentWidth = cellSize.Width
                };
                columnSpans.Add(columnSpanX);
            }
            else
            {
                if (cellSize.Width > columns[i].Width)
                    columns[i].Width = cellSize.Width;
            }
        }
    }

    public void FinalizeLayout()
    {
        ActualWidth = CalculateTotalWidth();
    }

    private int CalculateTotalWidth()
    {
        if (columns.Count <= 0)
            return MinWidth;

        // Distribute column span spaces

        foreach (ColumnSpanX columnSpan in columnSpans)
            InflateColumns(columnSpan.StartColumnIndex, columnSpan.SpanValue ?? int.MaxValue, columnSpan.MinContentWidth);

        // Distribute space to reach min width.

        InflateColumns(0, int.MaxValue, MinWidth);

        int totalWidth = columns
            .Select(x => x.Width)
            .Sum();

        if (BorderVisibility)
            totalWidth += columns.Count + 1;

        return totalWidth;
    }

    internal void InflateColumns(int startColumnIndex, int columnCount, int desiredWidth)
    {
        ColumnX[] spanColumns = columns
            .Skip(startColumnIndex)
            .Take(columnCount)
            .ToArray();

        int actualWidth = spanColumns
            .Select(x => x.Width)
            .Sum();

        if (BorderVisibility)
            actualWidth += spanColumns.Length - 1;

        if (actualWidth < desiredWidth)
        {
            int diff = desiredWidth - actualWidth;

            for (int i = 0; i < diff; i++)
            {
                ColumnX columnX = columns[i % spanColumns.Length];
                columnX.Width++;
            }
        }
    }
}