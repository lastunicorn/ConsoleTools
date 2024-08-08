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

namespace DustInTheWind.ConsoleTools.Controls.Tables.RenderingModel;

internal class RowX : IItemX
{
    public Size PreferredSize { get; private set; }

    public Size ActualSize { get; private set; }

    public RowBorderX Border { get; set; }

    public List<CellX> Cells { get; set; }

    public void CalculateLayout()
    {
        PreferredSize = CalculatePreferredSize();
    }

    private Size CalculatePreferredSize()
    {
        int width = 0;
        int height = 0;

        if (Cells != null)
        {
            foreach (CellX cell in Cells)
            {
                width += cell.PreferredSize.Width;
                height = Math.Max(height, cell.PreferredSize.Height);
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

    public void Render(ITablePrinter tablePrinter, ColumnsLayout columnsLayout)
    {
        InitializeCellRendering(columnsLayout);

        for (int lineIndex = 0; lineIndex < ActualSize.Height; lineIndex++)
        {
            RenderNextLine(tablePrinter);
            tablePrinter.WriteLine();
        }
    }

    private void InitializeCellRendering(ColumnsLayout columnsLayout)
    {
        int width = 0;
        int height = 0;

        for (int columnIndex = 0; columnIndex < Cells.Count; columnIndex++)
        {
            CellX cellX = Cells[columnIndex];

            int cellWidth = columnsLayout.GetCellWidth(columnIndex, cellX.ColumnSpan);
            cellX.InitializeRendering(cellWidth);

            width += cellX.ActualContentSize.Width;
            height = Math.Max(height, cellX.ActualContentSize.Height);
        }

        bool hasBorder = Border != null;
        if (hasBorder)
        {
            int cellsCount = Cells?.Count ?? 0;

            if (cellsCount == 0)
                cellsCount = 1;

            width += cellsCount + 1;
        }

        ActualSize = new Size(width, height);
    }

    private void RenderNextLine(ITablePrinter tablePrinter)
    {
        Border?.RenderRowLeftBorder(tablePrinter);

        for (int cellIndex = 0; cellIndex < Cells.Count; cellIndex++)
        {
            CellX cellX = Cells[cellIndex];

            cellX.RenderNextLine(tablePrinter);

            bool isLastCell = cellIndex >= Cells.Count - 1;

            if (isLastCell)
                Border?.RenderRowRightBorder(tablePrinter);
            else
                Border?.RenderRowInsideBorder(tablePrinter);
        }
    }

    public List<bool> ComputeVerticalBorderVisibility(int columnCount)
    {
        List<bool> visibilities = new() { true };

        foreach (CellX cell in Cells)
        {
            for (int i = 0; i < cell.ColumnSpan - 1 && visibilities.Count < columnCount; i++)
                visibilities.Add(false);

            visibilities.Add(true);
        }

        while (visibilities.Count <= columnCount)
            visibilities.Add(false);

        return visibilities;
    }
}