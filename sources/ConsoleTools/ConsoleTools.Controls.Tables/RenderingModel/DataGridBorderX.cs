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

namespace DustInTheWind.ConsoleTools.Controls.Tables.RenderingModel
{
    internal class DataGridBorderX
    {
        public BorderTemplate Template { get; set; }

        public ConsoleColor? ForegroundColor { get; set; }

        public ConsoleColor? BackgroundColor { get; set; }

        public int TotalWidth { get; set; }

        public List<ColumnX> ColumnWidths { get; set; }

        public void RenderRowLeftBorder(ITablePrinter tablePrinter)
        {
            tablePrinter.Write(Template.Left, ForegroundColor, BackgroundColor);
        }

        public void RenderRowRightBorder(ITablePrinter tablePrinter)
        {
            tablePrinter.Write(Template.Right, ForegroundColor, BackgroundColor);
        }

        public void RenderRowInsideBorder(ITablePrinter tablePrinter)
        {
            tablePrinter.Write(Template.Vertical, ForegroundColor, BackgroundColor);
        }

        public static DataGridBorderX CreateFrom(DataGridBorder dataGridBorder)
        {
            if (dataGridBorder == null)
                return new DataGridBorderX();

            return new DataGridBorderX
            {
                Template = dataGridBorder.Template,
                ForegroundColor = dataGridBorder.CalculateForegroundColor(),
                BackgroundColor = dataGridBorder.CalculateBackgroundColor()
            };
        }
    }
}