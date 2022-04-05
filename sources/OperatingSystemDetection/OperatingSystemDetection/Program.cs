// ConsoleTools
// Copyright (C) 2017-2022 Dust in the Wind
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
using System.Threading.Tasks;
using OperatingSystemDetection.Library;
using OperatingSystem = OperatingSystemDetection.Library.OperatingSystem;

namespace OperatingSystemDetection
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            try
            {
                if (args.Length > 0 && args[0] == "os")
                    DisplayOs();

                if (args.Length > 0 && args[0] == "cursor")
                {
                    await ShowHideCursor();

                    Console.WriteLine();
                    ReadCursorVisibility();
                }
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }

        private static void DisplayOs()
        {
            DisplayTitle("Test: Display OS");

            if (OperatingSystem.IsWindows())
                Console.WriteLine("OS is Windows");

            if (OperatingSystem.IsLinux())
                Console.WriteLine("OS is Linux");

            if (OperatingSystem.IsMacOS())
                Console.WriteLine("OS is MacOS");
        }

        private static async Task ShowHideCursor()
        {
            DisplayTitle("Test: Show/Hide cursor");

            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine();

                Console.Write("Hide Cursor: ");
                Cursor.SetVisibility(false);
                await Task.Delay(3000);
                Console.WriteLine();

                Console.Write("Show Cursor: ");
                Cursor.SetVisibility(true);
                await Task.Delay(3000);
                Console.WriteLine();
            }
        }

        private static void ReadCursorVisibility()
        {
            DisplayTitle("Test: Read cursor's visibility");

            Console.WriteLine();
            Console.WriteLine("Hide Cursor");
            Cursor.SetVisibility(false);
            Console.WriteLine("Cursor is visible: " + Cursor.IsVisible);

            Console.WriteLine();
            Console.WriteLine("Show Cursor");
            Cursor.SetVisibility(true);
            Console.WriteLine("Cursor is visible: " + Cursor.IsVisible);
        }

        private static void DisplayTitle(string text)
        {
            ConsoleColor initialColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(text);
            Console.ForegroundColor = initialColor;
        }

        private static void DisplayError(Exception ex)
        {
            ConsoleColor initialColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex);
            Console.ForegroundColor = initialColor;
        }
    }
}