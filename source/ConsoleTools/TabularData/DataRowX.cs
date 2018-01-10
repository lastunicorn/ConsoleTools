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

namespace DustInTheWind.ConsoleTools.TabularData
{
    internal class DataRowX
    {
        private readonly bool hasBorder;

        public Size Size { get; set; }

        public DataRowX(bool hasBorder)
        {
            this.hasBorder = hasBorder;
        }

        public List<DataCellX> Cells { get; } = new List<DataCellX>();

        public int NextIndex => Cells.Count;

        public void AddCell(DataCellX cell)
        {
            int initialCount = Cells.Count;

            int width = initialCount == 0 && hasBorder
                ? 1
                : Size.Width;

            width += cell.Size.Width;

            if (hasBorder)
                width++;

            int height = Size.Height < cell.Size.Height
                ? cell.Size.Height
                : Size.Height;

            Size = new Size(width, height);

            Cells.Add(cell);
        }
    }
}