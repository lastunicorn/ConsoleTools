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

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.RenderingTests;

[TestFixture]
public class Grid_CellPadding_Tests : TestsBase
{
    private DataGrid dataGrid;

    [SetUp]
    public void SetUp()
    {
        dataGrid = new DataGrid("My Title");

        dataGrid.Rows.Add("1234567", "123456", "one two");
        dataGrid.Rows.Add("1", "asd", "asas");
        dataGrid.Rows.Add("12", "a", "errr");
    }

    [Test]
    public void added_a_padding_left_of_2()
    {
        dataGrid.CellPaddingLeft = 2;

        dataGrid.CellPaddingLeft.Should().Be(2);
        dataGrid.CellPaddingRight.Should().BeNull();

        string expected = GetResourceFileContent("01-padding-left.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void added_a_padding_right_of_2()
    {
        dataGrid.CellPaddingRight = 2;

        dataGrid.CellPaddingLeft.Should().BeNull();
        dataGrid.CellPaddingRight.Should().Be(2);

        string expected = GetResourceFileContent("02-padding-right.txt");
        dataGrid.IsEqualTo(expected);
    }
}