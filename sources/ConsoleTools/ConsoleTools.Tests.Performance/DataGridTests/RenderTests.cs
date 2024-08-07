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
using Xunit.Abstractions;

namespace DustInTheWind.ConsoleTools.Tests.Performance.DataGridTests;

public class RenderTests : PerformanceTestBase
{
    public RenderTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper)
    {
    }

    //[Fact]
    //public void Render_Lines_10_000_Columns_10()
    //{
    //    DataGrid dataGrid = null;

    //    MeasureTime("Create DataGrid", () => dataGrid = CreateDataGrid(10_000, 10));
    //    MeasureTime("Render DataGrid to string", RenderDataGrid, dataGrid);
    //}

    //[Fact]
    //public void Render_Lines_100_000_Columns_10()
    //{
    //    DataGrid dataGrid = null;

    //    MeasureTime("Create DataGrid", () => dataGrid = CreateDataGrid(100_000, 10));
    //    MeasureTime("Render DataGrid to string", RenderDataGrid, dataGrid);
    //}

    //[Fact]
    //public void Render_Lines_200_000_Columns_10()
    //{
    //    DataGrid dataGrid = null;

    //    MeasureTime("Create DataGrid", () => dataGrid = CreateDataGrid(200_000, 10));
    //    MeasureTime("Render DataGrid to string", RenderDataGrid, dataGrid);
    //}

    [Fact]
    public void Render_Lines_100_Columns_10()
    {
        DataGrid dataGrid = null;

        MeasureTime("Create DataGrid", () => dataGrid = CreateDataGrid(100, 10));
        MeasureTime("Render DataGrid to string", RenderDataGrid, dataGrid);
    }

    [Fact]
    public void Render_Lines_100_Columns_100()
    {
        DataGrid dataGrid = null;

        MeasureTime("Create DataGrid", () => dataGrid = CreateDataGrid(100, 100));
        MeasureTime("Render DataGrid to string", RenderDataGrid, dataGrid);
    }

    [Fact]
    public void Render_Lines_100_Columns_1000()
    {
        DataGrid dataGrid = null;

        MeasureTime("Create DataGrid", () => dataGrid = CreateDataGrid(100, 1000));
        MeasureTime("Render DataGrid to string", RenderDataGrid, dataGrid);
    }

    [Fact]
    public void Render_Lines_100_Columns_10000()
    {
        DataGrid dataGrid = null;

        MeasureTime("Create DataGrid", () => dataGrid = CreateDataGrid(100, 10000));
        MeasureTime("Render DataGrid to string", RenderDataGrid, dataGrid);
    }

    private static DataGrid CreateDataGrid(int lineCount, int columnCount)
    {
        DataGrid dataGrid = new();

        for (int i = 0; i < lineCount; i++)
        {
            ContentRow row = new();

            for (int j = 0; j < columnCount; j++)
            {
                ContentCell cell = new($"{i} : {j}");
                row.AddCell(cell);
            }

            dataGrid.Rows.Add(row);
        }

        return dataGrid;
    }

    private static void RenderDataGrid(DataGrid dataGrid)
    {
        string s = dataGrid.ToString();
    }
}