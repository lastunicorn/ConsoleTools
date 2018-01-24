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

namespace DustInTheWind.ConsoleTools.Demo.Pause
{
    internal class Program
    {
        private static void Main()
        {
            DisplayApplicationHeader();

            WriteDummyText();
            DisplayDefaultPause();

            WriteDummyText();
            DisplayPPause();

            WriteDummyText();
            DisplayErasablePause();

            WriteDummyText();

            ConsoleTools.Pause.QuickDisplay("The demo is ended. Press any key to exit...");
        }

        private static void DisplayApplicationHeader()
        {
            CustomConsole.WriteLineEmphasies("ConsoleTools Demo - Pause");
            CustomConsole.WriteLineEmphasies("===============================================================================");
            CustomConsole.WriteLine();
        }

        private static void WriteDummyText()
        {
            CustomConsole.WriteLine("some text");
            Thread.Sleep(500);
            CustomConsole.WriteLine("some text");
            Thread.Sleep(500);
            CustomConsole.WriteLine("some text");
            Thread.Sleep(500);
        }

        private static void DisplayDefaultPause()
        {
            CustomConsole.WriteLine("This is the default pause:");

            ConsoleTools.Pause.QuickDisplay();
        }

        private static void DisplayPPause()
        {
            CustomConsole.WriteLine("This is the pause with custom Text and custom UnlockKey:");

            ConsoleTools.Pause pause = new ConsoleTools.Pause
            {
                Text = "Press P key to continue...",
                UnlockKey = ConsoleKey.P
            };
            pause.Display();
        }

        private static void DisplayErasablePause()
        {
            CustomConsole.WriteLine("This pause will erase itself at the end:");

            ConsoleTools.Pause pause = new ConsoleTools.Pause
            {
                EraseAfterClose = true
            };
            pause.Display();
        }
    }
}