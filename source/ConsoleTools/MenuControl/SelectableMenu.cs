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
    ///     - Displays the menu items vertically in a list.
    ///     - Selects a menu item using up/down keys.
    ///     - Selects a menu item using a shortcut key.
    ///     - Horizontaly align the menu items inside the menu (Left, Center, Right).
    ///     - Enable/Disable menu items.
    ///     - Display/Hide menu items.
    /// </summary>
    public class SelectableMenu : List<IMenuItem>
    {
        private readonly MenuItemCollection menuItems;

        private int screenWidth;
        private int firstLineIndex;
        private int menuHight;
        private volatile bool isCloseRequested;

        /// <summary>
        /// Gets the item that is currently selected.
        /// </summary>
        public IMenuItem SelectedItem { get; private set; }

        /// <summary>
        /// Gets the index of the selected menu item.
        /// The index is calculated based on the visible list of items.
        /// </summary>
        public int? SelectedIndex { get; private set; }

        /// <summary>
        /// Specifies the horizontal alignment for the items displayed inside the menu. 
        /// </summary>
        public HorizontalAlign ItemsHorizontalAlign { get; set; }

        /// <summary>
        /// Gets or sets a value that specifies if the first item is automatically selected when the menu is displayed.
        /// </summary>
        public bool SelectFirstByDefault { get; set; } = true;

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:DustInTheWind.ConsoleTools.MenuControl.SelectableMenu" /> class with
        /// the list of items.
        /// </summary>
        /// <param name="menuItems">The list of items to be displayed by the menu.</param>
        public SelectableMenu(IEnumerable<IMenuItem> menuItems)
        {
            if (menuItems == null) throw new ArgumentNullException(nameof(menuItems));
            this.menuItems = new MenuItemCollection(menuItems);
        }

        private void HandleCurrentIndexChanged(object sender, CurrentIndexChangedEventArgs e)
        {
            if (e.PreviousIndex.HasValue)
                DrawMenuItem(e.PreviousIndex.Value, false);

            if (e.CurrentIndex.HasValue)
                DrawMenuItem(e.CurrentIndex.Value, true);
        }

        /// <summary>
        /// Displays the menu and waits for the user to choose an item.
        /// This method blocks until the user chooses an item.
        /// </summary>
        public void Display()
        {
            Reset();

            Run(() =>
            {
                if (!menuItems.ExistSelectableItems)
                    throw new ApplicationException("There are no menu items to be displayed.");

                DrawMenu();

                if (SelectFirstByDefault)
                    menuItems.SelectFirst();

                ReadUserSelection();
            });

            SelectedItem?.Command?.Execute();
        }

        private void Reset()
        {
            screenWidth = Console.BufferWidth;
            isCloseRequested = false;

            firstLineIndex = -1;
            menuHight = 0;

            SelectedIndex = null;
            SelectedItem = null;

            menuItems.Reset();
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
                menuItems.SelectNone();
                menuItems.CurrentIndexChanged -= HandleCurrentIndexChanged;

                int firstLineAfterMenu = firstLineIndex + menuHight;
                Console.SetCursorPosition(0, firstLineAfterMenu);
                Console.CursorVisible = initialCursorVisible;
            }
        }

        /// <summary>
        /// This method does not immediately close the menu.
        /// It just sets an internal flag that asks the menu to close itself when it can.
        /// </summary>
        public void RequestClose()
        {
            isCloseRequested = true;
        }

        private void DrawMenu()
        {
            List<IMenuItem> visibleMenuItems = menuItems
                .Where(x => x != null && x.IsVisible)
                .ToList();

            firstLineIndex = Console.CursorTop;
            menuHight = visibleMenuItems.Count;

            for (int i = 0; i < menuHight; i++)
                Console.WriteLine();

            for (int i = 0; i < menuItems.Count; i++)
                DrawMenuItem(i, false);
        }

        private void DrawMenuItem(int? index, bool selected)
        {
            if (index == null)
                return;

            IMenuItem menuItemToDraw = menuItems[index.Value];
            int? visibleIndex = menuItems.CalculateVisibleIndex(menuItemToDraw);

            if (visibleIndex.HasValue && visibleIndex.Value >= 0)
            {
                int x = screenWidth / 2 - 2;
                int y = firstLineIndex + visibleIndex.Value;

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
                        menuItems.MoveToPrevious();
                        break;

                    case ConsoleKey.DownArrow:
                        menuItems.MoveToNext();
                        break;

                    case ConsoleKey.Enter:
                        if (menuItems.CurrentItem != null)
                            SelectCurrentItemAndCloseMenu();
                        break;

                    default:
                        bool success = menuItems.SelectItem(keyInfo.Key);
                        if (success)
                            SelectCurrentItemAndCloseMenu();
                        break;
                }
            }
        }

        private void SelectCurrentItemAndCloseMenu()
        {
            IMenuItem selectedItem = menuItems.CurrentItem;

            if (selectedItem?.IsSelectable != true)
                return;

            bool allow = selectedItem.Select();

            if (!allow)
                return;

            SelectedIndex = menuItems.VisibleCurrentIndex;
            SelectedItem = selectedItem;
            isCloseRequested = true;
        }
    }
}