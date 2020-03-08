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
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new

using System;
using System.Collections.Generic;
using System.Text;

namespace DustInTheWind.ConsoleTools.Controls.Tables
{
    /// <summary>
    /// Builds the border that separates two rows.
    /// </summary>
    internal class HorizontalSeparatorBuilder
    {
        private StringBuilder sb;

        private int topIndex;
        private int bottomIndex;

        private int topWidth;
        private int bottomWidth;

        private bool topWidthEnded;
        private bool bottomWidthEnded;

        /// <summary>
        /// Gets the character used to render an internal horizontal border.
        /// </summary>
        public char Horizontal { get; set; }

        /// <summary>
        /// Gets the character used to render the intersection between the top outer border and an internal vertical border.
        /// </summary>
        public char TopIntersection { get; set; }

        /// <summary>
        /// Gets the character used to render the intersection between the right outer border and an internal horizontal border.
        /// </summary>
        public char RightIntersection { get; set; }

        /// <summary>
        /// Gets the character used to render the intersection between the bottom outer border and an internal vertical border.
        /// </summary>
        public char BottomIntersection { get; set; }

        /// <summary>
        /// Gets the character used to render the intersection between the left outer border and an internal horizontal border.
        /// </summary>
        public char LeftIntersection { get; set; }

        /// <summary>
        /// Gets the character used to render the intersection between two internal borders: a vertical and a horizontal one.
        /// </summary>
        public char MiddleIntersection { get; set; }

        /// <summary>
        /// Gets the character used to render the top right corner.
        /// </summary>
        public char TopRight { get; set; }

        /// <summary>
        /// Gets the character used to render the bottom right corner.
        /// </summary>
        public char BottomRight { get; set; }

        public IList<int> TopCellWidths { get; set; }
        public IList<int> BottomCellWidths { get; set; }

        public string Build()
        {
            if (TopCellWidths == null)
                throw new ApplicationException("TopCellWidths must be provided.");

            if (BottomCellWidths == null)
                throw new ApplicationException("BottomCellWidths must be provided.");

            if (TopCellWidths.Count == 0 && BottomCellWidths.Count == 0)
                return string.Empty;

            Start();

            while (true)
            {
                if (topWidthEnded)
                    AddBottomCell();
                else if (bottomWidthEnded)
                    AddTopCell();
                else if (topWidth < bottomWidth)
                    AddTopCell();
                else if (bottomWidth < topWidth)
                    AddBottomCell();
                else
                    AddBothCells();

                if (topWidth == 0 && bottomWidth == 0)
                    break;
            }

            return sb.ToString();
        }

        private void Start()
        {
            sb = new StringBuilder();

            topIndex = -1;
            bottomIndex = -1;

            topWidthEnded = false;
            bottomWidthEnded = false;

            sb.Append(LeftIntersection);

            NextTopWidth();
            NextBottomWidth();
        }

        private void AddTopCell()
        {
            sb.Append(new string(Horizontal, topWidth));

            if (bottomWidthEnded)
            {
                sb.Append(BottomRight);
            }
            else
            {
                sb.Append(BottomIntersection);
                DecreaseBottom(topWidth + 1);
            }

            NextTopWidth();
        }

        private void DecreaseBottom(int value)
        {
            bottomWidth = Math.Max(bottomWidth - value, 0);
        }

        private void AddBottomCell()
        {
            sb.Append(new string(Horizontal, bottomWidth));

            if (topWidthEnded)
            {
                sb.Append(TopRight);
            }
            else
            {
                sb.Append(TopIntersection);
                DecreaseTop(bottomWidth + 1);
            }

            NextBottomWidth();
        }

        private void DecreaseTop(int value)
        {
            topWidth = Math.Max(topWidth - value, 0);
        }

        private void AddBothCells()
        {
            sb.Append(new string(Horizontal, topWidth));

            if (IsLastTop() && IsLastBottom())
                sb.Append(RightIntersection);
            else
                sb.Append(MiddleIntersection);

            NextTopWidth();
            NextBottomWidth();
        }

        private bool IsLastTop()
        {
            return topIndex + 1 == TopCellWidths.Count;
        }

        private bool IsLastBottom()
        {
            return bottomIndex + 1 == BottomCellWidths.Count;
        }

        private void NextTopWidth()
        {
            if (topIndex + 1 < TopCellWidths.Count)
            {
                topIndex++;
                topWidth = TopCellWidths[topIndex];
            }
            else
            {
                topWidth = 0;
                topWidthEnded = true;
            }
        }

        private void NextBottomWidth()
        {
            if (bottomIndex + 1 < BottomCellWidths.Count)
            {
                bottomIndex++;
                bottomWidth = BottomCellWidths[bottomIndex];
            }
            else
            {
                bottomWidth = 0;
                bottomWidthEnded = true;
            }
        }
    }
}