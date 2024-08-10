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

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.RenderingTests.DataGridTests;

[TestFixture]
public class Grid_CellContentOverflow_Tests : TestsBase
{
    [Test]
    public void HavingContentOverflowUnspecified()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        dataGrid.MaxWidth = 100;

        string expected = GetResourceFileContent("00-unspecified.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingContentOverflowDefault()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        dataGrid.CellContentOverflow = CellContentOverflow.Default;
        dataGrid.MaxWidth = 100;

        string expected = GetResourceFileContent("01-default.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingContentOverflowPreserveOverflow()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        dataGrid.CellContentOverflow = CellContentOverflow.PreserveOverflow;
        dataGrid.MaxWidth = 100;

        string expected = GetResourceFileContent("02-preserveoverflow.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingContentOverflowCutChar()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        dataGrid.CellContentOverflow = CellContentOverflow.CutChar;
        dataGrid.MaxWidth = 100;

        string expected = GetResourceFileContent("03-cutchar.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingContentOverflowCutCharWithEllipsis()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        dataGrid.CellContentOverflow = CellContentOverflow.CutCharWithEllipsis;
        dataGrid.MaxWidth = 100;

        string expected = GetResourceFileContent("04-cutcharwithellipsis.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingContentOverflowCutWord()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        dataGrid.CellContentOverflow = CellContentOverflow.CutWord;
        dataGrid.MaxWidth = 100;

        string expected = GetResourceFileContent("05-cutword.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingContentOverflowCutWordWithEllipsis()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        dataGrid.CellContentOverflow = CellContentOverflow.CutWordWithEllipsis;
        dataGrid.MaxWidth = 100;

        string expected = GetResourceFileContent("06-cutwordwithellipsis.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingContentOverflowWrapChar()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        dataGrid.CellContentOverflow = CellContentOverflow.WrapChar;
        dataGrid.MaxWidth = 100;

        string expected = GetResourceFileContent("07-wrapchar.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingContentOverflowWordWrap()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        dataGrid.CellContentOverflow = CellContentOverflow.WrapWord;
        dataGrid.MaxWidth = 100;

        string expected = GetResourceFileContent("08-wrapword.txt");
        dataGrid.IsEqualTo(expected);
    }

    private static DataGrid CreateDummyDataGrid()
    {
        DataGrid dataGrid = new();
        dataGrid.Title = "Grid CellContentOverflow Tests";

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