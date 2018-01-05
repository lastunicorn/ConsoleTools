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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DustInTheWind.ConsoleTools.TabularData
{
    internal class TableRenderer
    {
        private TableDimensions tableDimensions;
        private string rowSeparator;

        public TitleRow TitleRow { get; set; }
        public bool DisplayTitle { get; set; }

        public ReadOnlyCollection<Column> Columns { get; set; }
        public bool DisplayColumnHeaders { get; set; }

        public List<Row> Rows { get; set; }

        public BorderTemplate BorderTemplate { get; set; }
        public bool DisplayBorder { get; set; }
        public bool DrawLinesBetweenRows { get; set; }

        public int MinWidth { get; set; }
        public int PaddingLeft { get; set; }
        public int PaddingRight { get; set; }
        public HorizontalAlignment CellHorizontalAlignment { get; set; }

        private bool IsTitleVisible => TitleRow?.Text != null && TitleRow?.Text.Size.Height > 0 && DisplayTitle;
        private bool IsColumnHeaderRowVisible => DisplayColumnHeaders && tableDimensions.CalculatedHeaderRowHeight != 0;
        private bool AreDataRowsVisible => Rows.Count > 0;

        public void Render(ITablePrinter tablePrinter)
        {
            PrepareForRendering();

            if (DisplayBorder)
                DrawTableTopBorder(tablePrinter);

            if (IsTitleVisible)
                DrawTitleRow(tablePrinter);

            if (IsColumnHeaderRowVisible)
                DrawColumnHeadersRow(tablePrinter);

            DrawDataRows(tablePrinter);
        }

        private void PrepareForRendering()
        {
            tableDimensions = new TableDimensions
            {
                MinWidth = MinWidth,
                DisplayBorder = DisplayBorder,
                DisplayColumnHeaders = DisplayColumnHeaders,
                PaddingLeft = PaddingLeft,
                PaddingRight = PaddingRight,
                Title = TitleRow.Text,
                DisplayTitle = DisplayTitle,
                Columns = Columns,
                Rows = Rows
            };
            tableDimensions.CalculateTableDimensions();

            rowSeparator = BorderTemplate.GenerateDataRowSeparatorBorder(tableDimensions);
        }

        private void DrawTableTopBorder(ITablePrinter tablePrinter)
        {
            bool existsTitle = TitleRow?.Text != null && TitleRow?.Text.Size.Height > 0 && DisplayTitle;
            if (existsTitle)
            {
                string titleTopBorder = BorderTemplate.GenerateTitleTopBorder(tableDimensions);
                tablePrinter.WriteLineBorder(titleTopBorder);
                return;
            }

            bool existsColumnHeaders = Columns.Count > 0 && DisplayColumnHeaders;
            if (existsColumnHeaders)
            {
                string border = BorderTemplate.GenerateHeaderTopBorder(tableDimensions);
                tablePrinter.WriteLineBorder(border);
                return;
            }

            bool existsData = Rows.Count > 0;
            if (existsData)
            {
                string border = BorderTemplate.GenerateDataRowTopBorder(tableDimensions);
                tablePrinter.WriteLineBorder(border);
            }
        }

        private void DrawTitleRow(ITablePrinter tablePrinter)
        {
            TitleRow.Render(tableDimensions.CalculatedTotalWidth, tablePrinter);

            // Write bottom border <=> header top border
            if (DisplayBorder)
                DrawHorizontalBorderAfterTitle(tablePrinter);
        }

        private void DrawHorizontalBorderAfterTitle(ITablePrinter tablePrinter)
        {
            if (IsColumnHeaderRowVisible)
            {
                string border = BorderTemplate.GenerateTitleHeaderSeparator(tableDimensions);
                tablePrinter.WriteLineBorder(border);
            }
            else if (AreDataRowsVisible)
            {
                string border = BorderTemplate.GenerateTitleDataSeparator(tableDimensions);
                tablePrinter.WriteLineBorder(border);
            }
            else
            {
                string border = BorderTemplate.GenerateTitleBottomBorder(tableDimensions);
                tablePrinter.WriteLineBorder(border);
            }
        }

        private void DrawColumnHeadersRow(ITablePrinter tablePrinter)
        {
            int rowHeight = tableDimensions.CalculatedHeaderRowHeight;

            List<List<string>> cellContents = Columns
                .Select((x, i) =>
                {
                    int columnWidth = tableDimensions.CalculatedColumnsWidth[i];
                    return x.RenderHeader(columnWidth, rowHeight);
                })
                .ToList();

            for (int headerLineIndex = 0; headerLineIndex < rowHeight; headerLineIndex++)
            {
                if (DisplayBorder)
                    tablePrinter.WriteBorder(BorderTemplate.Left);

                for (int columnIndex = 0; columnIndex < Columns.Count; columnIndex++)
                {
                    string content = cellContents[columnIndex][headerLineIndex];
                    tablePrinter.WriteHeader(content);

                    if (DisplayBorder)
                    {
                        char cellBorderRight = columnIndex < Columns.Count - 1
                            ? BorderTemplate.Vertical
                            : BorderTemplate.Right;

                        tablePrinter.WriteBorder(cellBorderRight);
                    }
                }

                tablePrinter.WriteLine();
            }

            // Write bottom border <=> first row top border
            if (DisplayBorder)
                DrawHorizontalBorderAfterHeader(tablePrinter);
        }

        private void DrawHorizontalBorderAfterHeader(ITablePrinter tablePrinter)
        {
            if (Rows.Count == 0)
            {
                string border = BorderTemplate.GenerateHeaderBottomBorder(tableDimensions);
                tablePrinter.WriteLineBorder(border);
            }
            else
            {
                tablePrinter.WriteLineBorder(rowSeparator);
            }
        }

        private void DrawDataRows(ITablePrinter tablePrinter)
        {
            for (int rowIndex = 0; rowIndex < Rows.Count; rowIndex++)
            {
                List<int> cellWidths = tableDimensions.CalculatedColumnsWidth;
                int rowHeight = tableDimensions.CalculatedRowsHeight[rowIndex];

                Row row = Rows[rowIndex];
                row.Render(tablePrinter, cellWidths, rowHeight);

                if (DisplayBorder)
                    DrarHorizontalBorderAfterDataRow(tablePrinter, rowIndex);
            }
        }

        private void DrarHorizontalBorderAfterDataRow(ITablePrinter tablePrinter, int rowIndex)
        {
            if (rowIndex < Rows.Count - 1)
            {
                if (DrawLinesBetweenRows)
                    tablePrinter.WriteLineBorder(rowSeparator);
            }
            else
            {
                string rowBottomBorder = BorderTemplate.GenerateDataRowBottomBorder(tableDimensions);
                tablePrinter.WriteLineBorder(rowBottomBorder);
            }
        }
    }
}