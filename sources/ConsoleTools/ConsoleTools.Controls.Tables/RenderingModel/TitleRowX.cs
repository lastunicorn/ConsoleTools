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

namespace DustInTheWind.ConsoleTools.Controls.Tables.RenderingModel
{
    internal class TitleRowX
    {
        private readonly TitleRow titleRow;
        private readonly bool isVisible;
        private readonly CellX cellX;

        public Size Size { get; set; }

        public TitleRowX(TitleRow titleRow)
        {
            this.titleRow = titleRow;

            isVisible = titleRow != null;
            Size = titleRow?.CalculatePreferredSize() ?? Size.Empty;
            cellX = new CellX(titleRow?.TitleCell);
        }

        public void Render(ITablePrinter tablePrinter)
        {
            if (!isVisible)
                return;

            BorderTemplate borderTemplate = titleRow?.ParentDataGrid?.BorderTemplate;
            bool displayBorder = borderTemplate != null && titleRow?.ParentDataGrid?.DisplayBorder == true;

            Size cellSize = displayBorder
                ? Size.InflateWidth(-2)
                : Size;

            cellX.InitializeRendering(cellSize);

            for (int lineIndex = 0; lineIndex < cellSize.Height; lineIndex++)
            {
                if (displayBorder)
                    tablePrinter.WriteBorder(borderTemplate.Left);

                cellX.RenderNextLine(tablePrinter);

                if (displayBorder)
                    tablePrinter.WriteBorder(borderTemplate.Right);

                tablePrinter.WriteLine();
            }
        }
    }
}