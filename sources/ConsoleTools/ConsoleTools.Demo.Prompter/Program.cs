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

namespace DustInTheWind.ConsoleTools.Demo.PrompterDemo
{
    internal class Program
    {
        private static ControlRepeater prompterRepeater;

        private static void Main()
        {
            DisplayApplicationHeader();
            StartDemo();
            DisplayGoodby();
        }

        private static void DisplayApplicationHeader()
        {
            CustomConsole.WriteLineEmphasies("ConsoleTools Demo - Prompter");
            CustomConsole.WriteLineEmphasies("===============================================================================");
            CustomConsole.WriteLine();

            CustomConsole.WriteEmphasies("Note: ");
            CustomConsole.WriteLine("type 'help' for a list of commands.");
            CustomConsole.WriteLine();
        }

        private static void StartDemo()
        {
            prompterRepeater = new ControlRepeater
            {
                Control = new OceanPrompter()
            };

            prompterRepeater.Display();
        }

        private static void DisplayGoodby()
        {
            TextBlock goodbyText = new TextBlock
            {
                Text = "Bye!",
                ForegroundColor = CustomConsole.EmphasiesColor,
                MarginTop = 1
            };
            goodbyText.Display();
        }

        public static void RequestClose()
        {
            prompterRepeater.RequestClose();
        }
    }
}