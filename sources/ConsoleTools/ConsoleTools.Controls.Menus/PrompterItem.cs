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

using DustInTheWind.ConsoleTools.CommandLine;

namespace DustInTheWind.ConsoleTools.Controls.Menus
{
    /// <summary>
    /// Contains information about a cli command that the prompter can handle.
    /// </summary>
    public class PrompterItem
    {
        /// <summary>
        /// Gets or sets the name of the cli command.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The command that will be executed when this item is selected.
        /// </summary>
        public IPrompterCommand Command { get; set; }

        /// <summary>
        /// Executes the associated command.
        /// </summary>
        /// <param name="cliCommand"></param>
        public void Execute(CliCommand cliCommand)
        {
            Command?.Execute(cliCommand);
        }
    }
}