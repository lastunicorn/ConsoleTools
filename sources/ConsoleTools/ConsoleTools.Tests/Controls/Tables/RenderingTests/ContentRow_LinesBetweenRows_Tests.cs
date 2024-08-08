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
public class ContentRow_LinesBetweenRows_Tests : TestsBase
{
    [Test]
    public void three_rows_without_lines_between_them()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        string expected = GetResourceFileContent("01-no-line-separators.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void three_rows_with_lines_between_them()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        dataGrid.DisplayBorderBetweenRows = true;

        string expected = GetResourceFileContent("02-with-line-separators.txt");
        dataGrid.IsEqualTo(expected);
    }

    private static DataGrid CreateDummyDataGrid()
    {
        DataGrid dataGrid = new();
        dataGrid.Title = "My Title";

        dataGrid.Rows.Add("one", "ichi", "eins");
        dataGrid.Rows.Add("two", "ni", "zwei");
        dataGrid.Rows.Add("three", "san", "drei");

        return dataGrid;
    }
}