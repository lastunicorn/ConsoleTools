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
    private int nextRenderIndex;

    public List<CellX> Cells { get; set; }

    public BorderTemplate BorderTemplate { get; set; }

    public ConsoleColor? BorderForegroundColor { get; set; }

    public ConsoleColor? BorderBackgroundColor { get; set; }

    public Size PreferredSize { get; private set; }

    public Size ActualSize { get; private set; }

    public bool HasMoreLines => nextRenderIndex < ActualSize.Height;

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

        if (BorderTemplate != null)
        {
            int cellsCount = Cells?.Count ?? 0;

            if (cellsCount == 0)
                cellsCount = 1;

            width += cellsCount + 1;
        }

        return new Size(width, height);
    }

    public void InitializeRendering(ColumnXCollection columnXCollection)
    {
        int width = 0;
        int height = 0;

        for (int columnIndex = 0; columnIndex < Cells.Count; columnIndex++)
        {
            CellX cellX = Cells[columnIndex];

            int cellWidth = columnXCollection.GetCellWidth(columnIndex, cellX.ColumnSpan);
            cellX.InitializeRendering(cellWidth);

            width += cellX.ActualSize.Width;
            height = Math.Max(height, cellX.ActualSize.Height);
        }

        bool hasBorder = BorderTemplate != null;
        if (hasBorder)
        {
            int cellsCount = Cells?.Count ?? 0;

            if (cellsCount == 0)
                cellsCount = 1;

            width += cellsCount + 1;
        }

        ActualSize = new Size(width, height);

        nextRenderIndex = 0;
    }

    public void RenderNextLine(RenderingContext renderingContext)
    {
        renderingContext.StartLine();

        RenderRowLeftBorder(renderingContext);

        for (int cellIndex = 0; cellIndex < Cells.Count; cellIndex++)
        {
            CellX cellX = Cells[cellIndex];

            cellX.RenderNextLine(renderingContext);

            bool isLastCell = cellIndex >= Cells.Count - 1;

            if (isLastCell)
                RenderRowRightBorder(renderingContext);
            else
                RenderRowInsideBorder(renderingContext);
        }

        renderingContext.EndLine();

        nextRenderIndex++;
    }

    private void RenderRowLeftBorder(RenderingContext display)
    {
        if (BorderTemplate != null)
            display.Write(BorderTemplate.Left, BorderForegroundColor, BorderBackgroundColor);
    }

    private void RenderRowRightBorder(RenderingContext display)
    {
        if (BorderTemplate != null)
            display.Write(BorderTemplate.Right, BorderForegroundColor, BorderBackgroundColor);
    }

    private void RenderRowInsideBorder(RenderingContext display)
    {
        if (BorderTemplate != null)
            display.Write(BorderTemplate.Vertical, BorderForegroundColor, BorderBackgroundColor);
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