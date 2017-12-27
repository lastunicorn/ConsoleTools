// ConsoleTools
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
using DustInTheWind.ConsoleTools.InputControls;
using DustInTheWind.ConsoleTools.MenuControl;

namespace DustInTheWind.ConsoleTools.Demo.InputControls
{
    internal static class Program
    {
        private static bool exitWasRequested;

        private static void Main()
        {
            Console.SetBufferSize(80, 1024);

            IEnumerable<IMenuItem> menuItems = CreateMenuItems();

            SelectableMenu menu = new SelectableMenu(menuItems)
            {
                ItemsHorizontalAlign = HorizontalAlign.Center,
                SelectFirstByDefault = true
            };

            exitWasRequested = false;

            while (!exitWasRequested)
            {
                menu.Display();

                HandleUserSelection(menu);
            }
        }

        private static IEnumerable<IMenuItem> CreateMenuItems()
        {
            return new List<IMenuItem>
            {
                new LabelMenuItem
                {
                    Id = 1,
                    Text = "TextInputControl"
                },
                new LabelMenuItem
                {
                    Id = 2,
                    Text = "ListInputControl"
                },
                new SpaceMenuItem(),
                new LabelMenuItem
                {
                    Id = 3,
                    Text = "TextOutputControl"
                },
                new LabelMenuItem
                {
                    Id = 4,
                    Text = "ListOutputControl"
                },
                new SpaceMenuItem(),
                new LabelMenuItem
                {
                    Id = 0,
                    Text = "Exit"
                }
            };
        }

        private static void HandleUserSelection(SelectableMenu menu)
        {
            switch (menu.SelectedItem.Id)
            {
                case 0:
                    exitWasRequested = true;
                    break;

                case 1:
                    {
                        TextInputControl textInputControl = new TextInputControl();
                        string firstName = textInputControl.Read("First Name");
                        string lastName = textInputControl.Read("Last Name");

                        CustomConsole.WriteLine("Hi, {0} {1}!", firstName, lastName);
                        break;
                    }

                case 2:
                    {
                        ListInputControl listInputControl = new ListInputControl();
                        List<string> beverages = listInputControl.Read("What are your prefered beverages");

                        CustomConsole.WriteLine();
                        CustomConsole.Write("Beverages you like: ");
                        CustomConsole.WriteLineEmphasies(string.Join(", ", beverages));
                        break;
                    }

                case 3:
                    {
                        TextOutputControl textOutputControl = new TextOutputControl();
                        textOutputControl.Write("First Name", "John");
                        textOutputControl.Write("Last Name", "Doe");
                        textOutputControl.Write("Age", "25");
                        break;
                    }

                case 4:
                    {
                        ListOutputControl listOutputControl = new ListOutputControl();

                        string[] colorNames = Enum.GetNames(typeof(ConsoleColor));
                        listOutputControl.Write("Colors", colorNames);

                        break;
                    }
            }

            CustomConsole.WriteLine();
        }
    }
}
