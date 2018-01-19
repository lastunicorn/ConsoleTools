using System;
using ConsoleTools.PerformanceTests.Common;
using DustInTheWind.ConsoleTools;
using DustInTheWind.ConsoleTools.InputControls;
using DustInTheWind.ConsoleTools.TabularData;

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
            ValueOutput.QuickWrite("Average: ", averageTime.TotalMilliseconds + " ms");

            // Done

            Pause.QuickPause();
        }

        private static void HandleMeasurementDone(object sender, MeasurementDoneEventArgs e)
        {
            ValueOutput.QuickWrite("Time:", e.Time.TotalMilliseconds + " ms");
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
