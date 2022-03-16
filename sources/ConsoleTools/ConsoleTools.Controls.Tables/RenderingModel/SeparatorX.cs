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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DustInTheWind.ConsoleTools.Controls.Tables.RenderingModel
{
    internal class SeparatorX : IItemX
    {
        private List<bool> row1VerticalBorders;
        private List<bool> row2VerticalBorders;
        private int verticalBorderCount;

        public RowX Row1 { get; set; }

        public RowX Row2 { get; set; }

        public BorderTemplate BorderTemplate { get; set; }

        public ConsoleColor? ForegroundColor { get; set; }

        public ConsoleColor? BackgroundColor { get; set; }

        public void Render(ITablePrinter tablePrinter, IReadOnlyList<ColumnX> columns)
        {
            string line = BuildLine(columns);
            tablePrinter.WriteLine(line, ForegroundColor, BackgroundColor);
        }

        private string BuildLine(IReadOnlyList<ColumnX> columns)
        {
            verticalBorderCount = columns.Count + 1;
            row1VerticalBorders = Row1?.CalculateVerticalBorderVisibility(columns.Count) ?? Enumerable.Repeat(false, verticalBorderCount).ToList();
            row2VerticalBorders = Row2?.CalculateVerticalBorderVisibility(columns.Count) ?? Enumerable.Repeat(false, verticalBorderCount).ToList();

            StringBuilder sb = new();

            for (int i = 0; i < columns.Count; i++)
            {
                char cornerChar = CalculateCornerChar(i);
                sb.Append(cornerChar);

                string mainLine = new(BorderTemplate.Horizontal, columns[i].Width);
                sb.Append(mainLine);
            }

            char endingCorner = CalculateCornerChar(columns.Count);
            sb.Append(endingCorner);

            return sb.ToString();
        }

        private char CalculateCornerChar(int verticalBorderIndex)
        {
            bool row1VerticalBorder = row1VerticalBorders[verticalBorderIndex];
            bool row2VerticalBorder = row2VerticalBorders[verticalBorderIndex];

            if (row1VerticalBorder)
            {
                return row2VerticalBorder
                    ? GetMiddleRowCorner(verticalBorderIndex)
                    : GetBottomRowCorner(verticalBorderIndex);
            }
            else
            {
                return row2VerticalBorder
                    ? GetTopRowCorner(verticalBorderIndex) 
                    : GetNoRowCorner();
            }
        }

        private char GetMiddleRowCorner(int verticalBorderIndex)
        {
            bool isFirstCorner = verticalBorderIndex == 0;
            if (isFirstCorner)
                return BorderTemplate.LeftIntersection;

            bool isLastCorner = verticalBorderIndex == verticalBorderCount - 1;
            if (isLastCorner)
                return BorderTemplate.RightIntersection;

            return BorderTemplate.MiddleIntersection;
        }

        private char GetBottomRowCorner(int verticalBorderIndex)
        {
            bool isFirstCorner = verticalBorderIndex == 0;
            if (isFirstCorner)
                return BorderTemplate.BottomLeft;

            bool isLastCorner = verticalBorderIndex == verticalBorderCount - 1;
            if (isLastCorner)
                return BorderTemplate.BottomRight;

            return BorderTemplate.BottomIntersection;
        }

        private char GetTopRowCorner(int verticalBorderIndex)
        {
            bool isFirstCorner = verticalBorderIndex == 0;
            if (isFirstCorner)
                return BorderTemplate.TopLeft;

            bool isLastCorner = verticalBorderIndex == verticalBorderCount - 1;
            if (isLastCorner)
                return BorderTemplate.TopRight;

            return BorderTemplate.TopIntersection;
        }

        private char GetNoRowCorner()
        {
            return BorderTemplate.Horizontal;
        }

        public static SeparatorX CreateFrom(DataGridBorder dataGridBorder)
        {
            return new SeparatorX
            {
                BorderTemplate = dataGridBorder.Template,
                ForegroundColor = dataGridBorder.ForegroundColor,
                BackgroundColor = dataGridBorder.BackgroundColor
            };
        }
    }
}