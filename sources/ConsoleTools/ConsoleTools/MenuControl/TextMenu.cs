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
    public class TextMenu : Control
    {
        private readonly List<TextMenuItem> menuItems = new List<TextMenuItem>();

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

        /// <summary>
        /// Initialize a new instace of the <see cref="TextMenu"/> calss with
        /// the list of items to be displayed.
        /// </summary>
        /// <param name="menuItems">The list of items to be displayed by the menu.</param>
        public TextMenu(IEnumerable<TextMenuItem> menuItems)
        {
            if (menuItems == null) throw new ArgumentNullException(nameof(menuItems));

            this.menuItems.AddRange(menuItems);
        }

        /// <summary>
        /// Displays the menu and waits for the user to choose an item.
        /// This method blocks until the user chooses an item.
        /// </summary>
        protected override void OnDisplayContent()
        {
            Reset();
            DrawMenu();
            ReadUserSelection();
        }

        private void Reset()
        {
            SelectedIndex = null;
            SelectedVisibleIndex = null;
            SelectedItem = null;
        }

        private void DrawMenu()
        {
            IEnumerable<TextMenuItem> menuItemsToDisplay = menuItems
                .Where(x => x.IsVisible);

            bool existsItems = false;

            foreach (TextMenuItem menuItem in menuItemsToDisplay)
            {
                existsItems = true;

                if (menuItem.IsVisible)
                {
                    menuItem.Display();
                    CustomConsole.WriteLine();
                }
            }

            if (!existsItems)
                throw new ApplicationException("There are no menu items to be displayed.");
        }

        private void ReadUserSelection()
        {
            Console.WriteLine();

            while (true)
            {
                DisplayQuestion();

                string inputValue = Console.ReadLine();

                if (inputValue == null)
                    return;

                if (inputValue.Length == 0)
                    continue;

                Console.WriteLine();

                TextMenuItem selectedMenuItem = menuItems
                    .FirstOrDefault(x => x.Id == inputValue);

                if (selectedMenuItem == null || !selectedMenuItem.IsVisible)
                {
                    CustomConsole.WriteLineWarning(InvalidOptionText);
                    Console.WriteLine();
                    continue;
                }

                if (!selectedMenuItem.CanBeSelected())
                {
                    CustomConsole.WriteLineWarning(OptionDisabledText);
                    Console.WriteLine();
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

        private void DisplayQuestion()
        {
            CustomConsole.WriteEmphasies(QuestionText);
            CustomConsole.WriteEmphasies(Separator);

            if (SpaceAfterQuestion > 0)
            {
                string space = new string(' ', SpaceAfterQuestion);
                Console.Write(space);
            }
        }

        protected override void OnAfterDisplay()
        {
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
    }
}