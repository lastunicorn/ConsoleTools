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
using System.Collections.Generic;
using System.Linq;

namespace DustInTheWind.ConsoleTools.Controls.Tables.RenderingModel
{
    internal class DataGridX
    {
        private readonly bool displayBorder;

        private TitleRowX titleRowX;
        private RowX headerRowX;
        private readonly List<RowX> normalRows = new List<RowX>();
        private readonly List<ColumnX> columns = new List<ColumnX>();
        private int actualWidth;

        public TitleTopBorder TitleTopBorder { get; set; }
        public HeaderTopBorder HeaderTopBorder { get; set; }
        public DataTopBorder DataTopBorder { get; set; }

        public TitleHeaderSeparator TitleHeaderSeparator { get; set; }
        public TitleDataSeparator TitleDataSeparator { get; set; }
        public TitleBottomBorder TitleBottomBorder { get; set; }

        public HeaderDataSeparator HeaderDataSeparator { get; set; }
        public HeaderBottomBorder HeaderBottomBorder { get; set; }

        public DataDataSeparator DataDataSeparator { get; set; }
        public DataBottomBorder DataBottomBorder { get; set; }

        public DataGridX(bool displayBorder)
        {
            this.displayBorder = displayBorder;
        }

        public void AddTitleRow(TitleRowX titleRowX)
        {
            this.titleRowX = titleRowX;
        }

        public void AddHeaderRow(RowX headerRowX)
        {
            this.headerRowX = headerRowX;
            UpdateColumnsWidths(headerRowX);
        }

        public void AddNormalRow(RowX rowX)
        {
            normalRows.Add(rowX);
            UpdateColumnsWidths(rowX);
        }

        private void UpdateColumnsWidths(RowX row)
        {
            //bool isFullRow = row.Cells.Count == 0 && row.Cells[0].HorizontalMerge < 0;

            //if (isFullRow)
            //{
            //    int columnsTotalWidth = columns
            //        .Select(x => x.Width)
            //        .Sum();

            //    CellX cell = row.Cells[0];

            //    int diff = cell.Size.Width - columnsTotalWidth;

            //    for (int i = 0; i < diff; i++)
            //        columns[i % columns.Count].Width++;
            //}

            for (int i = 0; i < row.Cells.Count; i++)
            {
                while (columns.Count <= i)
                    columns.Add(new ColumnX());

                Size cellSize = row.Cells[i].Size;

                if (cellSize.Width > columns[i].Width)
                    columns[i].Width = cellSize.Width;
            }
        }

        public void CalculateLayout(int minWidth)
        {
            actualWidth = CalculateTotalWidth(minWidth);
        }

        private int CalculateTotalWidth(int minWidth)
        {
            int totalWidth = minWidth;

            if (titleRowX?.Size.Width > totalWidth)
                totalWidth = titleRowX.Size.Width;

            if (columns.Count <= 0)
                return totalWidth;

            int columnsTotalWidth = columns
                .Select(x => x.Width)
                .Sum();

            if (displayBorder)
                columnsTotalWidth += columns.Count + 1;

            if (columnsTotalWidth < totalWidth)
            {
                int diff = totalWidth - columnsTotalWidth;
                int colCount = columns.Count;

                for (int i = 0; i < diff; i++)
                    columns[i % colCount].Width++;
            }
            else if (columnsTotalWidth > totalWidth)
            {
                totalWidth = columnsTotalWidth;
            }

            return totalWidth;
        }

        public void Render(ITablePrinter tablePrinter)
        {
            RenderTitle(tablePrinter);
            RenderHeader(tablePrinter);
            RenderData(tablePrinter);
        }

        private void RenderTitle(ITablePrinter tablePrinter)
        {
            TitleTopBorder?.Render(tablePrinter, actualWidth);

            if (titleRowX != null)
            {
                Size rowSize = new Size(actualWidth, titleRowX.Size.Height);
                titleRowX?.Render(tablePrinter, rowSize);
            }

            TitleHeaderSeparator?.Render(tablePrinter, columns);
            TitleDataSeparator?.Render(tablePrinter, columns);
            TitleBottomBorder?.Render(tablePrinter, actualWidth);
        }

        private void RenderHeader(ITablePrinter tablePrinter)
        {
            HeaderTopBorder?.Render(tablePrinter, columns);

            headerRowX?.Render(tablePrinter, columns);

            HeaderDataSeparator?.Render(tablePrinter, columns);
            HeaderBottomBorder?.Render(tablePrinter, columns);
        }

        private void RenderData(ITablePrinter tablePrinter)
        {
            DataTopBorder?.Render(tablePrinter, columns);

            RenderNormalRows(tablePrinter);

            DataBottomBorder?.Render(tablePrinter, columns);
        }

        private void RenderNormalRows(ITablePrinter tablePrinter)
        {
            for (int rowIndex = 0; rowIndex < normalRows.Count; rowIndex++)
            {
                RowX row = normalRows[rowIndex];
                row.Render(tablePrinter, columns);

                bool isLastRow = rowIndex == normalRows.Count - 1;

                if (!isLastRow)
                    DataDataSeparator?.Render(tablePrinter, columns);
            }
        }
    }
}