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

// --------------------------------------------------------------------------------
// Bugs or fearure requests
// --------------------------------------------------------------------------------
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new

namespace DustInTheWind.ConsoleTools.TabularData
{
    /// <summary>
    /// Represents a column in the <see cref="Table"/> class.
    /// </summary>
    public class Column
    {
        /// <summary>
        /// Gets or sets the cell displayed in the header of the current column.
        /// </summary>
        public HeaderCell HeaderCell { get; }

        /// <summary>
        /// Gets or sets the text displayed in the header.
        /// </summary>
        public MultilineText Header
        {
            get { return HeaderCell.Content; }
            set { HeaderCell.Content = value; }
        }

        /// <summary>
        /// Gets or sets the horizontal alignment for the content of the cells represented by the current instance of the <see cref="Column"/>.
        /// </summary>
        public HorizontalAlignment HorizontalAlignment { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Table"/> instance that contains the current <see cref="Column"/> instance.
        /// </summary>
        public Table ParentTable { get; set; }

        /// <summary>
        /// Gets or sets the padding applyed to the left side of every cell.
        /// </summary>
        public int? PaddingLeft { get; set; }

        /// <summary>
        /// Gets or sets the padding applyed to the right side of every cell.
        /// </summary>
        public int? PaddingRight { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Column"/> class.
        /// </summary>
        public Column()
        {
            HeaderCell = new HeaderCell
            {
                ParentColumn = this
            };
            HorizontalAlignment = HorizontalAlignment.Default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Column"/> class with
        /// the a header.
        /// </summary>
        public Column(string header)
        {
            HeaderCell = new HeaderCell(header)
            {
                ParentColumn = this
            };
            HorizontalAlignment = HorizontalAlignment.Default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Column"/> class with
        /// the a name and the horizontal alignment applyed to the cells represented by the column.
        /// </summary>
        public Column(string header, HorizontalAlignment horizontalAlignment)
        {
            HeaderCell = new HeaderCell(header)
            {
                ParentColumn = this
            };
            HorizontalAlignment = horizontalAlignment;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Column"/> class.
        /// </summary>
        public Column(MultilineText header)
        {
            HeaderCell = new HeaderCell(header)
            {
                ParentColumn = this
            };
            HorizontalAlignment = HorizontalAlignment.Default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Column"/> class.
        /// </summary>
        public Column(MultilineText header, HorizontalAlignment horizontalAlignment)
        {
            HeaderCell = new HeaderCell(header)
            {
                ParentColumn = this
            };
            HorizontalAlignment = horizontalAlignment;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Column"/> class.
        /// </summary>
        public Column(object header)
        {
            HeaderCell = new HeaderCell(header)
            {
                ParentColumn = this
            };
            HorizontalAlignment = HorizontalAlignment.Default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Column"/> class.
        /// </summary>
        public Column(object header, HorizontalAlignment horizontalAlignment)
        {
            HeaderCell = new HeaderCell(header)
            {
                ParentColumn = this
            };
            HorizontalAlignment = horizontalAlignment;
        }
    }
}
