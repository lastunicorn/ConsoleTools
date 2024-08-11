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

using System.Collections.Generic;

namespace DustInTheWind.ConsoleTools.Controls.Tables;

/// <summary>
/// Represents the footer row of a table.
/// </summary>
public class FooterRow : RowBase
{
    /// <summary>
    /// Gets or sets the cell displayed in the footer row.
    /// This is the unique cell of the row.
    /// </summary>
    public FooterCell FooterCell { get; }

    /// <summary>
    /// Gets the number of cells contained by the footer row.
    /// It is always 1.
    /// </summary>
    public override int CellCount => 1;

    /// <summary>
    /// Gets a value that specifies if the current instance of the <see cref="FooterRow"/> has a content to be displayed.
    /// </summary>
    public bool HasContent => FooterCell?.Content?.IsEmpty == false;

    /// <summary>
    /// Initializes a new instance of the <see cref="FooterRow"/> class with
    /// empty content.
    /// </summary>
    public FooterRow()
    {
        FooterCell = new FooterCell
        {
            ParentRow = this,
            Content = MultilineText.Empty
        };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FooterRow"/> class with
    /// the text content.
    /// </summary>
    public FooterRow(string content)
    {
        FooterCell = new FooterCell
        {
            ParentRow = this,
            Content = content == null
                ? MultilineText.Empty
                : new MultilineText(content)
        };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FooterRow"/> class with
    /// a <see cref="MultilineText"/> content.
    /// </summary>
    public FooterRow(MultilineText content)
    {
        FooterCell = new FooterCell
        {
            ParentRow = this,
            Content = content ?? MultilineText.Empty
        };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FooterRow"/> class with
    /// an <see cref="object"/> representing the content.
    /// </summary>
    public FooterRow(object content)
    {
        FooterCell = new FooterCell
        {
            ParentRow = this,
            Content = content?.ToString() ?? MultilineText.Empty
        };
    }

    /// <summary>
    /// Returns 1 if the cell is the single cell of the current instance; <c>null</c> otherwise.
    /// </summary>
    public override int? IndexOfCell(CellBase cell)
    {
        if (cell is not FooterCell footerCell)
            return null;

        return footerCell == FooterCell ? 1 : null;
    }

    /// <summary>
    /// Returns an enumeration containing the single <see cref="FooterCell"/> contained by the current instance.
    /// </summary>
    /// <returns>An enumeration containing the single <see cref="FooterCell"/> contained by the current instance.</returns>
    public override IEnumerable<CellBase> EnumerateVisibleCells()
    {
        yield return FooterCell;
    }

    /// <summary>
    /// Enumerates all the cells contained by the current instance.
    /// </summary>
    /// <returns>An enumeration of all the cell contained by the current instance.</returns>
    public override IEnumerator<CellBase> GetEnumerator()
    {
        IEnumerable<CellBase> cellBases = new[] { FooterCell };
        return cellBases.GetEnumerator();
    }
}