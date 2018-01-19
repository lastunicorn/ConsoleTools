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

namespace ConsoleTools.PerformanceTests
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
            TextOutput.QuickWrite("Average: ", averageTime.TotalMilliseconds + " ms");

            // Done

            Pause.QuickPause();
        }

        private static void HandleMeasurementDone(object sender, MeasurementDoneEventArgs e)
        {
            TextOutput.QuickWrite("Time:", e.Time.TotalMilliseconds + " ms");
        }

        private static void GenerateGrid()
        {
            Table table = new Table("Table title");

            table.Columns.Add(new Column("Column 1"));
            table.Columns.Add(new Column("Column 2"));
            table.Columns.Add(new Column("Column 3"));

            for (int i = 0; i < 10000; i++)
            {
                string cell0 = "Cell " + i + ",0";
                string cell1 = "Cell " + i + ",1";
                string cell2 = "Cell " + i + ",2";

                table.AddRow(new[] { cell0, cell1, cell2 });
            }

            table.Render(new StringTablePrinter());
        }
    }
}
