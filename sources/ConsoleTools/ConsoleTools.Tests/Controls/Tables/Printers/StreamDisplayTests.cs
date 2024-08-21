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
using System.IO;
using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Controls.Displays;
using DustInTheWind.ConsoleTools.Controls.Tables;
using FluentAssertions;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.Printers;

[TestFixture]
public class StreamDisplayTests : TestsBase
{
    [Test]
    public void HavingDataGrid_WhenRenderedIntoStreamTablePrinter_ThenAllTableIsRendered()
    {
        DataGrid dataGrid = CreateDummyDataGrid();

        StreamDisplay streamDisplay = CreateDisplayFor(dataGrid);
        dataGrid.Render(streamDisplay);

        streamDisplay.Stream.Position = 0;

        StreamReader streamReader = new(streamDisplay.Stream);
        string renderedContent = streamReader.ReadToEnd();

        Console.WriteLine(renderedContent);

        string expectedContent = GetResourceFileContent("01-full-table.txt");
        renderedContent.Should().Be(expectedContent);
    }

    private static StreamDisplay CreateDisplayFor(DataGrid dataGrid)
    {
        MemoryStream memoryStream = new();

        ControlLayout controlLayout = new()
        {
            Control = dataGrid
        };

        StreamDisplay streamDisplay = new(memoryStream);
        return streamDisplay;
    }

    private static DataGrid CreateDummyDataGrid()
    {
        DataGrid dataGrid = new();

        dataGrid.Columns.Add("Column 0");
        dataGrid.Columns.Add("Column 1");
        dataGrid.Columns.Add("Column 2");
        dataGrid.Columns.Add("Column 3");

        for (int i = 0; i < 20; i++)
        {
            string cell0 = $"cell {i:00}:0";
            string cell1 = $"cell {i:00}:1";
            string cell2 = $"cell {i:00}:2";
            string cell3 = $"cell {i:00}:3";

            dataGrid.Rows.Add(cell0, cell1, cell2, cell3);
        }

        return dataGrid;
    }
}