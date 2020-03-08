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
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DustInTheWind.ConsoleTools.Controls.Tables
{
    /// <summary>
    /// Represents a row in the <see cref="DataGrid"/> class.
    /// </summary>
    public class DataRow
    {
        /// <summary>
        /// Gets the list of cells contained by the row.
        /// </summary>
        private readonly List<DataCell> cells = new List<DataCell>();

        /// <summary>
        /// Gets or sets the <see cref="DataGrid"/> instance that contains the current <see cref="DataRow"/> instance.
        /// </summary>
        public DataGrid ParentDataGrid { get; internal set; }

        /// <summary>
        /// Gets the number of cells contained by the current instance.
        /// </summary>
        public int CellCount => cells.Count;

        /// <summary>
        /// Gets or sets the horizontal alignment for the content of the cells contained by the current instance of the <see cref="DataRow"/>.
        /// </summary>
        public HorizontalAlignment CellHorizontalAlignment { get; set; }

        /// <summary>
        /// Gets or sets the padding applied to the left side of every cell.
        /// </summary>
        public int? PaddingLeft { get; set; }

        /// <summary>
        /// Gets or sets the padding applied to the right side of every cell.
        /// </summary>
        public int? PaddingRight { get; set; }

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
        public DataRow(IEnumerable<DataCell> cells)
        {
            if (cells == null)
                return;

            foreach (DataCell cell in cells)
                AddCell(cell);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataRow"/> class with
        /// the list of cells.
        /// </summary>
        /// <param name="cells">The list of cells that will be contained by the new row.</param>
        public DataRow(params DataCell[] cells)
        {
            if (cells == null)
                return;

            foreach (DataCell cell in cells)
                AddCell(cell);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataRow"/> class with
        /// the list of texts representing the cells content.
        /// </summary>
        /// <param name="cellContents">The list of texts that will be placed in cells.</param>
        public DataRow(IEnumerable<string> cellContents)
        {
            if (cellContents == null)
                return;

            foreach (string cell in cellContents)
                AddCell(cell);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataRow"/> class with
        /// the list of texts representing the cells content.
        /// </summary>
        /// <param name="cellContents">The list of texts that will be placed in cells.</param>
        public DataRow(params string[] cellContents)
        {
            if (cellContents == null)
                return;

            foreach (string cell in cellContents)
                AddCell(cell);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataRow"/> class with
        /// the list of <see cref="MultilineText"/> objects representing the cells content.
        /// </summary>
        /// <param name="cellContents">The list of <see cref="MultilineText"/> objects that will be placed in cells.</param>
        public DataRow(IEnumerable<MultilineText> cellContents)
        {
            if (cellContents == null)
                return;

            foreach (MultilineText cell in cellContents)
                AddCell(cell);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataRow"/> class with
        /// the list of <see cref="MultilineText"/> objects representing the cells content.
        /// </summary>
        /// <param name="cellContents">The list of <see cref="MultilineText"/> objects that will be placed in cells.</param>
        public DataRow(params MultilineText[] cellContents)
        {
            if (cellContents == null)
                return;

            foreach (MultilineText cell in cellContents)
                AddCell(cell);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataRow"/> class with
        /// the list of objects representing the cells content.
        /// </summary>
        /// <param name="cellContents">The list of objects that will be placed in cells.</param>
        public DataRow(IEnumerable cellContents)
        {
            if (cellContents == null)
                return;

            foreach (object cell in cellContents)
                AddCell(cell);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataRow"/> class with
        /// the list of objects representing the cells content.
        /// </summary>
        /// <param name="cellContents">The list of objects that will be placed in cells.</param>
        public DataRow(params object[] cellContents)
        {
            if (cellContents == null)
                return;

            foreach (object cell in cellContents)
                AddCell(cell);
        }

        /// <summary>
        /// Adds a new cell to the current instance of <see cref="DataRow"/>.
        /// </summary>
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
        /// Adds a new cell to the current instance of <see cref="DataRow"/>.
        /// </summary>
        public void AddCell(string cellContent)
        {
            DataCell newCell = new DataCell
            {
                ParentRow = this
            };

            if (cellContent != null)
                newCell.Content = new MultilineText(cellContent);

            cells.Add(newCell);
        }

        /// <summary>
        /// Adds a new cell to the current instance of <see cref="DataRow"/>.
        /// </summary>
        public void AddCell(MultilineText cellContent)
        {
            DataCell newCell = new DataCell
            {
                ParentRow = this
            };

            if (cellContent != null)
                newCell.Content = cellContent;

            cells.Add(newCell);
        }

        /// <summary>
        /// Adds a new cell to the current instance of <see cref="DataRow"/>.
        /// </summary>
        public void AddCell(object cellContent)
        {
            DataCell newCell = new DataCell
            {
                ParentRow = this
            };

            if (cellContent != null)
                newCell.Content = new MultilineText(cellContent.ToString());

            cells.Add(newCell);
        }

        /// <summary>
        /// Gets or sets the cell at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the cell to get or set.</param>
        /// <returns>The cell at the specified index.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public DataCell this[int index]
        {
            get => cells[index];
            set => cells[index] = value;
        }

        /// <summary>
        /// Returns the index of the specified cell or <c>null</c> if the <see cref="DataCell"/> instance
        /// is not found in the current <see cref="DataRow"/> instance.
        /// </summary>
        public int? IndexOfCell(DataCell cell)
        {
            int indexOfCell = cells.IndexOf(cell);
            return indexOfCell == -1 ? (int?) null : indexOfCell;
        }

        /// <summary>
        /// Renders the row current row.
        /// </summary>
        /// <param name="tablePrinter">The destination where the current instance must be rendered.</param>
        /// <param name="cellWidths">The widths of each cell that must be rendered.</param>
        /// <param name="height">The height of the row to be rendered. If there are not enough text lines
        /// in the content of a cell, spaces are written instead.</param>
        public void Render(ITablePrinter tablePrinter, List<int> cellWidths, int height)
        {
            List<List<string>> cellContents = cells
                .Select((x, i) =>
                {
                    Size size = new Size(cellWidths[i], height);
                    return x.Render(size).ToList();
                })
                .ToList();

            BorderTemplate borderTemplate = ParentDataGrid?.BorderTemplate;

            bool displayBorder = borderTemplate != null && ParentDataGrid?.DisplayBorder == true;

            for (int rowLineIndex = 0; rowLineIndex < height; rowLineIndex++)
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