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
using DustInTheWind.ConsoleTools.Spinners;

namespace DustInTheWind.ConsoleTools.Demo.Spinners
{
    internal class Program
    {
        private static void Main()
        {
            CustomConsole.WriteLineEmphasies("Progress spinner demo");
            CustomConsole.WriteLine("===============================================================================");
            CustomConsole.WriteLine();
            CustomConsole.WriteLine("Step 1: Select a template for the spinner.");
            CustomConsole.WriteLine("Step 2: The application will simulate an asyn work and display the spinner.");
            CustomConsole.WriteLine("-------------------------------------------------------------------------------");
            CustomConsole.WriteLine();

            while (true)
            {
                Worker worker = CreateWorker();

                if (worker == null)
                    break;

                CustomConsole.WriteLine();
                worker.Run();
            }
        }

        private static Worker CreateWorker()
        {
            CustomConsole.WriteLine("1  - arrow");
            CustomConsole.WriteLine("2  - stick");
            CustomConsole.WriteLine("3  - bubble");
            CustomConsole.WriteLine("4  - half rotate");
            CustomConsole.WriteLine("5  - fan");
            CustomConsole.WriteLine("6  - fill (dot, empty from start) - default");
            CustomConsole.WriteLine("7  - fill (dot, empty from end)");
            CustomConsole.WriteLine("8  - fill (dot, sudden empty)");
            CustomConsole.WriteLine("9  - fill (dot, with borders)");
            CustomConsole.WriteLine("10 - fill (block, length: 10 chars, step: 100ms)");
            CustomConsole.WriteLine("0 - exit");

            CustomConsole.WriteLine();

            while (true)
            {
                CustomConsole.WriteEmphasies("Select spinner template: ");
                string rawValue = Console.ReadLine();

                switch (rawValue)
                {
                    case "0":
                        return null;

                    case "1":
                        return new Worker
                        {
                            SpinnerTemplate = new ArrowTemplate(),
                            SpinnerStepMilliseconds = 400,
                            WorkInterval = TimeSpan.FromSeconds(5)
                        };
                    case "2":
                        return new Worker
                        {
                            SpinnerTemplate = new StickTemplate(),
                            SpinnerStepMilliseconds = 400,
                            WorkInterval = TimeSpan.FromSeconds(5)
                        };
                    case "3":
                        return new Worker
                        {
                            SpinnerTemplate = new BubbleTemplate(),
                            SpinnerStepMilliseconds = 400,
                            WorkInterval = TimeSpan.FromSeconds(5)
                        };
                    case "4":
                        return new Worker
                        {
                            SpinnerTemplate = new HalfRotateTemplate(),
                            SpinnerStepMilliseconds = 400,
                            WorkInterval = TimeSpan.FromSeconds(5)
                        };
                    case "5":
                        return new Worker
                        {
                            SpinnerTemplate = new FanTemplate(),
                            SpinnerStepMilliseconds = 400,
                            WorkInterval = TimeSpan.FromSeconds(5)
                        };
                    case "6":
                        return new Worker
                        {
                            SpinnerTemplate = new FillTemplate(),
                            SpinnerStepMilliseconds = 400,
                            WorkInterval = TimeSpan.FromSeconds(5)
                        };
                    case "7":
                        return new Worker
                        {
                            SpinnerTemplate = new FillTemplate { FilledBehavior = FilledBehavior.EmptyFromEnd },
                            SpinnerStepMilliseconds = 400,
                            WorkInterval = TimeSpan.FromSeconds(5)
                        };
                    case "8":
                        return new Worker
                        {
                            SpinnerTemplate = new FillTemplate { FilledBehavior = FilledBehavior.SuddenEmpty },
                            SpinnerStepMilliseconds = 400,
                            WorkInterval = TimeSpan.FromSeconds(5)
                        };
                    case "9":
                        return new Worker
                        {
                            SpinnerTemplate = new FillTemplate { ShowBorders = true },
                            SpinnerStepMilliseconds = 400,
                            WorkInterval = TimeSpan.FromSeconds(5)
                        };
                    case "10":
                        return new Worker
                        {
                            SpinnerTemplate = new FillTemplate('▓', 10),
                            SpinnerStepMilliseconds = 100,
                            WorkInterval = TimeSpan.FromSeconds(10)
                        };
                }
            }
        }
    }
}
