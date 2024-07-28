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

using System.Linq;
using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Controls.Tables;
using FluentAssertions;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.DataGridTests.ColumnTests;

[TestFixture]
public class AddColumnByHeaderTests
{
    [Test]
    public void HavingDataGridInstance_WhenAddNewColumnToGridByPassingHeaderText_ThenGridContainsOneColumn()
    {
        DataGrid dataGrid = new();

        dataGrid.Columns.Add("Column 1");

        dataGrid.Columns.Should().HaveCount(1);
    }

    [Test]
    public void HavingDataGridInstance_WhenAddNewColumnToGridByPassingHeaderText_ThenGridColumnHasThatHeaderText()
    {
        DataGrid dataGrid = new();

        dataGrid.Columns.Add("Column 1");

        dataGrid.Columns[0].HeaderCell.Content.ToList().Should().ContainInOrder("Column 1");
    }

    [Test]
    public void HavingDataGridInstance_WhenAddNewColumnToGridByPassingHeaderText_ThenGridColumnHasCorrectParentGrid()
    {
        DataGrid dataGrid = new();

        dataGrid.Columns.Add("Column 1");

        dataGrid.Columns[0].ParentDataGrid.Should().BeSameAs(dataGrid);
    }

    [Test]
    public void HavingDataGridInstance_WhenAddNewColumnToGridByPassingHeaderText_ThenGridColumnHasDefaultAlignment()
    {
        DataGrid dataGrid = new();

        dataGrid.Columns.Add("Column 1");

        dataGrid.Columns[0].CellHorizontalAlignment.Should().Be(HorizontalAlignment.Default);
    }

    [Test]
    public void HavingDataGridInstance_WhenAddNewColumnToGridByPassingHeaderTextAndCenterAlignment_ThenGridColumnHasCenterAlignment()
    {
        DataGrid dataGrid = new();

        dataGrid.Columns.Add("Column 1", HorizontalAlignment.Center);

        dataGrid.Columns[0].CellHorizontalAlignment.Should().Be(HorizontalAlignment.Center);
    }
}