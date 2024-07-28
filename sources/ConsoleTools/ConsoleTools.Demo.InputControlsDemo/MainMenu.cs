// ConsoleTools
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

using System.Collections.Generic;
using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Controls.Menus;
using DustInTheWind.ConsoleTools.Controls.Menus.MenuItems;
using DustInTheWind.ConsoleTools.Demo.InputControlsDemo.Commands;

namespace DustInTheWind.ConsoleTools.Demo.InputControlsDemo
{
    internal class MainMenu : ScrollMenu
    {
        public MainMenu()
        {
            ItemsHorizontalAlignment = HorizontalAlignment.Left;
            SelectFirstByDefault = true;

            IEnumerable<IMenuItem> menuItems = CreateMenuItems();
            AddItems(menuItems);
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
    }
}