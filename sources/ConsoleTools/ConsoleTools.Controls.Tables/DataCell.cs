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
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new/choose

using System;

namespace DustInTheWind.ConsoleTools.Controls.Tables
{
    /// <summary>
    /// Represents a cell that contains data.
    /// </summary>
    public class DataCell : CellBase
    {
        /// <summary>
        /// Gets or sets the row that contains the current cell.
        /// </summary>
        public DataRow ParentRow { get; internal set; }

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
        public override int CalculatePaddingLeft()
        {
            int? paddingLeft = PaddingLeft;

            if (paddingLeft != null)
                return paddingLeft.Value;

            paddingLeft = ParentRow?.CellPaddingLeft;

            if (paddingLeft != null)
                return paddingLeft.Value;

            Column column = GetColumn();
            paddingLeft = column?.CellPaddingLeft;

            if (paddingLeft != null)
                return paddingLeft.Value;

            paddingLeft = ParentRow?.ParentDataGrid?.CellPaddingLeft;

            if (paddingLeft != null)
                return paddingLeft.Value;

            paddingLeft = DefaultPaddingLeft;

            return paddingLeft.Value;
        }

        /// <summary>
        /// Calculates and returns the right padding for the content displayed in the cell.
        /// The value is calculated taking into account also the parent row, parent column and parent table.
        /// </summary>
        public override int CalculatePaddingRight()
        {
            int? paddingRight = PaddingRight;
            if (paddingRight != null)
                return paddingRight.Value;

            paddingRight = ParentRow?.CellPaddingRight;
            if (paddingRight != null)
                return paddingRight.Value;

            Column column = GetColumn();
            paddingRight = column?.CellPaddingRight;
            if (paddingRight != null)
                return paddingRight.Value;

            paddingRight = ParentRow?.ParentDataGrid?.CellPaddingRight;
            if (paddingRight != null)
                return paddingRight.Value;

            paddingRight = DefaultPaddingRight;

            return paddingRight.Value;
        }

        /// <summary>
        /// Calculates and returns the foreground color for the content displayed in the cell.
        /// The value is calculated taking into account also the parent row, parent column and parent table.
        /// </summary>
        public override ConsoleColor? CalculateForegroundColor()
        {
            ConsoleColor? color = ForegroundColor;
            if (color != null)
                return color;

            color = ParentRow?.ForegroundColor;
            if (color != null)
                return color;

            Column column = GetColumn();
            color = column?.ForegroundColor;
            if (color != null)
                return color;

            color = ParentRow?.ParentDataGrid?.ForegroundColor;

            return color;
        }

        /// <summary>
        /// Calculates and returns the background color for the content displayed in the cell.
        /// The value is calculated taking into account also the parent row, parent column and parent table.
        /// </summary>
        public override ConsoleColor? CalculateBackgroundColor()
        {
            ConsoleColor? color = BackgroundColor;
            if (color != null)
                return color;

            color = ParentRow?.BackgroundColor;
            if (color != null)
                return color;

            Column column = GetColumn();
            color = column?.BackgroundColor;
            if (color != null)
                return color;

            color = ParentRow?.ParentDataGrid?.BackgroundColor;

            return color;
        }

        /// <summary>
        /// Returns the calculated horizontal alignment for the content of the current data cell.
        /// The value is calculated based on the <see cref="HorizontalAlignment"/> property of the current data cell,
        /// and the values specified by the parent row, parent column and parent table.
        /// It never returns <see cref="HorizontalAlignment.Default"/>.
        /// </summary>
        public override HorizontalAlignment CalculateHorizontalAlignment()
        {
            HorizontalAlignment alignment = HorizontalAlignment;
            if (alignment != HorizontalAlignment.Default)
                return alignment;

            alignment = CalculateHorizontalAlignmentAtRowLevel();
            if (alignment != HorizontalAlignment.Default)
                return alignment;

            alignment = CalculateHorizontalAlignmentAtColumnLevel();
            if (alignment != HorizontalAlignment.Default)
                return alignment;

            alignment = CalculateHorizontalAlignmentAtTableLevel();
            if (alignment != HorizontalAlignment.Default)
                return alignment;

            alignment = DefaultHorizontalAlignment;

            return alignment;
        }

        private HorizontalAlignment CalculateHorizontalAlignmentAtRowLevel()
        {
            return ParentRow?.CellHorizontalAlignment ?? HorizontalAlignment.Default;
        }

        private HorizontalAlignment CalculateHorizontalAlignmentAtColumnLevel()
        {
            Column column = GetColumn();
            return column?.CellHorizontalAlignment ?? HorizontalAlignment.Default;
        }

        private Column GetColumn()
        {
            ColumnList columns = ParentRow?.ParentDataGrid?.Columns;
            int? columnIndex = ParentRow?.IndexOfCell(this);

            return columns != null && columnIndex.HasValue
                ? ParentRow?.ParentDataGrid?.Columns[columnIndex.Value]
                : null;
        }

        private HorizontalAlignment CalculateHorizontalAlignmentAtTableLevel()
        {
            DataGrid dataGrid = ParentRow?.ParentDataGrid;
            return dataGrid?.CellHorizontalAlignment ?? HorizontalAlignment.Default;
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