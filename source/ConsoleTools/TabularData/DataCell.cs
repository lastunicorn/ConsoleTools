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
    /// Represents a cell that contains data.
    /// </summary>
    public class DataCell : CellBase
    {
        /// <summary>
        /// Gets the default horizontal alignment for a data cell.
        /// </summary>
        public static HorizontalAlignment DefaultHorizontalAlignment { get; } = HorizontalAlignment.Left;

        /// <summary>
        /// Gets or sets the row that contains the current cell.
        /// </summary>
        public DataRow ParentRow { get; set; }

        /// <summary>
        /// Gets or sets the padding applyed to the left side of the cell.
        /// </summary>
        public int? PaddingLeft { get; set; }

        /// <summary>
        /// Gets or sets the padding applyed to the right side of the cell.
        /// </summary>
        public int? PaddingRight { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataCell" /> class with
        /// empty content.
        /// </summary>
        public DataCell()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataCell" /> class with
        /// the text contained by it.
        /// </summary>
        /// <param name="text">The text displayed in the cell.</param>
        public DataCell(string text)
            : base(text)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataCell" /> class with
        /// the text contained by it and its horizontal alignment.
        /// </summary>
        /// <param name="text">The text displayed in the cell.</param>
        /// <param name="horizontalAlignment">The horizontal alignment of the content of the new cell.</param>
        public DataCell(string text, HorizontalAlignment horizontalAlignment)
            : base(text, horizontalAlignment)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataCell" /> class with
        /// the text contained by it.
        /// </summary>
        /// <param name="text"></param>
        public DataCell(MultilineText text)
            : base(text)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataCell" /> class with
        /// the text contained by it and its horizontal alignment.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="horizontalAlignment">The horizontal alignment of the content of the new cell.</param>
        public DataCell(MultilineText text, HorizontalAlignment horizontalAlignment)
            : base(text, horizontalAlignment)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataCell" /> class with
        /// an object representing the content.
        /// </summary>
        public DataCell(object content)
            : base(content)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataCell" /> class with
        /// an object representing the content and its horizontal alignment.
        /// </summary>
        public DataCell(object content, HorizontalAlignment horizontalAlignment)
            : base(content, horizontalAlignment)
        {
        }

        /// <summary>
        /// Calculates and returns the left padding for the content displayed in the cell.
        /// The value is calculated taking into account also the parent row, parent column and parent table.
        /// </summary>
        protected override int CalculatePaddingLeft()
        {
            if (PaddingLeft.HasValue)
                return PaddingLeft.Value;

            if (ParentRow != null)
            {
                if (ParentRow.PaddingLeft.HasValue)
                    return ParentRow.PaddingLeft.Value;

                if (ParentRow.ParentTable != null)
                {
                    Column column = GetColumn();

                    if (column?.PaddingLeft != null)
                        return column.PaddingLeft.Value;

                    if (ParentRow.ParentTable.PaddingLeft.HasValue)
                        return ParentRow.ParentTable.PaddingLeft.Value;
                }
            }

            return 0;
        }

        /// <summary>
        /// Calculates and returns the right padding for the content displayed in the cell.
        /// The value is calculated taking into account also the parent row, parent column and parent table.
        /// </summary>
        protected override int CalculatePaddingRight()
        {
            if (PaddingRight.HasValue)
                return PaddingRight.Value;

            if (ParentRow != null)
            {
                if (ParentRow.PaddingRight.HasValue)
                    return ParentRow.PaddingRight.Value;


                if (ParentRow.ParentTable != null)
                {
                    Column column = GetColumn();

                    if (column?.PaddingRight != null)
                        return column.PaddingRight.Value;

                    if (ParentRow.ParentTable.PaddingRight.HasValue)
                        return ParentRow.ParentTable.PaddingRight.Value;
                }
            }

            return 0;
        }

        /// <summary>
        /// Returns the calculated horizontal alignment for the content of the current data cell.
        /// The value is calculated based on the <see cref="HorizontalAlignment"/> property of the current data cell,
        /// and the values specified by the parent row, parent column and parent table.
        /// It never returns <see cref="HorizontalAlignment.Default"/>.
        /// </summary>
        protected override HorizontalAlignment CalculateHorizontalAlignment()
        {
            HorizontalAlignment alignment = HorizontalAlignment;

            if (alignment == HorizontalAlignment.Default)
                alignment = CalculateHorizontalAlignmentAtRowLevel();

            if (alignment == HorizontalAlignment.Default)
                alignment = CalculateHorizontalAlignmentAtColumnLevel();

            if (alignment == HorizontalAlignment.Default)
                alignment = CalculateHorizontalAlignmentAtTableLevel();

            if (alignment == HorizontalAlignment.Default)
                alignment = DefaultHorizontalAlignment;

            return alignment;
        }

        private HorizontalAlignment CalculateHorizontalAlignmentAtRowLevel()
        {
            DataRow row = ParentRow;
            return row?.CellHorizontalAlignment ?? HorizontalAlignment.Default;
        }

        private HorizontalAlignment CalculateHorizontalAlignmentAtColumnLevel()
        {
            Column column = GetColumn();
            return column?.HorizontalAlignment ?? HorizontalAlignment.Default;
        }

        private Column GetColumn()
        {
            ColumnList columns = ParentRow?.ParentTable?.Columns;
            int? columnIndex = ParentRow?.IndexOfCell(this);

            return columns != null && columnIndex.HasValue
                ? ParentRow?.ParentTable?.Columns[columnIndex.Value]
                : null;
        }

        private HorizontalAlignment CalculateHorizontalAlignmentAtTableLevel()
        {
            Table table = ParentRow?.ParentTable;
            return table?.CellHorizontalAlignment ?? HorizontalAlignment.Default;
        }

        /// <summary>
        /// Converts a <see cref="string"/> into a <see cref="DataCell"/> instance.
        /// </summary>
        /// <param name="text">The text to be converted.</param>
        public static implicit operator DataCell(string text)
        {
            MultilineText multilineText = new MultilineText(text);
            return new DataCell(multilineText);
        }

        /// <summary>
        /// Converts a <see cref="DataCell"/> into its <see cref="string"/> representation.
        /// </summary>
        /// <param name="cell">The <see cref="DataCell"/> to be converted.</param>
        public static implicit operator string(DataCell cell)
        {
            return cell.Content?.ToString() ?? string.Empty;
        }
    }
}