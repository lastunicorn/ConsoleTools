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
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DustInTheWind.ConsoleTools.Controls.Tables.Printers;
using DustInTheWind.ConsoleTools.Controls.Tables.RenderingModel;

namespace DustInTheWind.ConsoleTools.Controls.Tables
{
    /// <summary>
    /// A control that renders a data grid into the console.
    /// </summary>
    public class DataGrid : BlockControl
    {
        private TitleRow titleRow;
        private HeaderRow headerRow;
        private DataGridBorder border;

        /// <summary>
        /// Gets the <see cref="TitleRow"/> instance that represents the title row of the data grid.
        /// </summary>
        public TitleRow TitleRow
        {
            get => titleRow;
            private set
            {
                titleRow = value;
                titleRow.ParentDataGrid = this;
            }
        }

        /// <summary>
        /// Gets or sets the title of the current instance of the <see cref="DataGrid"/>.
        /// </summary>
        public MultilineText Title
        {
            get => TitleRow.TitleCell.Content;
            set => TitleRow.TitleCell.Content = value;
        }

        /// <summary>
        /// Gets or sets the horizontal alignment for the content of the cells contained by the current data grid.
        /// </summary>
        public HorizontalAlignment CellHorizontalAlignment { get; set; } = ConsoleTools.HorizontalAlignment.Default;

        /// <summary>
        /// Gets or sets the padding applied to the left side of every cell.
        /// </summary>
        public int? CellPaddingLeft { get; set; }

        /// <summary>
        /// Gets or sets the padding applied to the right side of every cell.
        /// </summary>
        public int? CellPaddingRight { get; set; }

        /// <summary>
        /// Gets the list of columns contained by the current data grid.
        /// </summary>
        public ColumnList Columns { get; }

        /// <summary>
        /// Gets the columns header row of the data grid.
        /// </summary>
        public HeaderRow HeaderRow
        {
            get => headerRow;
            private set
            {
                headerRow = value;
                headerRow.ParentDataGrid = this;
            }
        }

        /// <summary>
        /// Gets the list of rows contained by the current data grid.
        /// </summary>
        public DataRowList Rows { get; }

        /// <summary>
        /// Gets the row at the specified index.
        /// </summary>
        /// <param name="rowIndex">The zero-based index of the row to get.</param>
        /// <returns>The row at the specified index.</returns>
        public DataRow this[int rowIndex] => Rows[rowIndex];

        /// <summary>
        /// Gets the cell at the specified location.
        /// </summary>
        /// <param name="rowIndex">The zero-based row index of the cell to get.</param>
        /// <param name="columnIndex">The zero-based column index of the cell to get.</param>
        /// <returns>The cell at the specified location.</returns>
        public DataCell this[int rowIndex, int columnIndex] => Rows[rowIndex][columnIndex];

        /// <summary>
        /// Gets an object representing the border of the data grid.
        /// </summary>
        public DataGridBorder Border
        {
            get => border;
            private set
            {
                border = value;
                border.ParentDataGrid = this;
            }
        }

        #region Obsolete Properties

        /// <summary>
        /// Gets or sets a value that specifies if the title is displayed.
        /// </summary>
        [Obsolete("Use TitleRow.IsVisible property instead.")]
        public bool DisplayTitle
        {
            get => TitleRow.IsVisible;
            set => TitleRow.IsVisible = value;
        }

        /// <summary>
        /// Gets or sets the foreground color for the title.
        /// Default value: <c>null</c>
        /// </summary>
        [Obsolete("Use TitleRow.ForegroundColor property instead.")]
        public ConsoleColor? TitleColor
        {
            get => TitleRow.ForegroundColor;
            set => TitleRow.ForegroundColor = value;
        }

        /// <summary>
        /// Gets or sets the background color for the title.
        /// Default value: <c>null</c>
        /// </summary>
        [Obsolete("Use TitleRow.BackgroundColor property instead.")]
        public ConsoleColor? TitleBackgroundColor
        {
            get => TitleRow.BackgroundColor;
            set => TitleRow.BackgroundColor = value;
        }

        /// <summary>
        /// Gets or sets a value that specifies if the column headers are displayed.
        /// Default value: <c>true</c>
        /// </summary>
        [Obsolete("Use HeaderRow.IsVisible property instead.")]
        public bool DisplayColumnHeaders
        {
            get => HeaderRow.IsVisible;
            set => HeaderRow.IsVisible = value;
        }

        /// <summary>
        /// Gets or sets the foreground color for the column headers.
        /// Default value: <c>null</c>
        /// </summary>
        [Obsolete("Use HeaderRow.ForegroundColor property instead.")]
        public ConsoleColor? HeaderColor
        {
            get => HeaderRow.ForegroundColor;
            set => HeaderRow.ForegroundColor = value;
        }

        /// <summary>
        /// Gets or sets the background color for the column headers.
        /// Default value: <c>null</c>
        /// </summary>
        [Obsolete("Use HeaderRow.BackgroundColor property instead.")]
        public ConsoleColor? HeaderBackgroundColor
        {
            get => HeaderRow.BackgroundColor;
            set => HeaderRow.BackgroundColor = value;
        }

        /// <summary>
        /// Gets or sets a value that specifies if the borders are visible.
        /// Default value: <c>true</c>
        /// </summary>
        [Obsolete("Use Border.IsVisible property instead.")]
        public bool DisplayBorder
        {
            get => Border.IsVisible;
            set => Border.IsVisible = value;
        }

        /// <summary>
        /// Gets or sets the data grid borders.
        /// </summary>
        [Obsolete("Use Border.Template property instead.")]
        public BorderTemplate BorderTemplate
        {
            get => Border.Template;
            set => Border.Template = value;
        }

