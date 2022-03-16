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

namespace DustInTheWind.ConsoleTools.Controls.Tables.RenderingModel
{
    internal class DataGridX
    {
        private readonly bool displayBorder;
        private readonly DataGridLayout dataGridLayout = new();
        private readonly List<IItemX> items = new();
        private IItemX lastItem;

        public int MinWidth { get; set; }

        public DataGridX(bool displayBorder)
        {
            this.displayBorder = displayBorder;
        }

        public void AddItem(IItemX item)
        {
            items.Add(item);

            if (item is RowX rowX)
                dataGridLayout.AddRow(rowX);
        }

        public void AddSeparator(SeparatorX separator)
        {
            separator.Row1 = lastItem as RowX;
            items.Add(separator);
            lastItem = separator;
        }

        public void AddRow(RowX rowX)
        {
            if (lastItem is SeparatorX lastSeparator) 
                lastSeparator.Row2 = rowX;

            items.Add(rowX);
            dataGridLayout.AddRow(rowX);
            lastItem = rowX;
        }

        public void Close()
        {
            dataGridLayout.BorderVisibility = displayBorder;
            dataGridLayout.MinWidth = MinWidth;
            dataGridLayout.MaxWidth = int.MaxValue;
            dataGridLayout.FinalizeLayout();
        }

        public void Render(ITablePrinter tablePrinter)
        {
            foreach (IItemX item in items)
                item.Render(tablePrinter, dataGridLayout.Columns);
        }
    }
}