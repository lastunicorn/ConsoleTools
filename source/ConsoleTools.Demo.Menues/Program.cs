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
using System.Threading;
using DustInTheWind.ConsoleTools.Demo.Menues.MenuItems;
using DustInTheWind.ConsoleTools.MenuControl;

namespace DustInTheWind.ConsoleTools.Demo.Menues
{
    /// <summary>
    /// This demo is not finished yet.
    /// </summary>
    internal static class Program
    {
        private static GameBoard gameBoard;
        private static SelectableMenu menu;

        private static void Main()
        {
            //string a = ConsoleReader.ReadLine();

            //CustomConsole.Write(a);
            //CustomConsole.Pause();





            Console.SetWindowSize(80, 50);
            Console.SetBufferSize(80, 1024);

            Console.CancelKeyPress += HandleCancelKeyPress;

            gameBoard = new GameBoard();
            gameBoard.Exiting += HandleGameBoardExiting;

            menu = new SelectableMenu
            {
                new NewGameMenuItem(gameBoard),
                new ResumeGameMenuItem(gameBoard),
                new SaveGameMenuItem(gameBoard),
                new LoadGameMenuItem(),

                new SpaceMenuItem(),

                new SettingsMenuItem(),
                new HelpMenuItem(),
                new CreditsMenuItem(),

                new SpaceMenuItem(),

                new ExitMenuItem(gameBoard)
            };

            while (!gameBoard.IsExitRequested)
            {
                IMenuItem menuItem = menu.Display();
                menuItem?.Execute();
            }

            CustomConsole.WriteLineEmphasies("Bye2 !");

            CustomConsole.Pause();
        }

        private static void HandleCancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;

            gameBoard.RequestExit();
            menu.Close();
        }

        private static void HandleGameBoardExiting(object sender, CancelEventArgs e)
        {
            CustomConsole.WriteLineEmphasies("Bye !");
        }
    }
}
