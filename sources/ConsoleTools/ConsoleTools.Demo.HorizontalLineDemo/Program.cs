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
using DustInTheWind.ConsoleTools.Demo.HorizontalLineDemo.Commands;
using DustInTheWind.ConsoleTools.Menues;

namespace DustInTheWind.ConsoleTools.Demo.HorizontalLineDemo
{
    internal static class Program
    {
        private static void Main()
        {
            Console.SetWindowSize(80, 50);
            Console.SetBufferSize(160, 512);

            DisplayApplicationHeader();

            ICommand[] commands = {
                new DefaultCommand(),
                new CustomCharCommand(),
                new CustomMarginCommand(),
                new CustomPaddingCommand(),
                new CustomForegroundColorCommand(),
                new CustomBackgroundColorCommand(),
                new CustomWidthCommand(),
                new AlignedCenterCommand(),
                new AlignedRightCommand()
            };

            foreach (ICommand command in commands)
                command.Execute();

            Pause.QuickDisplay();
        }

        private static void DisplayApplicationHeader()
        {
            CustomConsole.WriteLineEmphasies("ConsoleTools Demo - HorizontalLine");
            CustomConsole.WriteLineEmphasies("===============================================================================");
            CustomConsole.WriteLine();
            CustomConsole.WriteLine("This demo shows the usage of the HorizontalLine controls.");
            CustomConsole.WriteLine();
        }
    }
}
