// ConsoleTools
// Copyright (C) 2017-2022 Dust in the Wind
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

using System.Collections.Generic;
using System.Linq;

namespace DustInTheWind.ConsoleTools.Controls.Tables.RenderingModel
{
    internal class DataGridX
    {
        private readonly List<ColumnX> columns = new();
        private readonly List<ColumnSpanX> columnSpans = new();

        private readonly List<IItemX> items = new();
        private IItemX lastItem;

        public int MinWidth { get; set; }

        public int ItemCount => items.Count;

        public bool IsBorderVisible { get; set; }

        public void Add(SeparatorX separator)
        {
            separator.Row1 = lastItem as RowX;
            items.Add(separator);
            lastItem = separator;
        }

        public void Add(RowX rowX)
        {
            if (lastItem is SeparatorX lastSeparator)
                lastSeparator.Row2 = rowX;

            items.Add(rowX);
            UpdateColumnsWidths(rowX);
            lastItem = rowX;
        }

        private void UpdateColumnsWidths(RowX rowX)
        {
            for (int i = 0; i < rowX.Cells.Count; i++)
            {
                while (columns.Count <= i)
                    columns.Add(new ColumnX());

                CellX cellX = rowX.Cells[i];
                Size cellSize = cellX.Size;

                if (cellX.HorizontalMerge > 1)
                {
                    ColumnSpanX columnSpanX = new()
                    {
                        StartColumnIndex = i,
                        EndColumnIndex = i + cellX.HorizontalMerge - 1,
                        MinContentWidth = cellSize.Width
                    };
                    columnSpans.Add(columnSpanX);
                }
                else
                {
                    if (cellSize.Width > columns[i].Width)
                        columns[i].Width = cellSize.Width;
                }
            }
        }

        public void Finish()
        {
            int actualWidth = CalculateTotalWidth();
        }

        private int CalculateTotalWidth()
        {
            if (columns.Count == 0)
                return MinWidth;

            // Distribute column span spaces

            foreach (ColumnSpanX columnSpan in columnSpans)
                InflateColumns(columnSpan.StartColumnIndex, columnSpan.SpanValue ?? int.MaxValue, columnSpan.MinContentWidth);

            // Distribute space to reach min width.

            int desiredWidthWithoutExternalBorders = IsBorderVisible
                ? MinWidth - 2
                : MinWidth;

            InflateColumns(0, int.MaxValue, desiredWidthWithoutExternalBorders);


            int totalWidth = columns
                .Select(x => x.Width)
                .Sum();

            if (IsBorderVisible)
                totalWidth += columns.Count + 1;

            return totalWidth;
        }

        internal void InflateColumns(int startColumnIndex, int columnCount, int desiredWidth)
        {
            ColumnX[] spanColumns = columns
                .Skip(startColumnIndex)
                .Take(columnCount)
                .ToArray();

            int actualWidth = spanColumns
                .Select(x => x.Width)
                .Sum();

            if (IsBorderVisible)
                actualWidth += spanColumns.Length - 1;

            if (actualWidth < desiredWidth)
            {
                int diff = desiredWidth - actualWidth;

                for (int i = 0; i < diff; i++)
                {
                    ColumnX columnX = columns[i % spanColumns.Length];
                    columnX.Width++;
                }
            }
        }

        public void Render(IDisplay display)
        {
            foreach (IItemX item in items)
                item.Render(display, columns);
        }
    }
}