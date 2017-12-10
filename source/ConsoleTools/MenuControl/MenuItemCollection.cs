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

        public int? CurrentIndex
        {
            get { return currentIndex; }
            private set
            {
                int? previousIndex = currentIndex;
                currentIndex = value;

                CurrentIndexChangedEventArgs e = new CurrentIndexChangedEventArgs(previousIndex, currentIndex);
                OnCurrentIndexChanged(e);
            }
        }

        public IMenuItem CurrentItem
        {
            get
            {
                if (currentIndex.HasValue && currentIndex >= 0 && currentIndex < Count)
                    return this[currentIndex.Value];

                return null;
            }
        }

        public event EventHandler<CurrentIndexChangedEventArgs> CurrentIndexChanged;
        
        public bool ExistSelectableItems
        {
            get
            {
                int selectableItemsCount = this
                    .Count(x => x != null && x.IsVisible && !(x is SpaceMenuItem));

                return selectableItemsCount > 0;
            }
        }

        public int? VisibleSelectedIndex
        {
            get
            {
                if (!currentIndex.HasValue)
                    return null;

                int spaceMenuItemCount = this
                    .OfType<SpaceMenuItem>()
                    .Where(x => x.IsVisible)
                    .Take(currentIndex.Value)
                    .Count();

                return currentIndex.Value - spaceMenuItemCount;
            }
        }

        public void ModeToPrevious()
        {
            CurrentIndex = GetPreviousItemIndex();
        }

        public void ModeToNext()
        {
            CurrentIndex = GetNextItemIndex();
        }

        private int GetFirstItemIndex()
        {
            int index = -1;

            foreach (IMenuItem menuItem in this)
            {
                index++;

                if (menuItem != null && menuItem.IsVisible && !(menuItem is SpaceMenuItem))
                    return index;
            }

            return index;
        }

        private int? GetPreviousItemIndex()
        {
            int startIndex = CurrentIndex == null
                ? Count - 1
                : CurrentIndex.Value - 1;

            for (int i = startIndex; i >= 0; i--)
            {
                IMenuItem menuItem = this[i];

                if (menuItem != null && menuItem.IsVisible && !(menuItem is SpaceMenuItem))
                    return i;
            }

            return CurrentIndex;
        }

        private int? GetNextItemIndex()
        {
            int startIndex = CurrentIndex == null
                ? 0
                : CurrentIndex.Value + 1;

            for (int i = startIndex; i < Count; i++)
            {
                IMenuItem menuItem = this[i];

                if (menuItem != null && menuItem.IsVisible && !(menuItem is SpaceMenuItem))
                    return i;
            }

            return CurrentIndex;
        }

        protected virtual void OnCurrentIndexChanged(CurrentIndexChangedEventArgs e)
        {
            CurrentIndexChanged?.Invoke(this, e);
        }

        public void SelectFirst()
        {
            CurrentIndex = GetFirstItemIndex();
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

                if (x != null && x.IsVisible && x.ShortcutKey != null && x.ShortcutKey == consoleKey)
                {
                    CurrentIndex = index;
                    return true;
                }
            }

            return false;
        }
    }
}