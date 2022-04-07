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

namespace DustInTheWind.ConsoleTools.Demo.TextMenuDemo
{
    internal static class Program
    {
        private static GameApplication gameApplication;
        private static ControlRepeater mainMenuRepeater;

        private static void Main()
        {
            try
            {
                DisplayApplicationHeader();

                Console.SetWindowSize(80, 50);
                Console.SetBufferSize(80, 50);

                Console.CancelKeyPress += HandleCancelKeyPress;

                gameApplication = new GameApplication();

                mainMenuRepeater = new ControlRepeater
                {
                    Control = new MainMenu(gameApplication)
                };

                gameApplication.Exited += HandleGameApplicationExited;

                mainMenuRepeater.Display();

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
            mainMenuRepeater?.RequestClose();
            gameApplication.Exited -= HandleGameApplicationExited;
        }

        private static void DisplayApplicationHeader()
        {
            CustomConsole.WriteLineEmphasized("ConsoleTools Demo - TextMenu");
            CustomConsole.WriteLineEmphasized("===============================================================================");
            CustomConsole.WriteLine();
            CustomConsole.WriteLine("This demo shows how the TextMenu can be used.");
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
                ForegroundColor = CustomConsole.EmphasizedColor,
                Margin = "0 1 0 0"
            };
            goodbyText.Display();
        }
    }
}