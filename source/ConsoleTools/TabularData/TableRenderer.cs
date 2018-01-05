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
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DustInTheWind.ConsoleTools.TabularData
{
    public class TableRenderer
    {
        private TableDimensions tableDimensions;
        private AlignmentCalculator alignmentCalculator;
        private string rowSeparator;

        public MultilineText Title { get; set; }
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

        private bool IsTitleVisible => Title != null && Title.Size.Height > 0 && DisplayTitle;
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
                Title = Title,
                DisplayTitle = DisplayTitle,
                Columns = Columns,
                Rows = Rows
            };
            tableDimensions.CalculateTableDimensions();

            alignmentCalculator = new AlignmentCalculator
            {
                Columns = Columns,
                Rows = Rows,
                TableLevelCellAlignment = CellHorizontalAlignment
            };

            rowSeparator = BorderTemplate.GenerateDataRowSeparatorBorder(tableDimensions);
        }

        private void DrawTableTopBorder(ITablePrinter tablePrinter)
        {
            bool existsTitle = Title != null && Title.Size.Height > 0 && DisplayTitle;
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
            int cellInnerWidth = tableDimensions.CalculatedTotalWidth - PaddingLeft - PaddingRight;

            if (DisplayBorder)
                cellInnerWidth -= 2;

            // Write title
            for (int titleLineIndex = 0; titleLineIndex < Title.Size.Height; titleLineIndex++)
            {
                if (DisplayBorder)
                    tablePrinter.WriteBorder(BorderTemplate.Left);

                string content = BuildTitleCellContent(titleLineIndex, cellInnerWidth);
                tablePrinter.WriteTitle(content);

                if (DisplayBorder)
                    tablePrinter.WriteBorder(BorderTemplate.Right);

                tablePrinter.WriteLine();
            }

            // Write bottom border <=> header top border
            if (DisplayBorder)
                DrawHorizontalBorderAfterTitle(tablePrinter);
        }

        private string BuildTitleCellContent(int titleLineIndex, int cellInnerWidth)
        {
            string leftPadding = string.Empty.PadRight(PaddingLeft, ' ');
            string rightPadding = string.Empty.PadRight(PaddingRight, ' ');
            string innerContent = Title.Lines[titleLineIndex].PadRight(cellInnerWidth, ' ');

            return string.Concat(leftPadding, innerContent, rightPadding);
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
            // Write header cells
            for (int headerLineIndex = 0; headerLineIndex < tableDimensions.CalculatedHeaderRowHeight; headerLineIndex++)
            {
                if (DisplayBorder)
                    tablePrinter.WriteBorder(BorderTemplate.Left);

                for (int columnIndex = 0; columnIndex < Columns.Count; columnIndex++)
                {
                    string content = BuildHeaderCellContent(columnIndex, headerLineIndex);
                    //int columnWidth = tableDimensions.CalculatedColumnsWidth[columnIndex];
                    //string content = Columns[columnIndex].RenderHeader(columnWidth, headerLineIndex);
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

        private string BuildHeaderCellContent(int columnIndex, int headerLineIndex)
        {
            Column column = Columns[columnIndex];

            bool headerHasContent = headerLineIndex < column.Header.Size.Height;

            if (!headerHasContent)
                return string.Empty.PadRight(tableDimensions.CalculatedColumnsWidth[columnIndex], ' ');

            int cellInnerWidth = tableDimensions.CalculatedColumnsWidth[columnIndex] - PaddingLeft - PaddingRight;
            string innerContent;

            HorizontalAlignment alignment = alignmentCalculator.CalcualteHeaderCellAlignment(columnIndex);

            switch (alignment)
            {
                case HorizontalAlignment.Left:
                    innerContent = column.Header.Lines[headerLineIndex].PadRight(cellInnerWidth, ' ');
                    break;

                case HorizontalAlignment.Right:
                    innerContent = column.Header.Lines[headerLineIndex].PadLeft(cellInnerWidth, ' ');
                    break;

                case HorizontalAlignment.Center:
                    int totalSpaces = cellInnerWidth - column.Header.Size.Width;
                    int rightSpaces = (int)Math.Ceiling((double)totalSpaces / 2);
                    innerContent = column.Header.Lines[headerLineIndex].PadLeft(cellInnerWidth - rightSpaces, ' ').PadRight(cellInnerWidth, ' ');
                    break;

                default:
                    throw new ApplicationException("Internal error: Invalid calculated horizontal alignment.");
            }

            string leftPadding = string.Empty.PadRight(PaddingLeft, ' ');
            string rightPadding = string.Empty.PadRight(PaddingRight, ' ');

            return leftPadding + innerContent + rightPadding;
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
                Row row = Rows[rowIndex];

                for (int rowLineIndex = 0; rowLineIndex < tableDimensions.CalculatedRowsHeight[rowIndex]; rowLineIndex++)
                {
                    if (rowLineIndex > 0)
                        tablePrinter.WriteLine();

                    if (DisplayBorder)
                        tablePrinter.WriteBorder(BorderTemplate.Left);

                    for (int columnIndex = 0; columnIndex < row.CellCount; columnIndex++)
                    {
                        Cell cell = row[columnIndex];
                        int cellWidth = tableDimensions.CalculatedColumnsWidth[columnIndex];
                        string content = cell.Render(cellWidth, rowLineIndex);

                        tablePrinter.WriteNormal(content);

                        if (DisplayBorder)
                        {
                            char cellBorderRight = columnIndex < Columns.Count - 1
                                ? BorderTemplate.Vertical
                                : BorderTemplate.Right;

                            tablePrinter.WriteBorder(cellBorderRight);
                        }
                    }
                }

                tablePrinter.WriteLine();

                if (DisplayBorder)
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
    }
}