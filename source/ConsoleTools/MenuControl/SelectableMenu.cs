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
using System.Linq;
using System.Threading;

namespace DustInTheWind.ConsoleTools.MenuControl
{
    /// <summary>
    /// Features:
    ///     - Displays the menu items vertically in a list
    ///     - Selects a menu item using up/down keys
    ///     - Selects a menu item using a shortcut key
    ///     - Align the menu items inside the menu
    ///     - Enable/Disable menu items
    ///     - Display/Hide menu items
    /// </summary>
    public class SelectableMenu : List<IMenuItem>
    {
        private readonly MenuItemCollection menuItems;
        private readonly int screenWidth = Console.BufferWidth;

        private int rowAfterMenu;

        private bool isCloseRequested;

        public int? SelectedIndex { get; private set; }
        public HorizontalAlign ItemsHorizontalAlign { get; set; }
        public IMenuItem SelectedItem { get; private set; }
        public bool SelectFirstByDefault { get; set; } = true;

        public SelectableMenu(MenuItemCollection menuItems)
        {
            if (menuItems == null) throw new ArgumentNullException(nameof(menuItems));
            this.menuItems = menuItems;
        }

        private void HandleCurrentIndexChanged(object sender, CurrentIndexChangedEventArgs e)
        {
            if (e.PreviousIndex.HasValue)
                DrawMenuItem(e.PreviousIndex.Value, false);

            if (e.CurrentIndex.HasValue)
                DrawMenuItem(e.CurrentIndex.Value, true);
        }

        public void Display()
        {
            isCloseRequested = false;

            Run(() =>
            {
                if (!menuItems.ExistSelectableItems)
                    throw new ApplicationException("There are no menu items to be displayed.");

                DrawMenuItems();

                if (SelectFirstByDefault)
                    menuItems.SelectFirst();
                else
                    menuItems.SelectNone();

                ReadUserSelection();
            });
        }

        public void Resume()
        {
            Run(() =>
            {
                Refresh();
                ReadUserSelection();
            });
        }

        public void Refresh()
        {
            DrawMenuItem(menuItems.CurrentIndex, true);
        }

        private void Run(Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            bool initialCursorVisible = Console.CursorVisible;
            Console.CursorVisible = false;

            menuItems.CurrentIndexChanged += HandleCurrentIndexChanged;

            try
            {
                action.Invoke();
            }
            finally
            {
                menuItems.CurrentIndexChanged -= HandleCurrentIndexChanged;

                DrawMenuItem(menuItems.CurrentIndex, false);

                Console.SetCursorPosition(0, rowAfterMenu);
                Console.CursorVisible = initialCursorVisible;
            }
        }

        public void RequestClose()
        {
            isCloseRequested = true;
        }

        private void DrawMenuItems()
        {
            List<IMenuItem> visibleMenuItems = menuItems
                .Where(x => x != null && x.IsVisible)
                .ToList();

            for (int i = 0; i < visibleMenuItems.Count; i++)
                Console.WriteLine();

            rowAfterMenu = Console.CursorTop;

            for (int i = 0; i < menuItems.Count; i++)
                DrawMenuItem(i, false);
        }

        private void DrawMenuItem(int? index, bool selected)
        {
            if (index == null)
                return;

            IMenuItem menuItemToDraw = null;
            int visibleCount = 0;
            int visibleIndex = -1;

            for (int i = 0; i < menuItems.Count; i++)
            {
                IMenuItem menuItem = menuItems[i];

                if (menuItem == null || !menuItem.IsVisible)
                    continue;

                visibleCount++;

                if (i == index.Value)
                {
                    visibleIndex = visibleCount - 1;
                    menuItemToDraw = menuItem;
                }
            }

            if (visibleIndex >= 0)
            {
                int x = screenWidth / 2 - 2;
                int y = rowAfterMenu - visibleCount + visibleIndex;

                Console.SetCursorPosition(0, y);
                Console.Write(new string(' ', Console.BufferWidth - 1));

                menuItemToDraw.Display(x, y, selected, ItemsHorizontalAlign);
            }
        }

        private void ReadUserSelection()
        {
            while (true)
            {
                if (isCloseRequested)
                    return;

                if (!Console.KeyAvailable)
                {
                    Thread.Sleep(50);
                    continue;
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        menuItems.ModeToPrevious();
                        break;

                    case ConsoleKey.DownArrow:
                        menuItems.ModeToNext();
                        break;

                    case ConsoleKey.Enter:
                        {
                            if (menuItems.CurrentIndex == -1)
                                continue;

                            IMenuItem selectedItem = menuItems.CurrentItem;

                            bool allow = selectedItem.IsSelectable && selectedItem.BeforeSelect();
                            if (allow)
                            {
                                SelectedIndex = menuItems.VisibleSelectedIndex;
                                SelectedItem = selectedItem;
                                return;
                            }
                        }
                        break;

                    default:
                        {
                            bool success = menuItems.SelectItem(keyInfo.Key);

                            if (!success)
                                break;

                            IMenuItem selectedItem = menuItems.CurrentItem;

                            if (selectedItem == null)
                                break;

                            bool allow = selectedItem.IsSelectable && selectedItem.BeforeSelect();
                            if (allow)
                            {
                                SelectedIndex = menuItems.VisibleSelectedIndex;
                                SelectedItem = selectedItem;
                                return;
                            }
                        }
                        break;
                }
            }
        }
    }
}