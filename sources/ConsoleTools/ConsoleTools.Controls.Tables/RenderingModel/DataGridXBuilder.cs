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

        //public TitleRow TitleRow { get; set; }

        //public HeaderRow HeaderRow { get; set; }

        //public ContentRowList Rows { get; set; }

        //public FooterRow FooterRow { get; set; }

        //public DataGridBorder DataGridBorder { get; set; }

        //public int MinWidth { get; set; }

        public DataGridX Build()
        {
            dataGridX = new DataGridX(DataGrid.Border?.IsVisible == true)
            {
                MinWidth = DataGrid.MinWidth ?? 0
            };

            bool isTitleVisible = DataGrid.TitleRow is { IsVisible: true, HasContent: true };
            if (isTitleVisible)
                AddTitle();

            bool isColumnHeaderRowVisible = DataGrid.HeaderRow is { IsVisible: true, CellCount: > 0 };
            if (isColumnHeaderRowVisible)
                AddHeader();

            bool areNormalRowsVisible = DataGrid.Rows.Count > 0;
            if (areNormalRowsVisible)
                AddRows();

            bool isFooterVisible = DataGrid.FooterRow is { IsVisible: true, HasContent: true };
            if (isFooterVisible)
                AddFooter();

            AddRowSeparatorIfBorderIsVisible();

            dataGridX.Close();

            return dataGridX;
        }

        private void AddTitle()
        {
            AddRowSeparatorIfBorderIsVisible();

            RowX rowX = RowX.CreateFrom(DataGrid.TitleRow);
            dataGridX.AddRow(rowX);
        }

        private void AddHeader()
        {
            AddRowSeparatorIfBorderIsVisible();

            RowX headerRowX = RowX.CreateFrom(DataGrid.HeaderRow);
            dataGridX.AddRow(headerRowX);
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

                dataGridX.AddRow(row);
            }
        }

        private void AddFooter()
        {
            AddRowSeparatorIfBorderIsVisible();

            RowX rowX = RowX.CreateFrom(DataGrid.FooterRow);
            dataGridX.AddRow(rowX);
        }

        private void AddRowSeparatorIfBorderIsVisible()
        {
            if (DataGrid.Border?.IsVisible == true)
            {
                SeparatorX separatorX = SeparatorX.CreateFor(DataGrid);
                dataGridX.AddSeparator(separatorX);
            }
        }
    }
}