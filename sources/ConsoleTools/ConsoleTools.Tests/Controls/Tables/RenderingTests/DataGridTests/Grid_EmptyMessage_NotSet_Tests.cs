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
using FluentAssertions;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.RenderingTests.DataGridTests;

[TestFixture]
public class Grid_EmptyMessage_NotSet_Tests : TestsBase
{
    [Test]
    public void HavingGridWithNoTitleNoHeaderNoContentNoFooter_WhenRendered_ThenNothingIsDisplayed()
    {
        DataGrid dataGrid = new();

        string actual = dataGrid.ToString();

        actual.Should().BeEmpty();
    }

    [Test]
    public void HavingGridWithTitle_WhenRendered_ThenOnlyTitleIsDisplayed()
    {
        DataGrid dataGrid = new();
        dataGrid.Title = "Test empty grid";

        string expected = GetResourceFileContent("01-title.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingGridWithTitleHeader_WhenRendered_ThenTitleAndHeaderAreDisplayed()
    {
        DataGrid dataGrid = new();
        dataGrid.Title = "Test empty grid";

        dataGrid.Columns.Add("Column 0");
        dataGrid.Columns.Add("Column 1");
        dataGrid.Columns.Add("Column 2");

        string expected = GetResourceFileContent("02-title-header.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingGridWithTitleHeaderFooter_WhenRendered_ThenTitleHeaderAndFooterAreDisplayed()
    {
        DataGrid dataGrid = new();
        dataGrid.Title = "Test empty grid";

        dataGrid.Columns.Add("Column 0");
        dataGrid.Columns.Add("Column 1");
        dataGrid.Columns.Add("Column 2");

        dataGrid.Footer = "Footer text";

        string expected = GetResourceFileContent("03-title-header-footer.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingTitleHeaderContentFooter_WhenRendered_ThenTitleHeaderContentFooterAreDisplayed()
    {
        DataGrid dataGrid = new();
        dataGrid.Title = "Test empty grid";

        dataGrid.Columns.Add("Column 0");
        dataGrid.Columns.Add("Column 1");
        dataGrid.Columns.Add("Column 2");

        dataGrid.Rows.Add("Cell 1", "Cell 2", "Cell 3");
        dataGrid.Rows.Add("Cell 4", "Cell 5", "Cell 6");

        dataGrid.Footer = "Footer text";

        string expected = GetResourceFileContent("04-title-header-content-footer.txt");
        dataGrid.IsEqualTo(expected);
    }
}