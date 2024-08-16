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
public class ContentCell_Content_Tests : TestsBase
{
    [Test]
    public void HavingUnspecifiedContent()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        ContentCell cell1 = new("cell 1");
        ContentCell cell2 = new("cell 2");
        ContentCell cell3 = new();
        ContentCell cell4 = new("cell 4");
        ContentCell cell5 = new("cell 5");

        dataGrid.Rows.Add(cell1, cell2, cell3, cell4, cell5);

        string expected = GetResourceFileContent("01-content-unspecified.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingNullContent()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        ContentCell cell1 = new("cell 1");
        ContentCell cell2 = new("cell 2");
        ContentCell cell3 = new(null as object);
        ContentCell cell4 = new("cell 4");
        ContentCell cell5 = new("cell 5");

        dataGrid.Rows.Add(cell1, cell2, cell3, cell4, cell5);

        string expected = GetResourceFileContent("02-content-null.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingEmptyStringContent()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        ContentCell cell1 = new("cell 1");
        ContentCell cell2 = new("cell 2");
        ContentCell cell3 = new(string.Empty);
        ContentCell cell4 = new("cell 4");
        ContentCell cell5 = new("cell 5");

        dataGrid.Rows.Add(cell1, cell2, cell3, cell4, cell5);

        string expected = GetResourceFileContent("03-content-empty.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingEmptyMultilineContent()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        ContentCell cell1 = new("cell 1");
        ContentCell cell2 = new("cell 2");
        ContentCell cell3 = new(MultilineText.Empty);
        ContentCell cell4 = new("cell 4");
        ContentCell cell5 = new("cell 5");

        dataGrid.Rows.Add(cell1, cell2, cell3, cell4, cell5);

        string expected = GetResourceFileContent("04-content-emptymultiline.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingMultilineObjectAsContent()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        ContentCell cell1 = new("cell 1");
        ContentCell cell2 = new("cell 2");
        ContentCell cell3 = new(new MultilineText("cell 3"));
        ContentCell cell4 = new("cell 4");
        ContentCell cell5 = new("cell 5");

        dataGrid.Rows.Add(cell1, cell2, cell3, cell4, cell5);

        string expected = GetResourceFileContent("05-content-multiline.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingObjectAsContent()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        ContentCell cell1 = new("cell 1");
        ContentCell cell2 = new("cell 2");
        ContentCell cell3 = new(new object());
        ContentCell cell4 = new("cell 4");
        ContentCell cell5 = new("cell 5");

        dataGrid.Rows.Add(cell1, cell2, cell3, cell4, cell5);

        string expected = GetResourceFileContent("06-content-object.txt");
        dataGrid.IsEqualTo(expected);
    }

    private static DataGrid CreateDummyDataGrid()
    {
        DataGrid dataGrid = new();
        dataGrid.Title = "Cell Content Tests";

        dataGrid.Footer = "Footer text";

        return dataGrid;
    }
}