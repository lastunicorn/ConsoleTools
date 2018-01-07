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

using System;
using System.Runtime.Serialization;
using DustInTheWind.ConsoleTools.CommandProviders;

namespace DustInTheWind.ConsoleTools.Mvc
{
    /// <inheritdoc />
    /// <summary>
    /// Exception thrown when there is no route for the command provided by the user.
    /// </summary>
    public class MissingRouteException : Exception
    {
        private const string DefaultMessage = "There is no route declared for command {0}.";

        /// <summary>
        /// Gets the command for which no route exists.
        /// </summary>
        public CliCommand Command { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MissingRouteException"/> class with
        /// the command for which no route was found.
        /// </summary>
        /// <param name="command">The command for which no route was found.</param>
        public MissingRouteException(CliCommand command)
            : base(string.Format(DefaultMessage, command.Name))
        {
            Command = command;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MissingRouteException"/> class with
        /// the command for which no route was found and
        /// a message that describes the error.
        /// </summary>
        /// <param name="command">The command for which no route was found.</param>
        /// <param name="message">A message that describes the error.</param>
        public MissingRouteException(CliCommand command, string message)
            : base(message)
        {
            Command = command;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MissingRouteException"/> class with
        /// the command for which no route was found and
        /// a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="command">The command for which no route was found.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public MissingRouteException(CliCommand command, Exception innerException)
            : base(DefaultMessage, innerException)
        {
            Command = command;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MissingRouteException"/> class with
        /// the command for which no route was found,
        /// a message that describes the error and
        /// a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="command">The command for which no route was found.</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public MissingRouteException(CliCommand command, string message, Exception innerException)
            : base(message, innerException)
        {
            Command = command;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MissingRouteException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected MissingRouteException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}