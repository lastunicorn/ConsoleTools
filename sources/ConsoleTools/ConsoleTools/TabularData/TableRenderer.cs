// ConsoleTools
// Copyright (C) 2017-2018 Dust in the Wind
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
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new

using System.Linq;
using DustInTheWind.ConsoleTools.TabularData.RenderingModel;

namespace DustInTheWind.ConsoleTools.TabularData
{
    internal class TableRenderer
    {
        private DataGridX dataGridX;

        public TitleRow TitleRow { get; set; }
        public bool DisplayTitle { get; set; }

        public ColumnList Columns { get; set; }
        public bool DisplayColumnHeaders { get; set; }

        public DataRowList Rows { get; set; }

        public BorderTemplate BorderTemplate { get; set; }
        public bool DisplayBorder { get; set; }
        public bool DrawBordersBetweenRows { get; set; }

        public int MinWidth { get; set; }
        public HorizontalAlignment CellHorizontalAlignment { get; set; }

        public ITablePrinter TablePrinter { get; set; }

        public void Render()
        {
            BuildXObject();

            dataGridX.Render(TablePrinter);
        }

        private void BuildXObject()
        {
            dataGridX = new DataGridX(DisplayBorder)
            {
                MinWidth = MinWidth
            };

            bool isTitleVisible = DisplayTitle && TitleRow != null && TitleRow.HasContent;
            bool isColumnHeaderRowVisible = DisplayColumnHeaders && Columns != null && Columns.Count > 0 && Columns.Any(x => !x.Header.IsEmpty);
            bool areDataRowsVisible = Rows.Count > 0;

            if (isTitleVisible)
            {
                TitleRowX titleRowX = new TitleRowX(TitleRow);
                dataGridX.AddTitleRow(titleRowX);
            }

            if (isColumnHeaderRowVisible)
            {
                HeaderRowX headerRowX = new HeaderRowX(Columns, DisplayBorder);
                dataGridX.AddHeaderRow(headerRowX);
            }

            if (areDataRowsVisible)
            {
                var rows = Rows
                    .Select(x => new DataRowX(x, DisplayBorder));

                foreach (DataRowX row in rows)
                    dataGridX.AddDataRow(row);
            }

            if (DisplayBorder)
            {
                if (isTitleVisible)
                    dataGridX.TitleTopBorder = new TitleTopBorder(BorderTemplate);
                else if (isColumnHeaderRowVisible)
                    dataGridX.HeaderTopBorder = new HeaderTopBorder(BorderTemplate);
                else if (areDataRowsVisible)
                    dataGridX.DataTopBorder = new DataTopBorder(BorderTemplate);

                if (isTitleVisible)
                {
                    if (isColumnHeaderRowVisible)
                        dataGridX.TitleHeaderSeparator = new TitleHeaderSeparator(BorderTemplate);
                    else if (areDataRowsVisible)
                        dataGridX.TitleDataSeparator = new TitleDataSeparator(BorderTemplate);
                    else
                        dataGridX.TitleBottomBorder = new TitleBottomBorder(BorderTemplate);
                }

                if (isColumnHeaderRowVisible)
                {
                    if (areDataRowsVisible)
                        dataGridX.HeaderDataSeparator = new HeaderDataSeparator(BorderTemplate);
                    else
                        dataGridX.HeaderBottomBorder = new HeaderBottomBorder(BorderTemplate);
                }

                if (areDataRowsVisible)
                {
                    if (DrawBordersBetweenRows)
                        dataGridX.DataDataSeparator = new DataDataSeparator(BorderTemplate);

                    dataGridX.DataBottomBorder = new BottomBorderData(BorderTemplate);
                }
            }

            dataGridX.MakeFinalAdjustments();
        }
    }
}