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

using System.Collections.Generic;
using System.Linq;

namespace DustInTheWind.ConsoleTools.TabularData.RenderingModel
{
    internal class DataGridX
    {
        private readonly bool displayBorder;

        private TitleRowX titleRowX;
        private HeaderRowX headerRowX;
        private readonly List<DataRowX> dataRowXs = new List<DataRowX>();
        private DataRowX currentRowX;
        private int minWidth;

        /// <summary>
        /// Gets or sets a list containing the calculated widths of the columns.
        /// </summary>
        public List<int> ColumnsWidths { get; } = new List<int>();

        public int TotalWidth { get; private set; }

        public TitleTopBorder TitleTopBorder { get; set; }
        public HeaderTopBorder HeaderTopBorder { get; set; }
        public DataTopBorder DataTopBorder { get; set; }

        public TitleHeaderSeparator TitleHeaderSeparator { get; set; }
        public TitleDataSeparator TitleDataSeparator { get; set; }
        public TitleBottomBorder TitleBottomBorder { get; set; }

        public HeaderDataSeparator HeaderDataSeparator { get; set; }
        public HeaderBottomBorder HeaderBottomBorder { get; set; }

        public DataDataSeparator DataDataSeparator { get; set; }
        public BottomBorderData DataBottomBorder { get; set; }

        /// <summary>
        /// Gets the height of the header calculated by the current instance.
        /// </summary>
        public int ColumnHeaderRowHeight => headerRowX?.Size.Height ?? 0;

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

        public bool IsTitleVisible => titleRowX != null;
        public bool AreColumnsHeaderVisible => headerRowX != null;
        public bool AreDataRowsVisible => dataRowXs.Count > 0;

        public DataGridX(bool displayBorder)
        {
            this.displayBorder = displayBorder;
        }

        public void AddTitleRow(TitleRow titleRow)
        {
            titleRowX = new TitleRowX(titleRow);

            if (TotalWidth < titleRowX.Size.Width)
                TotalWidth = titleRowX.Size.Width;
        }

        public void AddHeaderRow(ColumnList columns)
        {
            if (columns == null || columns.Count == 0)
                return;

            headerRowX = new HeaderRowX(displayBorder)
            {
                Columns = columns
            };

            foreach (Column column in columns)
                AddHeaderCell(column);

            if (headerRowX.Size.Height == 0)
            {
                headerRowX = null;
                return;
            }

            if (TotalWidth < headerRowX.Size.Width)
                TotalWidth = headerRowX.Size.Width;
        }

        private void AddHeaderCell(Column column)
        {
            int j = headerRowX.NextIndex;

            while (ColumnsWidths.Count <= j)
                ColumnsWidths.Add(0);

            Size cellSize = column.HeaderCell.CalculatePreferedSize();

            if (cellSize.Width > ColumnsWidths[j])
            {
                ColumnsWidths[j] = cellSize.Width;
                DataCellX cell = new DataCellX { Size = new Size(cellSize.Width, cellSize.Height) };
                headerRowX.AddCell(cell);
            }
            else
            {
                DataCellX cell = new DataCellX { Size = new Size(ColumnsWidths[j], cellSize.Height) };
                headerRowX.AddCell(cell);
            }
        }

        public void AddDataRow(DataRow dataRow)
        {
            currentRowX = new DataRowX(displayBorder, dataRow);

            for (int i = 0; i < dataRow.CellCount; i++)
                AddDataCell(dataRow[i]);

            if (TotalWidth < currentRowX.Size.Width)
                TotalWidth = currentRowX.Size.Width;

            dataRowXs.Add(currentRowX);
            currentRowX = null;
        }

        private void AddDataCell(DataCell dataCell)
        {
            int j = currentRowX.NextIndex;

            while (ColumnsWidths.Count <= j)
                ColumnsWidths.Add(0);

            Size cellSize = dataCell.CalculatePreferedSize();

            if (cellSize.Width > ColumnsWidths[j])
            {
                ColumnsWidths[j] = cellSize.Width;
                DataCellX cell = new DataCellX { Size = new Size(cellSize.Width, cellSize.Height) };
                currentRowX.AddCell(cell);
            }
            else
            {
                DataCellX cell = new DataCellX { Size = new Size(ColumnsWidths[j], cellSize.Height) };
                currentRowX.AddCell(cell);
            }
        }

        public void Clear()
        {
            headerRowX = null;
            currentRowX = null;

            dataRowXs.Clear();

            TotalWidth = minWidth;

            ColumnsWidths.Clear();
        }

        public void MakeFinalAdjustments()
        {
            if (ColumnsWidths.Count > 0)
            {
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

                if (titleRowX?.Size.Width < TotalWidth)
                    titleRowX.Size = new Size(TotalWidth, titleRowX.Size.Height);
            }

            if (TitleTopBorder != null) TitleTopBorder.Width = TotalWidth;

            if (HeaderTopBorder != null) HeaderTopBorder.ColumnsWidths = ColumnsWidths;
            if (DataTopBorder != null) DataTopBorder.ColumnsWidths = ColumnsWidths;

            if (TitleHeaderSeparator != null) TitleHeaderSeparator.ColumnsWidths = ColumnsWidths;
            if (TitleDataSeparator != null) TitleDataSeparator.ColumnsWidths = ColumnsWidths;
            if (TitleBottomBorder != null) TitleBottomBorder.Width = TotalWidth;

            if (HeaderDataSeparator != null) HeaderDataSeparator.ColumnsWidths = ColumnsWidths;
            if (HeaderBottomBorder != null) HeaderBottomBorder.ColumnsWidths = ColumnsWidths;

            if (DataDataSeparator != null) DataDataSeparator.ColumnsWidths = ColumnsWidths;
            if (DataBottomBorder != null) DataBottomBorder.ColumnsWidths = ColumnsWidths;
        }

        public void Render(ITablePrinter tablePrinter)
        {
            TitleTopBorder?.Render(tablePrinter);
            HeaderTopBorder?.Render(tablePrinter);
            DataTopBorder?.Render(tablePrinter);

            titleRowX?.Render(tablePrinter);

            TitleHeaderSeparator?.Render(tablePrinter);
            TitleDataSeparator?.Render(tablePrinter);
            TitleBottomBorder?.Render(tablePrinter);

            headerRowX?.Render(tablePrinter, ColumnsWidths);

            HeaderDataSeparator?.Render(tablePrinter);
            HeaderBottomBorder?.Render(tablePrinter);

            RenderDataRows(tablePrinter);

            DataBottomBorder?.Render(tablePrinter);
        }

        public void RenderDataRows(ITablePrinter tablePrinter)
        {
            List<int> cellWidths = ColumnsWidths;

            for (int rowIndex = 0; rowIndex < dataRowXs.Count; rowIndex++)
            {
                DataRowX row = dataRowXs[rowIndex];
                row.Render(tablePrinter, cellWidths);

                bool isLastRow = rowIndex == dataRowXs.Count - 1;

                if (!isLastRow)
                    DataDataSeparator?.Render(tablePrinter);
            }
        }
    }
}