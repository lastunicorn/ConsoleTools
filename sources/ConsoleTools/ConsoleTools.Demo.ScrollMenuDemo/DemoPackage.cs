// ConsoleTools
// Copyright (C) 2017-2022 Dust in the Wind
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
using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Demo.Core;

namespace DustInTheWind.ConsoleTools.Demo.ScrollMenuDemo
{
    internal class DemoPackage : IDemoPackage
    {
        private static GameApplication gameApplication;
        private static ControlRepeater menuRepeater;

        public string Name => "ScrollMenu Demo";

        public void ExecuteDemo()
        {
            try
            {
                Console.Clear();
                Console.SetWindowSize(80, 50);
                Console.SetBufferSize(80, 50);

                DisplayApplicationHeader();

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
        }

        private static void HandleGameApplicationExited(object sender, EventArgs e)
        {
            menuRepeater?.RequestClose();
            gameApplication.Exited -= HandleGameApplicationExited;
        }

        private static void HandleMenuBeforeDisplay(object sender, EventArgs args)
        {
            HorizontalLine horizontalLine = new HorizontalLine();
            horizontalLine.Display();
        }

        private static void DisplayApplicationHeader()
        {
            ApplicationHeader applicationHeader = new ApplicationHeader()
            {
                Appendix = "ScrollMenu Demo",
                Description = new[]
                {
                    "This demo shows how the ScrollMenu can be used.",
                    "Press the up/down arrow keys to navigate through the menu.",
                    "Press Enter key to select an item."
                }
            };
            applicationHeader.Display();
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
                ForegroundColor = CustomConsole.EmphasizedColor,
                Margin = "0 1 0 0"
            };
            goodbyText.Display();
        }
    }
}