// ConsoleTools
// Copyright (C) 2017-2018 Dust in the Wind
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
// Bugs or fearure requests
// --------------------------------------------------------------------------------
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new

using System;
using System.Collections.Generic;
using System.Linq;

namespace DustInTheWind.ConsoleTools.TabularData
{
    /// <summary>
    /// Represents a row in the <see cref="Table"/> class.
    /// </summary>
    public class DataRow
    {
        /// <summary>
        /// Gets the list of cells contained by the row.
        /// </summary>
        private readonly List<DataCell> cells = new List<DataCell>();

        /// <summary>
        /// Gets or sets the <see cref="Table"/> instance that contains the current <see cref="DataRow"/> instance.
        /// </summary>
        public Table ParentTable { get; set; }

        /// <summary>
        /// Gets the number of cells contained by the current instance.
        /// </summary>
        public int CellCount => cells.Count;

        /// <summary>
        /// Gets or sets the horizontal alignment for the content of the cells contained by the current instance of the <see cref="DataRow"/>.
        /// </summary>
        public HorizontalAlignment CellHorizontalAlignment { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataRow"/> class with default values.
        /// </summary>
        public DataRow()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataRow"/> class with
        /// the list of cells.
        /// </summary>
        /// <param name="cells">The list of cells that will be contained by the new row.</param>
        public DataRow(DataCell[] cells)
        {
            if (cells == null)
                return;

            foreach (DataCell cell in cells)
                AddCell(cell);
        }

        /// <summary>
        /// Adds a new cell to the current instace of <see cref="DataRow"/>.
        /// </summary>
        /// <param name="cell"></param>
        public void AddCell(DataCell cell)
        {
            if (cell == null)
            {
                DataCell newCell = new DataCell
                {
                    ParentRow = this
                };
                cells.Add(newCell);
            }
            else
            {
                cell.ParentRow = this;
                cells.Add(cell);
            }
        }

        /// <summary>
        /// Gets or sets the cell at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the cell to get or set.</param>
        /// <returns>The cell at the specified index.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public DataCell this[int index]
        {
            get { return cells[index]; }
            set { cells[index] = value; }
        }

        /// <summary>
        /// Returns the index of the specified cell or <c>null</c> if the <see cref="DataCell"/> instance
        /// is not found in the current <see cref="DataRow"/> instance.
        /// </summary>
        public int? IndexOfCell(DataCell cell)
        {
            int indexOfCell = cells.IndexOf(cell);
            return indexOfCell == -1 ? (int?)null : indexOfCell;
        }

        public void Render(ITablePrinter tablePrinter, List<int> cellWidths, int minHeight)
        {
            List<List<string>> cellContents = cells
                .Select((x, i) =>
                {
                    int cellWidth = cellWidths[i];
                    return x.Render(cellWidth, minHeight);
                })
                .ToList();

            BorderTemplate borderTemplate = ParentTable?.BorderTemplate;

            bool displayBorder = borderTemplate != null && ParentTable?.DisplayBorder == true;

            for (int rowLineIndex = 0; rowLineIndex < minHeight; rowLineIndex++)
            {
                if (displayBorder)
                    tablePrinter.WriteBorder(borderTemplate.Left);

                for (int columnIndex = 0; columnIndex < cells.Count; columnIndex++)
                {
                    string content = cellContents[columnIndex][rowLineIndex];
                    tablePrinter.WriteNormal(content);

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
    }
}