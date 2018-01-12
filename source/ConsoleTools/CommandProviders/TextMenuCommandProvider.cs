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
// Bugs or fearure requests
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

        public event EventHandler<NewCommandEventArgs> NewCommand;

        public TextMenuCommandProvider(IEnumerable<TextMenuItem> menuItems)
        {
            if (menuItems == null) throw new ArgumentNullException(nameof(menuItems));

            textMenu = new TextMenu(menuItems);
        }

        public void Run()
        {
            stopWasRequested = false;

            do
            {
                textMenu.Display();

                CliCommand command = CliCommand.Parse(textMenu.SelectedItem.Id);

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

        public CliCommand RunOnce()
        {
            textMenu.Display();

            return CliCommand.Parse(textMenu.SelectedItem.Id);
        }

        public void RequestStop()
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
