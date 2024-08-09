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

using System.Diagnostics;
using Xunit.Abstractions;

namespace DustInTheWind.ConsoleTools.Tests.Performance;

public abstract class PerformanceTestBase
{
    protected ITestOutputHelper TestOutputHelper { get; }

    protected PerformanceTestBase(ITestOutputHelper testOutputHelper)
    {
        TestOutputHelper = testOutputHelper;
    }

    protected (TimeSpan, T) MeasureTime<T>(string title, Func<T> action)
    {
        if (action == null) throw new ArgumentNullException(nameof(action));

        Stopwatch stopwatch = Stopwatch.StartNew();

        T returnValue;

        try
        {
            returnValue = action.Invoke();
        }
        finally
        {
            stopwatch.Stop();
            DisplayMeasuredTime(title, stopwatch.Elapsed);
        }

        return (stopwatch.Elapsed, returnValue);
    }

    protected TimeSpan MeasureTime(string title, Action action)
    {
        if (action == null) throw new ArgumentNullException(nameof(action));

        Stopwatch stopwatch = Stopwatch.StartNew();

        try
        {
            action.Invoke();
        }
        finally
        {
            stopwatch.Stop();
            DisplayMeasuredTime(title, stopwatch.Elapsed);
        }

        return stopwatch.Elapsed;
    }

    protected TimeSpan MeasureTime<T>(string title, Action<T> action, T obj)
    {
        if (action == null) throw new ArgumentNullException(nameof(action));

        Stopwatch stopwatch = Stopwatch.StartNew();

        try
        {
            action.Invoke(obj);
        }
        finally
        {
            stopwatch.Stop();
            DisplayMeasuredTime(title, stopwatch.Elapsed);
        }

        return stopwatch.Elapsed;
    }

    private void DisplayMeasuredTime(string title, TimeSpan time)
    {
        string message = $"{title}: {time} ticks; {time.Ticks:D}";

        TestOutputHelper.WriteLine(message);
        Console.WriteLine(message);
    }
}