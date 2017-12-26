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
using System.Text;

namespace DustInTheWind.ConsoleTools.TabularData
{
    public class Table
    {
        private const HorizontalAlignment DefaultHorizontalAlignment = HorizontalAlignment.Left;

        /// <summary>
        /// Gets or sets the title of the current instance of the <see cref="Table"/>.
        /// </summary>
        public MultilineText Title { get; set; }

        /// <summary>
        /// Gets or sets the padding applyed to the left side of every cell.
        /// </summary>
        public int PaddingLeft { get; set; }

        /// <summary>
        /// Gets or sets the padding applyed to the right side of every cell.
        /// </summary>
        public int PaddingRight { get; set; }

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
        public bool DrawLinesBetweenRows { get; set; }

        /// <summary>
        /// Gets or sets the horizontal alignment for the content of the cells contained by the current table.
        /// </summary>
        public HorizontalAlignment HorizontalAlignment { get; set; }

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
        public bool DisplayColumnHeaders { get; set; }

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
        public bool DisplayBorder { get; set; }

        /// <summary>
        /// Gets or sets the table borders.
        /// </summary>
        public TableBorder Border { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Table"/> class.
        /// </summary>
        public Table()
            : this(MultilineText.Empty)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Table"/> class with
        /// the table title.
        /// </summary>
        public Table(string title)
            : this(new MultilineText(title))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Table"/> class with
        /// the table title.
        /// </summary>
        public Table(MultilineText title)
        {
            Title = title ?? MultilineText.Empty;

            HorizontalAlignment = DefaultHorizontalAlignment;
            DrawLinesBetweenRows = false;
            DisplayColumnHeaders = false;
            PaddingLeft = 1;
            PaddingRight = 1;

            DisplayBorder = true;
            Border = new TableBorder("+-+|+-+|+++++|-");
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
        /// Calculates and returns the dimensions of the current instance of the <see cref="Table"/> displayed in text mode.
        /// </summary>
        /// <returns>The dimensions of the current instance of the <see cref="Table"/> displayed in text mode.</returns>
        private TableDimensions CalculateTableDimensions()
        {
            TableDimensions dimensions = new TableDimensions
            {
                Width = MinWidth > 0 ? MinWidth : 0
            };

            int longRowWidth = 0;

            // Calculate table title row width.

            int titleRowWidth = 0;

            if (DisplayBorder)
                titleRowWidth += 1;

            titleRowWidth += PaddingLeft;

            if (Title != null)
                titleRowWidth += Title.Size.Width;

            titleRowWidth += PaddingRight;

            if (DisplayBorder)
                titleRowWidth += 1;


            if (dimensions.Width < titleRowWidth)
                dimensions.Width = titleRowWidth;

            // Calculate the header dimensions.

            if (DisplayColumnHeaders)
            {
                int headerRowWidth = 0;
                int headerRowHeight = 0;

                // The table left border
                if (DisplayBorder)
                    headerRowWidth += 1;

                for (int i = 0; i < Columns.Count; i++)
                {
                    Column column = Columns[i];

                    dimensions.ColumnsWidth.Add(0);

                    int cellWidth;
                    int cellHeight;

                    if (column.Header != null)
                    {
                        cellWidth = PaddingLeft + column.Header.Size.Width + PaddingRight;
                        cellHeight = column.Header.Size.Height;
                    }
                    else
                    {
                        cellWidth = PaddingLeft + PaddingRight;
                        cellHeight = 0;
                    }

                    if (dimensions.ColumnsWidth[i] < cellWidth)
                    {
                        dimensions.ColumnsWidth[i] = cellWidth;
                        headerRowWidth += cellWidth;

                        // The cell right border
                        if (DisplayBorder)
                            headerRowWidth += 1;
                    }
                    else
                    {
                        headerRowWidth += dimensions.ColumnsWidth[i] + 1; // The cell width + cell right border
                    }

                    if (headerRowHeight < cellHeight)
                        headerRowHeight = cellHeight;
                }

                dimensions.HeaderHeight = headerRowHeight;

                if (longRowWidth < headerRowWidth) longRowWidth = headerRowWidth;
            }

            //

            for (int i = 0; i < rows.Count; i++)
            {
                Row row = rows[i];

                int rowWidth = 0;
                int rowHeight = 0;

                // The table left border
                if (DisplayBorder)
                    rowWidth += 1;

                for (int j = 0; j < row.Cells.Count; j++)
                {
                    Cell cell = row.Cells[j];

                    if (j == dimensions.ColumnsWidth.Count)
                        dimensions.ColumnsWidth.Add(0);

                    int cellWidth;
                    int cellHeight;

                    if (cell.Content != null)
                    {
                        cellWidth = PaddingLeft + cell.Content.Size.Width + PaddingRight;
                        cellHeight = cell.Content.Size.Height;
                    }
                    else
                    {
                        cellWidth = PaddingLeft + PaddingRight;
                        cellHeight = 0;
                    }

                    if (dimensions.ColumnsWidth[j] < cellWidth)
                    {
                        dimensions.ColumnsWidth[j] = cellWidth;
                        rowWidth += cellWidth;

                        // The cell right border
                        if (DisplayBorder)
                            rowWidth += 1;
                    }
                    else
                    {
                        rowWidth += dimensions.ColumnsWidth[j];

                        // The cell right border
                        if (DisplayBorder)
                            rowWidth += 1;
                    }

                    if (rowHeight < cellHeight)
                        rowHeight = cellHeight;
                }

                dimensions.RowsHeight.Add(rowHeight);

                if (longRowWidth < rowWidth)
                    longRowWidth = rowWidth;
            }

            if (dimensions.Width < longRowWidth)
            {
                dimensions.Width = longRowWidth;
            }
            else if (dimensions.Width > longRowWidth)
            {
                int diff = dimensions.Width - longRowWidth;
                int colCount = dimensions.ColumnsWidth.Count;

                for (int i = 0; i < diff; i++)
                    dimensions.ColumnsWidth[i % colCount]++;
            }

            return dimensions;
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
            TableDimensions dimensions = CalculateTableDimensions();
            string rowSeparator = GetRowSeparatorBorder(dimensions);

            DrawTableTitle(tablePrinter, dimensions);
            DrawColumnHeaders(tablePrinter, dimensions, rowSeparator);
            DrawRows(tablePrinter, dimensions, rowSeparator);
        }

        private void DrawTableTitle(ITablePrinter tablePrinter, TableDimensions dimensions)
        {
            if (Title.Size.Height > 0)
            {
                // Write top border
                if (DisplayBorder)
                {
                    string titleTopBorder = GetTitleTopBorder(dimensions);
                    tablePrinter.WriteLineBorder(titleTopBorder);
                }

                int cellInnerWidth = dimensions.Width - PaddingLeft - PaddingRight;

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
                    string titleRowSeparator = GetTitleRowSeparator(dimensions);
                    tablePrinter.WriteLineBorder(titleRowSeparator);
                }
            }
            else
            {
                if (DisplayBorder)
                {
                    string rowTopBorder = GetRowTopBorder(dimensions);
                    tablePrinter.WriteLineBorder(rowTopBorder);
                }
            }
        }

        private void DrawColumnHeaders(ITablePrinter tablePrinter, TableDimensions dimensions, string rowSeparator)
        {
            if (!DisplayColumnHeaders || dimensions.HeaderHeight == 0)
                return;

            // Write header cells
            for (int headerLineIndex = 0; headerLineIndex < dimensions.HeaderHeight; headerLineIndex++)
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
                tablePrinter.WriteLineBorder(rowSeparator);
        }

        private string BuildHeaderContent(TableDimensions dimensions, int columnIndex, int headerLineIndex)
        {
            Column column = Columns[columnIndex];

            bool headerHasContent = headerLineIndex < column.Header.Size.Height;

            if (!headerHasContent)
                return string.Empty.PadRight(dimensions.ColumnsWidth[columnIndex], ' ');

            int cellInnerWidth = dimensions.ColumnsWidth[columnIndex] - PaddingLeft - PaddingRight;
            string innerContent;

            HorizontalAlignment alignment = ColumnHeaderHorizontalAlignment(columnIndex);

            switch (alignment)
            {
                default:
                case HorizontalAlignment.Default:
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
            }

            string leftPadding = string.Empty.PadRight(PaddingLeft, ' ');
            string rightPadding = string.Empty.PadRight(PaddingRight, ' ');

            return leftPadding + innerContent + rightPadding;
        }

        private HorizontalAlignment ColumnHeaderHorizontalAlignment(int columnIndex)
        {
            HorizontalAlignment alignment = HorizontalAlignment.Default;

            if (columnIndex < Columns.Count)
                alignment = Columns[columnIndex].HorizontalAlignment;

            if (alignment == HorizontalAlignment.Default)
            {
                alignment = HorizontalAlignment == HorizontalAlignment.Default
                    ? HorizontalAlignment.Left
                    : DefaultHorizontalAlignment;
            }

            return alignment;
        }

        private void DrawRows(ITablePrinter tablePrinter, TableDimensions dimensions, string rowSeparator)
        {
            for (int rowIndex = 0; rowIndex < rows.Count; rowIndex++)
            {
                Row row = rows[rowIndex];

                for (int rowLineIndex = 0; rowLineIndex < dimensions.RowsHeight[rowIndex]; rowLineIndex++)
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
                        string rowBottomBorder = GetRowBottomBorder(dimensions);
                        tablePrinter.WriteLineBorder(rowBottomBorder);
                    }
                }
            }
        }

