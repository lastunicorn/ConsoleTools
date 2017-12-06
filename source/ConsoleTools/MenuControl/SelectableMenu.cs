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

namespace DustInTheWind.ConsoleTools.MenuControl
{
    internal class SelectableMenu<T> : List<IMenuItem<T>>
    {
        private readonly int screenWidth = Console.BufferWidth;

        private List<IMenuItem<T>> visibleMenuItems;
        private int currentIndex = -1;

        private int rowAfterMenu;

        public int SelectedIndex { get; private set; }
        public HorizontalAlign ItemsHorizontalAlign { get; set; }
        public IMenuItem<T> SelectedItem { get; private set; }

        public T Display()
        {
            bool initialCursorVisible = Console.CursorVisible;
            Console.CursorVisible = false;

            try
            {
                visibleMenuItems = this
                    .Where(x => x != null && x.IsVisible)
                    .ToList();

                List<IMenuItem<T>> selectableItems = visibleMenuItems
                    .Where(x => !(x is SpaceMenuItem<string>))
                    .ToList();

                if (selectableItems.Count == 0)
                    throw new Exception("There are no menu items to be displayed.");

                DisplayMenuItems();
                return ReadUserSelection();
            }
            finally
            {
                DrawItem(currentIndex, false);

                Console.SetCursorPosition(0, rowAfterMenu);
                Console.CursorVisible = initialCursorVisible;
            }
        }

        public T Resume()
        {
            bool initialCursorVisible = Console.CursorVisible;
            Console.CursorVisible = false;

            try
            {
                DrawItem(currentIndex, true);
                return ReadUserSelection();
            }
            finally
            {
                DrawItem(currentIndex, false);

                Console.SetCursorPosition(0, rowAfterMenu);
                Console.CursorVisible = initialCursorVisible;
            }
        }

        public void Refresh()
        {
            bool initialCursorVisible = Console.CursorVisible;
            Console.CursorVisible = false;

            try
            {
                DrawItem(currentIndex, true);
            }
            finally
            {
                DrawItem(currentIndex, false);

                Console.SetCursorPosition(0, rowAfterMenu);
                Console.CursorVisible = initialCursorVisible;
            }
        }

        private void DisplayMenuItems()
        {
            for (int i = 0; i < visibleMenuItems.Count; i++)
                Console.WriteLine();

            rowAfterMenu = Console.CursorTop;

            bool itemWasSelected = false;
            currentIndex = -1;

            for (int i = 0; i < visibleMenuItems.Count; i++)
            {
                bool isSelect = !itemWasSelected && !(visibleMenuItems[i] is SpaceMenuItem<string>);

                DrawItem(i, isSelect);

                if (isSelect)
                {
                    itemWasSelected = true;
                    currentIndex = i;
                }
            }
        }

        private T ReadUserSelection()
        {
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        int previousIndex = GetPreviousItemIndex();
                        if (previousIndex == currentIndex)
                            continue;

                        DrawItem(currentIndex, false);
                        currentIndex = GetPreviousItemIndex();
                        DrawItem(currentIndex, true);

                        break;

                    case ConsoleKey.DownArrow:
                        int nextIndex = GetNextItemIndex();
                        if (nextIndex == currentIndex)
                            continue;

                        DrawItem(currentIndex, false);
                        currentIndex = nextIndex;
                        DrawItem(currentIndex, true);

                        break;

                    case ConsoleKey.Enter:
                        {
                            IMenuItem<T> selectedItem = visibleMenuItems[currentIndex];

                            bool allow = selectedItem.IsSelectable && selectedItem.BeforeSelect();
                            if (allow)
                            {
                                SelectedIndex = currentIndex - visibleMenuItems.Take(currentIndex).OfType<SpaceMenuItem<T>>().Count();
                                SelectedItem = selectedItem;
                                return selectedItem.Value;
                            }
                        }
                        break;

                    default:
                        {
                            IMenuItem<T> selectedItem = visibleMenuItems.FirstOrDefault(x => x.Key != null && x.Key == keyInfo.Key);

                            if (selectedItem != null)
                            {
                                bool allow = selectedItem.IsSelectable && selectedItem.BeforeSelect();
                                if (allow)
                                {
                                    SelectedIndex = currentIndex - visibleMenuItems.Take(currentIndex).OfType<SpaceMenuItem<T>>().Count();
                                    SelectedItem = selectedItem;
                                    return selectedItem.Value;
                                }
                            }
                        }
                        break;
                }
            }
        }

        private int GetPreviousItemIndex()
        {
            int startIndex = currentIndex == -1
                   ? visibleMenuItems.Count
                   : currentIndex - 1;

            for (int i = startIndex; i >= 0; i--)
            {
                if (visibleMenuItems[i] is SpaceMenuItem<T>)
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
                if (visibleMenuItems[i] is SpaceMenuItem<T>)
                    continue;

                return i;
            }

            return currentIndex;
        }

        private void DrawItem(int menuIndex, bool selected)
        {
            if (menuIndex == -1)
                return;

            IMenuItem<T> menuItem = visibleMenuItems[menuIndex];

            int x = screenWidth / 2 - 2;
            int y = rowAfterMenu - visibleMenuItems.Count + menuIndex;
            
            Console.SetCursorPosition(0, y);
            Console.Write(new string(' ', Console.BufferWidth - 1));

            menuItem.Display(x, y, selected, ItemsHorizontalAlign);
        }
    }
}