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
using System.Threading;
using System.Threading.Tasks;
using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Controls.Spinners;
using DustInTheWind.ConsoleTools.Demo.Utils;

namespace DustInTheWind.ConsoleTools.Demo.ProgressBarDemo.Demo;

internal class ColorsDemo : DemoBase
{
    private readonly ProgressBar progressBar;

    public override string Title => "Colors";

    public override MultilineText Description => new[]
    {
        "Custom characters for filling the bar: '-' and '+'",
        "Custom label: 'Custom Process'"
    };

    public ColorsDemo()
    {
        progressBar = new ProgressBar()
        {
            LabelText = "Colors",

            BarEmptyChar = '-',
            BarFillChar = '+',

            BarEmptyBackgroundColor = ConsoleColor.Blue,
            BarFillBackgroundColor = ConsoleColor.DarkBlue,

            BarEmptyForegroundColor = ConsoleColor.Green,
            BarFillForegroundColor = ConsoleColor.DarkGreen
        };
    }

    protected override void DoExecute()
    {
        RunSomeAsynchronousTask().Wait();
    }

    private Task RunSomeAsynchronousTask()
    {
        return Task.Run(() =>
        {
            progressBar.Display();

            progressBar.Value = 0;

            for (int i = 0; i <= 100; i++)
            {
                Thread.Sleep(30);
                progressBar.Value = i;
            }

            progressBar.Close();
        });
    }
}