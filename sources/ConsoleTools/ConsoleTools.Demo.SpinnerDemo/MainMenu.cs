// ConsoleTools
// Copyright (C) 2017-2020 Dust in the Wind
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
using DustInTheWind.ConsoleTools.Demo.SpinnerDemo.Commands;
using DustInTheWind.ConsoleTools.Menues;

namespace DustInTheWind.ConsoleTools.Demo.SpinnerDemo
{
    internal class MainMenu : TextMenu
    {
        public MainMenu()
        {
            IEnumerable<TextMenuItem> menuItems = CreateMenuItems();
            AddItems(menuItems);
        }

        private IEnumerable<TextMenuItem> CreateMenuItems()
        {
            yield return new TextMenuItem
            {
                Id = "1",
                Text = "stick",
                Command = new StickCommand()
            };

            yield return new TextMenuItem
            {
                Id = "2",
                Text = "bubble",
                Command = new BubbleCommand()
            };

            yield return new TextMenuItem
            {
                Id = "3",
                Text = "boomerang",
                Command = new BoomerangCommand()
            };

            yield return new TextMenuItem
            {
                Id = "4",
                Text = "half-block spin",
                Command = new HalhBlockSpinCommand()
            };

            yield return new TextMenuItem
            {
                Id = "5",
                Text = "half-block vertical",
                Command = new HalfBlockVerticalCommand()
            };

            yield return new TextMenuItem
            {
                Id = "6",
                Text = "fan",
                Command = new FanCommand()
            };

            yield return new TextMenuItem
            {
                Id = "11",
                Text = "fill (dot, empty from start) - default",
                Command = new FillEmptyFromStartCommand()
            };

            yield return new TextMenuItem
            {
                Id = "12",
                Text = "fill (dot, empty from end)",
                Command = new FillEmptyFromEndCommand()
            };

            yield return new TextMenuItem
            {
                Id = "13",
                Text = "fill (dot, sudden empty)",
                Command = new FillSuddenEmptyCommand()
            };

            yield return new TextMenuItem
            {
                Id = "14",
                Text = "fill (dot, with borders)",
                Command = new FillWithBordersCommand()
            };

            yield return new TextMenuItem
            {
                Id = "15",
                Text = "fill (block, length: 10 chars, step: 100ms)",
                Command = new FillBlock10Command()
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