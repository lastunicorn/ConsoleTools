// ConsoleTools
// Copyright (C) 2017-2020 Dust in the Wind
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
using DustInTheWind.ConsoleTools.Controls.Menus;

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
            TextBlock title = new TextBlock
            {
                Text = "ConsoleTools Demo - HorizontalLine",
                ForegroundColor = CustomConsole.EmphasizedColor
            };

            HorizontalLine horizontalLine = new HorizontalLine
            {
                Character = '=',
                ForegroundColor = CustomConsole.EmphasizedColor,
                Margin = 0
            };

            TextBlock description = new TextBlock
            {
                Text = "This demo shows the usage of the HorizontalLine controls.",
                Margin = "0 1"
            };

            title.Display();
            horizontalLine.Display();
            description.Display();
        }
    }
}