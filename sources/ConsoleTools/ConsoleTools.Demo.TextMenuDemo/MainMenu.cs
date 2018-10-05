// ConsoleTools
// Copyright (C) 2017-2018 Dust in the Wind
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
using DustInTheWind.ConsoleTools.Demo.TextMenuDemo.Commands;
using DustInTheWind.ConsoleTools.Menues;

namespace DustInTheWind.ConsoleTools.Demo.TextMenuDemo
{
    /// <summary>
    /// The main menu of the application.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <see cref="TextMenu"/> can be instantiated configured directly in the <see cref="Program"/> class.
    /// There is no need to inherit from it.
    /// </para>
    /// <para>
    /// Nevertheless, I suggest to encapsulate the configuration of the menu and the
    /// creation of the items inside a separate class as this one.
    /// They are verbose enough to deserve their own class.
    /// </para>
    /// </remarks>
    internal class MainMenu : TextMenu
    {
        public MainMenu(GameApplication application)
        {
            if (application == null) throw new ArgumentNullException(nameof(application));

            EraseAfterClose = true;
            Margin = "0 1";

            TitleText = "Demo Application";
            TitleForegroundColor = ConsoleColor.Cyan;

            IEnumerable<TextMenuItem> menuItems = CreateMenuItems(application);
            AddItems(menuItems);
        }

        private static IEnumerable<TextMenuItem> CreateMenuItems(GameApplication application)
        {
            return new[]
            {
                new TextMenuItem
                {
                    Id = "1",
                    Text = "New Game",
                    Command = new NewGameCommand(application.GameBoard)
                },
                new TextMenuItem
                {
                    Id = "2",
                    Text = "Save Game",
                    Command = new SaveGameCommand(application.GameBoard)
                },
                new TextMenuItem
                {
                    Id = "3",
                    Text = "Load Game",
                    Command = new LoadGameCommand(application.GameBoard)
                },
                new TextMenuItem
                {
                    Id = "4",
                    Text = "Close Game",
                    Command = new CloseGameCommand(application.GameBoard)
                },

                new TextMenuItem
                {
                    Id = "5",
                    Text = "Settings",
                    Command = new SettingsCommand()
                },
                new TextMenuItem
                {
                    Id = "6",
                    Text = "Credits",
                    Command = new CreditsCommand()
                },

                new TextMenuItem
                {
                    Id = "0",
                    Text = "Exit",
                    Command = new ExitCommand(application)
                }
            };
        }
    }
}