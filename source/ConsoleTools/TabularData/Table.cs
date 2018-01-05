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

namespace DustInTheWind.ConsoleTools.TabularData
{
    /// <summary>
    /// A control that renders a table with data into the console.
    /// </summary>
    public class Table
    {
        /// <summary>
        /// Gets the <see cref="TitleRow"/> instance that represence the title row of the table.
        /// </summary>
        public TitleRow TitleRow { get; }

        /// <summary>
        /// Gets or sets the title of the current instance of the <see cref="Table"/>.
        /// </summary>
        public MultilineText Title
        {
            get { return TitleRow.Content; }
            set { TitleRow.Content = value; }
        }

        /// <summary>
        /// Gets or sets a value that specifies if the title is displayed.
        /// </summary>
        public bool DisplayTitle { get; set; } = true;

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
        /// Default value: false
        /// </summary>
        public bool DisplayBorderBetweenRows { get; set; }

        /// <summary>
        /// Gets or sets the horizontal alignment for the content of the cells contained by the current table.
        /// </summary>
        public HorizontalAlignment CellHorizontalAlignment { get; set; } = HorizontalAlignment.Default;

        /// <summary>
        /// Gets the list of columns contained by the current table.
        /// </summary>
        public ColumnList Columns { get; }

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
        /// Gets or sets a value that specifies if the column headers are displayed.
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
        public DataCell this[int rowIndex, int columnIndex] => rows[rowIndex][columnIndex];

        /// <summary>
        /// Gets or sets a value that specifies if the borders are visible.
        /// </summary>
        public bool DisplayBorder { get; set; } = true;

        /// <summary>
        /// Gets or sets the table borders.
        /// </summary>
        public BorderTemplate BorderTemplate { get; set; } = BorderTemplate.PlusMinusBorderTemplate;

        /// <summary>
        /// Initializes a new instance of the <see cref="Table"/> class.
        /// </summary>
        public Table()
        {
            Columns = new ColumnList(this);

            TitleRow = new TitleRow
            {
                ParentTable = this
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Table"/> class with
        /// the table title.
        /// </summary>
        public Table(string title)
        {
            Columns = new ColumnList(this);

            TitleRow = new TitleRow(title)
            {
                ParentTable = this
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Table"/> class with
        /// the table title.
        /// </summary>
        public Table(MultilineText title)
        {
            Columns = new ColumnList(this);

            TitleRow = new TitleRow(title)
            {
                ParentTable = this
            };
        }

        /// <summary>
        /// Adds a new row to the current table.
        /// </summary>
        /// <param name="row">The row to be added.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void AddRow(Row row)
        {
            if (row == null) throw new ArgumentNullException(nameof(row));

            row.ParentTable = this;
            rows.Add(row);
        }

        /// <summary>
        /// Adds a new row to the current table.
        /// </summary>
        /// <param name="cells">The list of cells of the new row.</param>
        public void AddRow(DataCell[] cells)
        {
            if (cells == null) throw new ArgumentNullException(nameof(cells));

            Row row = new Row(cells)
            {
                ParentTable = this
            };
            rows.Add(row);
        }

        /// <summary>
        /// Adds a new row to the current table.
        /// </summary>
        /// <param name="texts">The list of cell contents of the new row.</param>
        public void AddRow(string[] texts)
        {
            if (texts == null) throw new ArgumentNullException(nameof(texts));

            Row row = new Row
            {
                ParentTable = this
            };

            foreach (string text in texts)
                row.AddCell(new DataCell(text));

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

        /// <summary>
        /// Renders the current instance into the specified <see cref="ITablePrinter"/>.
        /// </summary>
        /// <param name="tablePrinter">The <see cref="ITablePrinter"/> instacne used to render the data.</param>
        public void Render(ITablePrinter tablePrinter)
        {
            TableRenderer tableRenderer = new TableRenderer
            {
                TitleRow = TitleRow,
                DisplayTitle = DisplayTitle,
                Columns = Columns,
                Rows = rows,
                BorderTemplate = BorderTemplate,
                DisplayBorder = DisplayBorder,
                DrawLinesBetweenRows = DisplayBorderBetweenRows,
                MinWidth = MinWidth,
                PaddingLeft = PaddingLeft,
                PaddingRight = PaddingRight,
                DisplayColumnHeaders = DisplayColumnHeaders,
                CellHorizontalAlignment = CellHorizontalAlignment
            };
            tableRenderer.Render(tablePrinter);
        }
    }
}
