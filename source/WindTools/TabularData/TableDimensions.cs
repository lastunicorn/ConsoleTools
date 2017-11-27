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
        /// Gets or sets the width of the table.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the height of the header.
        /// </summary>
        public int HeaderHeight { get; set; }

        /// <summary>
        /// Gets a list containing the widths of the columns.
        /// </summary>
        public List<int> ColumnsWidth { get; } = new List<int>();

        /// <summary>
        /// Gets a list containing the heights of the rows.
        /// </summary>
        public List<int> RowsHeight { get; } = new List<int>();
    }
}