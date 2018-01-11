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

namespace DustInTheWind.ConsoleTools.TabularData
{
    internal class TableRenderer
    {
        private DataGridX dataGridX;
        private string rowSeparator;

        public TitleRow TitleRow { get; set; }
        public bool DisplayTitle { get; set; }

        public ColumnList Columns { get; set; }
        public bool DisplayColumnHeaders { get; set; }

        public List<DataRow> Rows { get; set; }

        public BorderTemplate BorderTemplate { get; set; }
        public bool DisplayBorder { get; set; }
        public bool DrawBordersBetweenRows { get; set; }

        public int MinWidth { get; set; }
        public HorizontalAlignment CellHorizontalAlignment { get; set; }


        private string RowSeparator
        {
            get
            {
                if (rowSeparator == null)
                    rowSeparator = BorderTemplate.GenerateDataRowSeparatorBorder(dataGridX.ColumnsWidths);

                return rowSeparator;
            }
        }

        public void Render(ITablePrinter tablePrinter)
        {
            CalculateTableDimensions();

            if (DisplayBorder)
                DrawTableTopBorder(tablePrinter);

            if (dataGridX.IsTitleVisible)
            {
                DrawTitleRow(tablePrinter);

                if (DisplayBorder)
                    DrawHorizontalBorderAfterTitle(tablePrinter);
            }

            if (dataGridX.AreColumnsHeaderVisible)
            {
                DrawColumnHeadersRow(tablePrinter);

                if (DisplayBorder)
                    DrawHorizontalBorderAfterHeader(tablePrinter);
            }

            if (dataGridX.AreDataRowsVisible)
                DrawDataRows(tablePrinter);
        }

        private void CalculateTableDimensions()
        {
            dataGridX = new DataGridX(DisplayBorder)
            {
                MinWidth = MinWidth
            };

            bool isTitleVisible = TitleRow?.Content != null && TitleRow?.Content.Size.Height > 0 && DisplayTitle;
            if (isTitleVisible)
                dataGridX.AddTitleRow(TitleRow);

            bool isColumnHeaderRowVisible = DisplayColumnHeaders && Columns?.Count != 0;
            if (isColumnHeaderRowVisible)
                dataGridX.AddHeaderRow(Columns);

            bool areDataRowsVisible = Rows.Count > 0;
            if (areDataRowsVisible)
            {
                foreach (DataRow row in Rows)
                    dataGridX.AddDataRow(row);
            }

            dataGridX.MakeFinalAdjustments();
        }

        private void DrawTableTopBorder(ITablePrinter tablePrinter)
        {
            if (dataGridX.IsTitleVisible)
            {
                string titleTopBorder = BorderTemplate.GenerateTitleTopBorder(dataGridX.TotalWidth);
                tablePrinter.WriteLineBorder(titleTopBorder);
            }
            else if (dataGridX.AreColumnsHeaderVisible)
            {
                string border = BorderTemplate.GenerateHeaderTopBorder(dataGridX.ColumnsWidths);
                tablePrinter.WriteLineBorder(border);
            }
            else if (dataGridX.AreDataRowsVisible)
            {
                string border = BorderTemplate.GenerateDataRowTopBorder(dataGridX.ColumnsWidths);
                tablePrinter.WriteLineBorder(border);
            }
        }

        private void DrawTitleRow(ITablePrinter tablePrinter)
        {
            dataGridX.RenderTitle(tablePrinter);
        }

        private void DrawHorizontalBorderAfterTitle(ITablePrinter tablePrinter)
        {
            if (dataGridX.AreColumnsHeaderVisible)
            {
                string border = BorderTemplate.GenerateTitleHeaderSeparator(dataGridX.ColumnsWidths);
                tablePrinter.WriteLineBorder(border);
            }
            else if (dataGridX.AreDataRowsVisible)
            {
                string border = BorderTemplate.GenerateTitleDataSeparator(dataGridX.ColumnsWidths);
                tablePrinter.WriteLineBorder(border);
            }
            else
            {
                string border = BorderTemplate.GenerateTitleBottomBorder(dataGridX.TotalWidth);
                tablePrinter.WriteLineBorder(border);
            }
        }

        private void DrawColumnHeadersRow(ITablePrinter tablePrinter)
        {
            dataGridX.RederColumnsHeaders(tablePrinter);
        }

        private void DrawHorizontalBorderAfterHeader(ITablePrinter tablePrinter)
        {
            if (dataGridX.AreDataRowsVisible)
            {
                tablePrinter.WriteLineBorder(RowSeparator);
            }
            else
            {
                string border = BorderTemplate.GenerateHeaderBottomBorder(dataGridX.ColumnsWidths);
                tablePrinter.WriteLineBorder(border);
            }
        }

        private void DrawDataRows(ITablePrinter tablePrinter)
        {
            List<int> cellWidths = dataGridX.ColumnsWidths;

            for (int rowIndex = 0; rowIndex < Rows.Count; rowIndex++)
            {
                int rowHeight = dataGridX.RowsHeights[rowIndex];

                DataRow row = Rows[rowIndex];
                row.Render(tablePrinter, cellWidths, rowHeight);

                if (DisplayBorder)
                    DrawHorizontalBorderAfterDataRow(tablePrinter, rowIndex);
            }
        }

        private void DrawHorizontalBorderAfterDataRow(ITablePrinter tablePrinter, int rowIndex)
        {
            bool isLastRow = rowIndex == Rows.Count - 1;

            if (isLastRow)
            {
                string rowBottomBorder = BorderTemplate.GenerateDataRowBottomBorder(dataGridX.ColumnsWidths);
                tablePrinter.WriteLineBorder(rowBottomBorder);
            }
            else
            {
                if (DrawBordersBetweenRows)
                    tablePrinter.WriteLineBorder(RowSeparator);
            }
        }
    }
}