// ConsoleTools
// Copyright (C) 2017-2018 Dust in the Wind
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
    /// Represents a column in the <see cref="Table"/> class.
    /// </summary>
    public class Column
    {
        private readonly HeaderCell headerCell;

        /// <summary>
        /// Gets or sets the text displayed in the header.
        /// </summary>
        public MultilineText Header
        {
            get { return headerCell.Content; }
            set { headerCell.Content = value; }
        }

        /// <summary>
        /// Gets or sets the horizontal alignment for the content of the cells represented by the current instance of the <see cref="Column"/>.
        /// </summary>
        public HorizontalAlignment CellHorizontalAlignment { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Table"/> instance that contains the current <see cref="Column"/> instance.
        /// </summary>
        public Table ParentTable { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Column"/> class with
        /// the a header.
        /// </summary>
        public Column(string header)
        {
            headerCell = new HeaderCell
            {
                Content = header,
                ParentColumn = this
            };
            CellHorizontalAlignment = HorizontalAlignment.Default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Column"/> class with
        /// the a name and the horizontal alignment applyed to the cells represented by the column.
        /// </summary>
        public Column(string header, HorizontalAlignment cellHorizontalAlignment)
        {
            headerCell = new HeaderCell
            {
                Content = header,
                ParentColumn = this
            };
            CellHorizontalAlignment = cellHorizontalAlignment;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Column"/> class.
        /// </summary>
        public Column(MultilineText header)
        {
            headerCell = new HeaderCell(header)
            {
                Content = header,
                ParentColumn = this
            };
            CellHorizontalAlignment = HorizontalAlignment.Default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Column"/> class.
        /// </summary>
        public Column(MultilineText header, HorizontalAlignment cellHorizontalAlignment)
        {
            headerCell = new HeaderCell(header)
            {
                Content = header,
                ParentColumn = this
            };
            CellHorizontalAlignment = cellHorizontalAlignment;
        }

        /// <summary>
        /// Returns the header cell including the paddings.
        /// </summary>
        /// <param name="minWidth">The minimum width of the header cell.</param>
        /// <param name="minHeight">The minimum height of the header cell.</param>
        /// <returns>A <see cref="List{T}"/> containing the lines of the header cell.</returns>
        public List<string> RenderHeader(int minWidth, int minHeight)
        {
            return headerCell.Render(minWidth, minHeight);
        }
    }
}
