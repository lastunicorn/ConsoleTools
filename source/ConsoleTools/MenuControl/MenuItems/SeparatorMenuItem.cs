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

namespace DustInTheWind.ConsoleTools.MenuControl.MenuItems
{
    /// <summary>
    /// Represents a separator used to group menu items in a menu.
    /// </summary>
    public class SeparatorMenuItem : IMenuItem
    {
        /// <summary>
        /// Gets the -1 value.
        /// The setter does nothing.
        /// </summary>
        public int Id
        {
            get { return -1; }
            set { }
        }

        /// <summary>
        /// Gets the <see cref="string.Empty"/> value.
        /// The setter does nothing.
        /// </summary>
        public string Text
        {
            get { return string.Empty; }
            set { }
        }

        /// <summary>
        /// Gets or sets a value that specifies if the current instance is displayed.
        /// Default value: <c>true</c>
        /// </summary>
        public bool IsVisible { get; set; } = true;

        /// <summary>
        /// Gets or sets the horizontal alignment of the current instance inside the menu.
        /// This value does not change the appearance of the <see cref="SeparatorMenuItem"/> in any way.
        /// </summary>
        public HorizontalAlignment HorizontalAlignment { get; set; }

        /// <summary>
        /// Gets the <c>false</c> value specifying that the current instance cannot be selected.
        /// The setter does nothing.
        /// </summary>
        public bool IsSelectable
        {
            get { return false; }
            set { }
        }

        /// <summary>
        /// Gets the <c>null</c> value because the <see cref="SeparatorMenuItem"/> should not be selected.
        /// The setter does nothing.
        /// </summary>
        public ConsoleKey? ShortcutKey
        {
            get { return null; }
            set { }
        }

        /// <summary>
        /// Gets or sets the command to be executed when the current instance is selected.
        /// It always returns <c>null</c>.
        /// The setter does nothing.
        /// </summary>
        public ICommand Command
        {
            get { return null; }
            set { }
        }

        /// <summary>
        /// Gets or sets the menu that contains the current instance.
        /// </summary>
        public SelectableMenu ParentMenu { get; set; }

        /// <summary>
        /// Gets or sets the padding applied to the left of the text.
        /// The padding is part of the menu item's width.
        /// Default value: 0
        /// </summary>
        public int PaddingLeft { get; set; } = 0;

        /// <summary>
        /// Gets or sets the padding applied to the right of the text.
        /// The padding is part of the menu item's width.
        /// Default value: 0
        /// </summary>
        public int PaddingRight { get; set; } = 0;

        /// <summary>
        /// This event is never raised because the <see cref="SeparatorMenuItem"/> cannot be selected.
        /// </summary>
        public event EventHandler<CancelEventArgs> BeforeSelect;

        /// <summary>
        /// Does nothing.
        /// </summary>
        public void Display(Size size, bool highlighted)
        {
        }

        /// <summary>
        /// Does nothing. Always returns <c>false</c>.
        /// </summary>
        /// <returns>Returns <c>false</c>.</returns>
        public bool Select()
        {
            return false;
        }

        /// <summary>
        /// Returns the minimum size necessary for the current instance to render.
        /// </summary>
        public Size Measure()
        {
            return new Size(PaddingLeft + PaddingRight, 1);
        }

        /// <summary>
        /// Raises the <see cref="BeforeSelect"/> event.
        /// </summary>
        protected virtual void OnBeforeSelect(CancelEventArgs e)
        {
            e.Cancel = true;
            BeforeSelect?.Invoke(this, e);
        }
    }
}