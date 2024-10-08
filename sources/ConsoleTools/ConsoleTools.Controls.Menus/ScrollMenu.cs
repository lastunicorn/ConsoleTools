﻿// ConsoleTools
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

// --------------------------------------------------------------------------------
// Bugs or feature requests
// --------------------------------------------------------------------------------
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new/choose

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DustInTheWind.ConsoleTools.Controls.Menus;

/// <summary>
/// A menu in which the user can navigate by using the up/down arrow keys.
/// </summary>
public class ScrollMenu : ErasableControl, IRepeatableSupport
{
    private const HorizontalAlignment DefaultHorizontalAlignment = HorizontalAlignment.Center;
    private readonly MenuItemCollection menuItems = new();
    private bool closeWasRequested;
    private Location itemsLocation;

    /// <summary>
    /// The size of the control after it was displayed.
    /// Does not include the margins
    /// </summary>
    private Size menuSize;

    private Size itemsSize;

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
    /// Gets or sets a value that specifies if the last highlighted item
    /// must remain highlighted when the menu is closed.
    /// Default value: <c>false</c>
    /// </summary>
    public bool KeepHighlightingOnClose { get; set; }

    /// <summary>
    /// Gets or sets a value that specifies if circular selection is allowed.
    /// When reaching the first item go to the last item.
    /// When reaching the last item go to the first item.
    /// Default value: <c>true</c>
    /// </summary>
    public bool AllowWrapAround
    {
        get => menuItems.AllowWrapAround;
        set => menuItems.AllowWrapAround = value;
    }

    /// <summary>
    /// Event raised when the current instance cannot be displayed anymore and it is in the "Closed" state.
    /// </summary>
    public event EventHandler Closed;

    /// <inheritdoc />
    /// <summary>
    /// Initializes a new instance of the <see cref="ScrollMenu" /> class.
    /// </summary>
    public ScrollMenu()
    {
        CursorVisibility = false;
        Margin = "0 1";
    }

    /// <inheritdoc />
    /// <summary>
    /// Initializes a new instance of the <see cref="T:DustInTheWind.ConsoleTools.Controls.Menus.ScrollMenu" /> class with
    /// the list of items.
    /// </summary>
    /// <param name="menuItems">The list of items to be displayed by the menu.</param>
    public ScrollMenu(IEnumerable<IMenuItem> menuItems)
        : this()
    {
        if (menuItems == null) throw new ArgumentNullException(nameof(menuItems));

        this.menuItems = new MenuItemCollection();

        foreach (IMenuItem menuItem in menuItems.Where(x => x != null))
        {
            menuItem.ParentMenu = this;
            this.menuItems.Add(menuItem);
        }
    }

    /// <summary>
    /// Adds a new item to the current instance.
    /// </summary>
    /// <param name="menuItem">The item to be added to the current instance.</param>
    public void AddItem(IMenuItem menuItem)
    {
        if (menuItem == null) throw new ArgumentNullException(nameof(menuItem));

        menuItem.ParentMenu = this;
        menuItems.Add(menuItem);
    }

    /// <summary>
    /// Adds a list of items to the current instance.
    /// </summary>
    /// <param name="menuItems">The list of items to be added to the current instance.</param>
    public void AddItems(IEnumerable<IMenuItem> menuItems)
    {
        if (menuItems == null) throw new ArgumentNullException(nameof(menuItems));

        bool existsNullItems = menuItems.Any(x => x == null);

        if (existsNullItems)
            throw new ArgumentException("Null items are not accepted.", nameof(menuItems));

        foreach (IMenuItem menuItem in menuItems)
        {
            menuItem.ParentMenu = this;
            this.menuItems.Add(menuItem);
        }
    }

    /// <summary>
    /// Erases oll the information of the previous display.
    /// Calculates the inner size (without the margins) of the control.
    /// </summary>
    protected override void OnBeforeDisplay()
    {
        if (menuItems.SelectableItemsCount == 0)
            throw new ApplicationException("There are no menu items to be displayed.");

        closeWasRequested = false;
        itemsLocation = Location.Origin;

        //for (int i = 0; i < InnerSize.Height; i++)
        //    Console.WriteLine();

        //Console.SetCursorPosition(0, Console.CursorTop - InnerSize.Height);

        base.OnBeforeDisplay();
    }

