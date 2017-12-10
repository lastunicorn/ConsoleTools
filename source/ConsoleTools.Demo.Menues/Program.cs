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
using System.ComponentModel;
using DustInTheWind.ConsoleTools.Demo.Menues.MenuItems;
using DustInTheWind.ConsoleTools.MenuControl;

namespace DustInTheWind.ConsoleTools.Demo.Menues
{
    /// <summary>
    /// This demo is not finished yet.
    /// </summary>
    internal static class Program
    {
        private static ApplicationState applicationState;
        private static GameBoard gameBoard;
        private static SelectableMenu menu;

        private static void Main()
        {
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
                menu.Display();
                menu.SelectedItem?.Execute();
            }

            CustomConsole.WriteLineEmphasies("Bye!");

            CustomConsole.Pause();
        }

        private static SelectableMenu CreateMenu()
        {
            return new SelectableMenu(new IMenuItem[]
            {
                new NewGameMenuItem(gameBoard),
                new SaveGameMenuItem(gameBoard),
                new LoadGameMenuItem(gameBoard),

                new SpaceMenuItem(),

                new SettingsMenuItem(),
                new CreditsMenuItem(),

                new SpaceMenuItem(),

                new ExitMenuItem(applicationState)
            });
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
