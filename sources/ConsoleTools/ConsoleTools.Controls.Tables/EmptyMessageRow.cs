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

using System.Collections.Generic;

namespace DustInTheWind.ConsoleTools.Controls.Tables;

/// <summary>
/// This is the row that is displayed in the content section of the <see cref="DataGrid"/>
/// when there are no content rows.
/// </summary>
public class EmptyMessageRow : RowBase
{
    /// <summary>
    /// Gets the only cell contained be this row.
    /// This cell is automatically created when the row is created.
    /// </summary>
    public EmptyMessageCell EmptyMessageCell { get; }

    /// <summary>
    /// Gets the number of cells contained in this instance.
    /// It is always 1.
    /// </summary>
    public override int CellCount => 1;

    /// <summary>
    /// Gets a value that specifies if the current instance of the <see cref="EmptyMessageRow"/>
    /// has a content to be displayed.
    /// </summary>
    public bool HasContent => EmptyMessageCell?.Content?.IsEmpty == false;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmptyMessageRow"/> class with
    /// empty content.
    /// </summary>
    public EmptyMessageRow()
    {
        EmptyMessageCell = new EmptyMessageCell
        {
            ParentRow = this,
            Content = MultilineText.Empty
        };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmptyMessageRow"/> class with
    /// the text content.
    /// </summary>
    public EmptyMessageRow(string content)
    {
        EmptyMessageCell = new EmptyMessageCell
        {
            ParentRow = this,
            Content = content == null
                ? MultilineText.Empty
                : new MultilineText(content)
        };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmptyMessageRow"/> class with
    /// a <see cref="MultilineText"/> content.
    /// </summary>
    public EmptyMessageRow(MultilineText content)
    {
        EmptyMessageCell = new EmptyMessageCell
        {
            ParentRow = this,
            Content = content ?? MultilineText.Empty
        };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmptyMessageRow"/> class with
    /// an <see cref="object"/> representing the content.
    /// </summary>
    public EmptyMessageRow(object content)
    {
        EmptyMessageCell = new EmptyMessageCell
        {
            ParentRow = this,
            Content = content?.ToString() ?? MultilineText.Empty
        };
    }

    /// <summary>
    /// Returns 1 if the cell is the single cell contained by the current instance; <c>null</c> otherwise.
    /// </summary>
    public override int? IndexOfCell(CellBase cell)
    {
        if (cell is not EmptyMessageCell emptyInfoCell)
            return null;

        return emptyInfoCell == EmptyMessageCell ? 1 : null;
    }

    /// <summary>
    /// Enumerates the one cell contained by the current instance.
    /// </summary>
    /// <returns>An enumeration containing the single cell contained by the current instance.</returns>
    public override IEnumerator<CellBase> GetEnumerator()
    {
        IEnumerable<CellBase> cellBases = new[] { EmptyMessageCell };
        return cellBases.GetEnumerator();
    }
}