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

using System.Collections.Generic;
using DustInTheWind.ConsoleTools.Demo.Mvc.Controllers;
using DustInTheWind.ConsoleTools.Mvc;

namespace DustInTheWind.ConsoleTools.Demo.Mvc
{
    internal static class Program
    {
        private static void Main()
        {
            DisplayApplicationHeader();
            RunDemo();
        }

        private static void DisplayApplicationHeader()
        {
            CustomConsole.WriteLineEmphasies("ConsoleTools Demo - Mvc");
            CustomConsole.WriteLineEmphasies("===============================================================================");
            CustomConsole.WriteLine();
            CustomConsole.WriteLine("This demo shows the usage of the MVC frameworkfor Console.");
            CustomConsole.WriteLine();
        }

        private static void RunDemo()
        {
            ConsoleApplication consoleApplication = new ConsoleApplication();

            List<Route> routes = new List<Route>
            {
                new Route("q", typeof(ExitController)),
                new Route("quit", typeof(ExitController)),
                new Route("exit", typeof(ExitController)),
                new Route("help", typeof(HelpController)),
                new Route("whale", typeof(WhaleController)),
                new Route("whales", typeof(WhaleController)),
                new Route("prompter", typeof(PrompterController))
            };

            consoleApplication.ConfigureRoutes(routes);

            consoleApplication.Run();
        }
    }
}
