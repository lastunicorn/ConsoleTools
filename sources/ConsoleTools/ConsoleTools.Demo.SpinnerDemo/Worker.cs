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
using System.Threading;
using DustInTheWind.ConsoleTools.Spinners;

namespace DustInTheWind.ConsoleTools.Demo.SpinnerDemo
{
    internal class Worker
    {
        public TimeSpan WorkPeriod { get; set; }
        public ISpinnerTemplate SpinnerTemplate { get; set; }
        public int SpinnerStepMilliseconds { get; set; }

        public void Run()
        {
            using (Spinner spinner = new Spinner(SpinnerTemplate))
            {
                spinner.MarginTop = 2;
                spinner.MarginBottom = 2;
                spinner.FrameIntervalMilliseconds = SpinnerStepMilliseconds;
                spinner.Label = new InlineTextBlock
                {
                    Text = "Doing some work",
                    MarginRight = 1
                };

                spinner.Display();

                try
                {
                    // Simulate work
                    Thread.Sleep(WorkPeriod);

                    spinner.DoneText = new InlineTextBlock("[Done]", CustomConsole.SuccessColor);
                    spinner.Close();
                }
                catch
                {
                    spinner.DoneText = new InlineTextBlock("[Error]", CustomConsole.ErrorColor);
                    spinner.Close();
                }
            }
        }
    }
}