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
using System.Linq;
using System.Reflection;
using System.Windows.Input;

namespace DustInTheWind.ConsoleTools.Mvc
{
    public class Router
    {
        private readonly ConsoleApplication consoleApplication;
        private readonly Prompter prompter;

        public List<Route> Routes { get; } = new List<Route>();

        public Router(ConsoleApplication consoleApplication, Prompter prompter)
        {
            if (consoleApplication == null) throw new ArgumentNullException(nameof(consoleApplication));
            if (prompter == null) throw new ArgumentNullException(nameof(prompter));

            this.consoleApplication = consoleApplication;
            this.prompter = prompter;
        }

        public IController CreateController(UserCommand command)
        {
            Route route = Routes.FirstOrDefault(x => x.CommandName == command.Name);

            if (route == null)
                throw new MissingRouteException(command);

            Type controllerType = route.ControllerType;

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
                        else if (parameterInfo.ParameterType.IsAssignableFrom(typeof(Prompter)))
                            parameters.Add(prompter);
                        else if (parameterInfo.ParameterType.IsAssignableFrom(typeof(ICommand)))
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