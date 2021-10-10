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
    /// Keeps the information about the columns from a table.
    /// It is also responsible to render the column headers.
    /// </summary>
    public class ColumnList : IEnumerable<Column>
    {
        private readonly DataGrid parentDataGrid;
        private readonly List<Column> columns = new List<Column>();

        /// <summary>
        /// Gets the number of columns contained in the current instance.
        /// </summary>
        public int Count => columns.Count;

        /// <summary>
        /// Gets the <see cref="Column"/> at the specified index.
        /// If the index is outside of the bounds of the list, <c>null</c> is returned.
        /// </summary>
        /// <param name="index">The index of the <see cref="Column"/> to return.</param>
        /// <returns>The <see cref="Column"/> at the specified index.</returns>
        public Column this[int index] => index >= 0 && index < columns.Count
            ? columns[index]
            : null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnList"/> class with
        /// the <see cref="DataGrid"/> that owns it.
        /// </summary>
        /// <param name="parentDataGrid">The <see cref="DataGrid"/> that owns the new instance.</param>
        public ColumnList(DataGrid parentDataGrid)
        {
            this.parentDataGrid = parentDataGrid ?? throw new ArgumentNullException(nameof(parentDataGrid));
        }

        /// <summary>
        /// Adds a new <see cref="Column"/> to the end of the list.
        /// </summary>
        /// <param name="columnHeader">The text to be displayed in the header of the column.</param>
        /// <returns>The newly created column.</returns>
        public Column Add(string columnHeader)
        {
            Column column = new Column(columnHeader)
            {
                ParentDataGrid = parentDataGrid
            };
            columns.Add(column);

            return column;
        }

        /// <summary>
        /// Adds a new <see cref="Column"/> to the end of the list.
        /// </summary>
        /// <param name="column">The <see cref="Column"/> instance to be added.</param>
        /// <returns>The newly added column.</returns>
        public Column Add(Column column)
        {
            if (column == null) throw new ArgumentNullException(nameof(column));

            column.ParentDataGrid = parentDataGrid;
            columns.Add(column);

            return column;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="Column"/>s contained by the current instance.
        /// </summary>
        public IEnumerator<Column> GetEnumerator()
        {
            return columns.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}