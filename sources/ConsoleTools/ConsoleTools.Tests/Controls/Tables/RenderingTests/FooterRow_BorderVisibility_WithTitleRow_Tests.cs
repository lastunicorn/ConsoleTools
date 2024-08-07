
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
public class FooterRow_BorderVisibility_WithTitleRow_Tests : TestsBase
{
    private DataGrid dataGrid;

    [SetUp]
    public void SetUp()
    {
        dataGrid = CreateDummyDataGrid();
    }

    [Test]
    public void HavingNoBorderExplicitlyVisible()
    {
        string expected = GetResourceFileContent("00-border-unspecified.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingTopBorderExplicitlyVisible()
    {
        dataGrid.FooterRow.BorderVisibility = ". + . . .";

        string expected = GetResourceFileContent("01-top-border.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingBottomBorderExplicitlyVisible()
    {
        dataGrid.FooterRow.BorderVisibility = ". . . + .";

        string expected = GetResourceFileContent("02-bottom-border.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingTopAndBottomBordersExplicitlyVisible()
    {
        dataGrid.FooterRow.BorderVisibility = ". + . + .";

        string expected = GetResourceFileContent("03-top-and-bottom-border.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingTopBorderExplicitlyHidden()
    {
        dataGrid.FooterRow.BorderVisibility = ". - . . .";

        string expected = GetResourceFileContent("04-top-border-hidden.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingBottomBorderExplicitlyHidden()
    {
        dataGrid.FooterRow.BorderVisibility = ". . . - .";

        string expected = GetResourceFileContent("05-bottom-border-hidden.txt");
        dataGrid.IsEqualTo(expected);
    }

    private static DataGrid CreateDummyDataGrid()
    {
        DataGrid dataGrid = new();

        dataGrid.Title = "Border Visibility Tests";

        dataGrid.FooterRow.FooterCell.Content = "Footer text";

        return dataGrid;
    }
}