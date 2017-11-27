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

namespace DustInTheWind.ConsoleTools.TabularData
{
    /// <summary>
    /// Represents a column in the <see cref="Table"/> class.
    /// </summary>
    public class Column
    {
        /// <summary>
        /// Gets or sets the name of the column.
        /// </summary>
        public string Name { get; }

        public MultilineText Header { get; set; }

        /// <summary>
        /// Gets or sets the horizontal alignment for the content of the cells represented by the current instance of the <see cref="Column"/>.
        /// </summary>
        public HorizontalAlignment HorizontalAlignment { get; set; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Column"/> class with
        /// the a name.
        /// </summary>
        /// <param name="name">The name of the column.</param>
        public Column(string name)
            : this(name, new MultilineText(name))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Column"/> class with
        /// the a name and the horizontal alignment applyed to the cells represented by the column.
        /// </summary>
        /// <param name="name">The name of the column.</param>
        public Column(string name, HorizontalAlignment horizontalAlignment)
            : this(name, new MultilineText(name), horizontalAlignment)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Column"/> class.
        /// </summary>
        /// <param name="name">The name of the column.</param>
        public Column(string name, MultilineText header, HorizontalAlignment horizontalAlignment = HorizontalAlignment.Default)
        {
            Name = name;
            Header = header;
            HorizontalAlignment = horizontalAlignment;
        }
    }
}
