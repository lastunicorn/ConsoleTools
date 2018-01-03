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
        
        public MissingRouteException(CliCommand command)
            : base(string.Format(DefaultMessage, command.Name))
        {
            Command = command;
        }

        public MissingRouteException(CliCommand command, string message)
            : base(message)
        {
            Command = command;
        }

        public MissingRouteException(CliCommand command, string message, Exception innerException)
            : base(message, innerException)
        {
            Command = command;
        }

        protected MissingRouteException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}