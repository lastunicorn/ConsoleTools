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
