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

using System;
using System.Collections.Generic;
using DustInTheWind.ConsoleTools.Demo.InputControls.Commands;
using DustInTheWind.ConsoleTools.MenuControl;
using DustInTheWind.ConsoleTools.MenuControl.MenuItems;

namespace DustInTheWind.ConsoleTools.Demo.InputControls
{
    internal static class Program
    {
        private static void Main()
        {
            Console.SetBufferSize(80, 1024);

            DisplayApplicationHeader();

            SelectableMenu menu = CreateMenu();

            while (true)
            {
                CustomConsole.WriteLine("-------------------------------------------------------------------------------");
                CustomConsole.WriteLine();

                menu.Display();

                CustomConsole.WriteLine();

                if (menu.SelectedItem?.Id == 0)
                    break;
            }
        }

        private static void DisplayApplicationHeader()
        {
            CustomConsole.WriteLineEmphasies("ConsoleTools Demo - InputControls");
            CustomConsole.WriteLineEmphasies("===============================================================================");
            CustomConsole.WriteLine();
            CustomConsole.WriteLine("This demo shows the usage of the input controls (text and list).");
            CustomConsole.WriteLine();
        }

        private static SelectableMenu CreateMenu()
        {
            IEnumerable<IMenuItem> menuItems = new List<IMenuItem>
            {
                new LabelMenuItem
                {
                    Id = 1,
                    Text = "TextInputControl",
                    Command = new TextInputCommand()
                },
                new LabelMenuItem
                {
                    Id = 2,
                    Text = "ListInputControl",
                    Command = new ListInputCommand()
                },
                new SpaceMenuItem(),
                new LabelMenuItem
                {
                    Id = 3,
                    Text = "TextOutputControl",
                    Command = new TextOutputCommand()
                },
                new LabelMenuItem
                {
                    Id = 4,
                    Text = "ListOutputControl",
                    Command = new ListOutputCommand()
                },
                new SpaceMenuItem(),
                new LabelMenuItem
                {
                    Id = 0,
                    Text = "Exit"
                }
            };

            return new SelectableMenu(menuItems)
            {
                ItemsHorizontalAlign = HorizontalAlign.Center,
                SelectFirstByDefault = true
            };
        }
    }
}
