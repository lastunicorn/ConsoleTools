// ConsoleTools
// Copyright (C) 2017-2018 Dust in the Wind
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
using ConsoleTools.PerformanceTests.Common;
using DustInTheWind.ConsoleTools;
using DustInTheWind.ConsoleTools.InputControls;
using DustInTheWind.ConsoleTools.TabularData;
using DustInTheWind.ConsoleTools.TabularData.Printers;

namespace ConsoleTools.PerformanceTests.Current
{
    internal static class Program
    {
        private static void Main()
        {
            // Perform test

            TestRunner testRunner = new TestRunner(GenerateGrid);
            testRunner.MeasurementDone += HandleMeasurementDone;

            testRunner.PerformTest();

            // Write results

            CustomConsole.WriteLine();

            TimeSpan averageTime = testRunner.CalculateAverage();
            ValueOutput<string>.QuickDisplay("Average: ", averageTime.TotalMilliseconds + " ms");

            // Done

            Pause.QuickPause();
        }

        private static void HandleMeasurementDone(object sender, MeasurementDoneEventArgs e)
        {
            ValueOutput<string>.QuickDisplay("Time:", e.Time.TotalMilliseconds + " ms");
        }

        private static void GenerateGrid()
        {
            DataGrid dataGrid = new DataGrid("Table title");

            dataGrid.Columns.Add(new Column("Column 1"));
            dataGrid.Columns.Add(new Column("Column 2"));
            dataGrid.Columns.Add(new Column("Column 3"));

            for (int i = 0; i < 10000; i++)
            {
                string cell0 = "Cell " + i + ",0";
                string cell1 = "Cell " + i + ",1";
                string cell2 = "Cell " + i + ",2";

                dataGrid.Rows.Add(cell0, cell1, cell2);
            }

            dataGrid.Render(new StringTablePrinter());
        }
    }
}
