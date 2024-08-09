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
using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.ContentCellTests;

[TestFixture]
public class ColumnSpanTests
{
    [Test]
    public void HavingDataGridWithMultipleContentCells_ThenColumnSpanIsEqualToOneForAllContentCells()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        IEnumerable<ContentCell> allContentCells = dataGrid.Rows
            .SelectMany(x => x)
            .Cast<ContentCell>();

        foreach (ContentCell contentCell in allContentCells)
            contentCell.ColumnSpan.Should().Be(1);
    }

    [Test]
    public void HavingContentCell_WhenSettingColumnSpanTo0_ThenThrows()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        Action action = () =>
        {
            dataGrid.Rows[2][1].ColumnSpan = 0;
        };

        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    [TestCase(-1)]
    [TestCase(-2)]
    [TestCase(-10)]
    public void HavingContentCell_WhenSettingColumnSpanToNegativeValue_ThenThrows(int columnSpan)
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        Action action = () =>
        {
            dataGrid.Rows[2][1].ColumnSpan = columnSpan;
        };

        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(10)]
    public void HavingContentCell_WhenSettingColumnSpanToPositiveValue_ThenColumnSpanHasThatValue(int columnSpan)
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        dataGrid.Rows[2][1].ColumnSpan = columnSpan;

        dataGrid.Rows[2][1].ColumnSpan.Should().Be(columnSpan);
    }

    private static DataGrid CreateDummyDataGrid()
    {
        DataGrid dataGrid = new();
        dataGrid.Title = "Cell Padding Tests";

        dataGrid.Columns.Add("Column 0");
        dataGrid.Columns.Add("Column 1");
        dataGrid.Columns.Add("Column 2");

        for (int i = 0; i < 5; i++)
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