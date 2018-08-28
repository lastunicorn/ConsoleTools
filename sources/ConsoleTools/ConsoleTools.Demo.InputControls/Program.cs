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
using System.Collections.Generic;
using DustInTheWind.ConsoleTools.Demo.InputControls.Commands;
using DustInTheWind.ConsoleTools.MenuControl;
using DustInTheWind.ConsoleTools.MenuControl.MenuItems;

namespace DustInTheWind.ConsoleTools.Demo.InputControls
{
    internal static class Program
    {
        private static ScrollableMenu menu;

        private static void Main()
        {
            Console.SetBufferSize(80, 1024);
            Console.SetWindowSize(80, 60);

            DisplayApplicationHeader();
            
            menu = CreateMenu();
            menu.Display();
        }

        private static void DisplayApplicationHeader()
        {
            CustomConsole.WriteLineEmphasies("ConsoleTools Demo - InputControls");
            CustomConsole.WriteLineEmphasies("===============================================================================");
            CustomConsole.WriteLine();
            CustomConsole.WriteLine("This demo shows the usage of the input controls (text and list).");
            CustomConsole.WriteLine();
        }

        private static ScrollableMenu CreateMenu()
        {
            IEnumerable<IMenuItem> menuItems = CreateMenuItems();

            return new ScrollableMenu(menuItems)
            {
                ItemsHorizontalAlignment = HorizontalAlignment.Left,
                SelectFirstByDefault = true
            };
        }

        private static IEnumerable<IMenuItem> CreateMenuItems()
        {
            return new List<IMenuItem>
            {
                new LabelMenuItem
                {
                    Text = "Value Read - Strings",
                    Command = new ValueReadStringCommand()
                },
                new LabelMenuItem
                {
                    Text = "Value Read - Number",
                    Command = new ValueReadNumberCommand()
                },
                new LabelMenuItem
                {
                    Text = "Value Read - Quick (static method)",
                    Command = new ValueReadQuickCommand()
                },
                new LabelMenuItem
                {
                    Text = "Value Read - With default value",
                    Command = new ValueReadWithDefaultValueCommand()
                },

                new SeparatorMenuItem(),

                new LabelMenuItem
                {
                    Text = "Value Write",
                    Command = new ValueWriteCommand()
                },
                new LabelMenuItem
                {
                    Text = "Value Write - Quick (static method)",
                    Command = new ValueWriteQuickCommand()
                },

                new SeparatorMenuItem(),

                new LabelMenuItem
                {
                    Text = "List Read - Strings",
                    Command = new ListReadStringsCommand()
                },
                new LabelMenuItem
                {
                    Text = "List Read - Numbers",
                    Command = new ListReadNumbersCommand()
                },
                new LabelMenuItem
                {
                    Text = "List Read - Quick (static method)",
                    Command = new ListReadQuickCommand()
                },
                new LabelMenuItem
                {
                    Text = "List Read - With custom parser",
                    Command = new ListReadWithCustomParserCommand()
                },

                new SeparatorMenuItem(),

                new LabelMenuItem
                {
                    Text = "List Write - Custom",
                    Command = new ListWriteCommand()
                },

                new LabelMenuItem
                {
                    Text = "List Write - Quick (static method)",
                    Command = new ListWriteQuickCommand()
                },

                new SeparatorMenuItem(),

                new LabelMenuItem
                {
                    Text = "Yes/No Question",
                    Command = new YesNoCommand()
                },
                new LabelMenuItem
                {
                    Text = "Yes/No/Cancel Question",
                    Command = new YesNoCancelCommand()
                },

                new SeparatorMenuItem(),

                new LabelMenuItem
                {
                    Text = "Exit",
                    Command = new ExitCommand()
                }
            };
        }

        public static void Stop()
        {
            menu.RequestClose();
        }
    }
}