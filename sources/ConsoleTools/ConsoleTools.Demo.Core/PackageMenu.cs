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
using System.Collections.Generic;
using DustInTheWind.ConsoleTools.Controls.Menus;
using DustInTheWind.ConsoleTools.Controls.Menus.MenuItems;

namespace DustInTheWind.ConsoleTools.Demo.Utils;

public class PackageMenu : TextMenu
{
    private TextMenuItem exitMenuItem;

    public PackageMenu(DemoPackageBase demoPackage)
    {
        TitleForegroundColor = ConsoleColor.Magenta;
        Margin = "0 0 0 1";

        CreateItems(demoPackage.Demos);
        CreateExitItem();

        TitleText = demoPackage.Title;
        exitMenuItem.Text = "Back";
    }

    public PackageMenu(IEnumerable<IDemo> demos)
    {
        TitleForegroundColor = ConsoleColor.Magenta;
        Margin = "0 0 0 1";

        CreateItems(demos);
        CreateExitItem();

        TitleText = null;
        exitMenuItem.Text = "Exit";
    }

    private void CreateItems(IEnumerable<IDemo> demos)
    {
        int i = 0;

        foreach (IDemo demo in demos)
        {
            TextMenuItem textMenuItem = CreateMenuItem(demo, i);
            AddItem(textMenuItem);

            i++;
        }
    }

    private static TextMenuItem CreateMenuItem(IDemo demo, int i)
    {
        string title = demo.Title;

        bool itemDisplaysMenu = demo is DemoPackageBase demoPackageBase && (demoPackageBase.HasSubPackages || demoPackageBase.ForceDisplayMenu);
        if (itemDisplaysMenu)
            title += " [->]";

        return new TextMenuItem
        {
            Id = (i + 1).ToString(),
            Text = title,
            Command = new DemoCommand(demo)
        };
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
            },
            ForegroundColor = ConsoleColor.Magenta
        };

        AddItem(exitMenuItem);
    }
}