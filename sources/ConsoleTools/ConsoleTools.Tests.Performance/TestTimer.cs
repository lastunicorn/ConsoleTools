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

namespace DustInTheWind.ConsoleTools.Tests.Performance;

internal static class TestTimer
{
    public static (TimeSpan, T) MeasureTime<T>(string title, Func<T> action)
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

    public static TimeSpan MeasureTime(string title, Action action)
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

    public static TimeSpan MeasureTime<T>(string title, Action<T> action, T obj)
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

    private static void DisplayMeasuredTime(string title, TimeSpan time)
    {
        Console.WriteLine($"{title}: {time} ticks; {time.Ticks:N}");
    }
}