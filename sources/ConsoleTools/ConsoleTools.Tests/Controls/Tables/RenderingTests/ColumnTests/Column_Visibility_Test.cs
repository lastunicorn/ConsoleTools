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

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.RenderingTests.ColumnTests;

[TestFixture]
public class Column_Visibility_Test : TestsBase
{
    [Test]
    public void HavingColumnVisibilityNotSpecified_WhenRendered_ThenAllColumnsAreDisplayed()
    {
        DataGrid dataGrid = new("This is a Column Visibility test");

        dataGrid.Columns.Add(new Column("Col 0"));
        dataGrid.Columns.Add(new Column("Col 1"));
        dataGrid.Columns.Add(new Column("Col 2"));

        dataGrid.Rows.Add("0,0", "0,1", "0,2");
        dataGrid.Rows.Add("1,0", "1,1", "1,2");
        dataGrid.Rows.Add("2,0", "2,1", "2,2");

        string expected = GetResourceFileContent("01-visibility-notspecified.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingIsVisibleSetToTrueForColumn1_WhenRendered_ThenColumn1IsDisplayed()
    {
        DataGrid dataGrid = new("This is a Column Visibility test");

        dataGrid.Columns.Add(new Column("Col 0"));
        dataGrid.Columns.Add(new Column("Col 1")
        {
            IsVisible = true
        });
        dataGrid.Columns.Add(new Column("Col 2"));

        dataGrid.Rows.Add("0,0", "0,1", "0,2");
        dataGrid.Rows.Add("1,0", "1,1", "1,2");
        dataGrid.Rows.Add("2,0", "2,1", "2,2");

        string expected = GetResourceFileContent("02-visibility-true.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingIsVisibleSetToFalseForColumn1_WhenRendered_ThenColumn1IsMissing()
    {
        DataGrid dataGrid = new("This is a Column Visibility test");

        dataGrid.Columns.Add(new Column("Col 0"));
        dataGrid.Columns.Add(new Column("Col 1")
        {
            IsVisible = false
        });
        dataGrid.Columns.Add(new Column("Col 2"));

        dataGrid.Rows.Add("0,0", "0,1", "0,2");
        dataGrid.Rows.Add("1,0", "1,1", "1,2");
        dataGrid.Rows.Add("2,0", "2,1", "2,2");

        string expected = GetResourceFileContent("03-visibility-false.txt");
        dataGrid.IsEqualTo(expected);
    }
}