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

    /// <summary>
    /// Gets the list of items contained by the current instance.
    /// </summary>
    private readonly List<TextMenuItem> menuItems = new();

    private TextMenuItem selectedItem;

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
    /// Gets or sets the text displayed when the user chooses an inexistent option.
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
        private set
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

    /// <summary>
    /// Event raised when the current instance cannot be displayed anymore and it is in the "Closed" state.
    /// The <see cref="ControlRepeater"/> must also end its display loop.
    /// </summary>
    public event EventHandler Closed;

    /// <summary>
    /// Initialize a new instace of the <see cref="TextMenu"/> calss.
    /// </summary>
    public TextMenu()
    {
    }

    /// <summary>
    /// Initialize a new instace of the <see cref="TextMenu"/> calss with
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
    /// <param name="menuItems">The list of items to be added to the current instance.</param>
    public void AddItems(IEnumerable<TextMenuItem> menuItems)
    {
        if (menuItems == null) throw new ArgumentNullException(nameof(menuItems));

        bool existsNullItems = menuItems.Any(x => x == null);

        if (existsNullItems)
            throw new ArgumentException("Null items are not accepted.", nameof(menuItems));

        this.menuItems.AddRange(menuItems);
    }

    /// <summary>
    /// Erases all the information of the previous display.
    /// </summary>
    protected override void OnBeforeDisplay()
    {
        bool existsItems = menuItems.Any(x => x.IsVisible);
        if (!existsItems)
            throw new ApplicationException("There are no menu items to be displayed.");

        SelectedItem = null;
        closeWasRequested = false;
        //InnerSize = Size.Empty;

        base.OnBeforeDisplay();
    }

    /// <summary>
    /// Displays the menu and waits for the user to choose an item.
    /// This method blocks until the user chooses an item.
    /// </summary>
    protected override void DoDisplayContent(IDisplay display, RenderingOptions renderingOptions = null)
    {
        if (TitleText != null)
            DrawTitle(display);

        DrawMenu(display);
        ReadUserSelection(display);
    }

    private void DrawTitle(IDisplay display)
    {
        display.WriteLine(TitleText);
        display.WriteLine();
        display.WriteLine();

        //InnerSize = InnerSize.InflateHeight(2);
    }

    private void DrawMenu(IDisplay display)
    {
        IEnumerable<TextMenuItem> menuItemsToDisplay = menuItems
            .Where(x => x.IsVisible);

        foreach (TextMenuItem menuItem in menuItemsToDisplay)
        {
            display.StartLine();
            menuItem.Display();
            display.EndLine();

            //InnerSize = InnerSize.InflateHeight(menuItem.Size.Height);
        }
    }

    private void ReadUserSelection(IDisplay display)
    {
        display.WriteLine();
        //Console.WriteLine();
        //InnerSize = InnerSize.InflateHeight(1);

        while (!closeWasRequested)
        {
            DisplayQuestion();

            string inputValue = Console.ReadLine();

            if (inputValue == null)
            {
                OnClosed();
                return;
            }

            if (inputValue.Length == 0)
                continue;

            TextMenuItem selectedMenuItem = menuItems
                .FirstOrDefault(x => x.Id == inputValue);

            if (selectedMenuItem == null || !selectedMenuItem.IsVisible)
            {
                DisplayInvalidOptionWarning(display);
                continue;
            }

            if (!selectedMenuItem.CanBeSelected())
            {
                DisplayDisabledItemWarning(display);
                continue;
            }

            SelectedItem = selectedMenuItem;

            return;
        }
    }

    private void DisplayQuestion()
    {
        if (QuestionText == null)
            return;

        QuestionText.Display();

        //int textLength = QuestionText.CalculateOuterLength();

        //int questionHeight = (int)Math.Ceiling((double)textLength / Console.BufferWidth);
        //InnerSize = InnerSize.InflateHeight(questionHeight);
    }

    private void DisplayInvalidOptionWarning(IDisplay display)
    {
        display.WriteLine(InvalidOptionText, CustomConsole.WarningColor, CustomConsole.WarningBackgroundColor);
        display.WriteLine();

        //CustomConsole.WriteLineWarning(InvalidOptionText);
        //Console.WriteLine();

        //InnerSize = InnerSize.InflateHeight(2);
    }

    private void DisplayDisabledItemWarning(IDisplay display)
    {
        display.WriteLine(OptionDisabledText, CustomConsole.WarningColor, CustomConsole.WarningBackgroundColor);
        display.WriteLine();

        //CustomConsole.WriteLineWarning(OptionDisabledText);
        //Console.WriteLine();

        //InnerSize = InnerSize.InflateHeight(2);
    }

    /// <summary>
    /// Executes the selected item.
    /// </summary>
    protected override void OnAfterDisplay()
    {
        base.OnAfterDisplay();

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
    /// The <see cref="ControlRepeater"/> calls this method to announce the control that it should end its process.
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
}