    /// <summary>
    /// Displays the menu and waits for the user to choose an item.
    /// This method blocks until the user chooses an item.
    /// </summary>
    protected override void DoDisplayContent(ControlDisplay display)
    {
        menuItems.CurrentIndexChanged += HandleCurrentIndexChanged;

        try
        {
            itemsSize = CalculateItemsSize();
            menuSize = itemsSize + new Size(Padding.Left + Padding.Right, Padding.Top + Padding.Bottom);

            itemsLocation = CalculateMenuLocation();

            IEnumerable<IMenuItem> visibleMenuItems = menuItems
                .Where(x => x.IsVisible);

            foreach (IMenuItem menuItem in visibleMenuItems)
            {
                int left = itemsLocation.Left;
                int top = Console.CursorTop;

                Console.SetCursorPosition(left, top);

                Size menuItemSize = new(itemsSize.Width, 1);
                menuItem.Display(menuItemSize, false);

                Console.WriteLine();
            }

            itemsLocation = new Location(itemsLocation.Left, Console.CursorTop - itemsSize.Height);

            if (SelectFirstByDefault)
                menuItems.SelectFirst();

            ReadUserSelection();
        }
        finally
        {
            if (!KeepHighlightingOnClose)
                menuItems.SelectNone();

            menuItems.CurrentIndexChanged -= HandleCurrentIndexChanged;

            int lastMenuLine = itemsLocation.Top + itemsSize.Height - 1;
            Console.SetCursorPosition(0, lastMenuLine);
            Console.WriteLine();
        }
    }

    private void HandleCurrentIndexChanged(object sender, CurrentIndexChangedEventArgs e)
    {
        if (e.PreviousIndex.HasValue)
            DrawMenuItem(e.PreviousIndex.Value);

        if (e.CurrentIndex.HasValue)
            DrawMenuItem(e.CurrentIndex.Value);
    }

    private Location CalculateMenuLocation()
    {
        HorizontalAlignment calculatedHorizontalAlignment = CalculateHorizontalAlignment();

        int menuTop = Console.CursorTop;

        switch (calculatedHorizontalAlignment)
        {
            default:
                return new Location(0, menuTop);

            case HorizontalAlignment.Center:
                {
                    int menuLeft = (Console.BufferWidth - menuSize.Width) / 2;
                    return new Location(menuLeft, menuTop);
                }

            case HorizontalAlignment.Right:
                {
                    int menuLeft = Console.BufferWidth - menuSize.Width;
                    return new Location(menuLeft, menuTop);
                }
        }
    }

    private Size CalculateItemsSize()
    {
        IEnumerable<IMenuItem> visibleMenuItems = menuItems
            .Where(x => x.IsVisible);

        int menuHeight = 0;
        int menuWidth = 0;

        foreach (IMenuItem menuItem in visibleMenuItems)
        {
            menuHeight++;

            if (menuItem.Size.Width > menuWidth)
                menuWidth = menuItem.Size.Width;
        }

        return new Size(menuWidth, menuHeight);
    }

    private void DrawMenuItem(int index)
    {
        IMenuItem menuItemToDraw = menuItems[index];
        int? visibleIndex = menuItems.CalculateVisibleIndex(menuItemToDraw);

        if (visibleIndex.HasValue && visibleIndex.Value >= 0)
        {
            int left = itemsLocation.Left;
            int top = itemsLocation.Top + visibleIndex.Value;

            Console.SetCursorPosition(left, top);

            Size menuItemSize = new(itemsSize.Width, 1);
            bool isHighlighted = menuItemToDraw == menuItems.CurrentItem;

            menuItemToDraw.Display(menuItemSize, isHighlighted);
        }
    }

    private HorizontalAlignment CalculateHorizontalAlignment()
    {
        HorizontalAlignment calculatedHorizontalAlignment = HorizontalAlignment;

        if (calculatedHorizontalAlignment == HorizontalAlignment.Default)
            calculatedHorizontalAlignment = DefaultHorizontalAlignment;

        return calculatedHorizontalAlignment;
    }

    private void ReadUserSelection()
    {
        while (!closeWasRequested)
        {
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
                    {
                        bool isSelectedSuccessfully = SelectCurrentItem();

                        if (isSelectedSuccessfully)
                            return;
                    }

                    break;

                default:
                    bool success = menuItems.SelectItem(keyInfo.Key);
                    if (success)
                    {
                        bool isSelectedSuccessfully = SelectCurrentItem();

                        if (isSelectedSuccessfully)
                            return;
                    }

                    break;
            }
        }
    }

    private bool SelectCurrentItem()
    {
        IMenuItem selectedItem = menuItems.CurrentItem;

        if (selectedItem?.IsEnabled != true)
            return false;

        bool allow = selectedItem.Select();

        if (!allow)
            return false;

        SelectedIndex = menuItems.CurrentVisibleIndex;
        SelectedItem = selectedItem;

        return true;
    }

    protected override void OnAfterDisplay()
    {
        base.OnAfterDisplay();

        //int lastMenuLine = itemsLocation.Top + menuSize.Height - 1;
        //Console.SetCursorPosition(0, lastMenuLine);
        //Console.WriteLine();

        SelectedItem?.Command?.Execute();
    }

    /// <summary>
    /// Call this method to announce the control that it should end its process.
    /// </summary>
    public void RequestClose()
    {
        closeWasRequested = true;
    }
}