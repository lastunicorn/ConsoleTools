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

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.RenderingTests.HeaderCellTests;

[TestFixture]
public class HeaderCell_DefaultContent_Tests : TestsBase
{
    [Test]
    public void HavingDefaultContentSetAndUnspecifiedContent()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        string expected = GetResourceFileContent("01-content-unspecified.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingDefaultContentSetAndNullContent()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        dataGrid.Columns[1].HeaderCell.Content = null;

        string expected = GetResourceFileContent("02-content-null.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingDefaultContentSetAndEmptyContent()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        dataGrid.Columns[1].HeaderCell.Content = string.Empty;

        string expected = GetResourceFileContent("03-content-empty.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingDefaultContentSetAndEmptyMultilineContent()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        dataGrid.Columns[1].HeaderCell.Content = MultilineText.Empty;

        string expected = GetResourceFileContent("04-content-emptymultiline.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingDefaultContentSetAndMultilineObjectAsContent()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        dataGrid.Columns[1].HeaderCell.Content = new MultilineText("Column 1");

        string expected = GetResourceFileContent("05-content-multiline.txt");
        dataGrid.IsEqualTo(expected);
    }

    private static DataGrid CreateDummyDataGrid()
    {
        DataGrid dataGrid = new();
        dataGrid.Title = "Header Content Tests";

        dataGrid.Columns.Add(new Column());
        dataGrid.Columns.Add(new Column());
        dataGrid.Columns.Add(new Column());

        dataGrid.Columns[1].HeaderCell.DefaultContent = "Column";

        for (int i = 0; i < 3; i++)
        {
            string cell0 = $"cell {i:00}:0";
            string cell1 = $"cell {i:00}:1";
            string cell2 = $"cell {i:00}:2";

            dataGrid.Rows.Add(cell0, cell1, cell2);
        }

        dataGrid.Footer = "Footer text";

        return dataGrid;
    }
}