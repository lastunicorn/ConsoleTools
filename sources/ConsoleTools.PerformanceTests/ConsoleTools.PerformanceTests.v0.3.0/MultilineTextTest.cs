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
using System.IO;
using System.Text;
using ConsoleTools.PerformanceTests.Common;
using DustInTheWind.ConsoleTools;
using DustInTheWind.ConsoleTools.InputControls;
using DustInTheWind.ConsoleTools.TabularData;

namespace ConsoleTools.PerformanceTests
{
    internal class MultilineTextTest
    {
        private readonly string loremIpsum;
        private readonly TestRunner testRunner;
        private StringBuilder stringBuilder;

        public MultilineTextTest()
        {
            loremIpsum = File.ReadAllText("LoremIpsum.txt");

            testRunner = new TestRunner(ParseTexts);
            testRunner.MeasurementDone += HandleMeasurementDone;
        }

        public void Run()
        {
            stringBuilder = new StringBuilder();

            testRunner.PerformTest();

            DisplayResults();
        }

        private static void HandleMeasurementDone(object sender, MeasurementDoneEventArgs e)
        {
            StringOutput.QuickDisplay("Time:", e.Time.TotalMilliseconds + " ms");
        }

        private void ParseTexts()
        {
            MultilineText multilineText = new MultilineText(loremIpsum);
            stringBuilder.Append(multilineText);
        }

        private void DisplayResults()
        {
            CustomConsole.WriteLine();

            ValueOutput<bool>.QuickDisplay("Are texts the same", loremIpsum != stringBuilder.ToString());

            CustomConsole.WriteLine();

            TimeSpan averageTime = testRunner.CalculateAverage();
            StringOutput.QuickDisplay("Average: ", averageTime.TotalMilliseconds + " ms");
        }
    }
}