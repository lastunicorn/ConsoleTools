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

// --------------------------------------------------------------------------------
// Bugs or feature requests
// --------------------------------------------------------------------------------
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new

using System;
using System.Collections.Generic;
using System.Linq;

namespace DustInTheWind.ConsoleTools.Menues
{
    /// <summary>
    /// A collection of <see cref="IMenuItem"/> instances that implements the concept of a current item.
    /// </summary>
    public class MenuItemCollection : List<IMenuItem>
    {
        private int? currentIndex;

        /// <summary>
        /// Gets the index of the current item.
        /// </summary>
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

        /// <summary>
        /// Gets the current menu item.
        /// </summary>
        public IMenuItem CurrentItem => currentIndex.HasValue
            ? this[currentIndex.Value]
            : null;

        /// <summary>
        /// Gets the index of the current item, ignoring the hidden ones.
        /// </summary>
        public int? CurrentVisibleIndex
        {
            get
            {
                IMenuItem currentItem = CurrentItem;

                return currentItem == null
                    ? null
                    : CalculateVisibleIndex(currentItem);
            }
        }

        /// <summary>
        /// Gets or sets a vlue that specifies if circular selection is allowed.
        /// When reaching the first item go to the last item.
        /// When reaching the last item go to the first item.
        /// Default value: <c>true</c>
        /// </summary>
        public bool AllowWrapAround { get; set; } = true;

        /// <summary>
        /// Gets the number of items that are visible and can be selected.
        /// </summary>
        public int SelectableItemsCount => this.Count(x => x != null && x.IsVisible && x.IsEnabled);

        /// <summary>
        /// Event raised when the currently selected item was changed.
        /// </summary>
        public event EventHandler<CurrentIndexChangedEventArgs> CurrentIndexChanged;

        /// <summary>
        /// Calculates and returns the index of the specified item in the list of visible items.
        /// </summary>
        public int? CalculateVisibleIndex(IMenuItem menuItem)
        {
            int index = IndexOf(menuItem);

            if (index == -1 || !menuItem.IsVisible)
                return null;

            return this
                .Take(index)
                .Count(x => x != null && x.IsVisible);
        }

        /// <summary>
        /// Selects the previous visible and enabled item in the list.
        /// </summary>
        public void MoveToPrevious()
        {
            CurrentIndex = GetPreviousItemIndex();
        }

        private int? GetPreviousItemIndex()
        {
            int startIndex = CurrentIndex - 1 ?? Count - 1;

            // Search from current item up to the first item.

            for (int i = startIndex; i >= 0; i--)
            {
                IMenuItem menuItem = this[i];

                if (menuItem != null && menuItem.IsVisible && menuItem.IsEnabled)
                    return i;
            }

            if (AllowWrapAround)
            {
                // Search from last item up to current item.

                for (int i = Count - 1; i > startIndex; i--)
                {
                    IMenuItem menuItem = this[i];

                    if (menuItem != null && menuItem.IsVisible && menuItem.IsEnabled)
                        return i;
                }
            }

            // No valid item was found.

            return CurrentIndex;
        }

        /// <summary>
        /// Selects the next visible and enabled item in the list.
        /// </summary>
        public void MoveToNext()
        {
            CurrentIndex = GetNextItemIndex();
        }

        private int? GetNextItemIndex()
        {
            int startIndex = CurrentIndex + 1 ?? 0;

            // Search from current item down to the last item.

            for (int i = startIndex; i < Count; i++)
            {
                IMenuItem menuItem = this[i];

                if (menuItem != null && menuItem.IsVisible && menuItem.IsEnabled)
                    return i;
            }

            if (AllowWrapAround)
            {
                // Search from the first item down to the current item.

                for (int i = 0; i < startIndex; i++)
                {
                    IMenuItem menuItem = this[i];

                    if (menuItem != null && menuItem.IsVisible && menuItem.IsEnabled)
                        return i;
                }
            }

            // No valid item was found.

            return CurrentIndex;
        }

        /// <summary>
        /// Selects the first visible and enabled item in the list.
        /// </summary>
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

                if (menuItem != null && menuItem.IsVisible && menuItem.IsEnabled)
                    return index;
            }

            return index;
        }

        /// <summary>
        /// Raises the <see cref="CurrentIndexChanged"/> event.
        /// </summary>
        protected virtual void OnCurrentIndexChanged(CurrentIndexChangedEventArgs e)
        {
            CurrentIndexChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Unselects the currently selected item.
        /// </summary>
        public void Reset()
        {
            CurrentIndex = null;
        }

        /// <summary>
        /// Unselects the currently selected item.
        /// </summary>
        public void SelectNone()
        {
            CurrentIndex = null;
        }

        /// <summary>
        /// Selects the item by shortcut key.
        /// </summary>
        /// <param name="consoleKey"></param>
        /// <returns></returns>
        public bool SelectItem(ConsoleKey consoleKey)
        {
            int index = -1;

            foreach (IMenuItem x in this)
            {
                index++;

                if (x != null && x.IsVisible && x.IsEnabled && x.ShortcutKey != null && x.ShortcutKey == consoleKey)
                {
                    CurrentIndex = index;
                    return true;
                }
            }

            return false;
        }

        public bool SelectItem(IMenuItem menuItem)
        {
            int index = -1;

            foreach (IMenuItem x in this)
            {
                index++;

                if (x == menuItem && x.IsVisible && x.IsEnabled)
                {
                    CurrentIndex = index;
                    return true;
                }
            }

            return false;
        }
    }
}