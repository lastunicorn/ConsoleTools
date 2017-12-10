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
        private readonly int screenWidth = Console.BufferWidth;

        private List<IMenuItem> visibleMenuItems;
        private int currentIndex = -1;

        private int rowAfterMenu;

        private bool isCloseRequested;

        public int SelectedIndex { get; private set; }
        public HorizontalAlign ItemsHorizontalAlign { get; set; }
        public IMenuItem SelectedItem { get; private set; }
        public bool SelectFirstByDefault { get; set; } = true;

        public void Display()
        {
            isCloseRequested = false;

            RunWithoutCursor(() =>
            {
                visibleMenuItems = this
                    .Where(x => x != null && x.IsVisible)
                    .ToList();

                List<IMenuItem> selectableItems = visibleMenuItems
                    .Where(x => !(x is SpaceMenuItem))
                    .ToList();

                if (selectableItems.Count == 0)
                    throw new ApplicationException("There are no menu items to be displayed.");

                DisplayMenuItems();
                ReadUserSelection();
            });
        }

        private void RunWithoutCursor(Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            bool initialCursorVisible = Console.CursorVisible;
            Console.CursorVisible = false;

            try
            {
                action.Invoke();
            }
            finally
            {
                DrawItem(currentIndex, false);

                Console.SetCursorPosition(0, rowAfterMenu);
                Console.CursorVisible = initialCursorVisible;
            }
        }

        public void RequestClose()
        {
            isCloseRequested = true;
        }

        public void Resume()
        {
            RunWithoutCursor(() =>
            {
                DrawItem(currentIndex, true);
                ReadUserSelection();
            });
        }

        public void Refresh()
        {
            RunWithoutCursor(() =>
            {
                DrawItem(currentIndex, true);
            });
        }

        private void DisplayMenuItems()
        {
            for (int i = 0; i < visibleMenuItems.Count; i++)
                Console.WriteLine();

            rowAfterMenu = Console.CursorTop;

            for (int i = 0; i < visibleMenuItems.Count; i++)
                DrawItem(i, false);

            currentIndex = SelectFirstByDefault
                ? GetFirstItemIndex()
                : -1;

            DrawItem(currentIndex, true);
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
                        HandleUpArrawPressed();
                        break;

                    case ConsoleKey.DownArrow:
                        HandleDownArrowPressed();
                        break;

                    case ConsoleKey.Enter:
                        {
                            if (currentIndex == -1)
                                continue;

                            IMenuItem selectedItem = visibleMenuItems[currentIndex];

                            bool allow = selectedItem.IsSelectable && selectedItem.BeforeSelect();
                            if (allow)
                            {
                                int spaceMenuItemCount = visibleMenuItems
                                    .Take(currentIndex)
                                    .OfType<SpaceMenuItem>()
                                    .Count();

                                SelectedIndex = currentIndex - spaceMenuItemCount;
                                SelectedItem = selectedItem;
                                return;
                            }
                        }
                        break;

                    default:
                        {
                            IMenuItem selectedItem = visibleMenuItems.FirstOrDefault(x => x.ShortcutKey != null && x.ShortcutKey == keyInfo.Key);

                            if (selectedItem != null)
                            {
                                bool allow = selectedItem.IsSelectable && selectedItem.BeforeSelect();
                                if (allow)
                                {
                                    int spaceMenuItemCount = visibleMenuItems
                                        .Take(currentIndex)
                                        .OfType<SpaceMenuItem>()
                                        .Count();

                                    SelectedIndex = currentIndex - spaceMenuItemCount;
                                    SelectedItem = selectedItem;
                                    return;
                                }
                            }
                        }
                        break;
                }
            }
        }

        private void HandleUpArrawPressed()
        {
            int previousIndex = GetPreviousItemIndex();
            if (previousIndex == currentIndex)
                return;

            DrawItem(currentIndex, false);
            currentIndex = previousIndex;
            DrawItem(currentIndex, true);
        }

        private void HandleDownArrowPressed()
        {
            int nextIndex = GetNextItemIndex();
            if (nextIndex == currentIndex)
                return;

            DrawItem(currentIndex, false);
            currentIndex = nextIndex;
            DrawItem(currentIndex, true);
        }

        private int GetFirstItemIndex()
        {
            for (int i = 0; i < visibleMenuItems.Count; i++)
            {
                if (!(visibleMenuItems[i] is SpaceMenuItem))
                    return i;
            }

            return -1;
        }

        private int GetPreviousItemIndex()
        {
            int startIndex = currentIndex == -1
                   ? visibleMenuItems.Count - 1
                   : currentIndex - 1;

            for (int i = startIndex; i >= 0; i--)
            {
                if (visibleMenuItems[i] is SpaceMenuItem)
                    continue;

                return i;
            }

            return currentIndex;
        }

        private int GetNextItemIndex()
        {
            int startIndex = currentIndex == -1
                   ? 0
                   : currentIndex + 1;

            for (int i = startIndex; i < visibleMenuItems.Count; i++)
            {
                if (visibleMenuItems[i] is SpaceMenuItem)
                    continue;

                return i;
            }

            return currentIndex;
        }

        private void DrawItem(int menuIndex, bool selected)
        {
            if (menuIndex == -1)
                return;

            IMenuItem menuItem = visibleMenuItems[menuIndex];

            int x = screenWidth / 2 - 2;
            int y = rowAfterMenu - visibleMenuItems.Count + menuIndex;

            Console.SetCursorPosition(0, y);
            Console.Write(new string(' ', Console.BufferWidth - 1));

            menuItem.Display(x, y, selected, ItemsHorizontalAlign);
        }
    }
}