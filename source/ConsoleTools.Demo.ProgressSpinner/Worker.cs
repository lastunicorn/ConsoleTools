// ConsoleTools
// Copyright (C) 2017 Dust in the Wind
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

namespace DustInTheWind.ConsoleTools.Demo.Spinners
{
    internal class Worker
    {
        public TimeSpan WorkInterval { get; set; }
        public ISpinnerTemplate SpinnerTemplate { get; set; }
        public int SpinnerStepMilliseconds { get; set; }

        public void Run()
        {
            using (ProgressSpinner progressSpinner = new ProgressSpinner(SpinnerTemplate))
            {
                progressSpinner.StepMiliseconds = SpinnerStepMilliseconds;
                progressSpinner.Text = "Doing some work";

                progressSpinner.Start();

                try
                {
                    // Simulate work
                    Thread.Sleep(WorkInterval);
                }
                finally
                {
                    progressSpinner.Stop();
                    CustomConsole.WriteLineSuccess("[Done]");
                    CustomConsole.WriteLine();
                    CustomConsole.WriteLine();
                }
            }
        }
    }
}