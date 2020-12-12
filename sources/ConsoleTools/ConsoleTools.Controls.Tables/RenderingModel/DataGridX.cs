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

using System.Collections.Generic;
using System.Linq;

namespace DustInTheWind.ConsoleTools.Controls.Tables.RenderingModel
{
    internal class DataGridX
    {
        private readonly bool displayBorder;

        private DataGridBorderX dataGridBorderX;
        private TitleRowX titleRowX;
        private HeaderRowX headerRowX;
        private readonly List<DataRowX> dataRowXs = new List<DataRowX>();
        private readonly List<int> columnsWidths = new List<int>();

        public int MinWidth { get; set; }

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

        public void AddBorder(DataGridBorderX dataGridBorderX)
        {
            this.dataGridBorderX = dataGridBorderX;
        }

        public void AddTitleRow(TitleRowX titleRowX)
        {
            this.titleRowX = titleRowX;
        }

        public void AddHeaderRow(HeaderRowX headerRowX)
        {
            this.headerRowX = headerRowX;
            headerRowX.UpdateColumnsWidths(columnsWidths);
        }

        public void AddDataRow(DataRowX dataRowX)
        {
            dataRowX.UpdateColumnsWidths(columnsWidths);
            dataRowXs.Add(dataRowX);
        }

        public void Clear()
        {
            titleRowX = null;
            headerRowX = null;
            dataRowXs.Clear();
            columnsWidths.Clear();

            MinWidth = 0;

            TitleTopBorder = null;
            HeaderTopBorder = null;
            DataTopBorder = null;

            TitleHeaderSeparator = null;
            TitleDataSeparator = null;
            TitleBottomBorder = null;

            HeaderDataSeparator = null;
            HeaderBottomBorder = null;

            DataDataSeparator = null;
            DataBottomBorder = null;
        }

        public void MakeFinalAdjustments()
        {
            int totalWidth = CalculateTotalWidth();

            if (titleRowX?.Size.Width < totalWidth)
                titleRowX.Size = new Size(totalWidth, titleRowX.Size.Height);

            if (TitleTopBorder != null) TitleTopBorder.Width = totalWidth;

            if (HeaderTopBorder != null) HeaderTopBorder.ColumnsWidths = columnsWidths;
            if (DataTopBorder != null) DataTopBorder.ColumnsWidths = columnsWidths;

            if (TitleHeaderSeparator != null) TitleHeaderSeparator.ColumnsWidths = columnsWidths;
            if (TitleDataSeparator != null) TitleDataSeparator.ColumnsWidths = columnsWidths;
            if (TitleBottomBorder != null) TitleBottomBorder.Width = totalWidth;

            if (HeaderDataSeparator != null) HeaderDataSeparator.ColumnsWidths = columnsWidths;
            if (HeaderBottomBorder != null) HeaderBottomBorder.ColumnsWidths = columnsWidths;

            if (DataDataSeparator != null) DataDataSeparator.ColumnsWidths = columnsWidths;
            if (DataBottomBorder != null) DataBottomBorder.ColumnsWidths = columnsWidths;

            dataGridBorderX.TotalWidth = totalWidth;
            dataGridBorderX.ColumnWidths = columnsWidths;
        }

        private int CalculateTotalWidth()
        {
            int totalWidth = MinWidth;

            if (titleRowX?.Size.Width > totalWidth)
                totalWidth = titleRowX.Size.Width;

            if (columnsWidths.Count <= 0)
                return totalWidth;

            int columnsTotalWidth = columnsWidths.Sum();

            if (displayBorder)
                columnsTotalWidth += columnsWidths.Count + 1;

            if (columnsTotalWidth < totalWidth)
            {
                int diff = totalWidth - columnsTotalWidth;
                int colCount = columnsWidths.Count;

                for (int i = 0; i < diff; i++)
                    columnsWidths[i % colCount]++;
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
            TitleTopBorder?.Render(tablePrinter);

            titleRowX?.Render(tablePrinter);

            TitleHeaderSeparator?.Render(tablePrinter);
            TitleDataSeparator?.Render(tablePrinter);
            TitleBottomBorder?.Render(tablePrinter);
        }

        private void RenderHeader(ITablePrinter tablePrinter)
        {
            HeaderTopBorder?.Render(tablePrinter);

            headerRowX?.Render(tablePrinter, columnsWidths);

            HeaderDataSeparator?.Render(tablePrinter);
            HeaderBottomBorder?.Render(tablePrinter);
        }

        private void RenderData(ITablePrinter tablePrinter)
        {
            DataTopBorder?.Render(tablePrinter);

            RenderDataRows(tablePrinter);

            DataBottomBorder?.Render(tablePrinter);
        }

        private void RenderDataRows(ITablePrinter tablePrinter)
        {
            List<int> cellWidths = columnsWidths;

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