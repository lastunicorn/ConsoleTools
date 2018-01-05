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
using System.ComponentModel;
using DustInTheWind.ConsoleTools.TabularData;

namespace DustInTheWind.ConsoleTools.MenuControl
{
    public interface IMenuItem
    {
        int Id { get; }
        string Text { get; set; }
        bool IsVisible { get; }
        HorizontalAlignment HorizontalAlignment { get; set; }
        bool IsSelectable { get; }
        ConsoleKey? ShortcutKey { get; set; }
        ICommand Command { get; set; }
        SelectableMenu ParentMenu { get; set; }
        int PaddingLeft { get; set; }
        int PaddingRight { get; set; }

        event EventHandler<CancelEventArgs> BeforeSelect;

        void Display(Size size, bool highlighted);
        bool Select();
        Size Measure();
    }
}