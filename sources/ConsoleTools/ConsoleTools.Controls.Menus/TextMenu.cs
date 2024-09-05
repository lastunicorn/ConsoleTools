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

// --------------------------------------------------------------------------------
// Bugs or feature requests
// --------------------------------------------------------------------------------
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new/choose

using System;
using System.Collections.Generic;
using System.Linq;

namespace DustInTheWind.ConsoleTools.Controls.Menus;

/// <summary>
/// Displays a menu that asks the user to select an item by typing its id and executes the command associated with the item.
/// </summary>
/// <remarks>
/// Alternatively, if there is no Command associated with the item, the selected item can be retrieved and some decisions can be taken based on it.
/// </remarks>
public class TextMenu : ErasableControl, IRepeatableSupport
{
    private bool closeWasRequested;
    private readonly List<TextMenuItem> menuItems = new();
    private TextMenuItem selectedItem;

    /// <summary>
    /// Gets the collection of items contained by the current instance.
    /// </summary>
    public IReadOnlyCollection<TextMenuItem> MenuItems => menuItems;

    /// <summary>
    /// Gets or sets the title to be displayed at the top of the control, before the list of items.
    /// </summary>
    public string TitleText { get; set; }

    /// <summary>
    /// Gets or sets the foreground color for the title.
    /// </summary>
    public ConsoleColor? TitleForegroundColor { get; set; }

    /// <summary>
    /// Gets or sets the background color for the title.
    /// </summary>
    public ConsoleColor? TitleBackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the text displayed after the menu to ask the user to choose an item.
    /// </summary>
    public InlineTextBlock QuestionText { get; set; } = new()
    {
        Text = TextMenuResources.QuestionText,
        TextFormat = "{0}:",
        ForegroundColor = CustomConsole.EmphasizedColor,
        MarginRight = 1
    };

    /// <summary>
    /// Gets or sets the text displayed when the user chooses an nonexistent option.
    /// </summary>
    public string InvalidOptionText { get; set; } = TextMenuResources.InvalidOptionMessage;

    /// <summary>
    /// Gets or sets the text displayed when the user chooses an option that is disabled.
    /// </summary>
    public string OptionDisabledText { get; set; } = TextMenuResources.OptionDisabledMessage;

    /// <summary>
    /// Gets the item that was selected by the user.
    /// </summary>
    public TextMenuItem SelectedItem
    {
        get => selectedItem;
        set
        {
            selectedItem = value;

            SelectedIndex = menuItems.IndexOf(selectedItem);

            SelectedVisibleIndex = menuItems
                .Take(SelectedIndex.Value)
                .Count(x => x.IsVisible);
        }
    }

    /// <summary>
    /// Gets the index of the selected menu item.
    /// The index is calculated based on the visible list of items.
    /// </summary>
    public int? SelectedVisibleIndex { get; private set; }

    /// <summary>
    /// Gets the index of the selected menu item.
    /// </summary>
    public int? SelectedIndex { get; private set; }

    public override int NaturalContentWidth => 0;

    /// <summary>
    /// Event raised when the display of the control finished.
    /// </summary>
    public event EventHandler Closed;

    /// <summary>
    /// Initialize a new instance of the <see cref="TextMenu"/> class.
    /// </summary>
    public TextMenu()
    {
    }

    /// <summary>
    /// Initialize a new instance of the <see cref="TextMenu"/> class with
    /// the list of items to be displayed.
    /// </summary>
    /// <param name="menuItems">The list of items to be displayed by the menu.</param>
    public TextMenu(IEnumerable<TextMenuItem> menuItems)
    {
        if (menuItems == null) throw new ArgumentNullException(nameof(menuItems));

        this.menuItems.AddRange(menuItems.Where(x => x != null));
    }

    /// <summary>
    /// Adds a new item to the current instance.
    /// </summary>
    /// <param name="menuItem">The item to be added to the current instance.</param>
    public void AddItem(TextMenuItem menuItem)
    {
        if (menuItem == null) throw new ArgumentNullException(nameof(menuItem));

        menuItems.Add(menuItem);
    }

    /// <summary>
    /// Adds a list of items to the current instance.
    /// </summary>
    /// <param name="items">The list of items to be added to the current instance.</param>
    public void AddItems(IEnumerable<TextMenuItem> items)
    {
        if (items == null) throw new ArgumentNullException(nameof(items));

        foreach (TextMenuItem menuItem in items)
        {
            if (menuItem == null)
                throw new ArgumentException("Null items are not accepted.", nameof(items));

            menuItems.Add(menuItem);
        }
    }

    /// <summary>
    /// Erases all the information of the previous display.
    /// </summary>
    protected override void OnBeforeRender(BeforeRenderEventArgs e)
    {
        bool existsItems = menuItems.Any(x => x.IsVisible);
        if (!existsItems)
            throw new ApplicationException("There are no menu items to be displayed.");

        SelectedItem = null;
        closeWasRequested = false;

        base.OnBeforeRender(e);
    }

    /// <summary>
    /// Returns a renderer object that is able to render the current <see cref="TextMenu"/>
    /// instance using the specified <see cref="IDisplay"/>.
    /// </summary>
    /// <returns>The <see cref="IRenderer"/> instance.</returns>
    public override IRenderer GetRenderer(IDisplay display, RenderingOptions renderingOptions = null)
    {
        return new TextMenuRenderer(this, display, renderingOptions);
    }

    /// <summary>
    /// Executes the selected item.
    /// </summary>
    protected override void OnAfterRender()
    {
        base.OnAfterRender();

        OnClosed();

        SelectedItem?.Execute();
    }

    /// <summary>
    /// Displays a menu containing the specified menu items and
    /// returns the item selected by the user.
    /// </summary>
    public static TextMenuItem QuickDisplay(IEnumerable<TextMenuItem> menuItems)
    {
        TextMenu textMenu = new(menuItems);
        textMenu.Display();
        return textMenu.SelectedItem;
    }

    /// <summary>
    /// An internal flag is set to request that the display process to finish.
    /// </summary>
    public void RequestClose()
    {
        closeWasRequested = true;
    }

    /// <summary>
    /// Raises the <see cref="Closed"/> event.
    /// </summary>
    protected virtual void OnClosed()
    {
        Closed?.Invoke(this, EventArgs.Empty);
    }

    public override string ToString()
    {
        return $"TextMenu has {MenuItems.Count} items.";
    }
}