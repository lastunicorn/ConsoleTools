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

using System;
using System.Collections.Generic;
using System.Text;

namespace DustInTheWind.ConsoleTools.TabularData.RenderingModel
{
    internal class TitleDataSeparator
    {
        private readonly BorderTemplate borderTemplate;
        private string borderText;
        private List<int> columnsWidths;

        public List<int> ColumnsWidths
        {
            get { return columnsWidths; }
            set
            {
                if (value == columnsWidths)
                    return;

                columnsWidths = value;
                borderText = null;
            }
        }

        public TitleDataSeparator(BorderTemplate borderTemplate)
        {
            if (borderTemplate == null) throw new ArgumentNullException(nameof(borderTemplate));
            this.borderTemplate = borderTemplate;
        }

        public void Render(ITablePrinter tablePrinter)
        {
            if (borderText == null)
                borderText = GenerateTitleDataSeparator();

            //if (borderText == null)
            //{
            //    int titleCellWidth = columnsWidths.Sum() + columnsWidths.Count - 1;
            //    borderText = borderTemplate.GenerateHorizontalSeparator(new[] { titleCellWidth }, columnsWidths);
            //}

            tablePrinter.WriteLineBorder(borderText);
        }

        /// <summary>
        /// Generates the border displayed between title and the first data row.
        /// This border is used only when title is visible, column header row is hidden and there is at least one row of data.
        /// </summary>
        private string GenerateTitleDataSeparator()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(borderTemplate.LeftIntersection);

            for (int columnIndex = 0; columnIndex < columnsWidths.Count; columnIndex++)
            {
                int columnWidth = columnsWidths[columnIndex];
                sb.Append(new string(borderTemplate.Horizontal, columnWidth));

                char columnBorderRight = columnIndex < columnsWidths.Count - 1
                    ? borderTemplate.TopIntersection
                    : borderTemplate.RightIntersection;

                sb.Append(columnBorderRight);
            }

            return sb.ToString();
        }
    }
}