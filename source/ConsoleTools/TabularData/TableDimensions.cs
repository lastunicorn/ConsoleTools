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
using System.Linq;

namespace DustInTheWind.ConsoleTools.TabularData
{
    /// <summary>
    /// Represents the dimensions of a table displayed in text mode.
    /// </summary>
    public class TableDimensions
    {
        #region Input data

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

        #endregion

        #region Calculated Data

        public int CalculatedTitleRowWidth { get; private set; }

        /// <summary>
        /// Gets the total width of the table calculated by the current instance.
        /// </summary>
        public int CalculatedTotalWidth { get; private set; }

        /// <summary>
        /// Gets the height of the header calculated by the current instance.
        /// </summary>
        public int CalculatedHeaderRowHeight { get; private set; }

        public int CalculatedHeaderRowWidth { get; private set; }

        /// <summary>
        /// Gets a list containing the calculated widths of the columns.
        /// </summary>
        public List<int> CalculatedColumnsWidth { get; } = new List<int>();

        /// <summary>
        /// Gets a list containing the calculated heights of the rows.
        /// </summary>
        public List<int> CalculatedRowsHeight { get; } = new List<int>();

        private int longestDataRowWidth;

        #endregion

        /// <summary>
        /// Calculates and returns the dimensions of the current instance of the <see cref="Table"/> displayed in text mode.
        /// </summary>
        /// <returns>The dimensions of the current instance of the <see cref="Table"/> displayed in text mode.</returns>
        public void CalculateTableDimensions()
        {
            ClearAll();

            CalculateTitleRowDimensions();
            CalculateHeaderRowDimensions();

            if (Rows.Count > 0)
                CalculateDataRowsDimensions();

            CalculateTotalWidth();

            InflateColumnWidthIfNeeded();
        }

        private void ClearAll()
        {
            CalculatedTotalWidth = 0;

            CalculatedTitleRowWidth = 0;

            CalculatedHeaderRowHeight = 0;
            CalculatedHeaderRowWidth = 0;

            CalculatedColumnsWidth.Clear();
            CalculatedRowsHeight.Clear();

            longestDataRowWidth = 0;
        }

        private void CalculateTotalWidth()
        {
            if (CalculatedTotalWidth < MinWidth)
                CalculatedTotalWidth = MinWidth;

            if (CalculatedTotalWidth < CalculatedTitleRowWidth)
                CalculatedTotalWidth = CalculatedTitleRowWidth;

            if (CalculatedTotalWidth < CalculatedHeaderRowWidth)
                CalculatedTotalWidth = CalculatedHeaderRowWidth;

            if (CalculatedTotalWidth < longestDataRowWidth)
                CalculatedTotalWidth = longestDataRowWidth;
        }

        private void CalculateTitleRowDimensions()
        {
            if (Title == null || Title.Size.Height == 0)
                return;

            if (DisplayBorder)
                CalculatedTitleRowWidth += 1;

            CalculatedTitleRowWidth += PaddingLeft;

            if (Title != null)
                CalculatedTitleRowWidth += Title.Size.Width;

            CalculatedTitleRowWidth += PaddingRight;

            if (DisplayBorder)
                CalculatedTitleRowWidth += 1;
        }

        private void CalculateHeaderRowDimensions()
        {
            if (Columns.Count <= 0 || !DisplayColumnHeaders)
                return;

            // The table left border
            if (DisplayBorder)
                CalculatedHeaderRowWidth += 1;

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
                    CalculatedHeaderRowWidth += cellWidth;

                    // The cell right border
                    if (DisplayBorder)
                        CalculatedHeaderRowWidth += 1;
                }
                else
                {
                    CalculatedHeaderRowWidth += CalculatedColumnsWidth[i];

                    // The cell right border
                    if (DisplayBorder)
                        CalculatedHeaderRowWidth += 1;
                }

                if (CalculatedHeaderRowHeight < cellHeight)
                    CalculatedHeaderRowHeight = cellHeight;
            }
        }

        private void CalculateDataRowsDimensions()
        {
            foreach (Row row in Rows)
            {
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

                if (longestDataRowWidth < rowWidth)
                    longestDataRowWidth = rowWidth;
            }
        }

        private void InflateColumnWidthIfNeeded()
        {
            if (CalculatedColumnsWidth.Count == 0)
                return;

            int columnsTotalWidth = CalculatedColumnsWidth.Sum();
            if (DisplayBorder)
                columnsTotalWidth += CalculatedColumnsWidth.Count + 1;

            if (columnsTotalWidth < CalculatedTotalWidth)
            {
                int diff = CalculatedTotalWidth - columnsTotalWidth;
                int colCount = CalculatedColumnsWidth.Count;

                for (int i = 0; i < diff; i++)
                    CalculatedColumnsWidth[i % colCount]++;
            }
        }
    }
}