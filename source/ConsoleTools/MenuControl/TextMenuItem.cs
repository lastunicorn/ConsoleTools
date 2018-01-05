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

namespace DustInTheWind.ConsoleTools.MenuControl
{
    public class TextMenuItem
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public bool IsVisible { get; set; } = true;
        public bool Enabled { get; set; } = true;
        public ICommand Command { get; set; }

        public bool IsSelectable => (Command != null && Command.IsActive) || (Command == null && Enabled);

        public void Display()
        {
            if (IsSelectable)
                CustomConsole.Write($"{Id} - {Text}");
            else
                CustomConsole.WriteColor(ConsoleColor.DarkGray, $"{Id} - {Text}");
        }

        public bool Select()
        {
            Command?.Execute();

            return true;
        }
    }
}