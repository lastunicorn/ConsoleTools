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

using DustInTheWind.ConsoleTools.Controls.Menus;
using DustInTheWind.ConsoleTools.Demo.NetCore;

namespace DustInTheWind.ConsoleTools.Demo.NetFramework
{
    internal class MainMenu : TextMenu
    {
        public MainMenu(DemoPackages demoPackages)
        {
            int i = 0;

            foreach (IDemoPackage demoPackage in demoPackages)
            {
                AddItem(new TextMenuItem
                {
                    Id = (i + 1).ToString(),
                    Text = demoPackage.ShortDescription,
                    Command = new DemoCommand(demoPackage)
                });

                i++;
            }

            AddItem(new TextMenuItem
            {
                Id = 0.ToString(),
                Text = "Exit",
                Command = new ExitCommand()
            });

            Margin = "0 1";
        }
    }
}