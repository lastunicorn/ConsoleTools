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
using System.Text;

namespace DustInTheWind.ConsoleTools.TabularData
{
    public class TableBorder
    {
        public static readonly TableBorder PlusMinusBorder = new TableBorder("+-+|+-+|+++++|-");
        public static readonly TableBorder SingleLineBorder = new TableBorder("┌─┐│┘─└│┬┤┴├┼│─");
        public static readonly TableBorder DoubleLineBorder = new TableBorder("╔═╗║╝═╚║╦╣╩╠╬║═");

        public readonly char TopLeft;
        public readonly char Top;
        public readonly char TopRight;
        public readonly char Right;
        public readonly char BottomRight;
        public readonly char Bottom;
        public readonly char BottomLeft;
        public readonly char Left;

        public readonly char TopIntersection;
        public readonly char RightIntersection;
        public readonly char BottomIntersection;
        public readonly char LeftIntersection;
        public readonly char MiddleIntersection;

        public readonly char Vertical;
        public readonly char Horizontal;

        public TableBorder(string values)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));

            if (values.Length != 15)
                throw new ArgumentException("Please provide 15 characters for the border in order: top-left, top, top-right, right, bottom-right, bottom, bottom-left, left, top-intersection, right-intersection, bottom-intersection, left-intersection, middle-intersection, vertical, horizontal.", nameof(values));

            TopLeft = values[0];
            Top = values[1];
            TopRight = values[2];
            Right = values[3];
            BottomRight = values[4];
            Bottom = values[5];
            BottomLeft = values[6];
            Left = values[7];

            TopIntersection = values[8];
            RightIntersection = values[9];
            BottomIntersection = values[10];
            LeftIntersection = values[11];
            MiddleIntersection = values[12];

            Vertical = values[13];
            Horizontal = values[14];
        }

        public TableBorder(char topLeft, char top, char topRight, char right, char bottomRight, char bottom, char bottomLeft, char left,
            char topIntersection, char rightIntersection, char bottomIntersection, char leftIntersection, char middleIntersection,
            char vertical, char horizontal)
        {
            TopLeft = topLeft;
            Top = top;
            TopRight = topRight;
            Right = right;
            BottomRight = bottomRight;
            Bottom = bottom;
            BottomLeft = bottomLeft;
            Left = left;

            TopIntersection = topIntersection;
            RightIntersection = rightIntersection;
            BottomIntersection = bottomIntersection;
            LeftIntersection = leftIntersection;
            MiddleIntersection = middleIntersection;

            Vertical = vertical;
            Horizontal = horizontal;
        }

        /// <summary>
        /// Generates the border displayed at the top of the title row.
        /// </summary>
        public string GenerateTitleTopBorder(TableDimensions dimensions)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(TopLeft);
            sb.Append(string.Empty.PadRight(dimensions.CalculatedTotalWidth - 2, Top));
            sb.Append(TopRight);

            return sb.ToString();
        }

        /// <summary>
        /// Generates the border displayed at the bottom of the title row.
        /// This border is used only when the column header rows is not visible and there are no data to display.
        /// </summary>
        public string GenerateTitleBottomBorder(TableDimensions dimensions)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(BottomLeft);
            sb.Append(string.Empty.PadRight(dimensions.CalculatedTotalWidth - 2, Bottom));
            sb.Append(BottomRight);

            return sb.ToString();
        }

        /// <summary>
        /// Generates the border displayed between title and column header rows.
        /// This border is used only when both title and column header rows are visible.
        /// </summary>
        public string GenerateTitleHeaderSeparator(TableDimensions dimensions)
        {
            List<int> columnWidths = dimensions.CalculatedColumnsWidth;

            StringBuilder sb = new StringBuilder();

            sb.Append(LeftIntersection);

            for (int columnIndex = 0; columnIndex < columnWidths.Count; columnIndex++)
            {
                int columnWidth = columnWidths[columnIndex];
                sb.Append(string.Empty.PadRight(columnWidth, Horizontal));

                char columnBorderRight = columnIndex < columnWidths.Count - 1
                    ? TopIntersection
                    : RightIntersection;

                sb.Append(columnBorderRight);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Generates the border displayed between title and the first data row.
        /// This border is used only when title is visible, column header row is hidden and there is at least one row of data.
        /// </summary>
        public string GenerateTitleDataSeparator(TableDimensions dimensions)
        {
            List<int> columnWidths = dimensions.CalculatedColumnsWidth;

            StringBuilder sb = new StringBuilder();

            sb.Append(LeftIntersection);

            for (int columnIndex = 0; columnIndex < columnWidths.Count; columnIndex++)
            {
                int columnWidth = columnWidths[columnIndex];
                sb.Append(string.Empty.PadRight(columnWidth, Horizontal));

                char columnBorderRight = columnIndex < columnWidths.Count - 1
                    ? TopIntersection
                    : RightIntersection;

                sb.Append(columnBorderRight);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Generates the border displayed at the top of the column header row.
        /// This border is used only when title is hidden and the column header row is visible, being the first row of the table.
        /// </summary>
        public string GenerateHeaderTopBorder(TableDimensions dimensions)
        {
            List<int> columnWidths = dimensions.CalculatedColumnsWidth;

            StringBuilder sb = new StringBuilder();

            sb.Append(TopLeft);

            for (int columnIndex = 0; columnIndex < columnWidths.Count; columnIndex++)
            {
                int columnWidth = columnWidths[columnIndex];
                sb.Append(string.Empty.PadRight(columnWidth, Top));

                char columnBorderRight = columnIndex < columnWidths.Count - 1
                    ? TopIntersection
                    : TopRight;

                sb.Append(columnBorderRight);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Generates the border displayed at the bottom of the column header row.
        /// This border is used only when the column header row is visible and it is the last row of the table.
        /// </summary>
        public string GenerateHeaderBottomBorder(TableDimensions dimensions)
        {
            List<int> columnWidths = dimensions.CalculatedColumnsWidth;

            StringBuilder sb = new StringBuilder();

            sb.Append(BottomLeft);

            for (int columnIndex = 0; columnIndex < columnWidths.Count; columnIndex++)
            {
                int columnWidth = columnWidths[columnIndex];
                sb.Append(string.Empty.PadRight(columnWidth, Bottom));

                char columnBorderRight = columnIndex < columnWidths.Count - 1
                    ? BottomIntersection
                    : BottomRight;

                sb.Append(columnBorderRight);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Generates the border displayed between two data rows.
        /// </summary>
        public string GenerateDataRowSeparatorBorder(TableDimensions dimensions)
        {
            List<int> columnWidths = dimensions.CalculatedColumnsWidth;

            StringBuilder sb = new StringBuilder();

            sb.Append(LeftIntersection);

            for (int columnIndex = 0; columnIndex < columnWidths.Count; columnIndex++)
            {
                int columnWidth = columnWidths[columnIndex];
                sb.Append(string.Empty.PadRight(columnWidth, Horizontal));

                char columnBorderRight = columnIndex < columnWidths.Count - 1
                    ? MiddleIntersection
                    : RightIntersection;

                sb.Append(columnBorderRight);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Generates the border displayed at the top of the first data row.
        /// This border is used only when title and column header rows are hidden and the first data row is the first row of the table.
        /// </summary>
        public string GenerateDataRowTopBorder(TableDimensions dimensions)
        {
            List<int> columnWidths = dimensions.CalculatedColumnsWidth;

            StringBuilder sb = new StringBuilder();

            sb.Append(TopLeft);

            for (int columnIndex = 0; columnIndex < columnWidths.Count; columnIndex++)
            {
                int columnWidth = columnWidths[columnIndex];
                sb.Append(string.Empty.PadRight(columnWidth, Top));

                char columnBorderRight = columnIndex < columnWidths.Count - 1
                    ? TopIntersection
                    : TopRight;

                sb.Append(columnBorderRight);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Generates the border displayed at the bottom of the last data row.
        /// </summary>
        public string GenerateDataRowBottomBorder(TableDimensions dimensions)
        {
            List<int> columnWidths = dimensions.CalculatedColumnsWidth;

            StringBuilder sb = new StringBuilder();

            sb.Append(BottomLeft);

            for (int columnIndex = 0; columnIndex < columnWidths.Count; columnIndex++)
            {
                int columnWidth = columnWidths[columnIndex];
                sb.Append(string.Empty.PadRight(columnWidth, Bottom));

                char columnBorderRight = columnIndex < columnWidths.Count - 1
                    ? BottomIntersection
                    : BottomRight;

                sb.Append(columnBorderRight);
            }

            return sb.ToString();
        }
    }
}