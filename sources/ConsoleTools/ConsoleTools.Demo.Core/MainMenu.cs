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

using System.Collections.Generic;
using DustInTheWind.ConsoleTools.Controls.Menus;

namespace DustInTheWind.ConsoleTools.Demo.Core;

public class MainMenu : TextMenu
{
    private bool isMain = true;

    private TextMenuItem exitMenuItem;

    public bool IsMain
    {
        get => isMain;
        set
        {
            isMain = value;

            if (isMain)
            {
                TitleText = null;
                exitMenuItem.Text = "Exit";
            }
            else
            {
                TitleText = "Sub-Menu";
                exitMenuItem.Text = "Back";
            }
        }
    }

    public MainMenu(IEnumerable<IDemo> demos)
    {
        Margin = "0 0 0 1";

        CreateItems(demos);
        CreateExitItem();
    }

    private void CreateItems(IEnumerable<IDemo> demos)
    {
        int i = 0;

        foreach (IDemo demo in demos)
        {
            string title = demo.Title;
            if (demo is DemoPackageBase { HasSubPackages: true })
                title += " [->]";

            AddItem(new TextMenuItem
            {
                Id = (i + 1).ToString(),
                Text = title,
                Command = new DemoCommand(demo)
            });

            i++;
        }
    }

    private void CreateExitItem()
    {
        exitMenuItem = new TextMenuItem
        {
            Id = 0.ToString(),
            Text = "Exit",
            Command = new RelayCommand
            {
                ExecuteAction = RequestClose
            }
        };

        AddItem(exitMenuItem);
    }
}