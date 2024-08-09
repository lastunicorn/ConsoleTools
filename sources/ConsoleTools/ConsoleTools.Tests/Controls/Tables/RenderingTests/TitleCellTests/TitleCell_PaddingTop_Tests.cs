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

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.RenderingTests.TitleCellTests;

[TestFixture]
public class TitleCell_PaddingTop_Tests : TestsBase
{
    [Test]
    public void HavingNoPaddingTopSpecified_WhenRendered_TheCellContainsNoPaddingLine()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        string expected = GetResourceFileContent("01-paddingtop-unspecified.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingPaddingTop0_WhenRendered_TheCellContainsNoPaddingLine()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        dataGrid.TitleRow.TitleCell.PaddingTop = 0;

        string expected = GetResourceFileContent("02-paddingtop-0.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingPaddingTop1_WhenRendered_TheCellContainsOnePaddingLine()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        dataGrid.TitleRow.TitleCell.PaddingTop = 1;

        string expected = GetResourceFileContent("03-paddingtop-1.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingPaddingTop2_WhenRendered_TheCellContainsTwoPaddingLines()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        dataGrid.TitleRow.TitleCell.PaddingTop = 2;

        string expected = GetResourceFileContent("04-paddingtop-2.txt");
        dataGrid.IsEqualTo(expected);
    }

    private static DataGrid CreateDummyDataGrid()
    {
        DataGrid dataGrid = new();
        dataGrid.Title = "Cell Padding Tests";

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

        dataGrid.FooterRow.FooterCell.Content = "Footer text";

        return dataGrid;
    }
}