// ConsoleTools
// Copyright (C) 2017-2024 Dust in the Wind
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
using DustInTheWind.ConsoleTools.Demo.Utils;

namespace DustInTheWind.ConsoleTools.Demo.TextMenuDemo;

public class Demo : DemoBase
{
    private static GameApplication gameApplication;
    private static ControlRepeater mainMenuRepeater;

    public override string Title => "Text Menu";

    public override MultilineText Description => new[]
    {
        "Is it really necessary to show how the TextMenu is working? You've been using it in this entire Demo application.",
        "...",
        "Ok, if you really want it, let's simulate that we have a game. The main menu would look something like this:"
    };

    protected override void DoExecute()
    {
        try
        {
            Console.CancelKeyPress += HandleCancelKeyPress;

            gameApplication = new GameApplication();

            mainMenuRepeater = new ControlRepeater
            {
                Content = new MainMenu(gameApplication),
                RepeatCount = -1
            };

            gameApplication.Exited += HandleGameApplicationExited;

            mainMenuRepeater.Display();

            DisplayGoodby();
        }
        catch (Exception ex)
        {
            CustomConsole.WriteError(ex);
        }
    }

    private static void HandleGameApplicationExited(object sender, EventArgs e)
    {
        mainMenuRepeater?.RequestClose();
        gameApplication.Exited -= HandleGameApplicationExited;
    }

    private static void HandleCancelKeyPress(object sender, ConsoleCancelEventArgs e)
    {
        e.Cancel = true;
        gameApplication.RequestExit();
    }

    private static void DisplayGoodby()
    {
        TextBlock goodbyText = new()
        {
            Text = "Bye!",
            ForegroundColor = CustomConsole.EmphasizedColor,
            Margin = "0 1 0 0"
        };
        goodbyText.Display();
    }
}