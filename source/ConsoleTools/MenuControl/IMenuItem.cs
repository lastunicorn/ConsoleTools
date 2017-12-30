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
using System.ComponentModel;

namespace DustInTheWind.ConsoleTools.MenuControl
{
    public interface IMenuItem
    {
        int Id { get; }
        string Text { get; set; }
        bool IsVisible { get; }
        HorizontalAlign HorizontalAlign { get; set; }
        bool IsSelectable { get; }
        ConsoleKey? ShortcutKey { get; set; }
        ICommand Command { get; set; }

        event EventHandler<CancelEventArgs> BeforeSelect;

        void Display(int x, int y, bool selected, HorizontalAlign itemsHorizontalAlign);
        bool Select();
    }
}