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

namespace DustInTheWind.ConsoleTools.MenuControl
{
    public class SpaceMenuItem<T> : IMenuItem<T>
    {
        public int Id => -1;

        public string Text
        {
            get { return string.Empty; }
            set { }
        }

        public T Value => default(T);

        public bool IsVisible { get; }

        public HorizontalAlign HorizontalAlign { get; set; }

        public SpaceMenuItem()
        {
            IsVisible = true;
        }

        public bool IsSelectable => false;

        public ConsoleKey? Key
        {
            get { return null; }
            set { }
        }

        public void Display(int x, int y, bool selected, HorizontalAlign itemsHorizontalAlign)
        {
        }

        public virtual bool BeforeSelect()
        {
            return true;
        }
    }
}