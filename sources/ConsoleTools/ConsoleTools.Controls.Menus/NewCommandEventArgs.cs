// ConsoleTools
// Copyright (C) 2017-2020 Dust in the Wind
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
using DustInTheWind.ConsoleTools.CommandLine;

namespace DustInTheWind.ConsoleTools.Controls.Menus
{
    /// <summary>
    /// Provides data for NewCommand event.
    /// </summary>
    public class NewCommandEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the new <see cref="CliCommand"/> instance.
        /// </summary>
        public CliCommand Command { get; }

        /// <summary>
        /// Gets or sets a value that specifies if the command was already handled.
        /// </summary>
        public bool IsHandled { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewCommandEventArgs"/> class.
        /// </summary>
        public NewCommandEventArgs(CliCommand command)
        {
            Command = command;
        }
    }
}