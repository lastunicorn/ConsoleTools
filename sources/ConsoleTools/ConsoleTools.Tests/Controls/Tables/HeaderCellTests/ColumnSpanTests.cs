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

using System;
using DustInTheWind.ConsoleTools.Controls.Tables;
using FluentAssertions;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.HeaderCellTests;

[TestFixture]
public class ColumnSpanTests
{
    [Test]
    public void HavingDataGridWithHeaderRow_ThenColumnSpanIsEqualToOneForAllHeaderCells()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        foreach (CellBase cellBase in dataGrid.HeaderRow)
            cellBase.ColumnSpan.Should().Be(1);
    }

    [Test]
    [TestCase(-10)]
    [TestCase(-2)]
    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(10)]
    public void HavingContentCell_WhenSettingColumnSpanToAnyNegativePositiveOrZeroValue_ThenThrows(int columnSpan)
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        Action action = () =>
        {
            dataGrid.HeaderRow[1].ColumnSpan = columnSpan;
        };

        action.Should().Throw<InvalidOperationException>();
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