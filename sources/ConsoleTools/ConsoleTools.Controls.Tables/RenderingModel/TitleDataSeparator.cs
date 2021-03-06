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

using System;
using System.Collections.Generic;
using System.Text;

namespace DustInTheWind.ConsoleTools.Controls.Tables.RenderingModel
{
    internal class TitleDataSeparator
    {
        private string borderText;

        public BorderTemplate BorderTemplate { get; set; }

        public ConsoleColor? ForegroundColor { get; set; }

        public ConsoleColor? BackgroundColor { get; set; }

        public void Render(ITablePrinter tablePrinter, List<ColumnX> columns)
        {
            if (borderText == null)
                borderText = GenerateTitleDataSeparator(columns);

            tablePrinter.WriteLine(borderText, ForegroundColor, BackgroundColor);
        }

        /// <summary>
        /// Generates the border displayed between title and the first data row.
        /// This border is used only when title is visible, column header row is hidden and there is at least one row of data.
        /// </summary>
        /// <param name="columns"></param>
        private string GenerateTitleDataSeparator(List<ColumnX> columns)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(BorderTemplate.LeftIntersection);

            for (int columnIndex = 0; columnIndex < columns.Count; columnIndex++)
            {
                int columnWidth = columns[columnIndex].Width;
                sb.Append(new string(BorderTemplate.Horizontal, columnWidth));

                char columnBorderRight = columnIndex < columns.Count - 1
                    ? BorderTemplate.TopIntersection
                    : BorderTemplate.RightIntersection;

                sb.Append(columnBorderRight);
            }

            return sb.ToString();
        }

        public static TitleDataSeparator CreateFrom(DataGridBorder dataGridBorder)
        {
            return new TitleDataSeparator
            {
                BorderTemplate = dataGridBorder.Template,
                ForegroundColor = dataGridBorder.CalculateForegroundColor(),
                BackgroundColor = dataGridBorder.CalculateBackgroundColor()
            };
        }
    }
}