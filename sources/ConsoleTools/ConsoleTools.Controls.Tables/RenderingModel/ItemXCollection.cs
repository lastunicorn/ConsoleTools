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

using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace DustInTheWind.ConsoleTools.Controls.Tables.RenderingModel;

internal class ItemXCollection : Collection<IItemX>
{
    protected override void InsertItem(int index, IItemX item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        PrepareItemToBeAdded(item);
        base.InsertItem(index, item);
    }

    protected override void SetItem(int index, IItemX item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        PrepareItemToBeAdded(item);
        base.SetItem(index, item);
    }

    protected override void RemoveItem(int index)
    {
        // The item should be unchained. The links with the previous and next items should be removed.
        // Not implemented because it is not needed.
    }

    protected override void ClearItems()
    {
        // All the items should be unchained. The links with the previous and next items should be removed.
        // Not implemented because it is not needed.

        base.ClearItems();
    }

    private void PrepareItemToBeAdded(IItemX item)
    {
        switch (item)
        {
            case SeparatorX separatorX:
                {
                    IItemX lastItem = Items.LastOrDefault();
                    separatorX.Row1 = lastItem as RowX;
                    break;
                }

            case RowX rowX:
                {
                    IItemX lastItem = Items.LastOrDefault();

                    if (lastItem is SeparatorX lastSeparator)
                        lastSeparator.Row2 = rowX;

                    break;
                }

            default:
                throw new ArgumentException(nameof(item), $"Invalid item type: {item.GetType().FullName}");
        }
    }
}