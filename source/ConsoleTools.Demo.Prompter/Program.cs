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

using DustInTheWind.ConsoleTools.Demo.Prompter.Controllers;

namespace DustInTheWind.ConsoleTools.Demo.Prompter
{
    internal class Program
    {
        private static ConsoleTools.Prompter prompter;

        private static void Main()
        {
            CustomConsole.WriteLineEmphasies("ConsoleTools Demo - Prompter");
            CustomConsole.WriteLineEmphasies("===============================================================================");
            CustomConsole.WriteLine();

            CustomConsole.WriteEmphasies("Note: ");
            CustomConsole.WriteLine("type 'help' for a list of commands.");
            CustomConsole.WriteLine();

            StartDemo();
        }

        private static void StartDemo()
        {
            prompter = new ConsoleTools.Prompter();
            prompter.NewCommand += HandleNewCommand;

            prompter.Run();
        }

        private static void HandleNewCommand(object sender, NewCommandEventArgs e)
        {
            IController controller = CreateController(e.Command);

            controller.Execute();
            CustomConsole.WriteLine();
        }

        private static IController CreateController(UserCommand command)
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

