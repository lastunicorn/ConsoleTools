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
    /// Represents the single cell displayed in the title row.
    /// </summary>
    public class TitleCell : CellBase
    {
        private static readonly HorizontalAlignment DefaultHorizontalAlignment = HorizontalAlignment.Left;

        /// <summary>
        /// Gets or sets the <see cref="TitleRow"/> instance that owns the current <see cref="TitleCell"/> instance.
        /// </summary>
        public TitleRow ParentRow { get; set; }

        /// <summary>
        /// Calculates and returns the left padding for the content displayed in the cell.
        /// The value is calculated taking into account also the parent row and parent table.
        /// </summary>
        protected override int CalculatePaddingLeft()
        {
            return ParentRow?.ParentTable?.PaddingLeft ?? 0;
        }

        /// <summary>
        /// Calculates and returns the right padding for the content displayed in the cell.
        /// The value is calculated taking into account also the parent row and parent table.
        /// </summary>
        protected override int CalculatePaddingRight()
        {
            return ParentRow?.ParentTable?.PaddingRight ?? 0;
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