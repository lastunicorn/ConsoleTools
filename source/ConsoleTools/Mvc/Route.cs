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

namespace DustInTheWind.ConsoleTools.Mvc
{
    public class Route
    {
        public string CommandName { get; }
        public Type ControllerType { get; }

        public Route(string commandName, Type controllerType)
        {
            if (commandName == null) throw new ArgumentNullException(nameof(commandName));
            if (controllerType == null) throw new ArgumentNullException(nameof(controllerType));

            bool typeIsController = typeof(IController).IsAssignableFrom(controllerType);

            if (!typeIsController)
                throw new ArgumentException("Controller type must implement IController interface.", nameof(controllerType));

            CommandName = commandName;
            ControllerType = controllerType;
        }
    }
}