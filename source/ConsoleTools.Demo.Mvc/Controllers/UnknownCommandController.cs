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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DustInTheWind.ConsoleTools.CommandProviders;
using DustInTheWind.ConsoleTools.Mvc;

namespace DustInTheWind.ConsoleTools.Demo.Mvc.Controllers
{
    internal class UnknownCommandController : IController
    {
        private readonly CliCommand command;

        public UnknownCommandController(CliCommand command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
            this.command = command;
        }

        public void Execute(ReadOnlyCollection<UserCommandParameter> parameters)
        {
            CustomConsole.WriteLineError("Unknown command: " + command, ConsoleColor.DarkYellow);
        }
    }
}