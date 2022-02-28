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
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ConsoleTools.PerformanceTests.Common
{
    public sealed class TestRunner
    {
        private readonly Action actionToTest;
        private List<TimeSpan> times;

        public event EventHandler<MeasurementDoneEventArgs> MeasurementDone;

        public TestRunner(Action actionToTest)
        {
            this.actionToTest = actionToTest ?? throw new ArgumentNullException(nameof(actionToTest));
        }

        public void PerformTest()
        {
            times = new List<TimeSpan>();

            for (int i = 0; i < 100; i++)
            {
                TimeSpan time = Measure();
                times.Add(time);

                OnMeasurementDone(new MeasurementDoneEventArgs(time));
            }
        }

        private TimeSpan Measure()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            actionToTest();
            return stopwatch.Elapsed;
        }

        public TimeSpan CalculateAverage()
        {
            long averageTicks = times.Sum(x => x.Ticks) / times.Count;
            return new TimeSpan(averageTicks);
        }

        private void OnMeasurementDone(MeasurementDoneEventArgs e)
        {
            MeasurementDone?.Invoke(this, e);
        }
    }
}