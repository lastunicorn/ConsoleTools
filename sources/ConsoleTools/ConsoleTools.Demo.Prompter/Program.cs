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
using DustInTheWind.ConsoleTools.CommandProviders;
using DustInTheWind.ConsoleTools.Demo.Prompter.Controllers;

namespace DustInTheWind.ConsoleTools.Demo.Prompter
{
    internal class Program
    {
        private static CommandProviders.Prompter prompter;

        private static void Main()
        {
            DisplayApplicationHeader();
            StartDemo();
        }

        private static void DisplayApplicationHeader()
        {
            CustomConsole.WriteLineEmphasies("ConsoleTools Demo - Prompter");
            CustomConsole.WriteLineEmphasies("===============================================================================");
            CustomConsole.WriteLine();

            CustomConsole.WriteEmphasies("Note: ");
            CustomConsole.WriteLine("type 'help' for a list of commands.");
            CustomConsole.WriteLine();
        }

        private static void StartDemo()
        {
            prompter = new CommandProviders.Prompter();
            prompter.NewCommand += HandleNewCommand;

            prompter.Display();
        }

        private static void HandleNewCommand(object sender, NewCommandEventArgs e)
        {
            try
            {
                IController controller = CreateController(e.Command);
                controller.Execute(e.Command.Parameters);
            }
            catch (Exception ex)
            {
                CustomConsole.WriteError(ex);
            }
        }

        private static IController CreateController(CliCommand command)
        {
            switch (command.Name)
            {
                case "q":
                case "quit":
                case "exit":
                    return new ExitController(prompter);

                case "help":
                    return new HelpController();

                case "whale":
                case "whales":
                    return new WhaleController();

                case "prompter":
                    return new PrompterController(prompter);

                default:
                    return new UnknownCommandController(command);
            }
        }
    }
}