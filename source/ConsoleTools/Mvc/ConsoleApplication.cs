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
using DustInTheWind.ConsoleTools.CommandProviders;

namespace DustInTheWind.ConsoleTools.Mvc
{
    /// <summary>
    /// Represents the console application that processes commands.
    /// </summary>
    public class ConsoleApplication
    {
        private readonly ICommandProvider commandProvider;
        private readonly Router router;

        /// <summary>
        /// Gets or sets the <see cref="IServiceProvider"/> that is used to create the controllers.
        /// </summary>
        public IServiceProvider ServiceProvider
        {
            get { return router.ServiceProvider; }
            set { router.ServiceProvider = value; }
        }

        /// <summary>
        /// Gets the routes used to map a command to a controller that will be executed.
        /// </summary>
        public List<Route> Routes => router.Routes;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleApplication"/> class.
        /// </summary>
        public ConsoleApplication()
        {
            commandProvider = new Prompter();
            commandProvider.NewCommand += HandleNewCommand;

            router = new Router(this, commandProvider);
        }

        /// <summary>
        /// Starts to process commands.
        /// This method blocks until the application is stopped.
        /// </summary>
        public void Run()
        {
            commandProvider.Run();
        }

        /// <summary>
        /// Stops processing commands and exits the <see cref="Run"/> method.
        /// </summary>
        public void Exit()
        {
            commandProvider.RequestStop();
        }

        private void HandleNewCommand(object sender, NewCommandEventArgs e)
        {
            try
            {
                IController controller = router.CreateController(e.Command);
                controller.Execute(e.Command.Parameters);
            }
            catch (MissingRouteException ex)
            {
                CustomConsole.WriteLineError("Unknown command: {0}", ex.Command);
            }
            catch (Exception ex)
            {
                CustomConsole.WriteError(ex);
            }
            finally
            {
                CustomConsole.WriteLine();
            }
        }
    }
}