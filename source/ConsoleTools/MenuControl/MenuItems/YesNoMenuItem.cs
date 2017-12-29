// ConsoleTools
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

namespace DustInTheWind.ConsoleTools.MenuControl.MenuItems
{
    public class YesNoMenuItem : LabelMenuItem
    {
        public string QuestionText { get; set; }

        public override bool BeforeSelect()
        {
            Console.SetCursorPosition(lastX + lastLength + 1, lastY);
            string message = QuestionText + " [Y/n]";
            Console.Write(message);
            ConsoleKeyInfo key = Console.ReadKey(true);

            Console.SetCursorPosition(lastX + lastLength + 1, lastY);
            Console.Write(new string(' ', message.Length));

            bool allow = key.Key == ConsoleKey.Y || key.Key == ConsoleKey.Enter;
            return allow && base.BeforeSelect();
        }
    }
}