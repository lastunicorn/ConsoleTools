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
    /// Represents a table cell that contains data.
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// Gets or sets the content of the cell.
        /// </summary>
        public MultilineText Content { get; set; }

        /// <summary>
        /// Gets or sets the row that contains the current cell.
        /// </summary>
        public Row ParentRow { get; set; }

        public bool IsEmpty => Content == null || Content.IsEmpty;

        /// <summary>
        /// Gets or sets the horizontal alignment of the content displayed in the cell.
        /// </summary>
        public HorizontalAlignment HorizontalAlignment { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell" /> class with
        /// the text contained by it.
        /// </summary>
        /// <param name="text">The text displayed in the cell.</param>
        public Cell(string text)
        {
            Content = new MultilineText(text);
            HorizontalAlignment = HorizontalAlignment.Default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell" /> class with
        /// the text contained by it and its horizontal alignment.
        /// </summary>
        /// <param name="text">The text displayed in the cell.</param>
        /// <param name="horizontalAlignment">The horizontal alignment of the content of the new cell.</param>
        public Cell(string text, HorizontalAlignment horizontalAlignment)
        {
            Content = new MultilineText(text);
            HorizontalAlignment = horizontalAlignment;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell" /> class with
        /// the text contained by it and its horizontal alignment.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="horizontalAlignment">The horizontal alignment of the content of the new cell.</param>
        public Cell(MultilineText text, HorizontalAlignment horizontalAlignment)
        {
            Content = text;
            HorizontalAlignment = horizontalAlignment;
        }

        public Cell(object content)
        {
            Content = new MultilineText(content.ToString());
            HorizontalAlignment = HorizontalAlignment.Default;
        }

        public Cell(object content, HorizontalAlignment horizontalAlignment)
        {
            Content = new MultilineText(content.ToString());
            HorizontalAlignment = horizontalAlignment;
        }

        public override string ToString()
        {
            return Content?.ToString() ?? string.Empty;
        }

        public static implicit operator Cell(string text)
        {
            MultilineText multilineText = new MultilineText(text);
            return new Cell(multilineText);
        }

        public static implicit operator string(Cell cell)
        {
            return cell.Content?.ToString() ?? string.Empty;
        }
    }
}