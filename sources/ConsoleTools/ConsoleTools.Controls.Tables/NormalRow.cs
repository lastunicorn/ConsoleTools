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
using System.Collections;
using System.Collections.Generic;

namespace DustInTheWind.ConsoleTools.Controls.Tables
{
    /// <summary>
    /// Represents a row in the <see cref="DataGrid"/> class.
    /// </summary>
    public class NormalRow : RowBase
    {
        /// <summary>
        /// Gets the list of cells contained by the row.
        /// </summary>
        private readonly List<NormalCell> cells = new List<NormalCell>();

        /// <summary>
        /// Gets the number of cells contained by the current instance.
        /// </summary>
        public override int CellCount => cells.Count;

        /// <summary>
        /// Gets or sets the cell at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the cell to get or set.</param>
        /// <returns>The cell at the specified index.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public NormalCell this[int index]
        {
            get => cells[index];
            set => cells[index] = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NormalRow"/> class with default values.
        /// </summary>
        public NormalRow()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NormalRow"/> class with
        /// the list of cells.
        /// </summary>
        /// <param name="cells">The list of cells that will be contained by the new row.</param>
        public NormalRow(IEnumerable<NormalCell> cells)
        {
            if (cells == null)
                return;

            foreach (NormalCell cell in cells)
                AddCell(cell);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NormalRow"/> class with
        /// the list of cells.
        /// </summary>
        /// <param name="cells">The list of cells that will be contained by the new row.</param>
        public NormalRow(params NormalCell[] cells)
        {
            if (cells == null)
                return;

            foreach (NormalCell cell in cells)
                AddCell(cell);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NormalRow"/> class with
        /// the list of texts representing the cells content.
        /// </summary>
        /// <param name="cellContents">The list of texts that will be placed in cells.</param>
        public NormalRow(IEnumerable<string> cellContents)
        {
            if (cellContents == null)
                return;

            foreach (string cell in cellContents)
                AddCell(cell);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NormalRow"/> class with
        /// the list of texts representing the cells content.
        /// </summary>
        /// <param name="cellContents">The list of texts that will be placed in cells.</param>
        public NormalRow(params string[] cellContents)
        {
            if (cellContents == null)
                return;

            foreach (string cell in cellContents)
                AddCell(cell);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NormalRow"/> class with
        /// the list of <see cref="MultilineText"/> objects representing the cells content.
        /// </summary>
        /// <param name="cellContents">The list of <see cref="MultilineText"/> objects that will be placed in cells.</param>
        public NormalRow(IEnumerable<MultilineText> cellContents)
        {
            if (cellContents == null)
                return;

            foreach (MultilineText cell in cellContents)
                AddCell(cell);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NormalRow"/> class with
        /// the list of <see cref="MultilineText"/> objects representing the cells content.
        /// </summary>
        /// <param name="cellContents">The list of <see cref="MultilineText"/> objects that will be placed in cells.</param>
        public NormalRow(params MultilineText[] cellContents)
        {
            if (cellContents == null)
                return;

            foreach (MultilineText cell in cellContents)
                AddCell(cell);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NormalRow"/> class with
        /// the list of objects representing the cells content.
        /// </summary>
        /// <param name="cellContents">The list of objects that will be placed in cells.</param>
        public NormalRow(IEnumerable cellContents)
        {
            if (cellContents == null)
                return;

            foreach (object cell in cellContents)
                AddCell(cell);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NormalRow"/> class with
        /// the list of objects representing the cells content.
        /// </summary>
        /// <param name="cellContents">The list of objects that will be placed in cells.</param>
        public NormalRow(params object[] cellContents)
        {
            if (cellContents == null)
                return;

            foreach (object cell in cellContents)
                AddCell(cell);
        }

        /// <summary>
        /// Adds a new cell to the current instance of <see cref="NormalRow"/>.
        /// </summary>
        public void AddCell(NormalCell cell)
        {
            if (cell == null)
            {
                NormalCell newCell = new NormalCell
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
        /// Adds a new cell to the current instance of <see cref="NormalRow"/>.
        /// </summary>
        public void AddCell(string cellContent)
        {
            NormalCell newCell = new NormalCell
            {
                ParentRow = this
            };

            if (cellContent != null)
                newCell.Content = new MultilineText(cellContent);

            cells.Add(newCell);
        }

        /// <summary>
        /// Adds a new cell to the current instance of <see cref="NormalRow"/>.
        /// </summary>
        public void AddCell(MultilineText cellContent)
        {
            NormalCell newCell = new NormalCell
            {
                ParentRow = this
            };

            if (cellContent != null)
                newCell.Content = cellContent;

            cells.Add(newCell);
        }

        /// <summary>
        /// Adds a new cell to the current instance of <see cref="NormalRow"/>.
        /// </summary>
        public void AddCell(object cellContent)
        {
            NormalCell newCell = new NormalCell
            {
                ParentRow = this
            };

            if (cellContent != null)
                newCell.Content = new MultilineText(cellContent.ToString());

            cells.Add(newCell);
        }

        /// <summary>
        /// Returns the index of the specified cell or <c>null</c> if the <see cref="NormalCell"/> instance
        /// is not found in the current <see cref="NormalRow"/> instance.
        /// </summary>
        public int? IndexOfCell(NormalCell cell)
        {
            int indexOfCell = cells.IndexOf(cell);
            return indexOfCell == -1 ? (int?)null : indexOfCell;
        }

        public override IEnumerator<CellBase> GetEnumerator()
        {
            return cells.GetEnumerator();
        }
    }
}