// ConsoleTools
// Copyright (C) 2017-2024 Dust in the Wind
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
using DustInTheWind.ConsoleTools.Controls.Rendering;

namespace DustInTheWind.ConsoleTools.Controls.Tables;

/// <summary>
/// A control that renders a data grid into the console.
/// </summary>
public class DataGrid : BlockControl
{
    private TitleRow titleRow;
    private HeaderRow headerRow;
    private DataGridBorder border;
    private EmptyGridRow emptyGridRow;
    private FooterRow footerRow;

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
    public HorizontalAlignment CellHorizontalAlignment { get; set; } = Controls.HorizontalAlignment.Default;

    /// <summary>
    /// Gets or sets the padding applied to the left side of every content cell.
    /// </summary>
    public int? CellPaddingLeft { get; set; }

    /// <summary>
    /// Gets or sets the padding applied to the right side of every content cell.
    /// </summary>
    public int? CellPaddingRight { get; set; }

    /// <summary>
    /// Gets or sets the padding applied to the top side of every content cell.
    /// </summary>
    public int? CellPaddingTop { get; set; }

    /// <summary>
    /// Gets or sets the padding applied to the bottom side of every content cell.
    /// </summary>
    public int? CellPaddingBottom { get; set; }

    /// <summary>
    /// Gets or sets the default content to be displayed in a content cell if it has no explicitly
    /// set content.
    /// </summary>
    public MultilineText CellDefaultContent { get; set; }

    /// <summary>
    /// Gets or sets the content overflow behavior for all the content cells contained by the
    /// current data grid.
    /// The content overflow for title, header and footer cells must be set individually.
    /// </summary>
    public CellContentOverflow CellContentOverflow { get; set; }

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
    public ContentRowList Rows { get; }

    /// <summary>
    /// Gets the row at the specified index.
    /// </summary>
    /// <param name="rowIndex">The zero-based index of the row to get.</param>
    /// <returns>The row at the specified index.</returns>
    public ContentRow this[int rowIndex] => Rows[rowIndex];

    /// <summary>
    /// Gets the cell at the specified location.
    /// </summary>
    /// <param name="rowIndex">The zero-based row index of the cell to get.</param>
    /// <param name="columnIndex">The zero-based column index of the cell to get.</param>
    /// <returns>The cell at the specified location.</returns>
    public ContentCell this[int rowIndex, int columnIndex] => Rows[rowIndex][columnIndex];

    /// <summary>
    /// Gets an object representing the border of the data grid.
    /// </summary>
    [Obsolete("Use the other properties related to border.")]
    public DataGridBorder Border
    {
        get => border;
        private set
        {
            border = value;
            border.ParentDataGrid = this;
        }
    }

    /// <summary>
    /// Gets a value that specifies if border lines should be drawn between rows.
    /// Default value: false
    /// </summary>
    public bool DisplayBorderBetweenRows
    {
        get => border.DisplayBorderBetweenRows;
        set => border.DisplayBorderBetweenRows = value;
    }

    /// <summary>
    /// Gets the <see cref="FooterRow"/> instance that represents the footer row of the data grid.
    /// </summary>
    public FooterRow FooterRow
    {
        get => footerRow;
        private set
        {
            footerRow = value;
            footerRow.ParentDataGrid = this;
        }
    }

