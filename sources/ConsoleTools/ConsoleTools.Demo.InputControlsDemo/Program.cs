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
using DustInTheWind.ConsoleTools.Controls;

namespace DustInTheWind.ConsoleTools.Demo.InputControlsDemo
{
    internal static class Program
    {
        private static ControlRepeater menuRepeater;

        private static void Main()
        {
            Console.SetWindowSize(80, 60);
            Console.SetBufferSize(80, 1024);

            DisplayApplicationHeader();

            menuRepeater = new ControlRepeater
            {
                Control = new MainMenu()
            };

            menuRepeater.Display();
        }

        private static void DisplayApplicationHeader()
        {
            //StackPanel stackPanel = new StackPanel
            //{
            //    Controls = new object[]
            //    {
            //        new InlineTextBlock
            //        {
            //            Text = "ConsoleTools Demo - InputControls",
            //            ForegroundColor = CustomConsole.EmphasizedColor
            //        },
            //        new TextBlock
            //        {
            //            Text = CustomConsole.HorizontalWindowLine,
            //            ForegroundColor = CustomConsole.EmphasizedColor,
            //            MarginBottom = 1
            //        },
            //        new TextBlock
            //        {
            //            Text = "This demo shows the usage of the input controls (text and list).",
            //            MarginBottom = 1
            //        }
            //    }
            //};
            //
            //stackPanel.Display();

            CustomConsole.WriteLineEmphasies("ConsoleTools Demo - InputControls");
            CustomConsole.WriteLineEmphasies("===============================================================================");
            CustomConsole.WriteLine();
            CustomConsole.WriteLine("This demo shows the usage of the input controls (text and list).");
            CustomConsole.WriteLine();
        }

        public static void Stop()
        {
            menuRepeater.RequestClose();
        }
    }
}