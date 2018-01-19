using System;

namespace ConsoleTools.PerformanceTests.Common
{
    public class MeasurementDoneEventArgs : EventArgs
    {
        public TimeSpan Time { get; }

        public MeasurementDoneEventArgs(TimeSpan time)
        {
            Time = time;
        }
    }
}