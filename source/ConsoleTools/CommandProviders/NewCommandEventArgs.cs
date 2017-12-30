// ConsoleTools
// Copyright (C) 2017 Dust in the Wind
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

namespace DustInTheWind.ConsoleTools.CommandProviders
{
    /// <summary>
    /// Provides data for NewCommand event.
    /// </summary>
    public class NewCommandEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the new <see cref="CliCommand"/> object.
        /// </summary>
        public CliCommand Command { get; }

        /// <summary>
        /// Gets or sets a value that specifies if the <see cref="ICommandProvider"/> must be stopped after the current command.
        /// </summary>
        public bool Exit { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewCommandEventArgs"/> class.
        /// </summary>
        public NewCommandEventArgs(CliCommand command)
        {
            Command = command;
        }
    }
}