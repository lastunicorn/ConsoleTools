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

using System.Collections.Generic;

namespace DustInTheWind.ConsoleTools.TabularData
{
    /// <summary>
    /// Represents the dimensions of a table displayed in text mode.
    /// </summary>
    internal class TableDimensions
    {
        /// <summary>
        /// Gets or sets the minimum width of the table.
        /// </summary>
        public int MinWidth { get; set; }

        /// <summary>
        /// Gets or sets a value that specifies if the borders are visible.
        /// </summary>
        public bool DisplayBorder { get; set; }

        /// <summary>
        /// Gets or sets a value that specifies if thew column headers are displayed.
        /// </summary>
        public bool DisplayColumnHeaders { get; set; }

        /// <summary>
        /// Gets or sets the padding applyed to the left side of every cell.
        /// </summary>
        public int PaddingLeft { get; set; }

        /// <summary>
        /// Gets or sets the padding applyed to the right side of every cell.
        /// </summary>
        public int PaddingRight { get; set; }

        /// <summary>
        /// Gets or sets the title of the current instance of the <see cref="Table"/>.
        /// </summary>
        public MultilineText Title { get; set; }

        /// <summary>
        /// Gets the list of columns contained by the current table.
        /// </summary>
        public List<Column> Columns { get; set; }

        /// <summary>
        /// The list of rows contained by the current table.
        /// </summary>
        public List<Row> Rows { get; set; }

        /// <summary>
        /// Gets or sets the width of the table.
        /// </summary>
        public int CalculatedWidth { get; private set; }

        /// <summary>
        /// Gets or sets the height of the header.
        /// </summary>
        public int CalculatedHeaderHeight { get; private set; }

        /// <summary>
        /// Gets a list containing the widths of the columns.
        /// </summary>
        public List<int> CalculatedColumnsWidth { get; } = new List<int>();

        /// <summary>
        /// Gets a list containing the heights of the rows.
        /// </summary>
        public List<int> CalculatedRowsHeight { get; } = new List<int>();

        /// <summary>
        /// Calculates and returns the dimensions of the current instance of the <see cref="Table"/> displayed in text mode.
        /// </summary>
        /// <returns>The dimensions of the current instance of the <see cref="Table"/> displayed in text mode.</returns>
        public void CalculateTableDimensions()
        {
            CalculatedWidth = MinWidth > 0 ? MinWidth : 0;

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


            if (CalculatedWidth < titleRowWidth)
                CalculatedWidth = titleRowWidth;

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

                    CalculatedColumnsWidth.Add(0);

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

                    if (CalculatedColumnsWidth[i] < cellWidth)
                    {
                        CalculatedColumnsWidth[i] = cellWidth;
                        headerRowWidth += cellWidth;

                        // The cell right border
                        if (DisplayBorder)
                            headerRowWidth += 1;
                    }
                    else
                    {
                        headerRowWidth += CalculatedColumnsWidth[i] + 1; // The cell width + cell right border
                    }

                    if (headerRowHeight < cellHeight)
                        headerRowHeight = cellHeight;
                }

                CalculatedHeaderHeight = headerRowHeight;

                if (longRowWidth < headerRowWidth) longRowWidth = headerRowWidth;
            }

            //

            for (int i = 0; i < Rows.Count; i++)
            {
                Row row = Rows[i];

                int rowWidth = 0;
                int rowHeight = 0;

                // The table left border
                if (DisplayBorder)
                    rowWidth += 1;

                for (int j = 0; j < row.Cells.Count; j++)
                {
                    Cell cell = row.Cells[j];

                    if (j == CalculatedColumnsWidth.Count)
                        CalculatedColumnsWidth.Add(0);

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

                    if (CalculatedColumnsWidth[j] < cellWidth)
                    {
                        CalculatedColumnsWidth[j] = cellWidth;
                        rowWidth += cellWidth;

                        // The cell right border
                        if (DisplayBorder)
                            rowWidth += 1;
                    }
                    else
                    {
                        rowWidth += CalculatedColumnsWidth[j];

                        // The cell right border
                        if (DisplayBorder)
                            rowWidth += 1;
                    }

                    if (rowHeight < cellHeight)
                        rowHeight = cellHeight;
                }

                CalculatedRowsHeight.Add(rowHeight);

                if (longRowWidth < rowWidth)
                    longRowWidth = rowWidth;
            }

            if (CalculatedWidth < longRowWidth)
            {
                CalculatedWidth = longRowWidth;
            }
            else if (CalculatedWidth > longRowWidth)
            {
                int diff = CalculatedWidth - longRowWidth;
                int colCount = CalculatedColumnsWidth.Count;

                for (int i = 0; i < diff; i++)
                    CalculatedColumnsWidth[i % colCount]++;
            }
        }
    }
}