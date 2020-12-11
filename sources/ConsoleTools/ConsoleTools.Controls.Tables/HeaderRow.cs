using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DustInTheWind.ConsoleTools.Controls.Tables
{
    /// <summary>
    /// Represents the column header row of a table.
    /// </summary>
    public class HeaderRow : IEnumerable<HeaderCell>
    {
        private readonly ColumnList columns;

        /// <summary>
        /// Gets or sets the <see cref="DataGrid"/> instance that contains the current header row instance.
        /// </summary>
        public DataGrid ParentDataGrid { get; internal set; }

        /// <summary>
        /// Gets the number of cells contained by the header row.
        /// </summary>
        public int CellCount => columns.Count();

        /// <summary>
        /// Gets or sets a value that specifies if the column headers are displayed.
        /// Default value: <c>true</c>
        /// </summary>
        public bool IsVisible { get; set; } = true;

        /// <summary>
        /// Gets or sets the foreground color for the column headers.
        /// Default value: <c>null</c>
        /// </summary>
        public ConsoleColor? ForegroundColor { get; set; }

        /// <summary>
        /// Gets or sets the background color for the column headers.
        /// Default value: <c>null</c>
        /// </summary>
        public ConsoleColor? BackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the horizontal alignment for the content of all column header cells.
        /// </summary>
        public HorizontalAlignment CellHorizontalAlignment { get; set; } = HorizontalAlignment.Default;

        /// <summary>
        /// Gets or sets the padding applied to the left side of every cell.
        /// </summary>
        public int? CellPaddingLeft { get; set; }

        /// <summary>
        /// Gets or sets the padding applied to the right side of every cell.
        /// </summary>
        public int? CellPaddingRight { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderRow"/> class with
        /// the list of columns
        /// and the <see cref="DataGrid"/> that owns it.
        /// </summary>
        public HeaderRow(ColumnList columns)
        {
            this.columns = columns ?? throw new ArgumentNullException(nameof(columns));
        }

        /// <summary>
        /// Renders the row containing the column headers.
        /// </summary>
        /// <param name="tablePrinter">The destination where the current instance must be rendered.</param>
        /// <param name="cellWidths">The widths of each header cell that must be rendered.</param>
        /// <param name="rowHeight">The height of the row to be rendered. If there are not enough text lines
        /// in the content of a cell, spaces are written instead.</param>
        public void Render(ITablePrinter tablePrinter, List<int> cellWidths, int rowHeight)
        {
            // Get cells content.
            List<List<string>> cellContents = columns
                .Select((x, i) =>
                {
                    Size size = new Size(cellWidths[i], rowHeight);
                    return x.HeaderCell.RenderText(size).ToList();
                })
                .ToList();

            bool displayBorder = ParentDataGrid?.DisplayBorder ?? true;
            BorderTemplate borderTemplate = ParentDataGrid?.BorderTemplate;

            for (int headerLineIndex = 0; headerLineIndex < rowHeight; headerLineIndex++)
            {
                // Write left border.
                if (displayBorder && borderTemplate != null)
                    tablePrinter.WriteBorder(borderTemplate.Left);

                for (int columnIndex = 0; columnIndex < columns.Count; columnIndex++)
                {
                    // Write cell content.
                    string content = cellContents[columnIndex][headerLineIndex];
                    tablePrinter.WriteColumnHeader(content);

                    // Write intermediate or right border.
                    if (displayBorder && borderTemplate != null)
                    {
                        char cellBorderRight = columnIndex < columns.Count - 1
                            ? borderTemplate.Vertical
                            : borderTemplate.Right;

                        tablePrinter.WriteBorder(cellBorderRight);
                    }
                }

                tablePrinter.WriteLine();
            }
        }

        public IEnumerator<HeaderCell> GetEnumerator()
        {
            return new HeaderCellEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #region HeaderCellEnumerator Class

        private class HeaderCellEnumerator : IEnumerator<HeaderCell>
        {
            private readonly HeaderRow headerRow;
            private int index = -1;

            public HeaderCell Current { get; private set; }

            object IEnumerator.Current => Current;

            public HeaderCellEnumerator(HeaderRow headerRow)
            {
                this.headerRow = headerRow ?? throw new ArgumentNullException(nameof(headerRow));
            }

            public bool MoveNext()
            {
                index++;

                if (index >= headerRow.columns.Count)
                    return false;

                Current = headerRow.columns[index].HeaderCell;

                return true;
            }

            public void Reset()
            {
                index = -1;
                Current = null;
            }

            public void Dispose()
            {
            }
        }

        #endregion
    }
}