        /// <summary>
        /// Gets or sets the foreground color for the borders.
        /// Default value: <c>null</c>
        /// </summary>
        [Obsolete("Use Border.ForegroundColor property instead.")]
        public ConsoleColor? BorderColor
        {
            get => Border.ForegroundColor;
            set => Border.ForegroundColor = value;
        }

        /// <summary>
        /// Gets or sets the background color for the borders.
        /// Default value: <c>null</c>
        /// </summary>
        [Obsolete("Use Border.BackgroundColor property instead.")]
        public ConsoleColor? BorderBackgroundColor
        {
            get => Border.BackgroundColor;
            set => Border.BackgroundColor = value;
        }

        /// <summary>
        /// Gets a value that specifies if border lines should be drawn between rows.
        /// Default value: false
        /// </summary>
        [Obsolete("Use Border.DisplayBorderBetweenRows property instead.")]
        public bool DisplayBorderBetweenRows
        {
            get => border.DisplayBorderBetweenRows;
            set => border.DisplayBorderBetweenRows = value;
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="DataGrid"/> class.
        /// </summary>
        public DataGrid()
        {
            Rows = new DataRowList(this);
            Columns = new ColumnList(this);
            HeaderRow = new HeaderRow(Columns);
            TitleRow = new TitleRow();
            Border = new DataGridBorder();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataGrid"/> class with
        /// the data grid title.
        /// </summary>
        public DataGrid(string title)
        {
            Rows = new DataRowList(this);
            Columns = new ColumnList(this);
            HeaderRow = new HeaderRow(Columns);
            TitleRow = new TitleRow(title);
            Border = new DataGridBorder();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataGrid"/> class with
        /// the data grid title.
        /// </summary>
        public DataGrid(MultilineText title)
        {
            Rows = new DataRowList(this);
            Columns = new ColumnList(this);
            HeaderRow = new HeaderRow(Columns);
            TitleRow = new TitleRow(title);
            Border = new DataGridBorder();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataGrid"/> class with
        /// the data grid title.
        /// </summary>
        public DataGrid(object title)
        {
            Rows = new DataRowList(this);
            Columns = new ColumnList(this);
            HeaderRow = new HeaderRow(Columns);
            TitleRow = new TitleRow(title);
            Border = new DataGridBorder();
        }

        /// <summary>
        /// Renders the current instance into the console.
        /// </summary>
        protected override void DoDisplayContent(ControlDisplay display)
        {
            ConsoleTablePrinter consoleTablePrinter = new ConsoleTablePrinter();
            RenderInternal(consoleTablePrinter);
        }

        /// <summary>
        /// Renders the current instance into the specified <see cref="ITablePrinter"/>.
        /// </summary>
        /// <param name="tablePrinter">The <see cref="ITablePrinter"/> instance used to render the data.</param>
        public void Render(ITablePrinter tablePrinter)
        {
            RenderInternal(tablePrinter);
        }

        private void RenderInternal(ITablePrinter tablePrinter)
        {
            DataGridXBuilder dataGridXBuilder = new DataGridXBuilder
            {
                MinWidth = MinWidth ?? 0,
                TitleRow = TitleRow,
                HeaderRow = HeaderRow,
                Rows = Rows,
                DataGridBorder = Border
            };

            DataGridX dataGridX = dataGridXBuilder.Build();

            dataGridX.Render(tablePrinter);
        }

        /// <summary>
        /// Returns the string representation of the current instance.
        /// </summary>
        /// <returns>The string representation of the current instance.</returns>
        public override string ToString()
        {
            StringTablePrinter tablePrinter = new StringTablePrinter();
            RenderInternal(tablePrinter);

            return tablePrinter.ToString();
        }

        /// <summary>
        /// Creates a new <see cref="DataGrid"/> instance containing the data from the specified <see cref="DataTable"/>.
        /// </summary>
        /// <param name="dataTable">The <see cref="DataTable"/> instance that contains the data.</param>
        /// <returns>The newly created <see cref="DataGrid"/> instance.</returns>
        public static DataGrid BuildFrom(DataTable dataTable)
        {
            if (dataTable == null) throw new ArgumentNullException(nameof(dataTable));

            DataGridBuilderFromDataTable builder = new DataGridBuilderFromDataTable(dataTable);
            return builder.DataGrid;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="DataGrid"/> and populates it with the
        /// fields and properties from the specified collection of objects.
        /// </summary>
        /// <typeparam name="T">The type of the objects used to populate the <see cref="DataGrid"/>.</typeparam>
        /// <param name="data">The collection of objects to be added to the <see cref="DataGrid"/>.</param>
        /// <returns>The newly created <see cref="DataGrid"/> instance.</returns>
        public static DataGrid BuildFrom<T>(IEnumerable<T> data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            DataGridBuilderFromObject builder = new DataGridBuilderFromObject(typeof(T));
            builder.Add(data);

            return builder.DataGrid;
        }

        public static DataGrid BuildFrom<T>(T data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            DataGridBuilderFromObject builder;

            Type enumerableType = typeof(T).GetInterfaces()
                .FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>));

            if (enumerableType != null)
            {
                Type[] genericArguments = enumerableType.GetGenericArguments();

                Type genericArgument = genericArguments[0];
                builder = new DataGridBuilderFromObject(genericArgument);

                builder.Add((IEnumerable)data);
            }
            else
            {
                builder = new DataGridBuilderFromObject(typeof(T));

                builder.Add(data);
            }

            return builder.DataGrid;
        }
    }
}