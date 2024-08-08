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

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.RenderingTests;

[TestFixture]
public class ContentRow_BorderVisibility_MiddleRow_Tests : TestsBase
{
    private DataGrid dataGrid;

    [SetUp]
    public void SetUp()
    {
        dataGrid = CreateDummyDataGrid();
    }

    [Test]
    public void HavingNoBorderExplicitlySetVisible_WhenRendered_ThenNoBorderIsDisplayedAboveAndBelowRow()
    {
        string expected = GetResourceFileContent("00-border-unspecified.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingTopBorderExplicitlySetVisible_WhenRendered_ThenHorizontalBorderIsDisplayedAboveRow()
    {
        dataGrid.Rows[2].BorderVisibility = ". + . . .";

        string expected = GetResourceFileContent("01-top-border.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingBottomBorderExplicitlySetVisible_WhenRendered_ThenHorizontalBorderIsDisplayedBelowRow()
    {
        dataGrid.Rows[2].BorderVisibility = ". . . + .";

        string expected = GetResourceFileContent("02-bottom-border.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingTopAndBottomBordersExplicitlySetVisible_WhenRendered_ThenHorizontalBorderIsDisplayedAboveAndBelowRow()
    {
        dataGrid.Rows[2].BorderVisibility = ". + . + .";

        string expected = GetResourceFileContent("03-top-and-bottom-border.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingTopBorderExplicitlyHidden_WhenRendered_ThenHorizontalBorderIsHiddenAboveRow()
    {
        dataGrid.Rows[2].BorderVisibility = ". - . . .";

        string expected = GetResourceFileContent("04-top-border-hidden.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingBottomBorderExplicitlyHidden_WhenRendered_ThenHorizontalBorderIsHiddenBelowRow()
    {
        dataGrid.Rows[2].BorderVisibility = ". . . - .";

        string expected = GetResourceFileContent("05-bottom-border-hidden.txt");
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