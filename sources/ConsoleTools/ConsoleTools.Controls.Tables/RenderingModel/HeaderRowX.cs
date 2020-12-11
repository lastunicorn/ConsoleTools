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
    internal class HeaderRowX
    {
        private readonly bool hasBorder;
        private readonly HeaderRow headerRow;
        private readonly List<CellX> cells = new List<CellX>();

        public Size Size { get; private set; }

        public HeaderRowX(HeaderRow headerRow, bool hasBorder)
        {
            this.headerRow = headerRow ?? throw new ArgumentNullException(nameof(headerRow));
            this.hasBorder = hasBorder;

            CreateCells();
        }

        private void CreateCells()
        {
            IEnumerable<CellX> dataCellXes = headerRow
                .Select(x => new CellX(x));

            foreach (CellX dataCellX in dataCellXes)
            {
                AddCellToSize(dataCellX);
                cells.Add(dataCellX);
            }
        }

        private void AddCellToSize(CellX cell)
        {
            int initialCount = cells.Count;

            int width = initialCount == 0 && hasBorder
                ? 1
                : Size.Width;

            width += cell.Size.Width;

            if (hasBorder)
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

                Size size = new Size(cellWidths[i], Size.Height);
                cellX.InitializeRendering(size);
            }

            BorderTemplate borderTemplate = headerRow.ParentDataGrid?.BorderTemplate;
            bool displayBorder = borderTemplate != null && headerRow.ParentDataGrid?.DisplayBorder == true;

            for (int lineIndex = 0; lineIndex < Size.Height; lineIndex++)
            {
                if (displayBorder)
                    tablePrinter.WriteBorder(borderTemplate.Left);

                for (int columnIndex = 0; columnIndex < cells.Count; columnIndex++)
                {
                    CellX cellX = cells[columnIndex];
                    cellX.RenderNextLine(tablePrinter);

                    if (displayBorder)
                    {
                        char cellBorderRight = columnIndex < cells.Count - 1
                            ? borderTemplate.Vertical
                            : borderTemplate.Right;

                        tablePrinter.WriteBorder(cellBorderRight);
                    }
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