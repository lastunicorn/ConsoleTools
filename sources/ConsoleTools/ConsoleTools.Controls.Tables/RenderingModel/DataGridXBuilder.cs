// ConsoleTools
// Copyright (C) 2017-2020 Dust in the Wind
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
        private bool isTitleVisible;
        private bool isColumnHeaderRowVisible;
        private bool areNormalRowsVisible;

        public TitleRow TitleRow { get; set; }

        public HeaderRow HeaderRow { get; set; }

        public DataRowList Rows { get; set; }

        public DataGridBorder DataGridBorder { get; set; }

        public int MinWidth { get; set; }

        public DataGridX Build()
        {
            dataGridX = new DataGridX(DataGridBorder?.IsVisible == true);

            isTitleVisible = TitleRow != null && TitleRow.IsVisible && TitleRow.HasContent;
            isColumnHeaderRowVisible = HeaderRow != null && HeaderRow.IsVisible && HeaderRow.CellCount > 0;
            areNormalRowsVisible = Rows.Count > 0;

            if (isTitleVisible)
                AddTitle();

            if (isColumnHeaderRowVisible)
                AddHeader();

            if (areNormalRowsVisible)
                AddRows();

            if (DataGridBorder?.IsVisible == true)
                AddHorizontalBorders();

            dataGridX.CalculateLayout(MinWidth);

            return dataGridX;
        }

        private void AddTitle()
        {
            TitleRowX titleRowX = TitleRowX.CreateFrom(TitleRow);
            dataGridX.AddTitleRow(titleRowX);
        }

        private void AddHeader()
        {
            RowX headerRowX = RowX.CreateFrom(HeaderRow);
            dataGridX.AddHeaderRow(headerRowX);
        }

        private void AddRows()
        {
            IEnumerable<RowX> rows = Rows
                .Where(x => x.IsVisible)
                .Select(RowX.CreateFrom);

            foreach (RowX row in rows)
                dataGridX.AddNormalRow(row);
        }

        private void AddHorizontalBorders()
        {
            if (isTitleVisible)
                dataGridX.TitleTopBorder = TitleTopBorder.CreateFrom(DataGridBorder);
            else if (isColumnHeaderRowVisible)
                dataGridX.HeaderTopBorder = HeaderTopBorder.CreateFrom(DataGridBorder);
            else if (areNormalRowsVisible)
                dataGridX.DataTopBorder = DataTopBorder.CreateFrom(DataGridBorder);

            if (isTitleVisible)
            {
                if (isColumnHeaderRowVisible)
                    dataGridX.TitleHeaderSeparator = TitleHeaderSeparator.CreateFrom(DataGridBorder);
                else if (areNormalRowsVisible)
                    dataGridX.TitleDataSeparator = TitleDataSeparator.CreateFrom(DataGridBorder);
                else
                    dataGridX.TitleBottomBorder = TitleBottomBorder.CreateFrom(DataGridBorder);
            }

            if (isColumnHeaderRowVisible)
            {
                if (areNormalRowsVisible)
                    dataGridX.HeaderDataSeparator = HeaderDataSeparator.CreateFrom(DataGridBorder);
                else
                    dataGridX.HeaderBottomBorder = HeaderBottomBorder.CreateFrom(DataGridBorder);
            }

            if (areNormalRowsVisible)
            {
                if (DataGridBorder?.DisplayBorderBetweenRows == true)
                    dataGridX.DataDataSeparator = DataDataSeparator.CreateFrom(DataGridBorder);

                dataGridX.DataBottomBorder = DataBottomBorder.CreateFrom(DataGridBorder);
            }
        }
    }
}