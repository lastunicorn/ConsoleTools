﻿// ConsoleTools
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
public class Grid_CellHorizontalAlignment_Tests : TestsBase
{
    [Test]
    public void HavingAlignmentRightOnTableAndNoColumnsDeclared_WhenRendered_ThenAllCellContentIsAlignedToRight()
    {
        DataGrid dataGrid = CreateDummyDataGrid();
        dataGrid.HeaderRow.IsVisible = false;
        dataGrid.CellHorizontalAlignment = HorizontalAlignment.Right;

        string expected = GetResourceFileContent("01-no-columns.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void whole_table_is_aligned_to_Right_with_explicit_declared_columns()
    {
        DataGrid dataGrid = CreateDummyDataGrid();
        dataGrid.CellHorizontalAlignment = HorizontalAlignment.Right;
        dataGrid.HeaderRow.IsVisible = false;

        dataGrid.Columns.Add("Col 0");
        dataGrid.Columns.Add("Col 1");
        dataGrid.Columns.Add("Col 2");

        string expected = GetResourceFileContent("02-with-columns.txt");
        dataGrid.IsEqualTo(expected);
    }

    private static DataGrid CreateDummyDataGrid()
    {
        DataGrid dataGrid = new();
        dataGrid.Title = "This is a cell alignment test";

        dataGrid.Rows.Add("0,0", "0,1", "0,2");
        dataGrid.Rows.Add("1,0", "1,1", "1,2");
        dataGrid.Rows.Add("2,0", "2,1", "2,2");

        return dataGrid;
    }
}