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

namespace DustInTheWind.ConsoleTools.Demo.HorizontalLineDemo
{
    internal static class Program
    {
        private static void Main()
        {
            DisplayApplicationHeader();

            Default();
            CustomChar();
            CustomMargin();
            CustomForegroundColor();
            CustomBackgroundColor();
            CustomWidth();

            Pause.QuickDisplay();
        }

        private static void Default()
        {
            CustomConsole.WriteLine();
            CustomConsole.WriteLine("- Default:");
            CustomConsole.WriteLine();

            HorizontalLine horizontalLine = new HorizontalLine();
            horizontalLine.Display();
        }

        private static void CustomChar()
        {
            CustomConsole.WriteLine();
            CustomConsole.WriteLine("- Custom Character (*):");
            CustomConsole.WriteLine();

            HorizontalLine horizontalLine = new HorizontalLine
            {
                Character = '*'
            };
            horizontalLine.Display();
        }

        private static void CustomMargin()
        {
            CustomConsole.WriteLine();
            CustomConsole.WriteLine("- Custom Margins (3 3):");
            CustomConsole.WriteLine();

            HorizontalLine horizontalLine = new HorizontalLine
            {
                MarginTop = 3,
                MarginBottom = 3
            };
            horizontalLine.Display();
        }

        private static void CustomForegroundColor()
        {
            CustomConsole.WriteLine();
            CustomConsole.WriteLine("- Custom ForegroundColor (Magenta):");
            CustomConsole.WriteLine();

            HorizontalLine horizontalLine = new HorizontalLine
            {
                ForegroundColor = ConsoleColor.Magenta
            };
            horizontalLine.Display();
        }

        private static void CustomBackgroundColor()
        {
            CustomConsole.WriteLine();
            CustomConsole.WriteLine("- Custom BackgroundColor (Magenta):");
            CustomConsole.WriteLine();

            HorizontalLine horizontalLine = new HorizontalLine
            {
                BackgroundColor = ConsoleColor.Magenta
            };
            horizontalLine.Display();
        }

        private static void CustomWidth()
        {
            CustomConsole.WriteLine();
            CustomConsole.WriteLine("- Custom Width (50):");
            CustomConsole.WriteLine();

            HorizontalLine horizontalLine = new HorizontalLine
            {
                Width = 2048
            };
            horizontalLine.Display();
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
