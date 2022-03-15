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

namespace DustInTheWind.ConsoleTools.Controls.Tables.RenderingModel
{
    internal class TitleRowX
    {
        public Size Size { get; set; }

        public DataGridBorderX Border { get; set; }

        public CellX Cell { get; set; }

        public void CalculateLayout()
        {
            Size = CalculatePreferredSize();
        }

        private Size CalculatePreferredSize()
        {
            Size cellSize = Cell.Size;

            int rowWidth = cellSize.Width;
            int rowHeight = cellSize.Height;

            if (Border != null)
                rowWidth += 2;

            return new Size(rowWidth, rowHeight);
        }

        public void Render(ITablePrinter tablePrinter, Size actualSize)
        {
            Size cellSize = Border != null
                ? actualSize.InflateWidth(-2)
                : actualSize;

            for (int lineIndex = 0; lineIndex < cellSize.Height; lineIndex++)
            {
                Border?.RenderRowLeftBorder(tablePrinter);
                Cell.RenderNextLine(tablePrinter, cellSize);
                Border?.RenderRowRightBorder(tablePrinter);

                tablePrinter.WriteLine();
            }
        }

        public static TitleRowX CreateFrom(TitleRow titleRow)
        {
            CellX cellX = CellX.CreateFrom(titleRow.TitleCell);
            cellX.HorizontalMerge = int.MaxValue;

            TitleRowX titleRowX = new()
            {
                Cell = cellX,
                Border = titleRow.ParentDataGrid.Border.IsVisible
                    ? DataGridBorderX.CreateFrom(titleRow.ParentDataGrid.Border)
                    : null
            };

            titleRowX.CalculateLayout();

            return titleRowX;
        }
    }
}