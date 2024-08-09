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

using System.Collections.Generic;
using System.Linq;
using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Controls.Tables;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.RenderingTests.ContentCellTests;

[TestFixture]
public class ContentCell_ColumnSpan_ContentRowTop_NoTitleRow_NoHeaderRow_Tests : TestsBase
{
    [Test]
    public void HavingNoColumnSpanSpecified_WhenRendered()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        List<ContentCell> row0Cells = CreateCells(4, 0);
        dataGrid.Rows.Add(row0Cells);

        List<ContentCell> row1Cells = CreateCells(4, 1);
        dataGrid.Rows.Add(row1Cells);

        List<ContentCell> row2Cells = CreateCells(4, 2);
        dataGrid.Rows.Add(row2Cells);

        string expected = GetResourceFileContent("00-columnspan-unspecified.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingColumnSpan1_WhenRendered()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        List<ContentCell> row0Cells = CreateCells(4, 0);
        dataGrid.Rows.Add(row0Cells);

        List<ContentCell> row1Cells = CreateCells(4, 1);
        dataGrid.Rows.Add(row1Cells);

        List<ContentCell> row2Cells = CreateCells(4, 2);
        dataGrid.Rows.Add(row2Cells);

        row0Cells[1].ColumnSpan = 1;

        string expected = GetResourceFileContent("01-columnspan-1.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingColumnSpan2_WhenRendered()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        List<ContentCell> row0Cells = CreateCells(3, 0);
        dataGrid.Rows.Add(row0Cells);

        List<ContentCell> row1Cells = CreateCells(4, 1);
        dataGrid.Rows.Add(row1Cells);

        List<ContentCell> row2Cells = CreateCells(4, 2);
        dataGrid.Rows.Add(row2Cells);

        row0Cells[1].ColumnSpan = 2;

        string expected = GetResourceFileContent("02-columnspan-2.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingColumnSpan3_WhenRendered()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        List<ContentCell> row0Cells = CreateCells(2, 0);
        dataGrid.Rows.Add(row0Cells);

        List<ContentCell> row1Cells = CreateCells(4, 1);
        dataGrid.Rows.Add(row1Cells);

        List<ContentCell> row2Cells = CreateCells(4, 2);
        dataGrid.Rows.Add(row2Cells);

        row0Cells[1].ColumnSpan = 3;

        string expected = GetResourceFileContent("03-columnspan-3.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingColumnSpan10_WhenRendered()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        List<ContentCell> row0Cells = CreateCells(2, 0);
        dataGrid.Rows.Add(row0Cells);

        List<ContentCell> row1Cells = CreateCells(4, 1);
        dataGrid.Rows.Add(row1Cells);

        List<ContentCell> row2Cells = CreateCells(4, 2);
        dataGrid.Rows.Add(row2Cells);

        row0Cells[1].ColumnSpan = 10;

        string expected = GetResourceFileContent("04-columnspan-10.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingColumnSpan3AndHorizontalAlignRight_WhenRendered()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        List<ContentCell> row0Cells = CreateCells(2, 0);
        dataGrid.Rows.Add(row0Cells);

        List<ContentCell> row1Cells = CreateCells(4, 1);
        dataGrid.Rows.Add(row1Cells);

        List<ContentCell> row2Cells = CreateCells(4, 2);
        dataGrid.Rows.Add(row2Cells);

        row0Cells[1].ColumnSpan = 3;
        row0Cells[1].HorizontalAlignment = HorizontalAlignment.Right;

        string expected = GetResourceFileContent("05-columnspan-3-align-right.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingColumnSpan3AndBordersBetweenRows_WhenRendered()
    {
        DataGrid dataGrid = CreateDummyDataGrid();
        dataGrid.DisplayBorderBetweenRows = true;

        List<ContentCell> row0Cells = CreateCells(2, 0);
        dataGrid.Rows.Add(row0Cells);

        List<ContentCell> row1Cells = CreateCells(4, 1);
        dataGrid.Rows.Add(row1Cells);

        List<ContentCell> row2Cells = CreateCells(4, 2);
        dataGrid.Rows.Add(row2Cells);

        row0Cells[1].ColumnSpan = 3;

        string expected = GetResourceFileContent("06-columnspan-3-bordersbetweenrows.txt");
        dataGrid.IsEqualTo(expected);
    }

    private static DataGrid CreateDummyDataGrid()
    {
        DataGrid dataGrid = new();
        dataGrid.Title = "Cell Column Span Tests";
        dataGrid.TitleRow.IsVisible = false;
        dataGrid.BorderTemplate = BorderTemplate.SingleLineBorderTemplate;

        dataGrid.Columns.Add("Column 0");
        dataGrid.Columns.Add("Column 1");
        dataGrid.Columns.Add("Column 2");
        dataGrid.Columns.Add("Column 3");

        dataGrid.HeaderRow.IsVisible = false;

        dataGrid.Footer = "Footer text";

        return dataGrid;
    }

    private static List<ContentCell> CreateCells(int count, int rowIndex)
    {
        return Enumerable.Range(0, count)
            .Select(x => new ContentCell($"cell {rowIndex}:{x}"))
            .ToList();
    }
}