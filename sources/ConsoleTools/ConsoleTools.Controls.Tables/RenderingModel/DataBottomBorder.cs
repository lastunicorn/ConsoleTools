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
    internal class DataBottomBorder
    {
        private string borderText;

        public BorderTemplate BorderTemplate { get; set; }

        public ConsoleColor? ForegroundColor { get; set; }

        public ConsoleColor? BackgroundColor { get; set; }

        public void Render(ITablePrinter tablePrinter, IReadOnlyList<ColumnX> columns)
        {
            if (borderText == null)
            {
                List<int> columnWidths = columns
                    .Select(x => x.Width)
                    .ToList();

                borderText = BorderTemplate.GenerateBottomBorder(columnWidths);}

            tablePrinter.WriteLine(borderText, ForegroundColor, BackgroundColor);
        }

        public static DataBottomBorder CreateFrom(DataGridBorder dataGridBorder)
        {
            return new DataBottomBorder
            {
                BorderTemplate = dataGridBorder.Template,
                ForegroundColor = dataGridBorder.CalculateForegroundColor(),
                BackgroundColor = dataGridBorder.CalculateBackgroundColor()
            };
        }
    }
}