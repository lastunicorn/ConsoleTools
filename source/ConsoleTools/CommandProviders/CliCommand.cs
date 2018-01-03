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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DustInTheWind.ConsoleTools.CommandProviders
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
        public ReadOnlyCollection<UserCommandParameter> Parameters { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CliCommand"/> class with
        /// the command name and the list of parameters.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        /// <param name="parameters">The list of parameters associated with the command.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public CliCommand(string name, IEnumerable<UserCommandParameter> parameters)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            Name = name;

            if (parameters == null)
            {
                Parameters = new List<UserCommandParameter>(0).AsReadOnly();
            }
            else
            {
                if (parameters.Any(x => x == null))
                    throw new ArgumentException("The parameters can not be null.", nameof(parameters));

                Parameters = parameters.ToList().AsReadOnly();
            }
        }

        /// <summary>
        /// Represents an empty command. Command name is empty string and contains no parameters.
        /// </summary>
        public static CliCommand Empty { get; } = new CliCommand(string.Empty, new UserCommandParameter[0]);

        public bool IsEmpty => string.IsNullOrEmpty(Name) && Parameters.Count == 0;

        /// <summary>
        /// Parses a string and creates a new instance of the <see cref="CliCommand"/> from it.
        /// </summary>
        /// <param name="commandText">The string representation of a command.</param>
        /// <returns>A new instance of <see cref="CliCommand"/> created from the string receives as parameter.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static CliCommand Parse(string commandText)
        {
            if (commandText == null) throw new ArgumentNullException(nameof(commandText));

            string[] commandChunks = commandText.ToLower().Split(' ');

            UserCommandParameter[] parameters;

            string commandName = commandChunks.Length > 0
                ? commandChunks[0]
                : string.Empty;

            if (commandChunks.Length > 1)
            {
                parameters = new UserCommandParameter[commandChunks.Length - 1];

                for (int i = 1; i < commandChunks.Length; i++)
                {
                    string[] paramChunks = commandChunks[i].Split(' ');
                    string paramName = paramChunks.Length > 0 ? paramChunks[0] : string.Empty;
                    string paramValue = paramChunks.Length > 1 ? paramChunks[1] : string.Empty;

                    parameters[i - 1] = new UserCommandParameter
                    {
                        Name = paramName,
                        Value = paramValue
                    };
                }
            }
            else
            {
                parameters = new UserCommandParameter[0];
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