﻿// ConsoleTools
// Copyright (C) 2017 Dust in the Wind
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
using DustInTheWind.ConsoleTools.MenuControl;
using DustInTheWind.ConsoleTools.MenuControl.MenuItems;

namespace DustInTheWind.ConsoleTools.Demo.Menues.MenuItems
{
    internal class NewGameMenuItem : LabelMenuItem
    {
        private readonly GameBoard gameBoard;

        public NewGameMenuItem(GameBoard gameBoard)
        {
            if (gameBoard == null) throw new ArgumentNullException(nameof(gameBoard));
            this.gameBoard = gameBoard;

            Text = "New Game";
            HorizontalAlign = HorizontalAlign.Center;
        }

        public override void Execute()
        {
            gameBoard.StartGame();
        }
    }
}