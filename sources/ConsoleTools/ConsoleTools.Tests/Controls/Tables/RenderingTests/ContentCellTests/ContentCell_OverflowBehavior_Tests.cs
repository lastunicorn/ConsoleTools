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

using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Controls.Tables;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.RenderingTests.ContentCellTests;

[TestFixture]
public class ContentCell_OverflowBehavior_Tests : TestsBase
{
    [Test]
    public void HavingOverflowBehaviorUnspecified()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        dataGrid.MaxWidth = 100;

        string expected = GetResourceFileContent("00-unspecified.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingOverflowBehaviorOverflow()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        dataGrid.Rows[1][1].OverflowBehavior = OverflowBehavior.Overflow;
        dataGrid.MaxWidth = 100;

        string expected = GetResourceFileContent("01-overflow.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingOverflowBehaviorCutChar()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        dataGrid.Rows[1][1].OverflowBehavior = OverflowBehavior.CutChar;
        dataGrid.MaxWidth = 100;

        string expected = GetResourceFileContent("02-cutchar.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingOverflowBehaviorCutCharWithEllipsis()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        dataGrid.Rows[1][1].OverflowBehavior = OverflowBehavior.CutCharWithEllipsis;
        dataGrid.MaxWidth = 100;

        string expected = GetResourceFileContent("03-cutcharwithellipsis.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingOverflowBehaviorCutWord()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        dataGrid.Rows[1][1].OverflowBehavior = OverflowBehavior.CutWord;
        dataGrid.MaxWidth = 100;

        string expected = GetResourceFileContent("04-cutword.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingOverflowBehaviorCutWordWithEllipsis()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        dataGrid.Rows[1][1].OverflowBehavior = OverflowBehavior.CutWordWithEllipsis;
        dataGrid.MaxWidth = 100;

        string expected = GetResourceFileContent("05-cutwordwithellipsis.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingOverflowBehaviorWrapChar()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        dataGrid.Rows[1][1].OverflowBehavior = OverflowBehavior.WrapChar;
        dataGrid.MaxWidth = 100;

        string expected = GetResourceFileContent("06-wrapchar.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingOverflowBehaviorWordWrap()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        dataGrid.Rows[1][1].OverflowBehavior = OverflowBehavior.WrapWord;
        dataGrid.MaxWidth = 100;

        string expected = GetResourceFileContent("07-wrapword.txt");
        dataGrid.IsEqualTo(expected);
    }

    private static DataGrid CreateDummyDataGrid()
    {
        DataGrid dataGrid = new();
        dataGrid.Title = "Cell OverflowBehavior Tests";

        dataGrid.DisplayBorderBetweenRows = true;

        dataGrid.Columns.Add("Column 0");
        dataGrid.Columns.Add("Column 1");
        dataGrid.Columns.Add("Column 2");

        for (int i = 0; i < 3; i++)
        {
            string cell0 = $"cell {i:00}:0 Lorem ipsum consectetur adipiscing elit";
            string cell1 = $"cell {i:00}:1 Lorem ipsum consectetur adipiscing elit";
            string cell2 = $"cell {i:00}:2 Lorem ipsum consectetur adipiscing elit";

            dataGrid.Rows.Add(cell0, cell1, cell2);
        }

        dataGrid.Footer = "Footer text";

        return dataGrid;
    }
}