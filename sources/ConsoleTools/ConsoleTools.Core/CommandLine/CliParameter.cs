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

namespace DustInTheWind.ConsoleTools.CommandLine
{
    /// <summary>
    /// Represents a parameter for the <see cref="CliCommand"/> class.
    /// </summary>
    public class CliParameter
    {
        /// <summary>
        /// Gets or sets the name of the parameter.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value of the parameter.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Returns the string representation of the current <see cref="CliParameter"/> instance.
        /// </summary>
        public override string ToString()
        {
            return Name + "=" + Value;
        }
    }
}