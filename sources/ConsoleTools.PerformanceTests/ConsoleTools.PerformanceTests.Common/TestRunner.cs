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
            if (actionToTest == null) throw new ArgumentNullException(nameof(actionToTest));
            this.actionToTest = actionToTest;
        }

        public void PerformTest()
        {
            times = new List<TimeSpan>();

            for (int i = 0; i < 10; i++)
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