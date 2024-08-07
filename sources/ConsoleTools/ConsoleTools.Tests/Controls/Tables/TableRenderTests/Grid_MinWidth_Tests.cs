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
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.TableRenderTests;

[TestFixture]
public class Grid_MinWidth_Tests : TestsBase
{
    [Test]
    public void HavingColumnMinWidthNotSpecified()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        string expected = GetResourceFileContent("01-minwidth-notspecified.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingColumnMinWidthLessThanContent()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        dataGrid.MinWidth = 10;

        string expected = GetResourceFileContent("02-minwidth-less.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingColumnMinWidthGreaterThanContent()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        dataGrid.MinWidth = 50;

        string expected = GetResourceFileContent("03-minwidth-greater.txt");
        dataGrid.IsEqualTo(expected);
    }

    private static DataGrid CreateDummyDataGrid()
    {
        DataGrid dataGrid = new();

        dataGrid.Columns.Add("Column 0");
        dataGrid.Columns.Add("Column 1");
        dataGrid.Columns.Add("Column 2");

        for (int i = 0; i < 5; i++)
        {
            string cell0 = $"cell {i:00}:0";
            string cell1 = $"cell {i:00}:1";
            string cell2 = $"cell {i:00}:2";

            dataGrid.Rows.Add(cell0, cell1, cell2);
        }

        return dataGrid;
    }
}