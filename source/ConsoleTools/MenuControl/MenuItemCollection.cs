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
    public class MenuItemCollection : List<IMenuItem>
    {
        private int? currentIndex;

        public MenuItemCollection(IEnumerable<IMenuItem> menuItems)
            : base(menuItems)
        {
        }

        public int? CurrentIndex
        {
            get { return currentIndex; }
            private set
            {
                if (value < 0 || value >= Count)
                    throw new IndexOutOfRangeException();

                int? previousIndex = currentIndex;
                currentIndex = value;

                CurrentIndexChangedEventArgs e = new CurrentIndexChangedEventArgs(previousIndex, currentIndex);
                OnCurrentIndexChanged(e);
            }
        }

        public IMenuItem CurrentItem => currentIndex.HasValue
            ? this[currentIndex.Value]
            : null;

        public event EventHandler<CurrentIndexChangedEventArgs> CurrentIndexChanged;

        public bool ExistSelectableItems
        {
            get
            {
                int selectableItemsCount = this
                    .Count(x => x != null && x.IsVisible && x.IsSelectable);

                return selectableItemsCount > 0;
            }
        }

        public int? VisibleCurrentIndex
        {
            get
            {
                if (!currentIndex.HasValue || !CurrentItem.IsVisible)
                    return null;

                return this
                    .Take(currentIndex.Value + 1)
                    .Count(x => x != null && x.IsVisible);
            }
        }

        public int? CalculateVisibleIndex(IMenuItem menuItem)
        {
            int index = IndexOf(menuItem);

            if (index == -1 || !menuItem.IsVisible)
                return null;

            return this
                .Take(index)
                .Count(x => x != null && x.IsVisible);
        }

        public void MoveToPrevious()
        {
            CurrentIndex = GetPreviousItemIndex();
        }

        private int? GetPreviousItemIndex()
        {
            int startIndex = CurrentIndex == null
                ? Count - 1
                : CurrentIndex.Value - 1;

            for (int i = startIndex; i >= 0; i--)
            {
                IMenuItem menuItem = this[i];

                if (menuItem != null && menuItem.IsVisible && menuItem.IsSelectable)
                    return i;
            }

            return CurrentIndex;
        }

        public void MoveToNext()
        {
            CurrentIndex = GetNextItemIndex();
        }

        private int? GetNextItemIndex()
        {
            int startIndex = CurrentIndex == null
                ? 0
                : CurrentIndex.Value + 1;

            for (int i = startIndex; i < Count; i++)
            {
                IMenuItem menuItem = this[i];

                if (menuItem != null && menuItem.IsVisible && menuItem.IsSelectable)
                    return i;
            }

            return CurrentIndex;
        }

        public void SelectFirst()
        {
            CurrentIndex = GetFirstItemIndex();
        }

        private int GetFirstItemIndex()
        {
            int index = -1;

            foreach (IMenuItem menuItem in this)
            {
                index++;

                if (menuItem != null && menuItem.IsVisible && menuItem.IsSelectable)
                    return index;
            }

            return index;
        }

        protected virtual void OnCurrentIndexChanged(CurrentIndexChangedEventArgs e)
        {
            CurrentIndexChanged?.Invoke(this, e);
        }

        public void Reset()
        {
            CurrentIndex = null;
        }

        public void SelectNone()
        {
            CurrentIndex = null;
        }

        public bool SelectItem(ConsoleKey consoleKey)
        {
            int index = -1;

            foreach (IMenuItem x in this)
            {
                index++;

                if (x != null && x.IsVisible && x.IsSelectable && x.ShortcutKey != null && x.ShortcutKey == consoleKey)
                {
                    CurrentIndex = index;
                    return true;
                }
            }

            return false;
        }
    }
}