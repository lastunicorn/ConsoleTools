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

using System;
using System.Collections.Generic;

namespace DustInTheWind.ConsoleTools.TabularData
{
    /// <summary>
    /// Represents a row in the <see cref="Table"/> class.
    /// </summary>
    public class Row
    {
        /// <summary>
        /// Gets the list of cells contained by the row.
        /// </summary>
        private readonly List<Cell> cells = new List<Cell>();

        public Table ParentTable { get; set; }

        public int CellCount => cells.Count;

        /// <summary>
        /// Initializes a new instance of the <see cref="Row"/> class with default values.
        /// </summary>
        public Row()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Row"/> class with
        /// the list of cells.
        /// </summary>
        /// <param name="cells">The list of cells that will be contained by the new row.</param>
        public Row(Cell[] cells)
        {
            if (cells != null)
                AddCells(cells);
        }

        private void AddCells(IEnumerable<Cell> cells)
        {
            foreach (Cell cell in cells)
                AddCell(cell);
        }

        public void AddCell(Cell cell)
        {
            if (cell == null)
            {
                Cell newCell = new Cell(null)
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
        public Cell this[int index]
        {
            get { return cells[index]; }
            set { cells[index] = value; }
        }
    }
}
