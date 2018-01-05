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
    public class ColumnList : IEnumerable<Column>
    {
        private readonly Table parentTable;

        private readonly List<Column> columns = new List<Column>();

        public int Count => columns.Count;

        public Column this[int columnIndex] => columnIndex >= 0 && columnIndex < columns.Count
                ? columns[columnIndex]
                : null;
        
        public ColumnList(Table parentTable)
        {
            if (parentTable == null) throw new ArgumentNullException(nameof(parentTable));
            this.parentTable = parentTable;
        }

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