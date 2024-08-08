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
public class HeaderRow_AutoVisibility_Tests : TestsBase
{
    [Test]
    public void HavingNoColumnsDeclared_WhenRendered_ThenHeaderRowIsNotDisplayed()
    {
        DataGrid dataGrid = new("Header visibility test");

        dataGrid.Rows.Add("Cell Content 0,0", "Cell Content 0,1", "Cell Content 0,2");
        dataGrid.Rows.Add("Cell Content 1,0", "Cell Content 1,1", "Cell Content 1,2");
        dataGrid.Rows.Add("Cell Content 2,0", "Cell Content 2,1", "Cell Content 2,2");

        string expected = GetResourceFileContent("01-no-columns.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingColumn1WithEmptyHeaderText_WhenRendered_ThenHeaderRowIsDisplayed()
    {
        DataGrid dataGrid = new("Header visibility test");

        dataGrid.Columns.Add(new Column("Header 1"));
        dataGrid.Columns.Add(new Column(string.Empty));
        dataGrid.Columns.Add(new Column("Header 3"));

        dataGrid.Rows.Add("Cell Content 0,0", "Cell Content 0,1", "Cell Content 0,2");
        dataGrid.Rows.Add("Cell Content 1,0", "Cell Content 1,1", "Cell Content 1,2");
        dataGrid.Rows.Add("Cell Content 2,0", "Cell Content 2,1", "Cell Content 2,2");

        string expected = GetResourceFileContent("02-column-1-header-empty.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingColumn1WithNullHeaderText_WhenRendered_ThenHeaderRowIsDisplayed()
    {
        DataGrid dataGrid = new("Header visibility test");

        dataGrid.Columns.Add(new Column("Header 1"));
        dataGrid.Columns.Add(new Column(null as string));
        dataGrid.Columns.Add(new Column("Header 3"));

        dataGrid.Rows.Add("Cell Content 0,0", "Cell Content 0,1", "Cell Content 0,2");
        dataGrid.Rows.Add("Cell Content 1,0", "Cell Content 1,1", "Cell Content 1,2");
        dataGrid.Rows.Add("Cell Content 2,0", "Cell Content 2,1", "Cell Content 2,2");

        string expected = GetResourceFileContent("03-column-1-header-null.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingAllColumnsWithEmptyHeaderText_WhenRendered_ThenHeaderRowIsNotDisplayed()
    {
        DataGrid dataGrid = new("Header visibility test");

        dataGrid.Columns.Add(new Column(string.Empty));
        dataGrid.Columns.Add(new Column(string.Empty));
        dataGrid.Columns.Add(new Column(string.Empty));

        dataGrid.Rows.Add("Cell Content 0,0", "Cell Content 0,1", "Cell Content 0,2");
        dataGrid.Rows.Add("Cell Content 1,0", "Cell Content 1,1", "Cell Content 1,2");
        dataGrid.Rows.Add("Cell Content 2,0", "Cell Content 2,1", "Cell Content 2,2");

        string expected = GetResourceFileContent("04-columns-header-empty.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingAllColumnsWithNullHeaderText_WhenRendered_ThenHeaderRowIsNotDisplayed()
    {
        DataGrid dataGrid = new("Header visibility test");

        dataGrid.Columns.Add(new Column(null as string));
        dataGrid.Columns.Add(new Column(null as string));
        dataGrid.Columns.Add(new Column(null as string));

        dataGrid.Rows.Add("Cell Content 0,0", "Cell Content 0,1", "Cell Content 0,2");
        dataGrid.Rows.Add("Cell Content 1,0", "Cell Content 1,1", "Cell Content 1,2");
        dataGrid.Rows.Add("Cell Content 2,0", "Cell Content 2,1", "Cell Content 2,2");

        string expected = GetResourceFileContent("05-columns-header-null.txt");
        dataGrid.IsEqualTo(expected);
    }
}