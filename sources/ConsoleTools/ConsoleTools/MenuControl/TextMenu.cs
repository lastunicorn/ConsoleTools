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

namespace DustInTheWind.ConsoleTools.MenuControl
{
    /// <summary>
    /// Displays a menu that asks the user to select an item by typing its id and executes the command associated with the item.
    /// </summary>
    /// <remarks>
    /// Alternatively, if there is no Command associated with the item, the selected item can be retrieved and some decisions can be taken based on it.
    /// </remarks>
    public class TextMenu : ErasableControl, IRepeatableControl
    {
        private bool closeWasRequested;

        /// <summary>
        /// Gets the list of items contained by the current instance.
        /// </summary>
        private readonly List<TextMenuItem> menuItems = new List<TextMenuItem>();

        /// <summary>
        /// Gets or sets the title to be displayed at the top of the control, before the list of items.
        /// </summary>
        public TextBlock Title { get; set; }

        /// <summary>
        /// Gets or sets the text displayed after the menu to ask the user to choose an item.
        /// </summary>
        public string QuestionText { get; set; } = TextMenuResources.QuestionText;

        /// <summary>
        /// Gets or sets the separator text displayed after the question.
        /// Default value ":"
        /// </summary>
        public string Separator { get; set; } = ":";

        /// <summary>
        /// Gets or sets the number of spaces displayed after the question, before the user writes his choice.
        /// Default value: 1
        /// </summary>
        public int SpaceAfterQuestion { get; set; } = 1;

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
        public TextMenuItem SelectedItem { get; private set; }

        /// <summary>
        /// Gets the index of the selected menu item.
        /// The index is calculated based on the visible list of items.
        /// </summary>
        public int? SelectedVisibleIndex { get; private set; }

        /// <summary>
        /// Gets the index of the selected menu item.
        /// </summary>
        public int? SelectedIndex { get; private set; }

        public event EventHandler CloseNeeded;

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

        public void AddItem(TextMenuItem menuItem)
        {
            if (menuItem == null) throw new ArgumentNullException(nameof(menuItem));

            menuItems.Add(menuItem);
        }

        public void AddItems(IEnumerable<TextMenuItem> menuItems)
        {
            if (menuItems == null) throw new ArgumentNullException(nameof(menuItems));

            this.menuItems.AddRange(menuItems.Where(x => x != null));
        }

        /// <summary>
        /// Erases oll the information of the previous display.
        /// </summary>
        protected override void OnBeforeDisplay()
        {
            Reset();

            base.OnBeforeDisplay();
        }

        private void Reset()
        {
            SelectedIndex = null;
            SelectedVisibleIndex = null;
            SelectedItem = null;

            InnerSize = Size.Empty;
        }

        /// <summary>
        /// Displays the menu and waits for the user to choose an item.
        /// This method blocks until the user chooses an item.
        /// </summary>
        protected override void DoDisplayContent()
        {
            DrawTitle();
            DrawMenu();
            ReadUserSelection();
        }

        private void DrawTitle()
        {
            if (Title != null)
            {
                Title.Display();

                Size titleSize = Title.CalculateSize();
                InnerSize = InnerSize.InflateHeight(titleSize.Height);
            }
        }

        private void DrawMenu()
        {
            IEnumerable<TextMenuItem> menuItemsToDisplay = menuItems
                .Where(x => x != null && x.IsVisible);

            bool existsItems = false;

            foreach (TextMenuItem menuItem in menuItemsToDisplay)
            {
                existsItems = true;

                menuItem.Display();
                CustomConsole.WriteLine();

                InnerSize = InnerSize.InflateHeight(menuItem.Size.Height);
            }

            if (!existsItems)
                throw new ApplicationException("There are no menu items to be displayed.");
        }

        private void ReadUserSelection()
        {
            Console.WriteLine();
            InnerSize = InnerSize.InflateHeight(1);

            while (!closeWasRequested)
            {
                DisplayQuestion();

                string inputValue = Console.ReadLine();

                if (inputValue == null)
                {
                    OnCloseNeeded();
                    return;
                }

                if (inputValue.Length == 0)
                    continue;

                TextMenuItem selectedMenuItem = menuItems
                    .FirstOrDefault(x => x.Id == inputValue);

                if (selectedMenuItem == null || !selectedMenuItem.IsVisible)
                {
                    DisplayInvalidOptionWarning();
                    continue;
                }

                if (!selectedMenuItem.CanBeSelected())
                {
                    DisplayDisabledItemWarning();
                    continue;
                }

                SelectedItem = selectedMenuItem;
                SelectedIndex = menuItems.IndexOf(selectedMenuItem);
                SelectedVisibleIndex = menuItems
                    .Take(SelectedIndex.Value)
                    .Count(x => x != null && x.IsVisible);

                return;
            }
        }

        private void DisplayInvalidOptionWarning()
        {
            CustomConsole.WriteLineWarning(InvalidOptionText);
            Console.WriteLine();

            InnerSize = InnerSize.InflateHeight(2);
        }

        private void DisplayDisabledItemWarning()
        {
            CustomConsole.WriteLineWarning(OptionDisabledText);
            Console.WriteLine();

            InnerSize = InnerSize.InflateHeight(2);
        }

        private void DisplayQuestion()
        {
            int textLength = 0;

            CustomConsole.WriteEmphasies(QuestionText);
            textLength += QuestionText?.Length ?? 0;

            CustomConsole.WriteEmphasies(Separator);
            textLength += Separator?.Length ?? 0;

            if (SpaceAfterQuestion > 0)
            {
                string space = new string(' ', SpaceAfterQuestion);
                Console.Write(space);

                textLength += space.Length;
            }

            int questionHeight = (int)Math.Ceiling((double)textLength / Console.BufferWidth);
            InnerSize = InnerSize.InflateHeight(questionHeight);
        }

        /// <summary>
        /// Executes the selected item.
        /// </summary>
        protected override void OnAfterDisplay()
        {
            base.OnAfterDisplay();

            SelectedItem?.Select();
        }

        /// <summary>
        /// Displays a menu containing the specified menu items and
        /// returns the item selected by the user.
        /// </summary>
        public static TextMenuItem QuickDisplay(IEnumerable<TextMenuItem> menuItems)
        {
            TextMenu textMenu = new TextMenu(menuItems);
            textMenu.Display();
            return textMenu.SelectedItem;
        }

        public void RequestClose()
        {
            closeWasRequested = true;
        }

        protected virtual void OnCloseNeeded()
        {
            CloseNeeded?.Invoke(this, EventArgs.Empty);
        }
    }
}