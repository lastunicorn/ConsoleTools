﻿// ConsoleTools
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

using DustInTheWind.ConsoleTools.Demo.TabularData.Commands;
using DustInTheWind.ConsoleTools.MenuControl;

namespace DustInTheWind.ConsoleTools.Demo.TabularData
{
    internal class Program
    {
        private static void Main()
        {
            DisplayApplicationHeader();

            TextMenu menu = CreateMenu();

            while (true)
            {
                CustomConsole.WriteLine("-------------------------------------------------------------------------------");
                CustomConsole.WriteLine();

                menu.Display();

                if (menu.SelectedItem?.Id == "0")
                    break;
            }
        }

        private static TextMenu CreateMenu()
        {
            TextMenuItem[] menuItems = {
                new TextMenuItem
                {
                    Id = "1",
                    Text = "Long/short titles",
                    Command = new LongShortTitleCommand()
                },
                new TextMenuItem
                {
                    Id = "2",
                    Text = "Multiline titles",
                    Command = new MultilineTitleCommand()
                },
                new TextMenuItem
                {
                    Id = "3",
                    Text = "Multiline cell content",
                    Command = new MultilineCellCommand()
                },
                new TextMenuItem
                {
                    Id = "4",
                    Text = "Draw lines between rows",
                    Command = new DrawLinesBetweenRowsCommand()
                },
                new TextMenuItem
                {
                    Id = "5",
                    Text = "Cell padding",
                    Command = new CellPaddingCommand()
                },
                new TextMenuItem
                {
                    Id = "11",
                    Text = "Single-line Border",
                    Command = new SingleLineBorderCommand()
                },
                new TextMenuItem
                {
                    Id = "22",
                    Text = "Double-line Border",
                    Command = new DoubleLineBorderCommand()
                },
                new TextMenuItem
                {
                    Id = "33",
                    Text = "Simple Border",
                    Command = new SimpleBorderCommand()
                },
                new TextMenuItem
                {
                    Id = "0",
                    Text = "Exit"
                }
            };

            return new TextMenu(menuItems)
            {
                QuestionText = "Make your choice"
            };
        }

        private static void DisplayApplicationHeader()
        {
            CustomConsole.WriteLineEmphasies("ConsoleTools Demo - TabularData");
            CustomConsole.WriteLineEmphasies("===============================================================================");
            CustomConsole.WriteLine();
            CustomConsole.WriteLine("This demo shows how to display data in tables.");
            CustomConsole.WriteLine();
        }
    }
}