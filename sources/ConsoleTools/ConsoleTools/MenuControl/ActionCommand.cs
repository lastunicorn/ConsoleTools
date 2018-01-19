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

namespace DustInTheWind.ConsoleTools.MenuControl
{
    /// <summary>
    /// Represents a command that executes an action.
    /// </summary>
    public class ActionCommand : ICommand
    {
        /// <summary>
        /// Gets or sets the action to be executed by the command.
        /// </summary>
        public Action Action { get; set; }

        /// <summary>
        /// Gets or sets the function that decides if the command is active.
        /// </summary>
        public Func<bool> ActiveAction { get; set; }

        /// <summary>
        /// Gets a value that specifies if the current instance can be executed.
        /// </summary>
        public bool IsActive => ActiveAction?.Invoke() ?? true;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionCommand"/> class with
        /// null action.
        /// </summary>
        public ActionCommand()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionCommand"/> class with
        /// the action to be run when the command is executed.
        /// </summary>
        /// <param name="action">The action to be run when the command is executed.</param>
        public ActionCommand(Action action)
        {
            Action = action;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionCommand"/> class with
        /// the action to be run when the command is executed and
        /// the function that decides if the command is active.
        /// </summary>
        /// <param name="action">The action to be run when the command is executed.</param>
        /// <param name="activeAction">The function that decides if the command is active.</param>
        public ActionCommand(Action action, Func<bool> activeAction)
        {
            Action = action;
            ActiveAction = activeAction;
        }

        /// <summary>
        /// Executes the current instance.
        /// </summary>
        public void Execute()
        {
            if (IsActive)
                Action?.Invoke();
        }
    }
}