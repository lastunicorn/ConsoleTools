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

// --------------------------------------------------------------------------------
// Bugs or fearure requests
// --------------------------------------------------------------------------------
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new

using System;
using System.ComponentModel;

namespace DustInTheWind.ConsoleTools.MenuControl.MenuItems
{
    public class YesNoMenuItem : LabelMenuItem
    {
        public string QuestionText { get; set; }

        protected override void OnBeforeSelect(CancelEventArgs e)
        {
            Console.SetCursorPosition(lastX + lastLength + 1, lastY);
            string message = QuestionText + " [Y/n]";
            Console.Write(message);
            ConsoleKeyInfo key = Console.ReadKey(true);

            Console.SetCursorPosition(lastX + lastLength + 1, lastY);
            Console.Write(new string(' ', message.Length));

            bool allow = key.Key == ConsoleKey.Y || key.Key == ConsoleKey.Enter;
            e.Cancel |= !allow;

            base.OnBeforeSelect(e);
        }
    }
}