        private string BuildCellContent(TableDimensions dimensions, int rowIndex, int columnIndex, Cell cell, int rowLineIndex)
        {
            if (rowLineIndex >= cell.Content.Size.Height)
                return string.Empty.PadRight(dimensions.ColumnsWidth[columnIndex], ' ');

            int cellInnerWidth = dimensions.ColumnsWidth[columnIndex] - PaddingLeft - PaddingRight;
            string innerContent;

            HorizontalAlignment alignment = CellHorizontalAlignment(rowIndex, columnIndex);
            switch (alignment)
            {
                default:
                case HorizontalAlignment.Default:
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
            }

            string leftPadding = string.Empty.PadRight(PaddingLeft, ' ');
            string rightPadding = string.Empty.PadRight(PaddingRight, ' ');

            return leftPadding + innerContent + rightPadding;
        }

        /// <summary>
        /// Returns the horizontal alignment of a cell from the current instance of the <see cref="Table"/>.
        /// </summary>
        /// <param name="rowIndex">The zero-based row index of the cell to get.</param>
        /// <param name="columnIndex">The zero-based column index of the cell to get.</param>
        /// <returns>The horizontal alignment of the cell.</returns>
        private HorizontalAlignment CellHorizontalAlignment(int rowIndex, int columnIndex)
        {
            HorizontalAlignment alignment = rows[rowIndex][columnIndex].HorizontalAlignment;

            if (alignment == HorizontalAlignment.Default)
            {
                Column column = Columns.Count > columnIndex ? Columns[columnIndex] : null;
                if (column != null)
                {
                    alignment = column.HorizontalAlignment;
                }

                if (alignment == HorizontalAlignment.Default)
                {
                    alignment = HorizontalAlignment == HorizontalAlignment.Default
                        ? HorizontalAlignment.Left
                        : DefaultHorizontalAlignment;
                }
            }

            return alignment;
        }

