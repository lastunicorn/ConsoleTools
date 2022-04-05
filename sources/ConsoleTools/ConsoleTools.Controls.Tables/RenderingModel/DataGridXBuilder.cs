// ConsoleTools
// Copyright (C) 2017-2022 Dust in the Wind
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

namespace DustInTheWind.ConsoleTools.Controls.Tables.RenderingModel
{
    internal class DataGridXBuilder
    {
        private readonly DataGrid dataGrid;
        private DataGridX dataGridX;

        public DataGridXBuilder(DataGrid dataGrid)
        {
            this.dataGrid = dataGrid ?? throw new ArgumentNullException(nameof(dataGrid));
        }

        public DataGridX Build()
        {
            dataGridX = new DataGridX
            {
                IsBorderVisible = dataGrid.Border?.IsVisible == true,
                MinWidth = dataGrid.MinWidth ?? 0
            };

            bool isTitleVisible = dataGrid.TitleRow is { IsVisible: true, HasContent: true };
            if (isTitleVisible)
                AddTitle();

            bool isColumnHeaderRowVisible = dataGrid.HeaderRow is { IsVisible: true, CellCount: > 0 };
            if (isColumnHeaderRowVisible)
                AddHeader();

            bool areNormalRowsVisible = dataGrid.Rows.Count > 0;
            if (areNormalRowsVisible)
                AddRows();

            bool isFooterVisible = dataGrid.FooterRow is { IsVisible: true, HasContent: true };
            if (isFooterVisible)
                AddFooter();

            if (dataGridX.ItemCount > 0)
                AddRowSeparatorIfBorderIsVisible();

            dataGridX.Finish();

            return dataGridX;
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

        private void AddRows()
        {
            IEnumerable<RowX> rows = dataGrid.Rows
                .Where(x => x.IsVisible)
                .Select(RowX.CreateFrom);

            bool isFirstRow = true;

            foreach (RowX row in rows)
            {
                if (isFirstRow)
                {
                    AddRowSeparatorIfBorderIsVisible();
                    isFirstRow = false;
                }
                else if (dataGrid.DisplayBorderBetweenRows)
                {
                    AddRowSeparatorIfBorderIsVisible();
                }

                dataGridX.Add(row);
            }
        }

        private void AddFooter()
        {
            AddRowSeparatorIfBorderIsVisible();

            RowX rowX = RowX.CreateFrom(dataGrid.FooterRow);
            dataGridX.Add(rowX);
        }

        private void AddRowSeparatorIfBorderIsVisible()
        {
            if (dataGrid.Border?.IsVisible == true)
            {
                SeparatorX separatorX = SeparatorX.CreateFor(dataGrid);
                dataGridX.Add(separatorX);
            }
        }
    }
}