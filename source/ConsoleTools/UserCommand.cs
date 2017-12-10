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
using System.Linq;

namespace DustInTheWind.ConsoleTools
{
    /// <summary>
    /// Represents a command typed by the user at the prompter created by <see cref="Prompter"/> class.
    /// </summary>
    public class UserCommand
    {
        /// <summary>
        /// Gets the name of the command.
        /// </summary>
        public string CommandName { get; }

        /// <summary>
        /// Gets the list of parameters associated with the current command.
        /// </summary>
        public string[] Parameters { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserCommand"/> class with
        /// the command name and the list of parameters.
        /// </summary>
        /// <param name="commandName">The name of the command.</param>
        /// <param name="parameters">The list of parameters associated with the command.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public UserCommand(string commandName, string[] parameters)
        {
            if (commandName == null) throw new ArgumentNullException(nameof(commandName));

            CommandName = commandName;

            if (parameters == null)
            {
                Parameters = new string[0];
            }
            else
            {
                if (parameters.Any(x => x == null))
                    throw new ArgumentException("The parameters can not be null.", nameof(parameters));

                Parameters = parameters;
            }
        }

        /// <summary>
        /// Represents an empty command. Command name is empty string and contains no parameters.
        /// </summary>
        public static UserCommand Empty { get; } = new UserCommand(string.Empty, new string[0]);

        /// <summary>
        /// Parses a string and creates a new instance of the <see cref="UserCommand"/> from it.
        /// </summary>
        /// <param name="commandText">The string representation of a command.</param>
        /// <returns>A new instance of <see cref="UserCommand"/> created from the string receives as parameter.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static UserCommand Parse(string commandText)
        {
            if (commandText == null) throw new ArgumentNullException(nameof(commandText));

            string[] tmp = commandText.ToLower().Split(' ');

            string[] parameters;

            string commandName = tmp.Length > 0
                ? tmp[0]
                : string.Empty;

            if (tmp.Length > 1)
            {
                parameters = new string[tmp.Length - 1];
                Array.Copy(tmp, 1, parameters, 0, parameters.Length);
            }
            else
            {
                parameters = new string[0];
            }

            return new UserCommand(commandName, parameters);
        }

        /// <summary>
        /// Returns the sting representation of the current instance.
        /// </summary>
        /// <returns>The sting representation of the current instance.</returns>
        public override string ToString()
        {
            //return "Command: " + this.commandName + " Parameters: { " + string.Join(" } { ", this.parameters) + " }";
            return CommandName + " " + string.Join(" ", Parameters);
        }
    }
}