// ConsoleTools
// Copyright (C) 2017 Dust in the Wind
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

namespace DustInTheWind.ConsoleTools.TabularData
{
    public class Table
    {
        /// <summary>
        /// Gets or sets the title of the current instance of the <see cref="Table"/>.
        /// </summary>
        public MultilineText Title { get; set; }

        /// <summary>
        /// Gets or sets the padding applyed to the left side of every cell.
        /// </summary>
        public int PaddingLeft { get; set; } = 1;

        /// <summary>
        /// Gets or sets the padding applyed to the right side of every cell.
        /// </summary>
        public int PaddingRight { get; set; } = 1;

        /// <summary>
        /// Gets or sets the padding applyed to the right and left side of every cell.
        /// If the left padding is different from the right padding, -1 is returned.
        /// </summary>
        public int Padding
        {
            get { return PaddingLeft == PaddingRight ? PaddingLeft : -1; }
            set { PaddingLeft = PaddingRight = value; }
        }

        /// <summary>
        /// Gets a value that specifies if border lines should be drawn between rows.
        /// </summary>
        public bool DrawLinesBetweenRows { get; set; } = false;

        /// <summary>
        /// Gets or sets the horizontal alignment for the content of the cells contained by the current table.
        /// </summary>
        public HorizontalAlignment CellHorizontalAlignment { get; set; } = HorizontalAlignment.Default;

        /// <summary>
        /// Gets the list of columns contained by the current table.
        /// </summary>
        public List<Column> Columns { get; } = new List<Column>();

        /// <summary>
        /// The list of rows contained by the current table.
        /// </summary>
        private readonly List<Row> rows = new List<Row>();

        /// <summary>
        /// Gets the number of rows contained by the current instance of the <see cref="Table"/>.
        /// </summary>
        public int RowCount => rows.Count;

        /// <summary>
        /// Gets the number of columns contained by the current instance of the <see cref="Table"/>.
        /// </summary>
        public int ColumnCount => Columns.Count;

        /// <summary>
        /// Gets or sets the minimum width of the table.
        /// </summary>
        public int MinWidth { get; set; }

        /// <summary>
        /// Gets or sets a value that specifies if thew column headers are displayed.
        /// </summary>
        public bool DisplayColumnHeaders { get; set; } = true;

        /// <summary>
        /// Gets the row at the specified index.
        /// </summary>
        /// <param name="rowIndex">The zero-based index of the row to get.</param>
        /// <returns>The row at the specified index.</returns>
        public Row this[int rowIndex] => rows[rowIndex];

        /// <summary>
        /// Gets the cell at the specified location.
        /// </summary>
        /// <param name="rowIndex">The zero-based row index of the cell to get.</param>
        /// <param name="columnIndex">The zero-based column index of the cell to get.</param>
        /// <returns>The cell at the specified location.</returns>
        public Cell this[int rowIndex, int columnIndex] => rows[rowIndex][columnIndex];

        /// <summary>
        /// Gets or sets a value that specifies if the borders are visible.
        /// </summary>
        public bool DisplayBorder { get; set; } = true;

        /// <summary>
        /// Gets or sets the table borders.
        /// </summary>
        public TableBorder Border { get; set; } = TableBorder.PlusMinusBorder;

