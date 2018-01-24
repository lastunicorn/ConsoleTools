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
// Bugs or feature requests
// --------------------------------------------------------------------------------
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DustInTheWind.ConsoleTools.TabularData
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
        /// the table that owns it.
        /// </summary>
        /// <param name="parentDataGrid">The table that owns the new instance.</param>
        public ColumnList(DataGrid parentDataGrid)
        {
            if (parentDataGrid == null) throw new ArgumentNullException(nameof(parentDataGrid));
            this.parentDataGrid = parentDataGrid;
        }

        /// <summary>
        /// Adds a new <see cref="Column"/> to the end of the list.
        /// </summary>
        /// <param name="column">The <see cref="Column"/> instance to be added.</param>
        public void Add(Column column)
        {
            if (column == null) throw new ArgumentNullException(nameof(column));

            column.ParentDataGrid = parentDataGrid;
            columns.Add(column);
        }

        public void RenderHeaderRow(ITablePrinter tablePrinter, List<int> cellWidths, int rowHeight)
        {
            // Get cells content.
            List<List<string>> cellContents = columns
                .Select((x, i) =>
                {
                    Size size = new Size(cellWidths[i], rowHeight);
                    return x.HeaderCell.Render(size).ToList();
                })
                .ToList();

            bool displayBorder = parentDataGrid?.DisplayBorder ?? true;
            BorderTemplate borderTemplate = parentDataGrid?.BorderTemplate;

            for (int headerLineIndex = 0; headerLineIndex < rowHeight; headerLineIndex++)
            {
                // Write left border.
                if (displayBorder && borderTemplate != null)
                    tablePrinter.WriteBorder(borderTemplate.Left);

                for (int columnIndex = 0; columnIndex < columns.Count; columnIndex++)
                {
                    // Write cell content.
                    string content = cellContents[columnIndex][headerLineIndex];
                    tablePrinter.WriteHeader(content);

                    // Write intermediate or right border.
                    if (displayBorder && borderTemplate != null)
                    {
                        char cellBorderRight = columnIndex < columns.Count - 1
                            ? borderTemplate.Vertical
                            : borderTemplate.Right;

                        tablePrinter.WriteBorder(cellBorderRight);
                    }
                }

                tablePrinter.WriteLine();
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="Column"/>s containined by the current instance.
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