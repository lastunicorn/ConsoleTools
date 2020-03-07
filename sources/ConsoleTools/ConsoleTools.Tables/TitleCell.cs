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

namespace DustInTheWind.ConsoleTools.TabularData
{
    /// <summary>
    /// Represents the single cell displayed in the title row.
    /// </summary>
    public class TitleCell : CellBase
    {
        /// <summary>
        /// Gets the default horizontal alignment for a title cell.
        /// </summary>
        public static HorizontalAlignment DefaultHorizontalAlignment { get; } = HorizontalAlignment.Left;

        /// <summary>
        /// Gets or sets the <see cref="TitleRow"/> instance that owns the current <see cref="TitleCell"/> instance.
        /// </summary>
        public TitleRow ParentRow { get; internal set; }

        /// <summary>
        /// Gets or sets the padding applyed to the left side of the cell.
        /// </summary>
        public int? PaddingLeft { get; set; }

        /// <summary>
        /// Gets or sets the padding applyed to the right side of the cell.
        /// </summary>
        public int? PaddingRight { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TitleCell" /> class with
        /// empty content.
        /// </summary>
        public TitleCell()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TitleCell" /> class with
        /// the text contained by it.
        /// </summary>
        /// <param name="text">The text displayed in the cell.</param>
        public TitleCell(string text)
            : base(text)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TitleCell" /> class with
        /// the text contained by it and its horizontal alignment.
        /// </summary>
        /// <param name="text">The text displayed in the cell.</param>
        /// <param name="horizontalAlignment">The horizontal alignment of the content of the new cell.</param>
        public TitleCell(string text, HorizontalAlignment horizontalAlignment)
            : base(text, horizontalAlignment)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TitleCell" /> class with
        /// the text contained by it.
        /// </summary>
        /// <param name="text"></param>
        public TitleCell(MultilineText text)
            : base(text)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TitleCell" /> class with
        /// the text contained by it and its horizontal alignment.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="horizontalAlignment">The horizontal alignment of the content of the new cell.</param>
        public TitleCell(MultilineText text, HorizontalAlignment horizontalAlignment)
            : base(text, horizontalAlignment)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TitleCell" /> class with
        /// an object representing the content.
        /// </summary>
        public TitleCell(object content)
            : base(content)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TitleCell" /> class with
        /// an object representing the content and its horizontal alignment.
        /// </summary>
        public TitleCell(object content, HorizontalAlignment horizontalAlignment)
            : base(content, horizontalAlignment)
        {
        }

        /// <summary>
        /// Calculates and returns the left padding for the content displayed in the cell.
        /// The value is calculated taking into account also the parent row and parent table.
        /// </summary>
        protected override int CalculatePaddingLeft()
        {
            if (PaddingLeft.HasValue)
                return PaddingLeft.Value;

            return ParentRow?.ParentDataGrid?.PaddingLeft ?? 0;
        }

        /// <summary>
        /// Calculates and returns the right padding for the content displayed in the cell.
        /// The value is calculated taking into account also the parent row and parent table.
        /// </summary>
        protected override int CalculatePaddingRight()
        {
            if (PaddingRight.HasValue)
                return PaddingRight.Value;

            return ParentRow?.ParentDataGrid?.PaddingRight ?? 0;
        }

        /// <summary>
        /// Calculates and returns the horizontal alignment of the content displayed in the cell.
        /// The value is calculated taking into account also the parent row and parent table.
        /// </summary>
        protected override HorizontalAlignment CalculateHorizontalAlignment()
        {
            HorizontalAlignment alignment = HorizontalAlignment;

            if (alignment == HorizontalAlignment.Default)
                alignment = DefaultHorizontalAlignment;

            return alignment;
        }
    }
}