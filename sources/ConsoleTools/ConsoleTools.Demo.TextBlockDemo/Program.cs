﻿// ConsoleTools
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
using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Controls.Menus;
using DustInTheWind.ConsoleTools.Demo.TextBlockDemo.Commands;

namespace DustInTheWind.ConsoleTools.Demo.TextBlockDemo
{
    internal class Program
    {
        private static void Main()
        {
            Console.SetWindowSize(80, 50);
            Console.SetBufferSize(160, 512);

            DisplayApplicationHeader();
            RunDemos();

            Pause.QuickDisplay();
        }

        private static void DisplayApplicationHeader()
        {
            ApplicationHeader applicationHeader = new ApplicationHeader
            {
                Title = "ConsoleTools Demo - TextBlock"
            };
            applicationHeader.Display();
        }

        private static void RunDemos()
        {
            ICommand[] commands =
            {
                new SingleShortLineCommand(),
                new SingleLongLineCommand(),
                new MultipleShortLinesCommand(),
                new MultipleLongLinesCommand(),
                new MarginsCommand(),
                new PaddingsCommand(),
                new ForegroundColorCommand(),
                new BackgroundColorCommand(),
                new MinWidthCommand(),
                new MaxWidthCommand(),
                new WidthCommand(),
                new HorizontalAlignmentCenterCommand(),
                new HorizontalAlignmentRightCommand()
            };

            foreach (ICommand command in commands)
                command.Execute();
        }
    }
}