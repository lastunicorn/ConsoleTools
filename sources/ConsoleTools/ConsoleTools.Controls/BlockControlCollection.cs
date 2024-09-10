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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DustInTheWind.ConsoleTools.Controls;

public class BlockControlCollection : Collection<Control>, ICloseSupport
{
    private bool isClosed;
    private bool suppressCloseStateEvent;
    private int closedCount;

    public bool IsClosed => isClosed || closedCount > 0;

    public event EventHandler CloseStateChanged;

    protected override void InsertItem(int index, Control item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        ExecuteAndCheckCloseState(() =>
        {
            if (item is ICloseSupport itemWithCloseSupport)
            {
                itemWithCloseSupport.CloseStateChanged += HandleContentCloseStateChanged;

                if (itemWithCloseSupport.IsClosed)
                    closedCount++;
            }

            base.InsertItem(index, item);
        });
    }

    protected override void RemoveItem(int index)
    {
        Control item = Items[index];

        ExecuteAndCheckCloseState(() =>
        {
            if (item is ICloseSupport itemWithCloseSupport)
            {
                itemWithCloseSupport.CloseStateChanged -= HandleContentCloseStateChanged;

                if (itemWithCloseSupport.IsClosed)
                    closedCount--;
            }

            base.RemoveItem(index);
        });
    }

    protected override void SetItem(int index, Control item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        ExecuteAndCheckCloseState(() =>
        {
            Control itemToRemove = Items[index];

            if (itemToRemove is ICloseSupport itemToRemoveWithCloseSupport)
            {
                itemToRemoveWithCloseSupport.CloseStateChanged -= HandleContentCloseStateChanged;

                if (itemToRemoveWithCloseSupport.IsClosed)
                    closedCount--;
            }

            if (item is ICloseSupport itemWithCloseSupport)
            {
                itemWithCloseSupport.CloseStateChanged += HandleContentCloseStateChanged;

                if (itemWithCloseSupport.IsClosed)
                    closedCount++;
            }

            base.SetItem(index, item);
        });
    }

    protected override void ClearItems()
    {
        ExecuteAndCheckCloseState(() =>
        {
            base.ClearItems();
        });
    }

    private void HandleContentCloseStateChanged(object sender, EventArgs e)
    {
        if (sender is not ICloseSupport itemWithCloseSupport)
            return;

        ExecuteAndCheckCloseState(() =>
        {
            if (itemWithCloseSupport.IsClosed)
                closedCount++;
            else
                closedCount--;
        });
    }

    public void RequestClose()
    {
        suppressCloseStateEvent = true;

        try
        {
            ExecuteAndCheckCloseState(() =>
            {
                isClosed = true;

                IEnumerable<ICloseSupport> childrenWithCloseSupports = Items
                    .OfType<ICloseSupport>();

                foreach (ICloseSupport closeSupport in childrenWithCloseSupports)
                    closeSupport.RequestClose();
            });
        }
        finally
        {
            suppressCloseStateEvent = false;
        }
    }

    public void ResetClose()
    {
        suppressCloseStateEvent = true;

        try
        {
            ExecuteAndCheckCloseState(() =>
            {
                isClosed = false;

                IEnumerable<ICloseSupport> childrenWithCloseSupports = Items
                    .OfType<ICloseSupport>();

                foreach (ICloseSupport closeSupport in childrenWithCloseSupports)
                    closeSupport.ResetClose();
            });
        }
        finally
        {
            suppressCloseStateEvent = false;
        }
    }

    private void ExecuteAndCheckCloseState(Action action)
    {
        if (suppressCloseStateEvent)
        {
            action?.Invoke();
            return;
        }

        bool initialCloseState = IsClosed;

        try
        {
            action?.Invoke();
        }
        finally
        {
            if (IsClosed != initialCloseState)
                OnCloseStateChanged();
        }
    }

    protected virtual void OnCloseStateChanged()
    {
        CloseStateChanged?.Invoke(this, EventArgs.Empty);
    }
}