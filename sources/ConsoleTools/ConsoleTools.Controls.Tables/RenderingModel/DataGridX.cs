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

namespace DustInTheWind.ConsoleTools.Controls.Tables.RenderingModel;

/// <summary>
/// This class contains the analysis of the <see cref="DataGrid"/> performed at the beginning of
/// the rendering process.
/// </summary>
internal class DataGridX
{
    private readonly ItemXCollection rows = new();
    private readonly ColumnXCollection columns = new();

    private int renderItemIndex;

    public bool HasBorders
    {
        get => columns.HasBorders;
        set => columns.HasBorders = value;
    }

    public int MinWidth
    {
        get => columns.MinWidth;
        set => columns.MinWidth = value;
    }

    public int MaxWidth
    {
        get => columns.MaxWidth;
        set => columns.MaxWidth = value;
    }

    public int RowCount => rows.Count;

    public bool HasMoreLines => renderItemIndex < rows.Count;

    public void Add(ColumnX column)
    {
        columns.AddColumn(column);
    }

    public void Add(SeparatorX separator)
    {
        rows.Add(separator);
    }

    public void Add(RowX rowX)
    {
        rows.Add(rowX);
        UpdateColumnsWidths(rowX);
    }

    private void UpdateColumnsWidths(RowX rowX)
    {
        for (int i = 0; i < rowX.Cells.Count; i++)
        {
            CellX cellX = rowX.Cells[i];
            columns.GetOrCreate(i).AccomodateCell(cellX);
        }
    }

    public void Finish()
    {
        columns.PerformLayout();
    }

    public void InitializeRendering()
    {
        renderItemIndex = 0;

        if (renderItemIndex < rows.Count)
        {
            IItemX itemX = rows[renderItemIndex];
            itemX.InitializeRendering(columns);
        }
    }

    public void RenderNextLine(RenderingContext display)
    {
        IItemX itemX = GetNextItemToRender();
        itemX?.RenderNextLine(display);
    }

    private IItemX GetNextItemToRender()
    {
        bool isNewLine = false;

        while (renderItemIndex < rows.Count)
        {
            IItemX itemX = rows[renderItemIndex];

            if (isNewLine)
                itemX.InitializeRendering(columns);

            if (!itemX.HasMoreLines)
            {
                renderItemIndex++;
                isNewLine = true;
            }
            else
            {
                return itemX;
            }
        }

        return null;
    }
}