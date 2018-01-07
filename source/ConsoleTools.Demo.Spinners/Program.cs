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

namespace DustInTheWind.ConsoleTools.Demo.Spinners
{
    /// <summary>
    /// The <see cref="Spinner"/> example is in the Worker.cs file.
    /// </summary>
    internal class Program
    {
        private static void Main()
        {
            DisplayApplicationHeader();

            Spinner.Run(() =>
            {
                Thread.Sleep(2000);
                throw new Exception("alez");
            });

            WorkerProvider workerProvider = new WorkerProvider();

            foreach (Worker worker in workerProvider.CreateWorkers())
            {
                if (worker == null)
                    break;

                CustomConsole.WriteLine();
                worker.Run();
            }
        }

        private static void DisplayApplicationHeader()
        {
            CustomConsole.WriteLineEmphasies("ConsoleTools Demo - Progress spinner");
            CustomConsole.WriteLine("===============================================================================");
            CustomConsole.WriteLine();
            CustomConsole.WriteLine("Step 1: Select a template for the spinner.");
            CustomConsole.WriteLine("Step 2: The application will simulate an asyn work and display the spinner.");
            CustomConsole.WriteLine("-------------------------------------------------------------------------------");
            CustomConsole.WriteLine();
        }
    }
}