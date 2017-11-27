// WindTools
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

namespace DustInTheWind.WindTools.TabularData
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

        public bool DisplayColumnHeaders { get; set; }

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
        }

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

            int titleRowWidth = 1 + PaddingLeft + PaddingRight + 1;
            if (Title != null)
            {
                titleRowWidth += Title.Size.Width;
            }
            if (dimensions.Width < titleRowWidth) dimensions.Width = titleRowWidth;

            // Calculate the header dimensions.

            if (DisplayColumnHeaders)
            {
                int headerRowWidth = 1; // The table left border
                int headerRowHeight = 0;

                for (int i = 0; i < Columns.Count; i++)
                {
                    Column column = Columns[i];

                    dimensions.ColumnsWidth.Add(0);

                    int cellWidth = 0;
                    int cellHeight = 0;

                    if (column.Header != null)
                    {
                        cellWidth = PaddingLeft + column.Header.Size.Width + PaddingRight;
                        cellHeight = column.Header.Size.Height;
                    }
                    else
                    {
                        cellWidth = PaddingLeft + PaddingRight;
                    }

                    if (dimensions.ColumnsWidth[i] < cellWidth)
                    {
                        dimensions.ColumnsWidth[i] = cellWidth;
                        headerRowWidth += cellWidth + 1; // The cell width + cell right border
                    }
                    else
                    {
                        headerRowWidth += dimensions.ColumnsWidth[i] + 1; // The cell width + cell right border
                    }

                    if (headerRowHeight < cellHeight)
                    {
                        headerRowHeight = cellHeight;
                    }
                }

                dimensions.HeaderHeight = headerRowHeight;

                if (longRowWidth < headerRowWidth) longRowWidth = headerRowWidth;
            }

            //

            for (int i = 0; i < rows.Count; i++)
            {
                Row row = rows[i];

                int rowWidth = 1; // The table left border
                int rowHeight = 0;

                for (int j = 0; j < row.Cells.Count; j++)
                {
                    Cell cell = row.Cells[j];

                    if (j == dimensions.ColumnsWidth.Count)
                    {
                        dimensions.ColumnsWidth.Add(0);
                    }

                    int cellWidth = 0;
                    int cellHeight = 0;

                    if (cell.Content != null)
                    {
                        cellWidth = PaddingLeft + cell.Content.Size.Width + PaddingRight;
                        cellHeight = cell.Content.Size.Height;
                    }
                    else
                    {
                        cellWidth = PaddingLeft + PaddingRight;
                    }

                    if (dimensions.ColumnsWidth[j] < cellWidth)
                    {
                        dimensions.ColumnsWidth[j] = cellWidth;
                        rowWidth += cellWidth + 1; // The cell width + cell right border
                    }
                    else
                    {
                        rowWidth += dimensions.ColumnsWidth[j] + 1; // The cell width + cell right border
                    }

                    if (rowHeight < cellHeight)
                    {
                        rowHeight = cellHeight;
                    }
                }

                dimensions.RowsHeight.Add(rowHeight);

                if (longRowWidth < rowWidth) longRowWidth = rowWidth;
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
                {
                    dimensions.ColumnsWidth[i % colCount]++;
                }
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
            string rowSeparator = GetHorizontalRowBorder(dimensions);

            DrawTableTitle(tablePrinter, dimensions, rowSeparator);
            DrawColumnHeaders(tablePrinter, dimensions, rowSeparator);
            DrawRows(tablePrinter, dimensions, rowSeparator);
        }

        private void DrawTableTitle(ITablePrinter tablePrinter, TableDimensions dimensions, string rowSeparator)
        {
            if (Title.Size.Height > 0)
            {
                // Write top border
                tablePrinter.WriteLineBorder("+" + string.Empty.PadRight(dimensions.Width - 2, '-') + "+");

                // Write title
                for (int i = 0; i < Title.Size.Height; i++)
                {
                    tablePrinter.WriteBorder("| ");
                    tablePrinter.WriteTitle(Title.Lines[i].PadRight(dimensions.Width - 4, ' '));
                    tablePrinter.WriteLineBorder(" |");
                }
            }

            // Write bottom border <=> header top border
            tablePrinter.WriteLineBorder(rowSeparator);
        }

        private void DrawColumnHeaders(ITablePrinter tablePrinter, TableDimensions dimensions, string rowSeparator)
        {
            if (!DisplayColumnHeaders || dimensions.HeaderHeight == 0)
                return;

            // Write header cells
            for (int headerLineIndex = 0; headerLineIndex < dimensions.HeaderHeight; headerLineIndex++)
            {
                tablePrinter.WriteBorder("|");

                for (int columnIndex = 0; columnIndex < Columns.Count; columnIndex++)
                {
                    string content = BuildHeaderContent(dimensions, columnIndex, headerLineIndex);

                    tablePrinter.WriteHeader(content);
                    tablePrinter.WriteBorder("|");
                }

                tablePrinter.WriteLine();
            }

            // Write bottom border <=> first row top border
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
                    if (rowLineIndex > 0) tablePrinter.WriteLine();

                    tablePrinter.WriteBorder("|");

                    for (int columnIndex = 0; columnIndex < row.Cells.Count; columnIndex++)
                    {
                        Cell cell = row.Cells[columnIndex];
                        string content = BuildCellContent(dimensions, rowIndex, columnIndex, cell, rowLineIndex);

                        tablePrinter.WriteNormal(content);
                        tablePrinter.WriteBorder("|");
                    }
                }

                if (DrawLinesBetweenRows)
                {
                    tablePrinter.WriteLine();
                    tablePrinter.WriteLineBorder(rowSeparator);
                }
            }

            if (!DrawLinesBetweenRows)
            {
                tablePrinter.WriteLine();
                tablePrinter.WriteLineBorder(rowSeparator);
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
                    int rightSpaces = (int) Math.Ceiling((double) totalSpaces / 2);
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

        /// <summary>
        /// Returns the line border between two rows.
        /// </summary>
        /// <param name="tableDimensions">The <see cref="TableDimensions"/> instance used to create the border line.</param>
        /// <returns>The line border between two rows</returns>
        /// <exception cref="ArgumentNullException"></exception>
        private string GetHorizontalRowBorder(TableDimensions tableDimensions)
        {
            if (tableDimensions == null) throw new ArgumentNullException(nameof(tableDimensions));

            StringBuilder value = new StringBuilder("+");

            foreach (int columnWidth in tableDimensions.ColumnsWidth)
            {
                value.Append(string.Empty.PadRight(columnWidth, '-'));
                value.Append("+");
            }

            return value.ToString();
        }
    }
}