    /// <summary>
    /// Gets or sets the footer text for the current instance of the <see cref="DataGrid"/>.
    /// </summary>
    public MultilineText Footer
    {
        get => FooterRow.FooterCell.Content;
        set => FooterRow.FooterCell.Content = value;
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
    [Obsolete("Use IsBorderVisible property instead.")]
    public bool DisplayBorder
    {
        get => Border.IsVisible;
        set => Border.IsVisible = value;
    }

    /// <summary>
    /// Gets or sets the data grid borders.
    /// </summary>
    public BorderTemplate BorderTemplate
    {
        get => Border.Template;
        set => Border.Template = value;
    }

    /// <summary>
    /// Gets or sets the foreground color for the borders.
    /// Default value: <c>null</c>
    /// </summary>
    [Obsolete("Use BorderForegroundColor property instead.")]
    public ConsoleColor? BorderColor
    {
        get => Border.ForegroundColor;
        set => Border.ForegroundColor = value;
    }

    /// <summary>
    /// Gets or sets the foreground color for the borders.
    /// Default value: <c>null</c>
    /// </summary>
    public ConsoleColor? BorderForegroundColor
    {
        get => Border.ForegroundColor;
        set => Border.ForegroundColor = value;
    }

    /// <summary>
    /// Gets or sets the background color for the borders.
    /// Default value: <c>null</c>
    /// </summary>
    public ConsoleColor? BorderBackgroundColor
    {
        get => Border.BackgroundColor;
        set => Border.BackgroundColor = value;
    }

    #endregion

    /// <summary>
    /// Gets or sets a value specifying if any border at all is allowed to be displayed.
    /// If this value is <c>false</c>, borders from row, column or cell level are ignored, too.
    /// </summary>
    public bool AreBordersAllowed { get; set; } = true;

    /// <summary>
    /// Gets or sets a value specifying if the grid level border is displayed or not.
    /// If a row, column or cell is specifying custom borders they will be displayed regardless of this value. 
    /// </summary>
    public bool IsBorderVisible
    {
        get => Border.IsVisible;
        set => Border.IsVisible = value;
    }

    /// <summary>
    /// Gets the row containing the message to be displayed in the content area when there is no
    /// data in the grid.
    /// </summary>
    public EmptyGridRow EmptyGridRow
    {
        get => emptyGridRow;
        private set
        {
            emptyGridRow = value;
            emptyGridRow.ParentDataGrid = this;
        }
    }

    /// <summary>
    /// Gets or sets the text to be displayed in the content area when there is no data in the
    /// grid.
    /// </summary>
    public MultilineText EmptyGridMessage
    {
        get => EmptyGridRow.EmptyGridCell.Content;
        set => EmptyGridRow.EmptyGridCell.Content = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataGrid"/> class.
    /// </summary>
    public DataGrid()
    {
        Rows = new ContentRowList(this);
        Columns = new ColumnList(this);
        HeaderRow = new HeaderRow(Columns);
        TitleRow = new TitleRow();
        FooterRow = new FooterRow();
        EmptyGridRow = new EmptyGridRow();
        Border = new DataGridBorder();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataGrid"/> class with
    /// the data grid title.
    /// </summary>
    public DataGrid(string title)
    {
        Rows = new ContentRowList(this);
        Columns = new ColumnList(this);
        HeaderRow = new HeaderRow(Columns);
        TitleRow = new TitleRow(title);
        FooterRow = new FooterRow();
        EmptyGridRow = new EmptyGridRow();
        Border = new DataGridBorder();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataGrid"/> class with
    /// the data grid title.
    /// </summary>
    public DataGrid(MultilineText title)
    {
        Rows = new ContentRowList(this);
        Columns = new ColumnList(this);
        HeaderRow = new HeaderRow(Columns);
        TitleRow = new TitleRow(title);
        FooterRow = new FooterRow();
        EmptyGridRow = new EmptyGridRow();
        Border = new DataGridBorder();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataGrid"/> class with
    /// the data grid title.
    /// </summary>
    public DataGrid(object title)
    {
        Rows = new ContentRowList(this);
        Columns = new ColumnList(this);
        HeaderRow = new HeaderRow(Columns);
        TitleRow = new TitleRow(title);
        FooterRow = new FooterRow();
        EmptyGridRow = new EmptyGridRow();
        Border = new DataGridBorder();
    }

    protected override int NaturalContentWidth => 0;

    /// <summary>
    /// Creates a new <see cref="IRenderer"/> for the current instance.
    /// </summary>
    public override IRenderer GetRenderer(IDisplay display, RenderingOptions renderingOptions = null)
    {
        return new DataGridRenderer(this, display, renderingOptions);
    }

    /// <summary>
    /// Creates a new <see cref="DataGrid"/> instance containing the data from the specified <see cref="DataTable"/>.
    /// </summary>
    /// <param name="dataTable">The <see cref="DataTable"/> instance that contains the data.</param>
    /// <returns>The newly created <see cref="DataGrid"/> instance.</returns>
    public static DataGrid BuildFrom(DataTable dataTable)
    {
        if (dataTable == null) throw new ArgumentNullException(nameof(dataTable));

        DataGridBuilderFromDataTable builder = new(dataTable);
        return builder.DataGrid;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="DataGrid"/> and populates it with the
    /// fields and properties of the objects from the specified collection.
    /// </summary>
    /// <typeparam name="T">The type of the objects used to populate the <see cref="DataGrid"/>.</typeparam>
    /// <param name="data">The collection of objects to be added to the <see cref="DataGrid"/>.</param>
    /// <returns>The newly created <see cref="DataGrid"/> instance.</returns>
    public static DataGrid BuildFrom<T>(IEnumerable<T> data)
    {
        if (data == null) throw new ArgumentNullException(nameof(data));

        DataGridBuilderFromObject builder = new(typeof(T));
        builder.Add(data);

        return builder.DataGrid;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="DataGrid"/> and populates it with the
    /// fields and properties of the objects from the specified collection.
    /// </summary>
    /// <param name="data">The collection of objects to be added to the <see cref="DataGrid"/>.</param>
    /// <returns>The newly created <see cref="DataGrid"/> instance.</returns>
    public static DataGrid BuildFrom(IEnumerable data)
    {
        if (data == null) throw new ArgumentNullException(nameof(data));

        DataGridBuilderFromObject builder = new(data.GetType());
        builder.Add(data);

        return builder.DataGrid;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="DataGrid"/> and populates it with the
    /// fields and properties of the specified object.
    /// </summary>
    /// <param name="data">The object to be added to the <see cref="DataGrid"/>.</param>
    /// <returns>The newly created <see cref="DataGrid"/> instance.</returns>
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