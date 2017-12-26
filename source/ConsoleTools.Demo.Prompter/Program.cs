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
using DustInTheWind.ConsoleTools.TabularData;

namespace DustInTheWind.ConsoleTools.Demo.Prompter
{
    internal class Program
    {
        private static void Main()
        {
            ConsoleTools.Prompter prompter = new ConsoleTools.Prompter();
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
                    if (CustomConsole.QuestionChar("Are you sure? [y/n] ") == 'y')
                    {
                        CustomConsole.WriteLine();
                        CustomConsole.WriteLine("Bye!");
                        e.Exit = true;
                    }
                    else
                    {
                        CustomConsole.WriteLine();
                    }
                    break;

                case "help":
                    CustomConsole.WriteLine("Valid commands: whales, help, q, quit, exit");
                    CustomConsole.WriteLine();

                    break;

                case "whale":
                case "whales":
                    Table table = new Table("Whales");

                    table.Columns.Add(new Column("Name"));
                    table.Columns.Add(new Column("Population"));
                    table.Columns.Add(new Column("Weight"));

                    table.DisplayColumnHeaders = true;

                    table.AddRow(new[] { "Blue whale", "10,000-25,000", "50-150 tonnes" });
                    table.AddRow(new[] { "Humpback whale", "80,000", "25–30 tonnes" });
                    table.AddRow(new[] { "Killer whale", "100,000", "4.5 tonnes" });
                    table.AddRow(new[] { "Beluga", "100,000", "1.5 tonnes" });
                    table.AddRow(new[] { "Narwhal", "25,000", "900-1,500 kilograms" });
                    table.AddRow(new[] { "Sperm whale", "200,000–2,000,000", "25–50 tonnes" });

                    CustomConsole.WriteLine(table.ToString());
                    CustomConsole.WriteLine();
                    break;

                default:
                    CustomConsole.WriteLine("Unknown command: " + e.Command, ConsoleColor.DarkYellow);
                    CustomConsole.WriteLine();
                    break;
            }
        }
    }
}