        private string GetTitleTopBorder(TableDimensions tableDimensions)
        {
            StringBuilder value = new StringBuilder();

            value.Append(Border.TopLeft);
            value.Append(string.Empty.PadRight(tableDimensions.Width - 2, Border.Top));
            value.Append(Border.TopRight);

            return value.ToString();
        }

        private string GetTitleRowSeparator(TableDimensions tableDimensions)
        {
            StringBuilder value = new StringBuilder();

            value.Append(Border.LeftIntersection);

            for (int columnIndex = 0; columnIndex < tableDimensions.ColumnsWidth.Count; columnIndex++)
            {
                int columnWidth = tableDimensions.ColumnsWidth[columnIndex];
                value.Append(string.Empty.PadRight(columnWidth, Border.Top));

                char columnBorderRight = columnIndex < tableDimensions.ColumnsWidth.Count - 1
                    ? Border.TopIntersection
                    : Border.RightIntersection;

                value.Append(columnBorderRight);
            }

            return value.ToString();
        }

        /// <summary>
        /// Returns the line border between two rows.
        /// </summary>
        /// <param name="tableDimensions">The <see cref="TableDimensions"/> instance used to create the border line.</param>
        /// <returns>The line border between two rows</returns>
        /// <exception cref="ArgumentNullException"></exception>
        private string GetRowSeparatorBorder(TableDimensions tableDimensions)
        {
            StringBuilder value = new StringBuilder();

            value.Append(Border.LeftIntersection);

            for (int columnIndex = 0; columnIndex < tableDimensions.ColumnsWidth.Count; columnIndex++)
            {
                int columnWidth = tableDimensions.ColumnsWidth[columnIndex];
                value.Append(string.Empty.PadRight(columnWidth, Border.Horizontal));

                char columnBorderRight = columnIndex < tableDimensions.ColumnsWidth.Count - 1
                    ? Border.MiddleIntersection
                    : Border.RightIntersection;

                value.Append(columnBorderRight);
            }

            return value.ToString();
        }

        private string GetRowTopBorder(TableDimensions tableDimensions)
        {
            StringBuilder value = new StringBuilder();

            value.Append(Border.TopLeft);

            for (int columnIndex = 0; columnIndex < tableDimensions.ColumnsWidth.Count; columnIndex++)
            {
                int columnWidth = tableDimensions.ColumnsWidth[columnIndex];
                value.Append(string.Empty.PadRight(columnWidth, Border.Top));

                char columnBorderRight = columnIndex < tableDimensions.ColumnsWidth.Count - 1
                    ? Border.TopIntersection
                    : Border.TopRight;

                value.Append(columnBorderRight);
            }

            return value.ToString();
        }

        private string GetRowBottomBorder(TableDimensions tableDimensions)
        {
            StringBuilder value = new StringBuilder();

            value.Append(Border.BottomLeft);

            for (int columnIndex = 0; columnIndex < tableDimensions.ColumnsWidth.Count; columnIndex++)
            {
                int columnWidth = tableDimensions.ColumnsWidth[columnIndex];
                value.Append(string.Empty.PadRight(columnWidth, Border.Bottom));

                char columnBorderRight = columnIndex < tableDimensions.ColumnsWidth.Count - 1
                    ? Border.BottomIntersection
                    : Border.BottomRight;

                value.Append(columnBorderRight);
            }

            return value.ToString();
        }

        public void SetCellAlignment(int rowIndex, int columnIndex, HorizontalAlignment alignment)
        {
            Row row = rows[rowIndex];
            Cell cell = row[columnIndex];

            cell.HorizontalAlignment = alignment;
        }
    }
}
