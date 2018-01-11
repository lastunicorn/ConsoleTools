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

using System.Collections.Generic;
using System.Linq;

namespace DustInTheWind.ConsoleTools.TabularData
{
    internal class DataGridX
    {
        private readonly bool displayBorder;
        public List<TitleRowX> TitleRows { get; } = new List<TitleRowX>();

        private DataRowX headerRow;
        private readonly List<DataRowX> dataRows = new List<DataRowX>();
        private DataRowX currentRow;
        private int minWidth;

        /// <summary>
        /// Gets or sets a list containing the calculated widths of the columns.
        /// </summary>
        public List<int> ColumnsWidths { get; } = new List<int>();

        public int TotalWidth { get; private set; }

        public List<int> RowsHeights => dataRows
            .Select(x => x.Size.Height)
            .ToList();

        /// <summary>
        /// Gets the height of the header calculated by the current instance.
        /// </summary>
        public int CalculatedHeaderRowHeight => headerRow?.Size.Height ?? 0;

        public int MinWidth
        {
            get { return minWidth; }
            set
            {
                minWidth = value;

                if (TotalWidth < minWidth)
                    TotalWidth = minWidth;
            }
        }

        public DataGridX(bool displayBorder)
        {
            this.displayBorder = displayBorder;
        }

        public void AddTitleRow(TitleRow title)
        {
            int titleRowWidth = 0;

            if (displayBorder)
                titleRowWidth += 1;

            Size cellSize = title.TitleCell.CalculateDimensions();

            titleRowWidth += cellSize.Width;

            if (displayBorder)
                titleRowWidth += 1;

            if (TotalWidth < titleRowWidth)
                TotalWidth = titleRowWidth;
        }

        public void AddHeaderRow(ColumnList columns)
        {
            headerRow = new DataRowX(displayBorder);

            foreach (Column column in columns)
                AddHeaderCell(column);

            if (TotalWidth < headerRow.Size.Width)
                TotalWidth = headerRow.Size.Width;
        }

        private void AddHeaderCell(Column column)
        {
            int j = headerRow.NextIndex;

            while (ColumnsWidths.Count <= j)
                ColumnsWidths.Add(0);

            Size cellSize = column.HeaderCell.CalculateDimensions();

            if (cellSize.Width > ColumnsWidths[j])
            {
                ColumnsWidths[j] = cellSize.Width;
                DataCellX cell = new DataCellX { Size = new Size(cellSize.Width, cellSize.Height) };
                headerRow.AddCell(cell);
            }
            else
            {
                DataCellX cell = new DataCellX { Size = new Size(ColumnsWidths[j], cellSize.Height) };
                headerRow.AddCell(cell);
            }
        }

        public void AddDataRow(DataRow dataRow)
        {
            currentRow = new DataRowX(displayBorder);

            for (int i = 0; i < dataRow.CellCount; i++)
                AddDataCell(dataRow[i]);

            if (TotalWidth < currentRow.Size.Width)
                TotalWidth = currentRow.Size.Width;

            dataRows.Add(currentRow);
            currentRow = null;
        }

        private void AddDataCell(DataCell dataCell)
        {
            int j = currentRow.NextIndex;

            while (ColumnsWidths.Count <= j)
                ColumnsWidths.Add(0);

            Size cellSize = dataCell.CalculateDimensions();

            if (cellSize.Width > ColumnsWidths[j])
            {
                ColumnsWidths[j] = cellSize.Width;
                DataCellX cell = new DataCellX { Size = new Size(cellSize.Width, cellSize.Height) };
                currentRow.AddCell(cell);
            }
            else
            {
                DataCellX cell = new DataCellX { Size = new Size(ColumnsWidths[j], cellSize.Height) };
                currentRow.AddCell(cell);
            }
        }

        public void Clear()
        {
            headerRow = null;
            currentRow = null;

            dataRows.Clear();

            TotalWidth = minWidth;

            ColumnsWidths.Clear();
            RowsHeights.Clear();
        }

        public void ExpandColumnsIfNeeded()
        {
            if (ColumnsWidths.Count == 0)
                return;

            int columnsTotalWidth = ColumnsWidths.Sum();
            if (displayBorder)
                columnsTotalWidth += ColumnsWidths.Count + 1;

            if (columnsTotalWidth < TotalWidth)
            {
                int diff = TotalWidth - columnsTotalWidth;
                int colCount = ColumnsWidths.Count;

                for (int i = 0; i < diff; i++)
                    ColumnsWidths[i % colCount]++;
            }
        }
    }
}