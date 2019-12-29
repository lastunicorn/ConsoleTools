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

namespace DustInTheWind.ConsoleTools.Demo.TextMenuDemo
{
    /// <summary>
    /// This class is representative for all the business classes that can exists in a real application.
    /// It emulates a game that can be started, stopped
    /// </summary>
    internal class GameBoard
    {
        public bool IsGameStarted { get; private set; }

        public void StartGame()
        {
            IsGameStarted = true;
            CustomConsole.WriteLineSuccess("New game started.");
        }

        public void StopGame()
        {
            if (IsGameStarted)
            {
                IsGameStarted = false;
                CustomConsole.WriteLineSuccess("Current game stoped.");
            }
            else
            {
                CustomConsole.WriteLine("Current game is already stoped.");
            }
        }

        public void LoadGame()
        {
            IsGameStarted = true;
            CustomConsole.WriteLineSuccess("Game loaded successfully.");
        }

        public void Save()
        {
            if (IsGameStarted)
                CustomConsole.WriteLineSuccess("Game saved successfully");
            else
                CustomConsole.WriteLineWarning("No game is running.");
        }
    }
}