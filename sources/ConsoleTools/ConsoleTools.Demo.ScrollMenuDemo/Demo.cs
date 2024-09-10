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

namespace DustInTheWind.ConsoleTools.Demo.ScrollMenuDemo;

public class Demo : DemoBase
{
    private static GameApplication gameApplication;
    private static ControlRepeater menuRepeater;

    public override string Title => "Scroll Menu";

    public override MultilineText Description => "Press the up/down arrow keys to navigate through the menu.";

    protected override void DoExecute()
    {
        try
        {
            gameApplication = new GameApplication();
            gameApplication.Exited += HandleGameApplicationExited;

            MainMenu menu = new(gameApplication);
            menu.BeforeRender += HandleMenuBeforeRender;

            menuRepeater = new ControlRepeater
            {
                Content = menu,
                RepeatCount = -1
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

    private static void HandleMenuBeforeRender(object sender, EventArgs args)
    {
        HorizontalLine horizontalLine = new();
        horizontalLine.Display();
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