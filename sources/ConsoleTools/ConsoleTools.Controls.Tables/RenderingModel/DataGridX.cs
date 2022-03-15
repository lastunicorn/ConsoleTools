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

using System.Collections.Generic;

namespace DustInTheWind.ConsoleTools.Controls.Tables.RenderingModel
{
    internal class DataGridX
    {
        private readonly bool displayBorder;

        private RowX titleRowX;
        private RowX headerRowX;
        private readonly List<RowX> normalRows = new();
        private readonly DataGridLayout dataGridLayout = new();

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

        public void AddTitleRow(RowX rowX)
        {
            titleRowX = rowX;
            dataGridLayout.AddRow(rowX);
        }

        public void AddHeaderRow(RowX headerRowX)
        {
            this.headerRowX = headerRowX;
            dataGridLayout.AddRow(headerRowX);
        }

        public void AddNormalRow(RowX rowX)
        {
            normalRows.Add(rowX);
            dataGridLayout.AddRow(rowX);
        }
        
        public void CalculateLayout(int minWidth)
        {
            dataGridLayout.BorderVisibility = displayBorder;
            dataGridLayout.MinWidth = minWidth;
            dataGridLayout.MaxWidth = int.MaxValue;
            dataGridLayout.FinalizeLayout();
        }

        public void Render(ITablePrinter tablePrinter)
        {
            RenderTitle(tablePrinter);
            RenderHeader(tablePrinter);
            RenderData(tablePrinter);
        }

        private void RenderTitle(ITablePrinter tablePrinter)
        {
            TitleTopBorder?.Render(tablePrinter, dataGridLayout.ActualWidth);

            titleRowX?.Render(tablePrinter, dataGridLayout.Columns);
            TitleHeaderSeparator?.Render(tablePrinter, dataGridLayout.Columns);
            TitleDataSeparator?.Render(tablePrinter, dataGridLayout.Columns);
            TitleBottomBorder?.Render(tablePrinter, dataGridLayout.ActualWidth);
        }

        private void RenderHeader(ITablePrinter tablePrinter)
        {
            HeaderTopBorder?.Render(tablePrinter, dataGridLayout.Columns);

            headerRowX?.Render(tablePrinter, dataGridLayout.Columns);

            HeaderDataSeparator?.Render(tablePrinter, dataGridLayout.Columns);
            HeaderBottomBorder?.Render(tablePrinter, dataGridLayout.Columns);
        }

        private void RenderData(ITablePrinter tablePrinter)
        {
            DataTopBorder?.Render(tablePrinter, dataGridLayout.Columns);

            RenderNormalRows(tablePrinter);

            DataBottomBorder?.Render(tablePrinter, dataGridLayout.Columns);
        }

        private void RenderNormalRows(ITablePrinter tablePrinter)
        {
            for (int rowIndex = 0; rowIndex < normalRows.Count; rowIndex++)
            {
                RowX row = normalRows[rowIndex];
                row.Render(tablePrinter, dataGridLayout.Columns);

                bool isLastRow = rowIndex == normalRows.Count - 1;

                if (!isLastRow)
                    DataDataSeparator?.Render(tablePrinter, dataGridLayout.Columns);
            }
        }
    }
}