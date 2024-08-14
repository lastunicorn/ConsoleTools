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

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.RenderingTests.DataGridTests;

[TestFixture]
public class Grid_EmptyMessage_Set_Tests : TestsBase
{
    [Test]
    public void HavingGridWithNoTitleNoHeaderNoContentNoFooter_WhenRendered_ThenEmptyTextIsDisplayedInTheContentArea()
    {
        DataGrid dataGrid = new();

        dataGrid.EmptyMessage = "No data here...";

        string expected = GetResourceFileContent("01-no-title-no-header-emptytext-no-footer.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingGridWithTitle_WhenRendered_ThenEmptyTextIsDisplayedInTheContentArea()
    {
        DataGrid dataGrid = new();
        dataGrid.Title = "Test empty grid";

        dataGrid.EmptyMessage = "No data here...";

        string expected = GetResourceFileContent("02-title-emptytext.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingGridWithTitleHeader_WhenRendered_ThenEmptyTextIsDisplayedInTheContentArea()
    {
        DataGrid dataGrid = new();
        dataGrid.Title = "Test empty grid";

        dataGrid.Columns.Add("Column 0");
        dataGrid.Columns.Add("Column 1");
        dataGrid.Columns.Add("Column 2");

        dataGrid.EmptyMessage = "No data here...";

        string expected = GetResourceFileContent("03-title-header-emptytext.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingGridWithTitleHeaderFooter_WhenRendered_ThenEmptyTextIsDisplayedInTheContentArea()
    {
        DataGrid dataGrid = new();
        dataGrid.Title = "Test empty grid";

        dataGrid.Columns.Add("Column 0");
        dataGrid.Columns.Add("Column 1");
        dataGrid.Columns.Add("Column 2");

        dataGrid.EmptyMessage = "No data here...";

        dataGrid.Footer = "Footer text";

        string expected = GetResourceFileContent("04-title-header-emptytext-footer.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingContentRows_WhenRendered_ThenContentRowsAreDisplayedInsteadOfEmptyText()
    {
        DataGrid dataGrid = new();
        dataGrid.Title = "Test empty grid";

        dataGrid.Columns.Add("Column 0");
        dataGrid.Columns.Add("Column 1");
        dataGrid.Columns.Add("Column 2");

        dataGrid.Rows.Add("Cell 1", "Cell 2", "Cell 3");
        dataGrid.Rows.Add("Cell 4", "Cell 5", "Cell 6");

        dataGrid.EmptyMessage = "No data here...";

        dataGrid.Footer = "Footer text";

        string expected = GetResourceFileContent("05-contentrows-emptytext.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingGridWithMultilineEmptyText_WhenRendered_ThenAllLinesOfTheEmptyTextAreDisplayed()
    {
        DataGrid dataGrid = new();
        dataGrid.Title = "Test empty grid";

        dataGrid.Columns.Add("Column 0");
        dataGrid.Columns.Add("Column 1");
        dataGrid.Columns.Add("Column 2");

        dataGrid.EmptyMessage = new MultilineText("No data here...", "Please adjust the filters and try again.");

        dataGrid.Footer = "Footer text";

        string expected = GetResourceFileContent("06-multiline-emptytext.txt");
        dataGrid.IsEqualTo(expected);
    }
}