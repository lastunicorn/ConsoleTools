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
using System.Linq;
using System.Reflection;
using DustInTheWind.ConsoleTools.CommandProviders;

namespace DustInTheWind.ConsoleTools.Mvc
{
    public class Router
    {
        private readonly ConsoleApplication consoleApplication;
        private readonly ICommandProvider commandProvider;

        public List<Route> Routes { get; } = new List<Route>();

        public IServiceProvider ServiceProvider { get; set; }

        public Router(ConsoleApplication consoleApplication, ICommandProvider commandProvider)
        {
            if (consoleApplication == null) throw new ArgumentNullException(nameof(consoleApplication));
            if (commandProvider == null) throw new ArgumentNullException(nameof(commandProvider));

            this.consoleApplication = consoleApplication;
            this.commandProvider = commandProvider;
        }

        public IController CreateController(CliCommand command)
        {
            Route route = Routes.FirstOrDefault(x => x.CommandName == command.Name);

            if (route == null)
                throw new MissingRouteException(command);

            Type controllerType = route.ControllerType;

            return ServiceProvider == null
                ? InstatiateControllerByReflection(command, controllerType)
                : (IController)ServiceProvider.GetService(controllerType);
        }

        private IController InstatiateControllerByReflection(CliCommand command, Type controllerType)
        {
            ConstructorInfo[] constructors = controllerType.GetConstructors();

            // Search for parameterless constructor
            ConstructorInfo goodConstructor = constructors.FirstOrDefault(x => !x.GetParameters().Any());

            List<object> parameters = new List<object>();

            if (goodConstructor == null)
            {
                foreach (ConstructorInfo constructor in constructors)
                {
                    parameters.Clear();
                    bool invalidParameter = false;

                    foreach (ParameterInfo parameterInfo in constructor.GetParameters())
                    {
                        if (parameterInfo.ParameterType.IsAssignableFrom(typeof(ConsoleApplication)))
                            parameters.Add(consoleApplication);
                        else if (parameterInfo.ParameterType.IsAssignableFrom(typeof(ICommandProvider)))
                            parameters.Add(commandProvider);
                        else if (parameterInfo.ParameterType.IsAssignableFrom(typeof(CliCommand)))
                            parameters.Add(command);
                        else
                        {
                            invalidParameter = true;
                            break;
                        }
                    }

                    if (invalidParameter)
                        continue;

                    goodConstructor = constructor;
                    break;
                }
            }

            if (goodConstructor == null)
            {
                string message = string.Format("The controller {0} has no constructor that can be used.", controllerType.FullName);
                throw new ApplicationException(message);
            }

            return (IController)goodConstructor.Invoke(parameters.ToArray());
        }
    }
}