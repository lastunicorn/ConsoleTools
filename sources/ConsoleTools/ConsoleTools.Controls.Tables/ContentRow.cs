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

namespace DustInTheWind.ConsoleTools.Controls.Tables;

/// <summary>
/// Represents a row in the <see cref="DataGrid"/> class.
/// </summary>
public class ContentRow : RowBase
{
    /// <summary>
    /// Gets the list of cells contained by the row.
    /// </summary>
    private readonly List<ContentCell> cells = new();

    /// <summary>
    /// Gets the number of cells contained by the current instance.
    /// </summary>
    public override int CellCount => cells.Count;

    /// <summary>
    /// Gets or sets the cell at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the cell to get or set.</param>
    /// <returns>The cell at the specified index.</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public ContentCell this[int index]
    {
        get => cells[index];
        set => cells[index] = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ContentRow"/> class with default values.
    /// </summary>
    public ContentRow()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ContentRow"/> class with
    /// the list of cells.
    /// </summary>
    /// <param name="cells">The list of cells that will be contained by the new row.</param>
    public ContentRow(IEnumerable<ContentCell> cells)
    {
        if (cells == null)
            return;

        foreach (ContentCell cell in cells)
            AddCell(cell);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ContentRow"/> class with
    /// the list of cells.
    /// </summary>
    /// <param name="cells">The list of cells that will be contained by the new row.</param>
    public ContentRow(params ContentCell[] cells)
    {
        if (cells == null)
            return;

        foreach (ContentCell cell in cells)
            AddCell(cell);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ContentRow"/> class with
    /// the list of texts representing the cells content.
    /// </summary>
    /// <param name="cellContents">The list of texts that will be placed in cells.</param>
    public ContentRow(IEnumerable<string> cellContents)
    {
        if (cellContents == null)
            return;

        foreach (string cell in cellContents)
            AddCell(cell);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ContentRow"/> class with
    /// the list of texts representing the cells content.
    /// </summary>
    /// <param name="cellContents">The list of texts that will be placed in cells.</param>
    public ContentRow(params string[] cellContents)
    {
        if (cellContents == null)
            return;

        foreach (string cell in cellContents)
            AddCell(cell);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ContentRow"/> class with
    /// the list of <see cref="MultilineText"/> objects representing the cells content.
    /// </summary>
    /// <param name="cellContents">The list of <see cref="MultilineText"/> objects that will be placed in cells.</param>
    public ContentRow(IEnumerable<MultilineText> cellContents)
    {
        if (cellContents == null)
            return;

        foreach (MultilineText cell in cellContents)
            AddCell(cell);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ContentRow"/> class with
    /// the list of <see cref="MultilineText"/> objects representing the cells content.
    /// </summary>
    /// <param name="cellContents">The list of <see cref="MultilineText"/> objects that will be placed in cells.</param>
    public ContentRow(params MultilineText[] cellContents)
    {
        if (cellContents == null)
            return;

        foreach (MultilineText cell in cellContents)
            AddCell(cell);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ContentRow"/> class with
    /// the list of objects representing the cells content.
    /// </summary>
    /// <param name="cellContents">The list of objects that will be placed in cells.</param>
    public ContentRow(IEnumerable cellContents)
    {
        if (cellContents == null)
            return;

        foreach (object cell in cellContents)
            AddCell(cell);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ContentRow"/> class with
    /// the list of objects representing the cells content.
    /// </summary>
    /// <param name="cellContents">The list of objects that will be placed in cells.</param>
    public ContentRow(params object[] cellContents)
    {
        if (cellContents == null)
            return;

        foreach (object cell in cellContents)
            AddCell(cell);
    }

    /// <summary>
    /// Adds a new cell to the current instance of <see cref="ContentRow"/>.
    /// If no cell is provided, a new one is created.
    /// </summary>
    /// <returns>The added cell.</returns>
    public ContentCell AddCell(ContentCell cell = null)
    {
        if (cell == null)
            cell = new ContentCell();

        cell.ParentRow = this;
        cells.Add(cell);

        return cell;
    }

    /// <summary>
    /// Adds a new cell to the current instance of <see cref="ContentRow"/>.
    /// </summary>
    /// <returns>The newly created cell.</returns>
    public ContentCell AddCell(string cellContent)
    {
        ContentCell cell = new()
        {
            ParentRow = this
        };

        if (cellContent != null)
            cell.Content = new MultilineText(cellContent);

        cells.Add(cell);

        return cell;
    }

    /// <summary>
    /// Adds a new cell to the current instance of <see cref="ContentRow"/>.
    /// </summary>
    /// <returns>The newly created cell.</returns>
    public ContentCell AddCell(MultilineText cellContent)
    {
        ContentCell cell = new()
        {
            ParentRow = this
        };

        if (cellContent != null)
            cell.Content = cellContent;

        cells.Add(cell);

        return cell;
    }

    /// <summary>
    /// Adds a new cell to the current instance of <see cref="ContentRow"/>.
    /// </summary>
    /// <returns>The newly created cell.</returns>
    public ContentCell AddCell(object cellContent)
    {
        ContentCell cell = new()
        {
            ParentRow = this
        };

        if (cellContent != null)
            cell.Content = new MultilineText(cellContent.ToString());

        cells.Add(cell);

        return cell;
    }

    /// <summary>
    /// Returns the index of the specified cell.
    /// If the cell is not part of the current <see cref="ContentRow"/> instance, returns <c>null</c>.
    /// </summary>
    public int? IndexOfCell(ContentCell cell)
    {
        int indexOfCell = cells.IndexOf(cell);

        return indexOfCell == -1
            ? null
            : indexOfCell;
    }

    /// <summary>
    /// Enumerates the visible cells contained by the current instance.
    /// The cells from the hidden columns are excluded.
    /// </summary>
    /// <returns>An enumeration of the visible cells contained by the current instance.</returns>
    public IEnumerable<CellBase> EnumerateVisibleCells()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            bool isColumnVisible = ParentDataGrid?.Columns[i]?.IsVisible ?? true;
            if (!isColumnVisible)
                continue;

            yield return cells[i];
        }
    }

    /// <summary>
    /// Enumerates all the cells contained by the current instance.
    /// </summary>
    /// <returns>An enumeration of all the cell contained by the current instance.</returns>
    public override IEnumerator<CellBase> GetEnumerator()
    {
        return cells.GetEnumerator();
    }
}