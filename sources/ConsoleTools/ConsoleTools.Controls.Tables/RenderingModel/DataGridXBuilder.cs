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
        dataGridX.IsBorderVisible = dataGrid.AreBordersAllowed && dataGrid.IsBorderVisible;
        dataGridX.MinWidth = dataGrid.MinWidth ?? 0;

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
            AddRowSeparatorIfBorderIsVisible();

        dataGridX.Finish();
    }

    private void AddTitle()
    {
        AddRowSeparatorIfBorderIsVisible();

        RowX rowX = RowX.CreateFrom(dataGrid.TitleRow);
        dataGridX.Add(rowX);
    }

    private void AddHeader()
    {
        AddRowSeparatorIfBorderIsVisible();

        RowX headerRowX = RowX.CreateFrom(dataGrid.HeaderRow);
        dataGridX.Add(headerRowX);
    }

    private void AddContentRows()
    {
        IEnumerable<ContentRow> visibleRows = dataGrid.Rows
            .Where(x => x.IsVisible);

        ContentRow previousRow = null;

        foreach (ContentRow currentRow in visibleRows)
        {
            if (currentRow.ParentDataGrid == null || currentRow.ParentDataGrid.AreBordersAllowed)
            {
                SeparatorX separatorX = CreateSeparatorForContentRow(currentRow, previousRow);

                if (separatorX != null)
                    dataGridX.Add(separatorX);
            }

            AddContentRow(currentRow);

            previousRow = currentRow;
        }
    }

    private SeparatorX CreateSeparatorForContentRow(ContentRow currentRow, ContentRow previousRow)
    {
        if (currentRow.BorderVisibility != null)
        {
            // Current row has border specified.

            if (currentRow.BorderVisibility.Value.Top)
            {
                return new SeparatorX
                {
                    BorderTemplate = currentRow.ComputeBorderTemplate(),
                    ForegroundColor = currentRow.ComputeBorderForegroundColor(),
                    BackgroundColor = currentRow.ComputeBorderBackgroundColor()
                };
            }
        }

        if (previousRow?.BorderVisibility != null)
        {
            // Previous row has border specified.

            if (previousRow.BorderVisibility.Value.Bottom)
            {
                return new SeparatorX
                {
                    BorderTemplate = previousRow.ComputeBorderTemplate(),
                    ForegroundColor = previousRow.ComputeBorderForegroundColor(),
                    BackgroundColor = previousRow.ComputeBorderBackgroundColor()
                };
            }
        }

        if (dataGrid.IsBorderVisible)
        {
            if (previousRow == null || dataGrid.DisplayBorderBetweenRows)
            {
                // This is the first row.

                return new SeparatorX
                {
                    BorderTemplate = dataGrid.ComputeBorderTemplate(),
                    ForegroundColor = dataGrid.ComputeBorderForegroundColor(),
                    BackgroundColor = dataGrid.ComputeBorderBackgroundColor()
                };
            }
        }

        return null;
    }

    private void AddContentRow(ContentRow currentRow)
    {
        RowX rowX = RowX.CreateFrom(currentRow);

        dataGridX.Add(rowX);
    }

    private void AddFooter()
    {
        AddRowSeparatorIfBorderIsVisible();

        RowX rowX = RowX.CreateFrom(dataGrid.FooterRow);
        dataGridX.Add(rowX);
    }

    private void AddRowSeparatorIfBorderIsVisible()
    {
        if (dataGrid.AreBordersAllowed && dataGrid.IsBorderVisible)
        {
            SeparatorX separatorX = new()
            {
                BorderTemplate = dataGrid.ComputeBorderTemplate(),
                ForegroundColor = dataGrid.ComputeBorderForegroundColor(),
                BackgroundColor = dataGrid.ComputeBorderBackgroundColor()
            };

            dataGridX.Add(separatorX);
        }
    }
}