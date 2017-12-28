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
    internal class Program
    {
        private static bool exitWasRequested;
        private static readonly TimeSpan AsyncWorkTime = TimeSpan.FromSeconds(8);

        private static void Main()
        {
            CustomConsole.WriteLineEmphasies("Progress spinner demo");
            CustomConsole.WriteLine("===============================================================================");
            CustomConsole.WriteLine();
            CustomConsole.WriteLine("Step 1: Select a template for the spinner.");
            CustomConsole.WriteLine("Step 2: The application will simulate an async work for " + AsyncWorkTime);
            CustomConsole.WriteLine("-------------------------------------------------------------------------------");
            CustomConsole.WriteLine();

            while (!exitWasRequested)
            {
                ITemplate template = CreateSpinnerTemplate();

                if (template == null)
                    break;

                CustomConsole.WriteLine();
                using (ProgressSpinner progressSpinner = new ProgressSpinner(template))
                {
                    CustomConsole.Write("Doing some work: ");
                    progressSpinner.Start();

                    try
                    {
                        // Symulate work
                        Thread.Sleep(AsyncWorkTime);
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

        private static ITemplate CreateSpinnerTemplate()
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
            CustomConsole.WriteLine("10 - fill (block, length 7)");
            CustomConsole.WriteLine("0 - exit");

            CustomConsole.WriteLine();

            while (true)
            {
                CustomConsole.WriteEmphasies("Select spinner template: ");
                string rawValue = Console.ReadLine();

                switch (rawValue)
                {
                    case "0":
                        exitWasRequested = true;
                        return null;

                    case "1": return new ArrowTemplate();
                    case "2": return new StickTemplate();
                    case "3": return new BubbleTemplate();
                    case "4": return new HalfRotateTemplate();
                    case "5": return new FanTemplate();
                    case "6": return new FillTemplate();
                    case "7": return new FillTemplate { FilledBehavior = FilledBehavior.EmptyFromEnd };
                    case "8": return new FillTemplate { FilledBehavior = FilledBehavior.SuddenEmpty };
                    case "9": return new FillTemplate { ShowBorders = true };
                    case "10": return new FillTemplate('▓', 7);
                }
            }
        }
    }
}
