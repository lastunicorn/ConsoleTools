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

namespace DustInTheWind.ConsoleTools.Demo.ScrollMenu
{
    internal static class Program
    {
        private static GameApplication gameApplication;
        private static ControlRepeater menuRepeater;

        private static void Main()
        {
            try
            {
                DisplayApplicationHeader();

                Console.SetWindowSize(80, 50);
                Console.SetBufferSize(80, 50);

                Console.CancelKeyPress += HandleCancelKeyPress;

                gameApplication = new GameApplication();
                gameApplication.Exited += HandleGameApplicationExited;

                MainMenu menu = new MainMenu(gameApplication);
                menu.BeforeDisplay += HandleMenuBeforeDisplay;

                menuRepeater = new ControlRepeater
                {
                    Control = menu
                };

                menuRepeater.Display();

                DisplayGoodby();
            }
            catch (Exception ex)
            {
                CustomConsole.WriteError(ex);
            }
            finally
            {
                Pause.QuickDisplay();
            }
        }

        private static void HandleGameApplicationExited(object sender, EventArgs e)
        {
            menuRepeater?.RequestClose();
            gameApplication.Exited -= HandleGameApplicationExited;
        }

        private static void HandleMenuBeforeDisplay(object sender, EventArgs args)
        {
            CustomConsole.WriteLine();
            CustomConsole.WriteLine("-------------------------------------------------------------------------------");
            CustomConsole.WriteLine();
        }

        private static void DisplayApplicationHeader()
        {
            CustomConsole.WriteLineEmphasies("ConsoleTools Demo - ScrollMenu");
            CustomConsole.WriteLineEmphasies("===============================================================================");
            CustomConsole.WriteLine();
            CustomConsole.WriteLine("This demo shows how the ScrollMenu can be used.");
            CustomConsole.WriteLine("Press the up/down arrow keys to navigate through the menu.");
            CustomConsole.WriteLine("Press Enter key to select an item.");
            CustomConsole.WriteLine();
        }

        private static void HandleCancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
            gameApplication.RequestExit();
        }

        private static void DisplayGoodby()
        {
            TextBlock goodbyText = new TextBlock
            {
                Text = "Bye!",
                ForegroundColor = CustomConsole.EmphasiesColor,
                MarginTop = 1
            };
            goodbyText.Display();
        }
    }
}