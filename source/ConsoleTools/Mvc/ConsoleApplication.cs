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
using DustInTheWind.ConsoleTools.CommandProviders;

namespace DustInTheWind.ConsoleTools.Mvc
{
    public class ConsoleApplication
    {
        private readonly ICommandProvider commandProvider;
        private readonly Router router;

        public IServiceProvider ServiceProvider
        {
            set { router.ServiceProvider = value; }
        }

        public ConsoleApplication()
        {
            commandProvider = new Prompter();
            commandProvider.NewCommand += HandleNewCommand;

            router = new Router(this, commandProvider);
        }

        public void ConfigureRoutes(IEnumerable<Route> routes)
        {
            router.Routes.AddRange(routes);
        }

        public void Run()
        {
            commandProvider.Run();
        }

        public void Exit()
        {
            commandProvider.RequestStop();
        }

        private void HandleNewCommand(object sender, NewCommandEventArgs e)
        {
            try
            {
                IController controller = router.CreateController(e.Command);
                controller.Execute();
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