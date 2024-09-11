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

using System;
using System.Diagnostics;
using System.Threading;
using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Controls.Spinners;

namespace DustInTheWind.ConsoleTools.Demo.SpinnerDemo;

/// <summary>
/// This is a dummy worker that simulates some heavy work and displays a spinner during this time.
/// </summary>
internal class Worker
{
    public TimeSpan WorkTimeSpan { get; set; } = TimeSpan.FromSeconds(7);

    public ISpinnerTemplate SpinnerTemplate { get; set; }

    public int SpinnerStepMilliseconds { get; set; } = 300;

    public void Run()
    {
        using Spinner spinner = new(SpinnerTemplate);

        spinner.MarginBottom = 2;
        spinner.FrameIntervalMilliseconds = SpinnerStepMilliseconds;
        spinner.Label = new InlineText
        {
            Text = "Doing some work",
            MarginRight = 1
        };

        spinner.Display();

        try
        {
            bool isNormalFinish = SimulateWork();

            spinner.DoneText = isNormalFinish
                ? new InlineText("[Done]", CustomConsole.SuccessColor)
                : new InlineText("[Canceled]", CustomConsole.WarningColor);

            spinner.Close();
        }
        catch
        {
            spinner.DoneText = new InlineText("[Error]", CustomConsole.ErrorColor);
            spinner.Close();
        }
    }

    private bool SimulateWork()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        while (true)
        {
            bool isCanceledByUser = IsCanceledByUser();
            if (isCanceledByUser)
                return false;

            Thread.Sleep(50);

            if (stopwatch.Elapsed >= WorkTimeSpan)
                return true;
        }
    }

    private static bool IsCanceledByUser()
    {
        while (Console.KeyAvailable)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
                return true;
        }

        return false;
    }
}