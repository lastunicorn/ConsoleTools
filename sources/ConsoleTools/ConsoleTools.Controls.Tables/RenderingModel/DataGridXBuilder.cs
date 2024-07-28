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

using System.Collections.Generic;
using System.Linq;

namespace DustInTheWind.ConsoleTools.Controls.Tables.RenderingModel
{
    internal class DataGridXBuilder
    {
        private DataGridX dataGridX;

        public DataGrid DataGrid { get; set; }

        public DataGridX Build()
        {
            dataGridX = new DataGridX(DataGrid.Border?.IsVisible == true)
            {
                MinWidth = DataGrid.MinWidth ?? 0
            };

            bool isTitleRowVisible = DataGrid.TitleRow is { IsVisible: true, HasContent: true };
            if (isTitleRowVisible)
                AddTitle();

            bool isHeaderRowVisible = DataGrid.HeaderRow is { IsVisible: true, CellCount: > 0 } &&
                                      DataGrid.Columns.Any(x => x.IsVisible && !x.HeaderCell.IsEmpty);
            if (isHeaderRowVisible)
                AddHeader();

            bool areNormalRowsVisible = DataGrid.Rows.Count > 0;
            if (areNormalRowsVisible)
                AddRows();

            bool isFooterRowVisible = DataGrid.FooterRow is { IsVisible: true, HasContent: true };
            if (isFooterRowVisible)
                AddFooter();

            if (dataGridX.ItemCount > 0)
                AddRowSeparatorIfBorderIsVisible();

            dataGridX.Finish();

            return dataGridX;
        }

        private void AddTitle()
        {
            AddRowSeparatorIfBorderIsVisible();

            RowX rowX = RowX.CreateFrom(DataGrid.TitleRow);
            dataGridX.Add(rowX);
        }

        private void AddHeader()
        {
            AddRowSeparatorIfBorderIsVisible();

            RowX headerRowX = RowX.CreateFrom(DataGrid.HeaderRow);
            dataGridX.Add(headerRowX);
        }

        private void AddRows()
        {
            IEnumerable<RowX> rows = DataGrid.Rows
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
                else if (DataGrid.Border?.DisplayBorderBetweenRows == true)
                {
                    AddRowSeparatorIfBorderIsVisible();
                }

                dataGridX.Add(row);
            }
        }

        private void AddFooter()
        {
            AddRowSeparatorIfBorderIsVisible();

            RowX rowX = RowX.CreateFrom(DataGrid.FooterRow);
            dataGridX.Add(rowX);
        }

        private void AddRowSeparatorIfBorderIsVisible()
        {
            if (DataGrid.Border?.IsVisible == true)
            {
                SeparatorX separatorX = SeparatorX.CreateFor(DataGrid);
                dataGridX.Add(separatorX);
            }
        }
    }
}