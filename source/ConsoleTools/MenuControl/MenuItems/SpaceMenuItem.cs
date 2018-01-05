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

namespace DustInTheWind.ConsoleTools.MenuControl.MenuItems
{
    public class SpaceMenuItem : IMenuItem
    {
        public int Id => -1;

        public string Text
        {
            get { return string.Empty; }
            set { }
        }

        public bool IsVisible { get; }

        public HorizontalAlignment HorizontalAlignment { get; set; }

        public SpaceMenuItem()
        {
            IsVisible = true;
        }

        public bool IsSelectable => false;

        public ConsoleKey? ShortcutKey
        {
            get { return null; }
            set { }
        }

        public ICommand Command { get; set; }

        public SelectableMenu ParentMenu { get; set; }

        public int PaddingLeft { get; set; } = 0;
        public int PaddingRight { get; set; } = 0;

        public event EventHandler<CancelEventArgs> BeforeSelect;

        public void Display(Size size, bool highlighted)
        {
        }

        public bool Select()
        {
            CancelEventArgs args = new CancelEventArgs();
            OnBeforeSelect(args);

            return !args.Cancel;
        }

        public Size Measure()
        {
            return new Size(PaddingLeft + PaddingRight, 1);
        }

        protected virtual void OnBeforeSelect(CancelEventArgs e)
        {
            e.Cancel = true;
            BeforeSelect?.Invoke(this, e);
        }
    }
}