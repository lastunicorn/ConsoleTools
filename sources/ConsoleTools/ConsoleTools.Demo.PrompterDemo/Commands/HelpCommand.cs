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

using DustInTheWind.ConsoleTools.CommandLine;
using DustInTheWind.ConsoleTools.Menues;

namespace DustInTheWind.ConsoleTools.Demo.PrompterDemo.Commands
{
    internal class HelpCommand : IPrompterCommand
    {
        public bool IsActive { get; } = true;

        public void Execute(CliCommand cliCommand)
        {
            DisplayHelp();
        }

        private static void DisplayHelp()
        {
            CustomConsole.WriteLineEmphasies("Valid commands:");
            CustomConsole.WriteLine();

            CustomConsole.WriteEmphasies("  - whale, whales   ");
            CustomConsole.WriteLine("- Displays a table with whales.");

            CustomConsole.WriteEmphasies("  - prompter        ");
            CustomConsole.WriteLine("- Asks the user to provide a new prompter text.");

            CustomConsole.WriteEmphasies("  - help            ");
            CustomConsole.WriteLine("- Displays this help page");

            CustomConsole.WriteEmphasies("  - quit, q, exit   ");
            CustomConsole.WriteLine("- Exits the application.");
        }
    }
}