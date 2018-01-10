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
// Bugs or fearure requests
// --------------------------------------------------------------------------------
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new

using System.Collections.Generic;
using System.Linq;

namespace DustInTheWind.ConsoleTools.TabularData
{
    internal class DataGridX
    {
        public List<TitleRowX> TitleRows { get; } = new List<TitleRowX>();

        private readonly List<DataRowX> dataRows = new List<DataRowX>();
        private DataRowX currentRow;

        public List<int> CalculatedColumnsWidth { get; set; }
        public int TableWidth { get; private set; }
        
        public List<int> CalculatedRowsHeight => dataRows.Select(x => x.Size.Height).ToList();
        
        public void StartNewRow(bool hasBorder)
        {
            EndDataRow();

            currentRow = new DataRowX(hasBorder);
            dataRows.Add(currentRow);
        }

        public void EndDataRow()
        {
            if (currentRow == null)
                return;

            if (TableWidth < currentRow.Size.Width)
                TableWidth = currentRow.Size.Width;

            currentRow = null;
        }

        public void AddDataCell(DataCell dataCell)
        {
            int j = currentRow.NextIndex;

            while (CalculatedColumnsWidth.Count <= j)
                CalculatedColumnsWidth.Add(0);

            Size cellSize = dataCell.CalculateDimensions();

            if (cellSize.Width > CalculatedColumnsWidth[j])
            {
                CalculatedColumnsWidth[j] = cellSize.Width;
                DataCellX cell = new DataCellX { Size = new Size(cellSize.Width, cellSize.Height) };
                currentRow.AddCell(cell);
            }
            else
            {
                DataCellX cell = new DataCellX { Size = new Size(CalculatedColumnsWidth[j], cellSize.Height) };
                currentRow.AddCell(cell);
            }
        }
    }
}