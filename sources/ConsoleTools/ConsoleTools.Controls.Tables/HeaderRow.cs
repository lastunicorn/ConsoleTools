// ConsoleTools
// Copyright (C) 2017-2024 Dust in the Wind
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DustInTheWind.ConsoleTools.Controls.Tables;

/// <summary>
/// Represents the column header row of a table.
/// </summary>
public class HeaderRow : RowBase
{
    private readonly ColumnList columns;

    /// <summary>
    /// Gets the number of cells contained by the header row.
    /// </summary>
    public override int CellCount => columns.Count();

    /// <summary>
    /// Gets the cell at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the cell to get.</param>
    /// <returns>The cell at the specified index.</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public HeaderCell this[int index] => columns[index]?.HeaderCell;

    /// <summary>
    /// Initializes a new instance of the <see cref="HeaderRow"/> class with
    /// the list of columns
    /// and the <see cref="DataGrid"/> that owns it.
    /// </summary>
    public HeaderRow(ColumnList columns)
    {
        this.columns = columns ?? throw new ArgumentNullException(nameof(columns));
    }

    /// <summary>
    /// Enumerates the visible cells contained by the current instance.
    /// The cells from the hidden columns are excluded.
    /// </summary>
    /// <returns>An enumeration of the visible cells contained by the current instance.</returns>
    public IEnumerable<HeaderCell> EnumerateVisibleCells()
    {
        return columns
            .Where(x => x.IsVisible)
            .Select(x => x.HeaderCell);
    }

    /// <summary>
    /// Enumerates all the cells contained by the current instance.
    /// Includes also the cells from the hidden columns.
    /// </summary>
    /// <returns>An enumeration of all the cell contained by the current instance.</returns>
    public override IEnumerator<CellBase> GetEnumerator()
    {
        return new HeaderCellEnumerator(this);
    }

    #region Enumerator Class

    private class HeaderCellEnumerator : IEnumerator<HeaderCell>
    {
        private readonly HeaderRow headerRow;
        private int index = -1;

        public HeaderCell Current { get; private set; }

        object IEnumerator.Current => Current;

        public HeaderCellEnumerator(HeaderRow headerRow)
        {
            this.headerRow = headerRow ?? throw new ArgumentNullException(nameof(headerRow));
        }

        public bool MoveNext()
        {
            index++;

            if (index >= headerRow.columns.Count)
                return false;

            Current = headerRow.columns[index].HeaderCell;

            return true;
        }

        public void Reset()
        {
            index = -1;
            Current = null;
        }

        public void Dispose()
        {
        }
    }

    #endregion
}