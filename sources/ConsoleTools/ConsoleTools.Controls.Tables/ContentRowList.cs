// ConsoleTools
// Copyright (C) 2017-2022 Dust in the Wind
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
    /// Contains the list of <see cref="ContentRow"/>s displayed by the table.
    /// </summary>
    public class ContentRowList : IEnumerable<ContentRow>
    {
        private readonly DataGrid parentDataGrid;

        private readonly List<ContentRow> rows = new List<ContentRow>();

        /// <summary>
        /// Gets the number of rows contained in the current instance.
        /// </summary>
        public int Count => rows.Count;

        /// <summary>
        /// Gets the <see cref="ContentRow"/> at the specified index.
        /// If the index is outside of the bounds of the list, <c>null</c> is returned.
        /// </summary>
        /// <param name="rowIndex">The index of the <see cref="ContentRow"/> to return.</param>
        /// <returns>The <see cref="ContentRow"/> at the specified index.</returns>
        public ContentRow this[int rowIndex] => rowIndex >= 0 && rowIndex < rows.Count
            ? rows[rowIndex]
            : null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentRowList"/> class with
        /// the <see cref="DataGrid"/> that owns it.
        /// </summary>
        /// <param name="parentDataGrid">The <see cref="DataGrid"/> that owns the new instance.</param>
        public ContentRowList(DataGrid parentDataGrid)
        {
            this.parentDataGrid = parentDataGrid ?? throw new ArgumentNullException(nameof(parentDataGrid));
        }

        /// <summary>
        /// Adds a new row to the current table.
        /// If successfully added, the <see cref="RowBase.ParentDataGrid"/> property is set,
        /// removing it from any other <see cref="DataGrid"/> instance.
        /// </summary>
        /// <param name="row">The row to be added.</param>
        /// <returns>The added row.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public ContentRow Add(ContentRow row)
        {
            if (row == null) throw new ArgumentNullException(nameof(row));

            row.ParentDataGrid = parentDataGrid;
            rows.Add(row);

            return row;
        }

        /// <summary>
        /// Adds a new row to the current table containing the specified cells.
        /// </summary>
        /// <param name="cells">The list of cells for the new row.</param>
        /// <returns>The newly created row.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public ContentRow Add(IEnumerable<ContentCell> cells)
        {
            if (cells == null) throw new ArgumentNullException(nameof(cells));

            ContentRow row = new ContentRow(cells)
            {
                ParentDataGrid = parentDataGrid
            };
            rows.Add(row);

            return row;
        }

        /// <summary>
        /// Adds a new row to the current table containing the specified cells.
        /// </summary>
        /// <param name="cells">The list of cells for the new row.</param>
        /// <returns>The newly created row.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public ContentRow Add(params ContentCell[] cells)
        {
            if (cells == null) throw new ArgumentNullException(nameof(cells));

            ContentRow row = new ContentRow(cells)
            {
                ParentDataGrid = parentDataGrid
            };
            rows.Add(row);

            return row;
        }

        /// <summary>
        /// Adds a new row to the current table and creates a content cell for each provided string.
        /// </summary>
        /// <param name="cellContents">The text contents for the cells in the new row.</param>
        /// <returns>The newly created row.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public ContentRow Add(IEnumerable<string> cellContents)
        {
            if (cellContents == null) throw new ArgumentNullException(nameof(cellContents));

            ContentRow row = new ContentRow
            {
                ParentDataGrid = parentDataGrid
            };

            foreach (string text in cellContents)
                row.AddCell(new ContentCell(text));

            rows.Add(row);

            return row;
        }

        /// <summary>
        /// Adds a new row to the current table and creates a content cell for each provided string.
        /// </summary>
        /// <param name="cellContents">The text contents for the cells in the new row.</param>
        /// <returns>The newly created row.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public ContentRow Add(params string[] cellContents)
        {
            if (cellContents == null) throw new ArgumentNullException(nameof(cellContents));

            ContentRow row = new ContentRow
            {
                ParentDataGrid = parentDataGrid
            };

            foreach (string text in cellContents)
                row.AddCell(new ContentCell(text));

            rows.Add(row);

            return row;
        }

        /// <summary>
        /// Adds a new row to the current table and creates a content cell for each provided <see cref="MultilineText"/> instance.
        /// </summary>
        /// <param name="cellContents">The list of cell contents of the new row.</param>
        /// <returns>The newly created row.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public ContentRow Add(IEnumerable<MultilineText> cellContents)
        {
            if (cellContents == null) throw new ArgumentNullException(nameof(cellContents));

            ContentRow row = new ContentRow
            {
                ParentDataGrid = parentDataGrid
            };

            foreach (MultilineText text in cellContents)
                row.AddCell(new ContentCell(text));

            rows.Add(row);

            return row;
        }

        /// <summary>
        /// Adds a new row to the current table and creates a content cell for each provided <see cref="MultilineText"/> instance.
        /// </summary>
        /// <param name="cellContents">The list of cell contents of the new row.</param>
        /// <returns>The newly created row.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public ContentRow Add(params MultilineText[] cellContents)
        {
            if (cellContents == null) throw new ArgumentNullException(nameof(cellContents));

            ContentRow row = new ContentRow
            {
                ParentDataGrid = parentDataGrid
            };

            foreach (MultilineText text in cellContents)
                row.AddCell(new ContentCell(text));

            rows.Add(row);

            return row;
        }

        /// <summary>
        /// Adds a new row to the current table and creates a content cell for each provided <see cref="object"/>.
        /// </summary>
        /// <param name="cellContents">The list of cell contents of the new row.</param>
        /// <returns>The newly created row.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public ContentRow Add(IEnumerable<object> cellContents)
        {
            if (cellContents == null) throw new ArgumentNullException(nameof(cellContents));

            ContentRow row = new ContentRow
            {
                ParentDataGrid = parentDataGrid
            };

            foreach (object cellContent in cellContents)
                row.AddCell(new ContentCell(cellContent));

            rows.Add(row);

            return row;
        }

        /// <summary>
        /// Adds a new row to the current table and creates a content cell for each provided <see cref="object"/>.
        /// </summary>
        /// <param name="cellContents">The list of cell contents of the new row.</param>
        /// <returns>The newly created row.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public ContentRow Add(params object[] cellContents)
        {
            if (cellContents == null) throw new ArgumentNullException(nameof(cellContents));

            ContentRow row = new ContentRow
            {
                ParentDataGrid = parentDataGrid
            };

            foreach (object cellContent in cellContents)
                row.AddCell(new ContentCell(cellContent));

            rows.Add(row);

            return row;
        }

        /// <summary>
        /// Adds multiple rows to the current table.
        /// If a row is successfully added, the <see cref="RowBase.ParentDataGrid"/> property is set,
        /// removing it from any other <see cref="DataGrid"/> instance.
        /// </summary>
        /// <param name="rows">The collection of rows to be added.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void AddRange(IEnumerable<ContentRow> rows)
        {
            if (rows == null) throw new ArgumentNullException(nameof(rows));

            foreach (ContentRow contentRow in rows)
            {
                contentRow.ParentDataGrid = parentDataGrid;
                this.rows.Add(contentRow);
            }
        }

        /// <summary>
        /// Adds multiple rows to the current table.
        /// If a row is successfully added, the <see cref="RowBase.ParentDataGrid"/> property is set,
        /// removing it from any other <see cref="DataGrid"/> instance.
        /// </summary>
        /// <param name="rows">The collection of rows to be added.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void AddRange(params ContentRow[] rows)
        {
            if (rows == null) throw new ArgumentNullException(nameof(rows));

            foreach (ContentRow contentRow in rows)
            {
                contentRow.ParentDataGrid = parentDataGrid;
                this.rows.Add(contentRow);
            }
        }

        /// <summary>
        /// Removes the row at the specified index.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">index is less than 0.-or-index is equal to or greater than the number of rows.</exception>
        public void RemoveAt(int index)
        {
            rows.RemoveAt(index);
        }

        /// <summary>
        /// Removes the first occurrence of the <see cref="ContentRow"/> instance from the list.
        /// </summary>
        /// <returns><c>true</c> if item is successfully removed; otherwise, <c>false</c>.
        /// This method also returns <c>false</c> if item was not found in the list.</returns>
        public bool Remove(ContentRow row)
        {
            return rows.Remove(row);
        }

        /// <summary>
        /// Removes all the rows from the current instance.
        /// </summary>
        public void Clear()
        {
            rows.Clear();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="ContentRow"/>s contained by the current instance.
        /// </summary>
        public IEnumerator<ContentRow> GetEnumerator()
        {
            return rows.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}