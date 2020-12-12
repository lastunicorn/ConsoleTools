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
    internal class DataRowX
    {
        private readonly DataRow dataRow;
        private readonly DataGridBorderX dataGridBorderX;
        private readonly List<CellX> cells = new List<CellX>();

        public Size Size { get; private set; }

        public DataRowX(DataRow dataRow, DataGridBorderX dataGridBorderX)
        {
            this.dataRow = dataRow ?? throw new ArgumentNullException(nameof(dataRow));
            this.dataGridBorderX = dataGridBorderX ?? throw new ArgumentNullException(nameof(dataGridBorderX));

            CreateCells();
        }

        private void CreateCells()
        {
            IEnumerable<CellX> dataCellXes = dataRow
                .Select(CellX.CreateFrom);

            foreach (CellX dataCellX in dataCellXes)
            {
                AddCellToSize(dataCellX);
                cells.Add(dataCellX);
            }
        }

        private void AddCellToSize(CellX cell)
        {
            int initialCount = cells.Count;

            int width = initialCount == 0 && dataGridBorderX.IsVisible
                ? 1
                : Size.Width;

            width += cell.Size.Width;

            if (dataGridBorderX.IsVisible)
                width++;

            int height = Size.Height < cell.Size.Height
                ? cell.Size.Height
                : Size.Height;

            Size = new Size(width, height);
        }

        public void Render(ITablePrinter tablePrinter, List<int> cellWidths)
        {
            for (int i = 0; i < cells.Count; i++)
            {
                CellX cellX = cells[i];
                cellX.Size = new Size(cellWidths[i], Size.Height);
            }

            for (int lineIndex = 0; lineIndex < Size.Height; lineIndex++)
            {
                dataGridBorderX.RenderRowLeftBorder(tablePrinter);

                for (int columnIndex = 0; columnIndex < cells.Count; columnIndex++)
                {
                    CellX cellX = cells[columnIndex];
                    cellX.RenderNextLine(tablePrinter);

                    bool isLastCell = columnIndex >= cells.Count - 1;

                    if (isLastCell)
                        dataGridBorderX.RenderRowRightBorder(tablePrinter);
                    else
                        dataGridBorderX.RenderRowInsideBorder(tablePrinter);
                }

                tablePrinter.WriteLine();
            }
        }

        public void UpdateColumnsWidths(List<int> columnsWidths)
        {
            for (int i = 0; i < cells.Count; i++)
            {
                while (columnsWidths.Count <= i)
                    columnsWidths.Add(0);

                Size cellSize = cells[i].Size;

                if (cellSize.Width > columnsWidths[i])
                    columnsWidths[i] = cellSize.Width;
            }
        }
    }
}