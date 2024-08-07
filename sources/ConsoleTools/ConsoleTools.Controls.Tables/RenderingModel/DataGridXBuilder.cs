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

        AddColumns();

        bool isTitleRowVisible = dataGrid.TitleRow is { IsVisible: true, HasContent: true };
        if (isTitleRowVisible)
            AddTitle();

        bool isHeaderRowVisible = dataGrid.HeaderRow is { IsVisible: true, CellCount: > 0 } &&
                                  dataGrid.Columns.Any(x => x.IsVisible && !x.HeaderCell.IsEmpty);
        if (isHeaderRowVisible)
            AddHeader();

        bool areNormalRowsVisible = dataGrid.Rows.Count > 0;
        if (areNormalRowsVisible)
            AddContentRows();

        bool isFooterRowVisible = dataGrid.FooterRow is { IsVisible: true, HasContent: true };
        if (isFooterRowVisible)
            AddFooter();

        if (dataGridX.ItemCount > 0)
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

    private void AddTitle()
    {
        AddTopSeparatorForRow(dataGrid.TitleRow);

        RowX rowX = RowX.CreateFrom(dataGrid.TitleRow);
        dataGridX.Add(rowX);

        previousRow = dataGrid.TitleRow;
    }

    private void AddHeader()
    {
        AddTopSeparatorForRow(dataGrid.HeaderRow);

        RowX headerRowX = RowX.CreateFrom(dataGrid.HeaderRow);
        dataGridX.Add(headerRowX);

        previousRow = dataGrid.HeaderRow;
    }

    private void AddContentRows()
    {
        IEnumerable<ContentRow> visibleRows = dataGrid.Rows
            .Where(x => x.IsVisible);

        foreach (ContentRow currentRow in visibleRows)
            AddContentRow(currentRow);
    }

    private void AddContentRow(ContentRow contentRow)
    {
        AddTopSeparatorForRow(contentRow);

        RowX rowX = RowX.CreateFrom(contentRow);
        dataGridX.Add(rowX);

        previousRow = contentRow;
    }

    private void AddFooter()
    {
        AddTopSeparatorForRow(dataGrid.FooterRow);

        RowX rowX = RowX.CreateFrom(dataGrid.FooterRow);
        dataGridX.Add(rowX);

        previousRow = dataGrid.FooterRow;
    }

    private void AddTopSeparatorForRow(RowBase currentRow)
    {
        if (!dataGrid.AreBordersAllowed)
            return;

        SeparatorX separatorX = CreateTopSeparatorForRow(currentRow);

        if (separatorX != null)
            dataGridX.Add(separatorX);
    }

    private SeparatorX CreateTopSeparatorForRow(RowBase currentRow)
    {
        bool isTopSeparatorVisible = ComputeTopBorderVisibilityForRow(currentRow);

        if (!isTopSeparatorVisible)
            return null;

        return new SeparatorX
        {
            BorderTemplate = ComputeBorderTemplate(currentRow),
            ForegroundColor = ComputeBorderForegroundColor(currentRow),
            BackgroundColor = ComputeBorderBackgroundColor(currentRow)
        };
    }

    private bool ComputeTopBorderVisibilityForRow(RowBase currentRow)
    {
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

    private BorderTemplate ComputeBorderTemplate(RowBase currentRow)
    {
        BorderTemplate template = currentRow?.BorderTemplate;

        if (template != null)
            return template;

        template = previousRow?.BorderTemplate;

        if (template != null)
            return template;

        template = dataGrid?.BorderTemplate;

        return template;
    }

    private ConsoleColor? ComputeBorderForegroundColor(RowBase currentRow)
    {
        ConsoleColor? color = currentRow?.BorderForegroundColor;

        if (color != null)
            return color;

        color = previousRow?.BorderForegroundColor;

        if (color != null)
            return color;

        color = dataGrid?.BorderForegroundColor;

        return color;
    }

    private ConsoleColor? ComputeBorderBackgroundColor(RowBase currentRow)
    {
        ConsoleColor? color = currentRow?.BorderBackgroundColor;

        if (color != null)
            return color;

        color = previousRow?.BorderBackgroundColor;

        if (color != null)
            return color;

        color = dataGrid?.BorderBackgroundColor;

        return color;
    }
}