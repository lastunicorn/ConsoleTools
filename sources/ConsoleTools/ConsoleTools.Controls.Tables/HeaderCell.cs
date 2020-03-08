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

namespace DustInTheWind.ConsoleTools.Controls.Tables
{
    /// <summary>
    /// Represents the cell that contains a column header.
    /// </summary>
    public class HeaderCell : CellBase
    {
        /// <summary>
        /// Gets the default horizontal alignment for a column header cell.
        /// </summary>
        public static HorizontalAlignment DefaultHorizontalAlignment { get; } = HorizontalAlignment.Left;

        /// <summary>
        /// Gets or sets the column that contains the current cell.
        /// </summary>
        public Column ParentColumn { get; internal set; }

        /// <summary>
        /// Gets or sets the padding applyed to the left side of the cell.
        /// </summary>
        public int? PaddingLeft { get; set; }

        /// <summary>
        /// Gets or sets the padding applyed to the right side of the cell.
        /// </summary>
        public int? PaddingRight { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderCell" /> class with
        /// empty content.
        /// </summary>
        public HeaderCell()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderCell" /> class with
        /// the text contained by it.
        /// </summary>
        /// <param name="text">The text displayed in the cell.</param>
        public HeaderCell(string text)
            : base(text)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderCell" /> class with
        /// the text contained by it and its horizontal alignment.
        /// </summary>
        /// <param name="text">The text displayed in the cell.</param>
        /// <param name="horizontalAlignment">The horizontal alignment of the content of the new cell.</param>
        public HeaderCell(string text, HorizontalAlignment horizontalAlignment)
            : base(text, horizontalAlignment)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderCell" /> class with
        /// the text contained by it.
        /// </summary>
        /// <param name="text"></param>
        public HeaderCell(MultilineText text)
            : base(text)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderCell" /> class with
        /// the text contained by it and its horizontal alignment.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="horizontalAlignment">The horizontal alignment of the content of the new cell.</param>
        public HeaderCell(MultilineText text, HorizontalAlignment horizontalAlignment)
            : base(text, horizontalAlignment)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderCell" /> class with
        /// an object representing the content.
        /// </summary>
        public HeaderCell(object content)
            : base(content)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderCell" /> class with
        /// an object representing the content and its horizontal alignment.
        /// </summary>
        protected HeaderCell(object content, HorizontalAlignment horizontalAlignment)
            : base(content, horizontalAlignment)
        {
        }

        /// <summary>
        /// Calculates and returns the left padding for the content displayed in the cell.
        /// The value is calculated taking into account also the parent column and parent table.
        /// </summary>
        protected override int CalculatePaddingLeft()
        {
            if (PaddingLeft.HasValue)
                return PaddingLeft.Value;

            if (ParentColumn != null)
            {
                if (ParentColumn.PaddingLeft.HasValue)
                    return ParentColumn.PaddingLeft.Value;

                if (ParentColumn.ParentDataGrid?.PaddingLeft != null)
                    return ParentColumn.ParentDataGrid.PaddingLeft.Value;
            }

            return 0;
        }

        /// <summary>
        /// Calculates and returns the right padding for the content displayed in the cell.
        /// The value is calculated taking into account also the parent column and parent table.
        /// </summary>
        protected override int CalculatePaddingRight()
        {
            if (PaddingRight.HasValue)
                return PaddingRight.Value;

            if (ParentColumn != null)
            {
                if (ParentColumn.PaddingRight.HasValue)
                    return ParentColumn.PaddingRight.Value;

                if (ParentColumn.ParentDataGrid?.PaddingRight != null)
                    return ParentColumn.ParentDataGrid.PaddingRight.Value;
            }

            return 0;
        }

        /// <summary>
        /// Calculates and returns the horizontal alignment of the content displayed in the cell.
        /// The value is calculated taking into account also the parent row and parent table.
        /// </summary>
        protected override HorizontalAlignment CalculateHorizontalAlignment()
        {
            HorizontalAlignment alignment = HorizontalAlignment;

            if (alignment == HorizontalAlignment.Default)
                alignment = ParentColumn?.HorizontalAlignment ?? HorizontalAlignment.Default;

            if (alignment == HorizontalAlignment.Default)
                alignment = ParentColumn?.ParentDataGrid?.CellHorizontalAlignment ?? HorizontalAlignment.Default;

            if (alignment == HorizontalAlignment.Default)
                alignment = DefaultHorizontalAlignment;

            return alignment;
        }

        /// <summary>
        /// Converts the specified text into a <see cref="HeaderCell"/> instance.
        /// The text is used as the content for the cell.
        /// </summary>
        public static implicit operator HeaderCell(string text)
        {
            MultilineText multilineText = new MultilineText(text);
            return new HeaderCell(multilineText);
        }

        /// <summary>
        /// Converts the specified <see cref="HeaderCell"/> instance into a text.
        /// The text representation of the content is returned.
        /// </summary>
        public static implicit operator string(HeaderCell cell)
        {
            return cell.Content?.ToString() ?? string.Empty;
        }
    }
}