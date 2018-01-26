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
using DustInTheWind.ConsoleTools.InputControls;
using DustInTheWind.ConsoleTools.MenuControl;

namespace DustInTheWind.ConsoleTools.Demo.InputControls.Commands
{
    internal class ListWriteQuickCommand : ICommand
    {
        public bool IsActive => true;

        public void Execute()
        {
            string[] colors = Enum.GetNames(typeof(ConsoleColor));

            DisplayColorsQuick(colors);
        }

        /// <summary>
        /// Using the static method <see cref="ListWrite{T}.QuickDisplay"/> falls back
        /// to the default properties for colors, bullet, spaces, etc.
        /// </summary>
        private static void DisplayColorsQuick(string[] colors)
        {
            ListWrite<string>.QuickDisplay("Colors:", colors);
        }
    }
}