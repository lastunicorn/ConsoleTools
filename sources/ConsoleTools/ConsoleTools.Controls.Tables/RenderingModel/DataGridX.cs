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
using System.Linq;

namespace DustInTheWind.ConsoleTools.Controls.Tables.RenderingModel;

/// <summary>
/// This class contains the analysis of the <see cref="DataGrid"/> performed at the beginning of
/// the rendering process.
/// </summary>
internal class DataGridX
{
    private readonly List<IItemX> items = new();
    private readonly ColumnsLayout columnsLayout = new();

    public bool HasBorders
    {
        get => columnsLayout.HasBorders;
        set => columnsLayout.HasBorders = value;
    }

    public int MinWidth
    {
        get => columnsLayout.MinWidth;
        set => columnsLayout.MinWidth = value;
    }

    public int MaxWidth
    {
        get => columnsLayout.MaxWidth;
        set => columnsLayout.MaxWidth = value;
    }

    public int ItemCount => items.Count;

    public void AddColumn(ColumnX column)
    {
        columnsLayout.AddColumn(column.MinWidth);
    }

    public void Add(SeparatorX separator)
    {
        IItemX lastItem = items.LastOrDefault();
        separator.Row1 = lastItem as RowX;
        items.Add(separator);
    }

    public void Add(RowX rowX)
    {
        IItemX lastItem = items.LastOrDefault();

        if (lastItem is SeparatorX lastSeparator)
            lastSeparator.Row2 = rowX;

        items.Add(rowX);
        UpdateColumnsWidths(rowX);
    }

    private void UpdateColumnsWidths(RowX rowX)
    {
        for (int i = 0; i < rowX.Cells.Count; i++)
        {
            CellX cellX = rowX.Cells[i];

            int width = cellX.PreferredSize.Width;
            int span = cellX.ColumnSpan;

            columnsLayout.UpdateColumnWidth(i, width, span);
        }
    }

    public void Finish()
    {
        columnsLayout.FinalizeLayout();
    }

    public void Render(ITablePrinter tablePrinter)
    {
        foreach (IItemX item in items)
            item.Render(tablePrinter, columnsLayout);
    }
}