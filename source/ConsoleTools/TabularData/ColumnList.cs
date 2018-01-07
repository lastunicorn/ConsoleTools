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
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DustInTheWind.ConsoleTools.TabularData
{
    /// <summary>
    /// Represents the columns from a table.
    /// </summary>
    public class ColumnList : IEnumerable<Column>
    {
        private readonly Table parentTable;

        private readonly List<Column> columns = new List<Column>();

        /// <summary>
        /// Gets the number of columns contained in the current instance.
        /// </summary>
        public int Count => columns.Count;

        /// <summary>
        /// Gets the <see cref="Column"/> at the specified index.
        /// If the index is outside of the bounds of the list, <c>null</c> is returned.
        /// </summary>
        /// <param name="columnIndex">The index of the <see cref="Column"/> to return.</param>
        /// <returns>The <see cref="Column"/> at the specified index.</returns>
        public Column this[int columnIndex] => columnIndex >= 0 && columnIndex < columns.Count
                ? columns[columnIndex]
                : null;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnList"/> class with
        /// the table that owns it.
        /// </summary>
        /// <param name="parentTable">The table that owns the new instance.</param>
        public ColumnList(Table parentTable)
        {
            if (parentTable == null) throw new ArgumentNullException(nameof(parentTable));
            this.parentTable = parentTable;
        }

        /// <summary>
        /// Adds a new <see cref="Column"/> to the end of the list.
        /// </summary>
        /// <param name="column">The <see cref="Column"/> instance to be added.</param>
        public void Add(Column column)
        {
            if (column == null) throw new ArgumentNullException(nameof(column));

            column.ParentTable = parentTable;
            columns.Add(column);
        }

        public void RenderHeaderRow(ITablePrinter tablePrinter, List<int> cellWidths, int rowHeight)
        {
            List<List<string>> cellContents = columns
                .Select((x, i) =>
                {
                    int columnWidth = cellWidths[i];
                    return x.RenderHeader(columnWidth, rowHeight);
                })
                .ToList();

            bool displayBorder = parentTable?.DisplayBorder ?? true;
            BorderTemplate borderTemplate = parentTable?.BorderTemplate;

            for (int headerLineIndex = 0; headerLineIndex < rowHeight; headerLineIndex++)
            {
                if (displayBorder && borderTemplate != null)
                    tablePrinter.WriteBorder(borderTemplate.Left);

                for (int columnIndex = 0; columnIndex < columns.Count; columnIndex++)
                {
                    string content = cellContents[columnIndex][headerLineIndex];
                    tablePrinter.WriteHeader(content);

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