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

namespace DustInTheWind.ConsoleTools.Controls.Tables
{
    /// <summary>
    /// Represents a column in the <see cref="DataGrid"/> class.
    /// </summary>
    public class Column
    {
        /// <summary>
        /// Gets or sets the cell displayed in the header of the current column.
        /// </summary>
        public HeaderCell HeaderCell { get; }
        
        /// <summary>
        /// Gets or sets the <see cref="DataGrid"/> instance that contains the current <see cref="Column"/> instance.
        /// </summary>
        public DataGrid ParentDataGrid { get; internal set; }

        /// <summary>
        /// Gets or sets the foreground color applied to all the cells in the column.
        /// Default value: <c>null</c>
        /// </summary>
        public ConsoleColor? ForegroundColor { get; set; }

        /// <summary>
        /// Gets or sets the background color applied to all the cells in the column.
        /// Default value: <c>null</c>
        /// </summary>
        public ConsoleColor? BackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the horizontal alignment for the content of the cells represented by the current instance of the <see cref="Column"/>.
        /// </summary>
        public HorizontalAlignment CellHorizontalAlignment { get; set; }

        /// <summary>
        /// Gets or sets the padding applied to the left side of every cell.
        /// </summary>
        public int? CellPaddingLeft { get; set; }

        /// <summary>
        /// Gets or sets the padding applied to the right side of every cell.
        /// </summary>
        public int? CellPaddingRight { get; set; }

        /// <summary>
        /// Gets or sets the visibility of the column.
        /// If it is set to <c>false</c>, the column is ignored when <see cref="DataGrid"/> is rendered.
        /// </summary>
        public bool IsVisible { get; set; } = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="Column"/> class.
        /// </summary>
        public Column()
        {
            HeaderCell = new HeaderCell
            {
                ParentColumn = this
            };
            CellHorizontalAlignment = HorizontalAlignment.Default;
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
            CellHorizontalAlignment = HorizontalAlignment.Default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Column"/> class with
        /// the a name and the horizontal alignment applied to the cells represented by the column.
        /// </summary>
        public Column(string header, HorizontalAlignment horizontalAlignment)
        {
            HeaderCell = new HeaderCell(header)
            {
                ParentColumn = this
            };
            CellHorizontalAlignment = horizontalAlignment;
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
            CellHorizontalAlignment = HorizontalAlignment.Default;
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
            CellHorizontalAlignment = horizontalAlignment;
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
            CellHorizontalAlignment = HorizontalAlignment.Default;
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
            CellHorizontalAlignment = horizontalAlignment;
        }
    }
}