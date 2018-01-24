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
using DustInTheWind.ConsoleTools.MenuControl;

namespace DustInTheWind.ConsoleTools.CommandProviders
{
    public class TextMenuCommandProvider : ICommandProvider
    {
        private volatile bool stopWasRequested;

        private readonly TextMenu textMenu;

        /// <summary>
        /// Event raised when the user selects a new menu item.
        /// </summary>
        public event EventHandler<NewCommandEventArgs> NewCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextMenuCommandProvider"/> with
        /// the list of menu items from which the user must choose.
        /// </summary>
        public TextMenuCommandProvider(IEnumerable<TextMenuItem> menuItems)
        {
            if (menuItems == null) throw new ArgumentNullException(nameof(menuItems));

            textMenu = new TextMenu(menuItems);
        }

        public void Display()
        {
            stopWasRequested = false;

            do
            {
                textMenu.Display();

                string selectedItemId = textMenu.SelectedItem.Id;
                CliCommand command = new CliCommand(selectedItemId, new List<UserCommandParameter>());

                try
                {
                    NewCommandEventArgs eva = new NewCommandEventArgs(command);
                    OnNewCommand(eva);

                    if (eva.Exit)
                        stopWasRequested = true;
                }
                catch (Exception ex)
                {
                    CustomConsole.WriteError(ex);
                }
            }
            while (!stopWasRequested);
        }

        /// <summary>
        /// Creates a single command (<see cref="CliCommand"/>) from the menu item selected by the user and returns it.
        /// </summary>
        /// <returns>A <see cref="CliCommand"/> object representing the menu item selected by the user.</returns>
        public CliCommand DisplayOnce()
        {
            textMenu.Display();

            return CliCommand.Parse(textMenu.SelectedItem.Id);
        }

        /// <summary>
        /// Sets the stop flag.
        /// The loop will exit next time when it checks the stop flag (after the user selects an item).
        /// </summary>
        public void RequestClose()
        {
            stopWasRequested = true;
        }

        /// <summary>
        /// Raises the <see cref="NewCommand"/> event.
        /// </summary>
        protected virtual void OnNewCommand(NewCommandEventArgs e)
        {
            NewCommand?.Invoke(this, e);
        }
    }
}