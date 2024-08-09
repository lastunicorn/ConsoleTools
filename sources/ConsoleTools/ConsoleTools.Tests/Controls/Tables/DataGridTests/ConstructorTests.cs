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

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.DataGridTests;

[TestFixture]
public class ConstructorTests
{
    [Test]
    public void WhenInstantiatingDataGridWithoutParameters_ThenPropertiesAreCorrectlyInitialized()
    {
        DataGrid dataGrid = new();

        Assert.That(dataGrid.Title, Is.EqualTo(MultilineText.Empty));
        Assert.That(dataGrid.CellHorizontalAlignment, Is.EqualTo(HorizontalAlignment.Default));
        Assert.That(dataGrid.CellPaddingLeft, Is.Null);
        Assert.That(dataGrid.CellPaddingRight, Is.Null);
        Assert.That(dataGrid.DisplayBorderBetweenRows, Is.False);
        Assert.That(dataGrid.Columns.Count, Is.EqualTo(0));
        Assert.That(dataGrid.Rows.Count, Is.EqualTo(0));
    }

    [Test]
    public void WhenInstantiatingDataGridWithStringTitle_ThenPropertiesAreCorrectlyInitialized()
    {
        DataGrid dataGrid = new("My Title");

        Assert.That(dataGrid.Title, Is.EqualTo(new MultilineText("My Title")));
        Assert.That(dataGrid.CellHorizontalAlignment, Is.EqualTo(HorizontalAlignment.Default));
        Assert.That(dataGrid.CellPaddingLeft, Is.Null);
        Assert.That(dataGrid.CellPaddingRight, Is.Null);
        Assert.That(dataGrid.DisplayBorderBetweenRows, Is.False);
        Assert.That(dataGrid.Columns.Count, Is.EqualTo(0));
        Assert.That(dataGrid.Rows.Count, Is.EqualTo(0));
    }

    [Test]
    public void WhenInstantiatingDataGridWithMultilineTextTitle_ThenPropertiesAreCorrectlyInitialized()
    {
        DataGrid dataGrid = new(new MultilineText("My Title"));

        Assert.That(dataGrid.Title, Is.EqualTo(new MultilineText("My Title")));
        Assert.That(dataGrid.CellHorizontalAlignment, Is.EqualTo(HorizontalAlignment.Default));
        Assert.That(dataGrid.CellPaddingLeft, Is.Null);
        Assert.That(dataGrid.CellPaddingRight, Is.Null);
        Assert.That(dataGrid.DisplayBorderBetweenRows, Is.False);
        Assert.That(dataGrid.Columns.Count, Is.EqualTo(0));
        Assert.That(dataGrid.Rows.Count, Is.EqualTo(0));
    }
}