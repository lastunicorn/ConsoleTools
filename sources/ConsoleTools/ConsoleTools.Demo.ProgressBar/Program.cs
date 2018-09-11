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
using System.Threading.Tasks;

namespace DustInTheWind.ConsoleTools.Demo.ProgressBar
{
    internal class Program
    {
        private static void Main()
        {
            DisplayApplicationHeader();

            RunDemo();

            Pause.QuickDisplay();
        }

        private static void DisplayApplicationHeader()
        {
            CustomConsole.WriteLineEmphasies("ConsoleTools Demo - ProgressBar");
            CustomConsole.WriteLine("===============================================================================");
            CustomConsole.WriteLine();
            CustomConsole.WriteLine("This demo shows the usage of the ProgressBar control.");
            CustomConsole.WriteLine("-------------------------------------------------------------------------------");
            CustomConsole.WriteLine();
        }

        private static void RunDemo()
        {
            ManualResetEventSlim finishEvent = new ManualResetEventSlim();
            finishEvent.Reset();

            Spinners.ProgressBar progressBar = new Spinners.ProgressBar();

            Task.Run<Task>(async () =>
            {
                try
                {
                    progressBar.Display();

                    for (int i = 0; i <= 100; i++)
                    {
                        await Task.Delay(50);
                        progressBar.Value = i;
                    }
                }
                catch (Exception ex)
                {
                    CustomConsole.WriteLine();
                    CustomConsole.WriteError(ex);
                }
                finally
                {
                    finishEvent.Set();
                }
            });

            finishEvent.Wait();

            progressBar.Close();
        }
    }
}