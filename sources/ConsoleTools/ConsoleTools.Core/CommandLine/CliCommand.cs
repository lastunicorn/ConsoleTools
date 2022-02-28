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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DustInTheWind.ConsoleTools.CommandLine
{
    /// <summary>
    /// Represents a command typed by the user at the prompter created by <see cref="Prompter"/> class.
    /// </summary>
    public class CliCommand
    {
        /// <summary>
        /// Gets the name of the command.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the list of parameters associated with the current command.
        /// </summary>
        public ReadOnlyCollection<CliParameter> Parameters { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CliCommand"/> class with
        /// the command name and the list of parameters.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        /// <param name="parameters">The list of parameters associated with the command.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public CliCommand(string name, IEnumerable<CliParameter> parameters)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));

            if (parameters == null)
            {
                Parameters = new List<CliParameter>(0).AsReadOnly();
            }
            else
            {
                if (parameters.Any(x => x == null))
                    throw new ArgumentException("The parameters cannot be null.", nameof(parameters));

                Parameters = parameters.ToList().AsReadOnly();
            }
        }

        /// <summary>
        /// Represents an empty command. Command name is empty string and contains no parameters.
        /// </summary>
        public static CliCommand Empty { get; } = new CliCommand(string.Empty, new CliParameter[0]);

        /// <summary>
        /// Gets a value that specify if the current command has no name and no parameters.
        /// </summary>
        public bool IsEmpty => string.IsNullOrEmpty(Name) && Parameters.Count == 0;

        /// <summary>
        /// Parses a string and creates a new instance of the <see cref="CliCommand"/> from it.
        /// </summary>
        /// <param name="commandText">The string representation of a command.</param>
        /// <returns>A new instance of <see cref="CliCommand"/> created from the string receives as parameter.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static CliCommand Parse(string commandText)
        {
            if (commandText == null)
                return null;

            if (commandText.Length == 0)
                return Empty;

            string[] commandChunks = commandText.ToLower().Split(' ');

            CliParameter[] parameters;

            string commandName = commandChunks.Length > 0
                ? commandChunks[0]
                : string.Empty;

            if (commandChunks.Length > 1)
            {
                parameters = new CliParameter[commandChunks.Length - 1];

                for (int i = 1; i < commandChunks.Length; i++)
                {
                    string[] paramChunks = commandChunks[i].Split(':');
                    string paramName = paramChunks.Length > 0 ? paramChunks[0] : string.Empty;
                    string paramValue = paramChunks.Length > 1 ? paramChunks[1] : string.Empty;

                    parameters[i - 1] = new CliParameter
                    {
                        Name = paramName,
                        Value = paramValue
                    };
                }
            }
            else
            {
                parameters = new CliParameter[0];
            }

            return new CliCommand(commandName, parameters);
        }

        /// <summary>
        /// Returns the sting representation of the current instance.
        /// </summary>
        /// <returns>The sting representation of the current instance.</returns>
        public override string ToString()
        {
            return Name + " " + string.Join(" ", Parameters);
        }
    }
}