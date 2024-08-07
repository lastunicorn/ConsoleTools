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
using System.Linq;

namespace DustInTheWind.ConsoleTools.Controls.Tables.RenderingModel;

internal class RowX : IItemX
{
    public Size Size { get; private set; }

    public RowBorderX Border { get; set; }

    public List<CellX> Cells { get; set; }

    public void CalculateLayout()
    {
        Size = CalculatePreferredSize();
    }

    private Size CalculatePreferredSize()
    {
        int width = 0;
        int height = 0;

        if (Cells != null)
        {
            foreach (CellX cell in Cells)
            {
                width += cell.Size.Width;
                height = Math.Max(height, cell.Size.Height);
            }
        }

        if (Border != null)
        {
            int cellsCount = Cells?.Count ?? 0;

            if (cellsCount == 0)
                cellsCount = 1;

            width += cellsCount + 1;
        }

        return new Size(width, height);
    }

    public void Render(ITablePrinter tablePrinter, IReadOnlyList<int> columnsWidth)
    {
        for (int lineIndex = 0; lineIndex < Size.Height; lineIndex++)
        {
            Border?.RenderRowLeftBorder(tablePrinter);

            for (int columnIndex = 0; columnIndex < Cells.Count; columnIndex++)
            {
                CellX cellX = Cells[columnIndex];
                Size cellSize = CalculateCellSize(columnsWidth, columnIndex, cellX.HorizontalMerge);

                cellX.RenderNextLine(tablePrinter, cellSize);

                bool isLastCell = columnIndex >= Cells.Count - 1;

                if (isLastCell)
                    Border?.RenderRowRightBorder(tablePrinter);
                else
                    Border?.RenderRowInsideBorder(tablePrinter);
            }

            tablePrinter.WriteLine();
        }
    }

    private Size CalculateCellSize(IReadOnlyList<int> columnsWidth, int columnIndex, int columnSpan)
    {
        int cellWidth;

        if (columnSpan >= 2)
        {
            int[] spannedColumns = columnsWidth
                .Skip(columnIndex)
                .Take(columnSpan)
                .ToArray();

            cellWidth = spannedColumns
                .Sum();

            if (Border != null && spannedColumns.Length > 0)
                cellWidth += spannedColumns.Length - 1;
        }
        else
        {
            cellWidth = columnsWidth[columnIndex];
        }

        int cellHeight = Size.Height;

        return new Size(cellWidth, cellHeight);
    }

    public List<bool> ComputeVerticalBorderVisibility(int columnCount)
    {
        List<bool> visibilities = new() { true };

        foreach (CellX cell in Cells)
        {
            for (int i = 0; i < cell.HorizontalMerge - 1 && visibilities.Count < columnCount; i++)
                visibilities.Add(false);

            visibilities.Add(true);
        }

        while (visibilities.Count <= columnCount)
            visibilities.Add(false);

        return visibilities;
    }

    public static RowX CreateFrom(ContentRow contentRow)
    {
        if (contentRow == null) throw new ArgumentNullException(nameof(contentRow));

        RowX rowX = new()
        {
            Border = contentRow.ParentDataGrid == null || contentRow.ParentDataGrid.AreBordersAllowed
                ? RowBorderX.CreateFrom(contentRow)
                : null,
            Cells = contentRow.EnumerateVisibleCells()
                .Select(CellX.CreateFrom)
                .ToList()
        };

        rowX.CalculateLayout();

        return rowX;
    }

    public static RowX CreateFrom(HeaderRow headerRow)
    {
        if (headerRow == null) throw new ArgumentNullException(nameof(headerRow));

        RowX headerRowX = new()
        {
            Border = headerRow.ParentDataGrid == null || headerRow.ParentDataGrid.AreBordersAllowed
                ? RowBorderX.CreateFrom(headerRow)
                : null,
            Cells = headerRow.EnumerateVisibleCells()
                .Select(CellX.CreateFrom)
                .ToList()
        };

        headerRowX.CalculateLayout();

        return headerRowX;
    }

    public static RowX CreateFrom(TitleRow titleRow)
    {
        if (titleRow == null) throw new ArgumentNullException(nameof(titleRow));

        CellX cellX = CellX.CreateFrom(titleRow.TitleCell);
        cellX.HorizontalMerge = int.MaxValue;

        RowX rowX = new()
        {
            Border = titleRow.ParentDataGrid == null || titleRow.ParentDataGrid.AreBordersAllowed
                ? RowBorderX.CreateFrom(titleRow)
                : null,
            Cells = new List<CellX> { cellX }
        };

        rowX.CalculateLayout();

        return rowX;
    }

    public static RowX CreateFrom(FooterRow footerRow)
    {
        if (footerRow == null) throw new ArgumentNullException(nameof(footerRow));

        CellX cellX = CellX.CreateFrom(footerRow.FooterCell);
        cellX.HorizontalMerge = int.MaxValue;

        RowX rowX = new()
        {
            Border = footerRow.ParentDataGrid == null || footerRow.ParentDataGrid.AreBordersAllowed
                ? RowBorderX.CreateFrom(footerRow)
                : null,
            Cells = new List<CellX> { cellX }
        };

        rowX.CalculateLayout();

        return rowX;
    }
}