        /// <summary>
        /// Initializes a new instance of the <see cref="Table"/> class.
        /// </summary>
        public Table()
        {
            Title = MultilineText.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Table"/> class with
        /// the table title.
        /// </summary>
        public Table(string title)
        {
            Title = (title == null)
                ? MultilineText.Empty
                : new MultilineText(title);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Table"/> class with
        /// the table title.
        /// </summary>
        public Table(MultilineText title)
        {
            Title = title ?? MultilineText.Empty;
        }

        /// <summary>
        /// Adds a new row to the current table.
        /// </summary>
        /// <param name="row">The row to be added.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void AddRow(Row row)
        {
            if (row == null) throw new ArgumentNullException(nameof(row));

            rows.Add(row);
        }

        /// <summary>
        /// Adds a new row to the current table.
        /// </summary>
        /// <param name="cells">The list of cells of the new row.</param>
        public void AddRow(Cell[] cells)
        {
            if (cells == null) throw new ArgumentNullException(nameof(cells));

            rows.Add(new Row(cells));
        }

        /// <summary>
        /// Adds a new row to the current table.
        /// </summary>
        /// <param name="texts">The list of cell contents of the new row.</param>
        public void AddRow(string[] texts)
        {
            if (texts == null) throw new ArgumentNullException(nameof(texts));

            Row row = new Row();

            foreach (string text in texts)
                row.Cells.Add(new Cell(text));

            rows.Add(row);
        }

        /// <summary>
        /// Returns the string representation of the current instance.
        /// </summary>
        /// <returns>The string representation of the current instance.</returns>
        public override string ToString()
        {
            StringTablePrinter tablePrinter = new StringTablePrinter();
            Render(tablePrinter);

            return tablePrinter.ToString();
        }

        public void Render(ITablePrinter tablePrinter)
        {
            TableDimensions tableDimensions = new TableDimensions
            {
                MinWidth = MinWidth,
                DisplayBorder = DisplayBorder,
                DisplayColumnHeaders = DisplayColumnHeaders,
                PaddingLeft = PaddingLeft,
                PaddingRight = PaddingRight,
                Title = Title,
                Columns = Columns,
                Rows = rows
            };
            tableDimensions.CalculateTableDimensions();
            
            AlignmentCalculator alignmentCalculator = new AlignmentCalculator
            {
                Columns = Columns,
                Rows = rows,
                TableLevelCellAlignment = CellHorizontalAlignment
            };

            string rowSeparator = Border.GenerateDataRowSeparatorBorder(tableDimensions);

            DrawTableTitleRow(tablePrinter, tableDimensions);
            DrawColumnHeadersRow(tablePrinter, tableDimensions, rowSeparator);
            DrawDataRows(tablePrinter, tableDimensions, rowSeparator);
        }

        private void DrawTableTitleRow(ITablePrinter tablePrinter, TableDimensions dimensions)
        {
            bool existsTitle = Title.Size.Height > 0;

            if (existsTitle)
            {
                // Write top border
                if (DisplayBorder)
                {
                    string titleTopBorder = Border.GenerateTitleTopBorder(dimensions);
                    tablePrinter.WriteLineBorder(titleTopBorder);
                }

                int cellInnerWidth = dimensions.CalculatedTotalWidth - PaddingLeft - PaddingRight;

                if (DisplayBorder)
                    cellInnerWidth -= 2;

                // Write title
                for (int i = 0; i < Title.Size.Height; i++)
                {
                    if (DisplayBorder)
                        tablePrinter.WriteBorder(Border.Left);

                    string leftPadding = string.Empty.PadRight(PaddingLeft, ' ');
                    string rightPadding = string.Empty.PadRight(PaddingRight, ' ');
                    string innerContent = Title.Lines[i].PadRight(cellInnerWidth, ' ');

                    tablePrinter.WriteTitle(leftPadding + innerContent + rightPadding);

                    if (DisplayBorder)
                        tablePrinter.WriteLineBorder(Border.Right);
                    else
                        tablePrinter.WriteLine();
                }

                // Write bottom border <=> header top border
                if (DisplayBorder)
                {
                    if (Columns.Count > 0 && DisplayColumnHeaders)
                    {
                        string border = Border.GenerateTitleHeaderSeparator(dimensions);
                        tablePrinter.WriteLineBorder(border);
                    }
                    else if (rows.Count > 0)
                    {
                        string border = Border.GenerateTitleDataSeparator(dimensions);
                        tablePrinter.WriteLineBorder(border);
                    }
                    else
                    {
                        string border = Border.GenerateTitleBottomBorder(dimensions);
                        tablePrinter.WriteLineBorder(border);
                    }
                }
            }
            else
            {
                if (DisplayBorder)
                {
                    if (Columns.Count > 0 && DisplayColumnHeaders)
                    {
                        string border = Border.GenerateHeaderTopBorder(dimensions);
                        tablePrinter.WriteLineBorder(border);
                    }
                    else if (rows.Count > 0)
                    {
                        string border = Border.GenerateDataRowTopBorder(dimensions);
                        tablePrinter.WriteLineBorder(border);
                    }
                }
            }
        }

        private void DrawColumnHeadersRow(ITablePrinter tablePrinter, TableDimensions dimensions, string rowSeparator)
        {
            if (!DisplayColumnHeaders || dimensions.CalculatedHeaderRowHeight == 0)
                return;

            // Write header cells
            for (int headerLineIndex = 0; headerLineIndex < dimensions.CalculatedHeaderRowHeight; headerLineIndex++)
            {
                if (DisplayBorder)
                    tablePrinter.WriteBorder(Border.Left);

                for (int columnIndex = 0; columnIndex < Columns.Count; columnIndex++)
                {
                    string content = BuildHeaderContent(dimensions, columnIndex, headerLineIndex);

                    tablePrinter.WriteHeader(content);

                    if (DisplayBorder)
                    {
                        char cellBorderRight = columnIndex < Columns.Count - 1
                            ? Border.Vertical
                            : Border.Right;

                        tablePrinter.WriteBorder(cellBorderRight);
                    }
                }

                tablePrinter.WriteLine();
            }

            // Write bottom border <=> first row top border
            if (DisplayBorder)
            {
                if (RowCount == 0)
                {
                    string border = Border.GenerateHeaderBottomBorder(dimensions);
                    tablePrinter.WriteLineBorder(border);
                }
                else
                {
                    tablePrinter.WriteLineBorder(rowSeparator);
                }
            }
        }

        private string BuildHeaderContent(TableDimensions dimensions, int columnIndex, int headerLineIndex)
        {
            Column column = Columns[columnIndex];

            bool headerHasContent = headerLineIndex < column.Header.Size.Height;

            if (!headerHasContent)
                return string.Empty.PadRight(dimensions.CalculatedColumnsWidth[columnIndex], ' ');

            int cellInnerWidth = dimensions.CalculatedColumnsWidth[columnIndex] - PaddingLeft - PaddingRight;
            string innerContent;

            HorizontalAlignment alignment = CalculateColumnHeaderCellHorizontalAlignment(columnIndex);

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

        private HorizontalAlignment CalculateColumnHeaderCellHorizontalAlignment(int columnIndex)
        {
            AlignmentCalculator alignmentCalculator = new AlignmentCalculator
            {
                Columns = Columns,
                Rows = rows,
                TableLevelCellAlignment = CellHorizontalAlignment
            };

            return alignmentCalculator.CalcualteHeaderCellAlignment(columnIndex);
        }

        private void DrawDataRows(ITablePrinter tablePrinter, TableDimensions dimensions, string rowSeparator)
        {
            for (int rowIndex = 0; rowIndex < rows.Count; rowIndex++)
            {
                Row row = rows[rowIndex];

                for (int rowLineIndex = 0; rowLineIndex < dimensions.CalculatedRowsHeight[rowIndex]; rowLineIndex++)
                {
                    if (rowLineIndex > 0)
                        tablePrinter.WriteLine();

                    if (DisplayBorder)
                        tablePrinter.WriteBorder(Border.Left);

                    for (int columnIndex = 0; columnIndex < row.Cells.Count; columnIndex++)
                    {
                        Cell cell = row.Cells[columnIndex];
                        string content = BuildCellContent(dimensions, rowIndex, columnIndex, cell, rowLineIndex);

                        tablePrinter.WriteNormal(content);

                        if (DisplayBorder)
                        {
                            char cellBorderRight = columnIndex < Columns.Count - 1
                                ? Border.Vertical
                                : Border.Right;

                            tablePrinter.WriteBorder(cellBorderRight);
                        }
                    }
                }

                tablePrinter.WriteLine();

                if (DisplayBorder)
                {
                    if (rowIndex < rows.Count - 1)
                    {
                        if (DrawLinesBetweenRows)
                            tablePrinter.WriteLineBorder(rowSeparator);
                    }
                    else
                    {
                        string rowBottomBorder = Border.GenerateDataRowBottomBorder(dimensions);
                        tablePrinter.WriteLineBorder(rowBottomBorder);
                    }
                }
            }
        }

        private string BuildCellContent(TableDimensions dimensions, int rowIndex, int columnIndex, Cell cell, int rowLineIndex)
        {
            if (rowLineIndex >= cell.Content.Size.Height)
                return string.Empty.PadRight(dimensions.CalculatedColumnsWidth[columnIndex], ' ');

            int cellInnerWidth = dimensions.CalculatedColumnsWidth[columnIndex] - PaddingLeft - PaddingRight;
            string innerContent;

            HorizontalAlignment alignment = CalculateCellHorizontalAlignment(rowIndex, columnIndex);

            switch (alignment)
            {
                case HorizontalAlignment.Left:
                    innerContent = cell.Content.Lines[rowLineIndex].PadRight(cellInnerWidth, ' ');
                    break;

                case HorizontalAlignment.Right:
                    innerContent = cell.Content.Lines[rowLineIndex].PadLeft(cellInnerWidth, ' ');
                    break;

                case HorizontalAlignment.Center:
                    int totalSpaces = cellInnerWidth - cell.Content.Size.Width;
                    //int leftSpaces = (int)Math.Floor(totalSpaces / 2);
                    int rightSpaces = (int)Math.Ceiling((double)totalSpaces / 2);
                    innerContent = cell.Content.Lines[rowLineIndex]
                        .PadLeft(cellInnerWidth - rightSpaces, ' ')
                        .PadRight(cellInnerWidth, ' ');
                    break;

                default:
                    throw new ApplicationException("Internal error: Invalid calculated horizontal alignment.");
            }

            string leftPadding = string.Empty.PadRight(PaddingLeft, ' ');
            string rightPadding = string.Empty.PadRight(PaddingRight, ' ');

            return leftPadding + innerContent + rightPadding;
        }

        private HorizontalAlignment CalculateCellHorizontalAlignment(int rowIndex, int columnIndex)
        {
            AlignmentCalculator alignmentCalculator = new AlignmentCalculator
            {
                Columns = Columns,
                Rows = rows,
                TableLevelCellAlignment = CellHorizontalAlignment
            };

            return alignmentCalculator.CalcualteDataCellAlignment(rowIndex, columnIndex);
        }

        public void SetCellAlignment(int rowIndex, int columnIndex, HorizontalAlignment alignment)
        {
            Row row = rows[rowIndex];
            Cell cell = row[columnIndex];

            cell.HorizontalAlignment = alignment;
        }
    }
}
