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

using System.Collections.Generic;
using DustInTheWind.ConsoleTools.Controls.Tables;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.RenderingTests.DataGridTests;

[TestFixture]
public class Grid_Title_Tests : TestsBase
{
    [Test]
    public void HavingTitleTextShorterThanTableContent_WhenRendered_ThenTitleRowIsExtendedToTableWidth()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        dataGrid.Title = "My Title";

        string expected = GetResourceFileContent("01-title-shorter.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingTitleTextLongerThanTableContent_WhenRendered_ThenTitleColumnsAreExtendedToTitleRowWidth()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        dataGrid.Title = "My Title My Title My Title My Title";

        string expected = GetResourceFileContent("02-title-longer.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingTitleTextOnMultipleLines_WhenRendered_ThenTitleRowContainsMultipleLines()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        dataGrid.Title = new List<string> { "My Title1", "My Title2", "My Title3", "My Title4" };

        string expected = GetResourceFileContent("03-title-multiline.txt");
        dataGrid.IsEqualTo(expected);
    }

    private static DataGrid CreateDummyDataGrid()
    {
        DataGrid dataGrid = new();
        
        dataGrid.Rows.Add("asd", "qwe", "zxczxc");

        return dataGrid;
    }
}