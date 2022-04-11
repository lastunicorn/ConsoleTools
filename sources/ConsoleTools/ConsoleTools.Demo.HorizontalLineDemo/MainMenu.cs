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

using System.Collections.Generic;
using DustInTheWind.ConsoleTools.Controls.Menus;
using DustInTheWind.ConsoleTools.Demo.HorizontalLineDemo.Commands;

namespace DustInTheWind.ConsoleTools.Demo.HorizontalLineDemo
{
    internal class MainMenu : TextMenu
    {
        public MainMenu()
        {
            IEnumerable<TextMenuItem> menuItems = CreateMenuItems();
            AddItems(menuItems);

            Margin = "0 1";
        }

        private IEnumerable<TextMenuItem> CreateMenuItems()
        {
            yield return new TextMenuItem
            {
                Id = "1",
                Text = "Default",
                Command = new DefaultCommand()
            };

            yield return new TextMenuItem
            {
                Id = "2",
                Text = "Custom Char",
                Command = new CustomCharCommand()
            };

            yield return new TextMenuItem
            {
                Id = "3",
                Text = "Custom margins",
                Command = new CustomMarginCommand()
            };

            yield return new TextMenuItem
            {
                Id = "4",
                Text = "Custom padding",
                Command = new CustomPaddingCommand()
            };

            yield return new TextMenuItem
            {
                Id = "5",
                Text = "Custom foreground color",
                Command = new CustomForegroundColorCommand()
            };

            yield return new TextMenuItem
            {
                Id = "6",
                Text = "Custom background color",
                Command = new CustomBackgroundColorCommand()
            };

            yield return new TextMenuItem
            {
                Id = "7",
                Text = "Custom Width",
                Command = new CustomWidthCommand()
            };

            yield return new TextMenuItem
            {
                Id = "8",
                Text = "Alignments",
                Command = new AlignmentsCommand()
            };

            yield return new TextMenuItem
            {
                Id = "0",
                Text = "Exit",
                Command = new ExitCommand()
            };
        }
    }
}