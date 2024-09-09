// ConsoleTools
// Copyright (C) 2017-2024 Dust in the Wind
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

using DustInTheWind.ConsoleTools.Controls.Tables;

namespace DustInTheWind.ConsoleTools.Demo.DataGridDemo.Demo.BorderDemo;

internal static class DummyDataGrid
{
    public static DataGrid Create(string title)
    {
        DataGrid dataGrid = new()
        {
            Title = title
        };

        dataGrid.Columns.Add("Column 0");
        dataGrid.Columns.Add("Column 1");
        dataGrid.Columns.Add("Column 2");

        for (int i = 0; i < 3; i++)
        {
            string cell0 = $"cell {i}:0";
            string cell1 = $"cell {i}:1";
            string cell2 = $"cell {i}:2";

            dataGrid.Rows.Add(cell0, cell1, cell2);
        }

        dataGrid.Footer = "Footer text";

        return dataGrid;
    }
}