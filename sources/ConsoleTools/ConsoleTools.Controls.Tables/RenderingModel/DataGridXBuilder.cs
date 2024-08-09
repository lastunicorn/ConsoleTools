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

internal class DataGridXBuilder
{
    private readonly DataGrid dataGrid;
    private DataGridX dataGridX;

    private RowBase previousRow;

    public DataGridXBuilder(DataGrid dataGrid)
    {
        this.dataGrid = dataGrid;
    }

    public DataGridX Build()
    {
        dataGridX = new DataGridX();

        if (dataGrid != null)
            PopulateDataGridX();

        return dataGridX;
    }

    private void PopulateDataGridX()
    {
        previousRow = null;

        dataGridX.HasBorders = dataGrid.AreBordersAllowed && dataGrid.IsBorderVisible;
        dataGridX.MinWidth = dataGrid.MinWidth ?? 0;
        dataGridX.MaxWidth = dataGrid.MaxWidth ?? int.MaxValue;

        AddColumns();

        ProcessTitleRow();
        ProcessHeaderRow();
        ProcessContentRows();
        ProcessFooterRow();

        if (dataGridX.RowCount > 0)
            AddTopSeparatorForRow(null);

        dataGridX.Finish();
    }

    private void AddColumns()
    {
        IEnumerable<Column> visibleColumns = dataGrid.Columns
            .Where(x => x.IsVisible);

        foreach (Column column in visibleColumns)
        {
            ColumnX columnX = new()
            {
                MinWidth = column.MinWidth
            };

            dataGridX.AddColumn(columnX);
        }
    }

    private void ProcessTitleRow()
    {
        bool isTitleRowVisible = dataGrid.TitleRow is { IsVisible: true, HasContent: true };

        if (!isTitleRowVisible)
            return;

        AddTopSeparatorForRow(dataGrid.TitleRow);
        AddRow(dataGrid.TitleRow);
    }

    private void ProcessHeaderRow()
    {
        bool isHeaderRowVisible = dataGrid.HeaderRow is { IsVisible: true, CellCount: > 0 } &&
                                  dataGrid.Columns.Any(x => x.IsVisible && !x.HeaderCell.IsEmpty);

        if (!isHeaderRowVisible)
            return;

        AddTopSeparatorForRow(dataGrid.HeaderRow);
        AddRow(dataGrid.HeaderRow);
    }

    private void ProcessContentRows()
    {
        bool contentExists = dataGrid.Rows.Count > 0;

        if (contentExists)
        {
            AddContentRows();
        }
        else if (dataGrid.EmptyText != null)
        {
            AddEmptyTextRow();
        }
    }

    private void AddContentRows()
    {
        IEnumerable<ContentRow> visibleRows = dataGrid.Rows
            .Where(x => x.IsVisible);

        foreach (ContentRow currentRow in visibleRows)
        {
            AddTopSeparatorForRow(currentRow);
            AddRow(currentRow);
        }
    }

    private void AddEmptyTextRow()
    {
        ContentCell cellContents = new(dataGrid.EmptyText)
        {
            ColumnSpan = int.MaxValue,
            PaddingLeft = 10,
            PaddingTop = 1,
            PaddingRight = 10,
            PaddingBottom = 1,
            HorizontalAlignment = HorizontalAlignment.Center
        };

        ContentRow emptyContentRow = new(cellContents)
        {
            ParentDataGrid = dataGrid
        };

        AddTopSeparatorForRow(emptyContentRow);
        AddRow(emptyContentRow);
    }

    private void ProcessFooterRow()
    {
        bool isFooterRowVisible = dataGrid.FooterRow is { IsVisible: true, HasContent: true };

        if (!isFooterRowVisible)
            return;

        AddTopSeparatorForRow(dataGrid.FooterRow);
        AddRow(dataGrid.FooterRow);
    }

    private void AddRow(RowBase rowBase)
    {
        RowX rowX = RowXBuilder.CreateFor(rowBase)
            .Build();

        dataGridX.Add(rowX);

        previousRow = rowBase;
    }

    private void AddTopSeparatorForRow(RowBase currentRow)
    {
        bool isTopSeparatorVisible = ComputeTopSeparatorVisibilityForRow(currentRow);

        if (!isTopSeparatorVisible)
            return;

        SeparatorXBuilder separatorXBuilder = new(previousRow, currentRow);
        SeparatorX separatorX = separatorXBuilder.Build();

        dataGridX.Add(separatorX);
    }

    private bool ComputeTopSeparatorVisibilityForRow(RowBase currentRow)
    {
        if (!dataGrid.AreBordersAllowed)
            return false;

        bool? isCurrentRowTopBorderVisible = currentRow?.BorderVisibility?.Top;
        if (isCurrentRowTopBorderVisible != null)
        {
            // Current row has border specified.

            return isCurrentRowTopBorderVisible.Value;
        }

        bool? isPreviousRowBottomBorderVisible = previousRow?.BorderVisibility?.Bottom;
        if (isPreviousRowBottomBorderVisible != null)
        {
            // Previous row has border specified.

            return isPreviousRowBottomBorderVisible.Value;
        }

        if (dataGrid.IsBorderVisible)
        {
            // Grid provides its default border.

            bool shouldDisplayTopBorder = currentRow is not ContentRow ||
                                          previousRow is not ContentRow ||
                                          dataGrid.DisplayBorderBetweenRows;

            if (shouldDisplayTopBorder)
                return true;
        }

        return false;
    }
}