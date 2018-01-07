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
using System.Linq;
using System.Threading;

namespace DustInTheWind.ConsoleTools.MenuControl
{
    /// <summary>
    /// A menu in which the user can navigate by using the up/down arrow keys.
    /// </summary>
    public class SelectableMenu
    {
        private const HorizontalAlignment DefaultHorizontalAlignment = HorizontalAlignment.Center;

        private readonly MenuItemCollection menuItems;

        private volatile bool isCloseRequested;
        private Size menuSize;
        private Location menuLocation;

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
        /// Specifies the horizontal alignment of the menu relative to the Console Buffer.
        /// </summary>
        public HorizontalAlignment HorizontalAlignment { get; set; } = HorizontalAlignment.Default;

        /// <summary>
        /// Specifies the horizontal alignment for the items displayed inside the menu. 
        /// </summary>
        public HorizontalAlignment ItemsHorizontalAlignment { get; set; } = HorizontalAlignment.Default;

        /// <summary>
        /// Gets or sets a value that specifies if the first item is automatically selected when the menu is displayed.
        /// </summary>
        public bool SelectFirstByDefault { get; set; } = true;

        /// <summary>
        /// Gets or sets a vlue that specifies if circular selection is allowed.
        /// When reaching the first item go to the last item.
        /// When reaching the last item go to the first item.
        /// </summary>
        public bool AllowCircularSelection
        {
            get { return menuItems.AllowCircularSelection; }
            set { menuItems.AllowCircularSelection = value; }
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:DustInTheWind.ConsoleTools.MenuControl.SelectableMenu" /> class with
        /// the list of items.
        /// </summary>
        /// <param name="menuItems">The list of items to be displayed by the menu.</param>
        public SelectableMenu(IEnumerable<IMenuItem> menuItems)
        {
            if (menuItems == null) throw new ArgumentNullException(nameof(menuItems));

            this.menuItems = new MenuItemCollection();

            foreach (IMenuItem menuItem in menuItems)
            {
                menuItem.ParentMenu = this;
                this.menuItems.Add(menuItem);
            }
        }

        private void HandleCurrentIndexChanged(object sender, CurrentIndexChangedEventArgs e)
        {
            if (e.PreviousIndex.HasValue)
                DrawMenuItem(e.PreviousIndex.Value);

            if (e.CurrentIndex.HasValue)
                DrawMenuItem(e.CurrentIndex.Value);
        }

        /// <summary>
        /// Displays the menu and waits for the user to choose an item.
        /// This method blocks until the user chooses an item.
        /// </summary>
        public void Display()
        {
            Reset();

            RunWithoutCursor(() =>
            {
                menuItems.CurrentIndexChanged += HandleCurrentIndexChanged;

                try
                {
                    if (menuItems.SelectableItemsCount == 0)
                        throw new ApplicationException("There are no menu items to be displayed.");

                    DrawMenu();

                    if (SelectFirstByDefault)
                        menuItems.SelectFirst();

                    ReadUserSelection();
                }
                finally
                {
                    menuItems.SelectNone();
                    menuItems.CurrentIndexChanged -= HandleCurrentIndexChanged;

                    int firstLineAfterMenu = menuLocation.Top + menuSize.Height;
                    Console.SetCursorPosition(0, firstLineAfterMenu);
                }
            });

            SelectedItem?.Command?.Execute();
        }

        private void Reset()
        {
            isCloseRequested = false;

            menuLocation = Location.Origin;
            menuSize = Size.Empty;

            SelectedIndex = null;
            SelectedItem = null;

            menuItems.Reset();
        }

        public void Resume()
        {
            RunWithoutCursor(() =>
            {
                menuItems.CurrentIndexChanged += HandleCurrentIndexChanged;

                try
                {
                    Refresh();
                    ReadUserSelection();
                }
                finally
                {
                    menuItems.SelectNone();
                    menuItems.CurrentIndexChanged -= HandleCurrentIndexChanged;

                    int firstLineAfterMenu = menuLocation.Top + menuSize.Height;
                    Console.SetCursorPosition(0, firstLineAfterMenu);
                }
            });

            SelectedItem?.Command?.Execute();
        }

        public void Refresh()
        {
            DrawMenuItem(menuItems.CurrentIndex);
        }

        private void RunWithoutCursor(Action action)
        {
            bool initialCursorVisible = Console.CursorVisible;
            Console.CursorVisible = false;

            try
            {
                action.Invoke();
            }
            finally
            {
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
            menuSize = CalculateMenuDimensions();

            // Write empty lines
            for (int i = 0; i < menuSize.Height; i++)
                Console.WriteLine();

            menuLocation = CalculateMenuLocation();

            for (int i = 0; i < menuItems.Count; i++)
                DrawMenuItem(i);
        }

        private Location CalculateMenuLocation()
        {
            HorizontalAlignment calcualtedHorizontalAlignment = CalcualteHorizontalAlignment();

            int menuTop = Console.CursorTop - menuSize.Height;

            switch (calcualtedHorizontalAlignment)
            {
                default:
                    return new Location(0, menuTop);

                case HorizontalAlignment.Center:
                    return new Location((Console.BufferWidth - menuSize.Width) / 2, menuTop);

                case HorizontalAlignment.Right:
                    return new Location(Console.BufferWidth - menuSize.Width, menuTop);
            }
        }

        private HorizontalAlignment CalcualteHorizontalAlignment()
        {
            HorizontalAlignment calcualtedHorizontalAlignment = HorizontalAlignment;

            if (calcualtedHorizontalAlignment == HorizontalAlignment.Default)
                calcualtedHorizontalAlignment = DefaultHorizontalAlignment;

            return calcualtedHorizontalAlignment;
        }

        private Size CalculateMenuDimensions()
        {
            int menuHight = menuItems
                .Count(x => x != null && x.IsVisible);

            int menuWidth = menuItems
                .Where(x => x != null && x.IsVisible)
                .Select(x => x.Measure())
                .Max(x => x.Width);

            return new Size(menuWidth, menuHight);
        }

        private void DrawMenuItem(int? index)
        {
            if (index == null)
                return;

            IMenuItem menuItemToDraw = menuItems[index.Value];
            int? visibleIndex = menuItems.CalculateVisibleIndex(menuItemToDraw);

            if (visibleIndex.HasValue && visibleIndex.Value >= 0)
            {
                int left = menuLocation.Left;
                int top = menuLocation.Top + visibleIndex.Value;

                Console.SetCursorPosition(left, top);

                Size menuItemSize = new Size(menuSize.Width, 1);
                bool isHighlighted = menuItemToDraw == menuItems.CurrentItem;

                menuItemToDraw.Display(menuItemSize, isHighlighted);
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

            SelectedIndex = menuItems.CurrentVisibleIndex;
            SelectedItem = selectedItem;
            isCloseRequested = true;
        }
    }
}