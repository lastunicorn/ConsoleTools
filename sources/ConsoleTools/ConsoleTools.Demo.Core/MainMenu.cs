﻿// ConsoleTools
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

using DustInTheWind.ConsoleTools.Controls.Menus;

namespace DustInTheWind.ConsoleTools.Demo.Core
{
    public class MainMenu : TextMenu
    {
        public MainMenu(DemoPackages demoPackages, DemoApplication demoApplication)
        {
            CreateItems(demoPackages, demoApplication);

            Margin = "0 0 0 1";
        }

        private void CreateItems(DemoPackages demoPackages, DemoApplication demoApplication)
        {
            int i = 0;

            foreach (IDemoPackage demoPackage in demoPackages)
            {
                AddItem(new TextMenuItem
                {
                    Id = (i + 1).ToString(),
                    Text = demoPackage.Name,
                    Command = new DemoCommand(demoPackage)
                });

                i++;
            }

            AddItem(new TextMenuItem
            {
                Id = 0.ToString(),
                Text = "Exit",
                Command = new ExitCommand(demoApplication)
            });
        }
    }
}