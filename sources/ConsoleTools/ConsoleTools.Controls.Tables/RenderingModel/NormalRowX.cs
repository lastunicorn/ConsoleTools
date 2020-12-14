// ConsoleTools
// Copyright (C) 2017-2020 Dust in the Wind
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

namespace DustInTheWind.ConsoleTools.Controls.Tables.RenderingModel
{
    internal class NormalRowX
    {
        public Size Size { get; private set; }

        public DataGridBorderX DataGridBorderX { get; set; }

        public List<CellX> Cells { get; set; } = new List<CellX>();

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

            if (DataGridBorderX != null)
                width += Cells?.Count ?? 0 + 1;

            return new Size(width, height);
        }

        public void Render(ITablePrinter tablePrinter, List<ColumnX> cellWidths)
        {
            for (int i = 0; i < Cells.Count; i++)
            {
                CellX cellX = Cells[i];
                cellX.Size = new Size(cellWidths[i].Width, Size.Height);
            }

            for (int lineIndex = 0; lineIndex < Size.Height; lineIndex++)
            {
                DataGridBorderX?.RenderRowLeftBorder(tablePrinter);

                for (int columnIndex = 0; columnIndex < Cells.Count; columnIndex++)
                {
                    CellX cellX = Cells[columnIndex];
                    Size cellSize = new Size(cellWidths[columnIndex].Width, Size.Height);
                    cellX.RenderNextLine(tablePrinter, cellSize);

                    bool isLastCell = columnIndex >= Cells.Count - 1;

                    if (isLastCell)
                        DataGridBorderX?.RenderRowRightBorder(tablePrinter);
                    else
                        DataGridBorderX?.RenderRowInsideBorder(tablePrinter);
                }

                tablePrinter.WriteLine();
            }
        }

        public void UpdateColumnsWidths(List<ColumnX> columnsWidths)
        {
            for (int i = 0; i < Cells.Count; i++)
            {
                while (columnsWidths.Count <= i)
                    columnsWidths.Add(new ColumnX());

                Size cellSize = Cells[i].Size;

                if (cellSize.Width > columnsWidths[i].Width)
                    columnsWidths[i].Width = cellSize.Width;
            }
        }

        public static NormalRowX CreateFrom(NormalRow normalRow)
        {
            if (normalRow == null) throw new ArgumentNullException(nameof(normalRow));

            NormalRowX normalRowX = new NormalRowX
            {
                DataGridBorderX = normalRow.ParentDataGrid?.Border?.IsVisible == true
                    ? DataGridBorderX.CreateFrom(normalRow.ParentDataGrid.Border)
                    : null,
                Cells = normalRow
                    .Select(CellX.CreateFrom)
                    .ToList()
            };

            normalRowX.CalculateLayout();

            return normalRowX;
        }
    }
}