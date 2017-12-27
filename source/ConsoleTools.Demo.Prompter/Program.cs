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
using DustInTheWind.ConsoleTools.InputControls;
using DustInTheWind.ConsoleTools.TabularData;

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

            prompter = new ConsoleTools.Prompter();
            prompter.NewCommand += ui_NewCommand;

            prompter.WaitForUserCommand();
        }

        private static void ui_NewCommand(object sender, NewCommandEventArgs e)
        {
            switch (e.Command.Name)
            {
                case "q":
                case "quit":
                case "exit":
                    e.Exit = AskToExit();
                    break;

                case "help":
                    DisplayHelp();

                    break;

                case "whale":
                case "whales":
                    DisplayWhales();
                    break;

                case "prompter":
                    ChangePrompter();
                    break;

                default:
                    HandleUnknownCommand(e);
                    break;
            }

            CustomConsole.WriteLine();
        }

        private static bool AskToExit()
        {
            YesNoControl yesNoControl = new YesNoControl("Are you sure?")
            {
                DefaultOption = YesNoAnswer.Yes
            };

            YesNoAnswer answer = yesNoControl.ReadAnswer();

            if (answer == YesNoAnswer.Yes)
            {
                CustomConsole.WriteLine();
                CustomConsole.WriteLine("Bye!");
                return true;
            }

            CustomConsole.WriteLine();
            return false;
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

        private static void DisplayWhales()
        {
            Table table = new Table("Whales");

            table.Columns.Add(new Column("Name"));
            table.Columns.Add(new Column("Population"));
            table.Columns.Add(new Column("Weight"));

            table.DisplayColumnHeaders = true;

            IEnumerable<Whale> whales = CreateWhales();

            foreach (Whale whale in whales)
            {
                table.AddRow(new[]
                {
                  whale.Name,
                  whale.Count,
                  whale.Weight
                });
            }

            CustomConsole.WriteLine(table.ToString());
        }

        private static void ChangePrompter()
        {
            TextInputControl textInputControl = new TextInputControl();
            string newPrompterText = textInputControl.Read("New Prompter Text");
            prompter.PrompterText = newPrompterText;
        }

        private static void HandleUnknownCommand(NewCommandEventArgs e)
        {
            CustomConsole.WriteLineError("Unknown command: " + e.Command, ConsoleColor.DarkYellow);
        }

        private static IEnumerable<Whale> CreateWhales()
        {
            return new List<Whale>
            {
                new Whale
                {
                    Name = "Blue whale",
                    Count = "10,000-25,000",
                    Weight = "50-150 tonnes"
                },
                new Whale
                {
                    Name = "Humpback whale",
                    Count = "80,000",
                    Weight = "25–30 tonnes"
                },
                new Whale
                {
                    Name = "Killer whale",
                    Count = "100,000",
                    Weight = "4.5 tonnes"
                },
                new Whale
                {
                    Name = "Beluga",
                    Count = "100,000",
                    Weight = "1.5 tonnes"
                },
                new Whale
                {
                    Name = "Narwhal",
                    Count = "25,000",
                    Weight = "900-1,500 kilograms"
                },
                new Whale
                {
                    Name = "Sperm whale",
                    Count = "200,000–2,000,000",
                    Weight = "25–50 tonnes"
                }
            };
        }
    }
}

