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

        public TitleRow TitleRow { get; set; }

        public HeaderRow HeaderRow { get; set; }

        public ContentRowList Rows { get; set; }

        public FooterRow FooterRow { get; set; }

        public DataGridBorder DataGridBorder { get; set; }

        public int MinWidth { get; set; }

        public DataGridX Build()
        {
            dataGridX = new DataGridX(DataGridBorder?.IsVisible == true)
            {
                MinWidth = MinWidth
            };

            bool isTitleVisible = TitleRow is { IsVisible: true, HasContent: true };
            if (isTitleVisible)
                AddTitle();

            bool isColumnHeaderRowVisible = HeaderRow is { IsVisible: true, CellCount: > 0 };
            if (isColumnHeaderRowVisible)
                AddHeader();

            bool areNormalRowsVisible = Rows.Count > 0;
            if (areNormalRowsVisible)
                AddRows();

            bool isFooterVisible = FooterRow is { IsVisible: true, HasContent: true };
            if (isFooterVisible)
                AddFooter();

            AddRowSeparatorIfNeeded();

            dataGridX.Close();

            return dataGridX;
        }

        private void AddTitle()
        {
            AddRowSeparatorIfNeeded();

            RowX rowX = RowX.CreateFrom(TitleRow);
            dataGridX.AddRow(rowX);
        }

        private void AddHeader()
        {
            AddRowSeparatorIfNeeded();

            RowX headerRowX = RowX.CreateFrom(HeaderRow);
            dataGridX.AddRow(headerRowX);
        }

        private void AddRows()
        {
            IEnumerable<RowX> rows = Rows
                .Where(x => x.IsVisible)
                .Select(RowX.CreateFrom);

            foreach (RowX row in rows)
            {
                AddRowSeparatorIfNeeded();

                dataGridX.AddRow(row);
            }
        }

        private void AddFooter()
        {
            AddRowSeparatorIfNeeded();

            RowX rowX = RowX.CreateFrom(FooterRow);
            dataGridX.AddRow(rowX);
        }

        private void AddRowSeparatorIfNeeded()
        {
            if (DataGridBorder?.IsVisible ?? false)
            {
                SeparatorX separatorX = SeparatorX.CreateFrom(DataGridBorder);
                dataGridX.AddSeparator(separatorX);
            }
        }
    }
}