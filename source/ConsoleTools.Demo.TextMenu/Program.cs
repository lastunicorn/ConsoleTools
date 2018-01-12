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
using System.ComponentModel;
using DustInTheWind.ConsoleTools.Demo.TextMenu.Commands;
using DustInTheWind.ConsoleTools.MenuControl;

namespace DustInTheWind.ConsoleTools.Demo.TextMenu
{
    internal static class Program
    {
        private static ApplicationState applicationState;
        private static GameBoard gameBoard;
        private static MenuControl.TextMenu menu;

        private static void Main()
        {
            try
            {
                DisplayApplicationHeader();

                Console.SetWindowSize(80, 50);
                Console.SetBufferSize(80, 1024);

                Console.CancelKeyPress += HandleCancelKeyPress;

                applicationState = new ApplicationState();
                applicationState.Exiting += HandleApplicationExiting;

                gameBoard = new GameBoard();

                menu = CreateMenu();

                while (!applicationState.IsExitRequested)
                {
                    CustomConsole.WriteLine();
                    CustomConsole.WriteLine("-------------------------------------------------------------------------------");
                    CustomConsole.WriteLine();

                    menu.Display();
                }

                CustomConsole.WriteLine();
                CustomConsole.WriteLineEmphasies("Bye!");
            }
            catch (Exception ex)
            {
                CustomConsole.WriteError(ex);

            }
            finally
            {
                Pause.QuickPause();
            }
        }

        private static void DisplayApplicationHeader()
        {
            CustomConsole.WriteLineEmphasies("ConsoleTools Demo - TextMenu");
            CustomConsole.WriteLineEmphasies("===============================================================================");
            CustomConsole.WriteLine();
            CustomConsole.WriteLine("This demo shows how the TextMenu can be used.");
            CustomConsole.WriteLine("Press the up/down arrow keys to navigate through the menu.");
            CustomConsole.WriteLine("Press Enter key to select an item.");
            CustomConsole.WriteLine();
        }

        private static MenuControl.TextMenu CreateMenu()
        {
            MenuControl.TextMenu textMenu = new MenuControl.TextMenu(new[]
            {
                new TextMenuItem
                {
                    Id = "1",
                    Text = "New Game",
                    Command = new NewGameCommand(gameBoard)
                },
                new TextMenuItem
                {
                    Id = "2",
                    Text = "Save Game",
                    Command = new SaveGameCommand(gameBoard)
                },
                new TextMenuItem
                {
                    Id = "3",
                    Text = "Load Game",
                    Command = new LoadGameCommand(gameBoard)
                },
                new TextMenuItem
                {
                    Id = "4",
                    Text = "Close Game",
                    Command = new CloseGameCommand(gameBoard)
                },

                new TextMenuItem
                {
                    Id = "5",
                    Text = "Settings",
                    Command = new SettingsCommand()
                },
                new TextMenuItem
                {
                    Id = "6",
                    Text = "Credits",
                    Command = new CreditsCommand()
                },

                new TextMenuItem
                {
                    Id = "0",
                    Text = "Exit",
                    Command = new ExitCommand(applicationState)
                }
            });

            return textMenu;
        }

        private static void HandleCancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
            applicationState.RequestExit();
        }

        private static void HandleApplicationExiting(object sender, CancelEventArgs e)
        {
            gameBoard.StopGame();
        }
    }
}
