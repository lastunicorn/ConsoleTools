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
using System.ComponentModel;
using DustInTheWind.ConsoleTools.Demo.SelectableMenu.Commands;
using DustInTheWind.ConsoleTools.MenuControl;
using DustInTheWind.ConsoleTools.MenuControl.MenuItems;

namespace DustInTheWind.ConsoleTools.Demo.SelectableMenu
{
    internal static class Program
    {
        private static ApplicationState applicationState;
        private static GameBoard gameBoard;
        private static MenuControl.SelectableMenu menu;

        private static void Main()
        {
            DisplayApplicationHeader();

            Console.SetWindowSize(80, 50);
            Console.SetBufferSize(80, 1024);

            Console.CancelKeyPress += HandleCancelKeyPress;

            applicationState = new ApplicationState();
            applicationState.Exiting += HandleApplicationExiting;

            gameBoard = new GameBoard();

            menu = CreateMenu();
            menu.SelectFirstByDefault = false;

            while (!applicationState.IsExitRequested)
            {
                CustomConsole.WriteLine();
                CustomConsole.WriteLine("-------------------------------------------------------------------------------");
                CustomConsole.WriteLine();

                menu.Display();
            }

            CustomConsole.WriteLineEmphasies("Bye!");

            Pause.QuickPause();
        }

        private static void DisplayApplicationHeader()
        {
            CustomConsole.WriteLineEmphasies("ConsoleTools Demo - SelectableMenu");
            CustomConsole.WriteLineEmphasies("===============================================================================");
            CustomConsole.WriteLine();
            CustomConsole.WriteLine("This demo shows how the SelectableMenu can be used.");
            CustomConsole.WriteLine("Press the up/down arrow keys to navigate through the menu.");
            CustomConsole.WriteLine("Press Enter key to select an item.");
            CustomConsole.WriteLine();
        }

        private static MenuControl.SelectableMenu CreateMenu()
        {
            MenuControl.SelectableMenu selectableMenu = new MenuControl.SelectableMenu(new IMenuItem[]
            {
                new LabelMenuItem
                {
                    Text = "New Game",
                    Command = new NewGameCommand(gameBoard)
                },
                new YesNoMenuItem
                {
                    Text = "Save Game",
                    VisibilityProvider = () => gameBoard.IsGameStarted,
                    Command = new SaveGameCommand()
                },
                new LabelMenuItem
                {
                    Text = "Load Game",
                    Command = new LoadGameCommand(gameBoard)
                },

                new SeparatorMenuItem(),
                
                new LabelMenuItem
                {
                    Text = "Settings",
                    Command = new SettingsCommand()
                }, 
                new LabelMenuItem
                {
                    Text = "Credits",
                    Command = new CreditsCommand()
                },

                new SeparatorMenuItem(),

                new LabelMenuItem
                {
                    Text = "Exit",
                    ShortcutKey = ConsoleKey.X,
                    Command = new ExitCommand(applicationState)
                }
            });

            // You can play with the following values.

            // This automatically selects the first item when the menu is displayed.
            //selectableMenu.SelectFirstByDefault = true;

            // 
            //selectableMenu.HorizontalAlignment = HorizontalAlignment.Left;
            //selectableMenu.ItemsHorizontalAlignment = HorizontalAlignment.Right;

            return selectableMenu;
        }

        private static void HandleCancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
            applicationState.RequestExit();
        }

        private static void HandleApplicationExiting(object sender, CancelEventArgs e)
        {
            menu.RequestClose();
            gameBoard.StopGame();
        }
    }
}
