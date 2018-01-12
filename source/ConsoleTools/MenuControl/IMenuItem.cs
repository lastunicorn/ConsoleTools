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

namespace DustInTheWind.ConsoleTools.MenuControl
{
    /// <summary>
    /// Represent a menu item displayed by the <see cref="ScrollableMenu"/>.
    /// </summary>
    public interface IMenuItem
    {
        /// <summary>
        /// Gets or sets the text displayed by the current instance.
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Gets or sets a value that specifies if the current instance is displayed.
        /// </summary>
        bool IsVisible { get; set; }

        /// <summary>
        /// Gets or sets the horizontal alignment of the current instance inside the menu.
        /// </summary>
        HorizontalAlignment HorizontalAlignment { get; set; }

        /// <summary>
        /// Gets or sets a value that specifies if the current instance can be selected.
        /// </summary>
        bool IsSelectable { get; set; }

        /// <summary>
        /// Gets or sets the shortcut key that will select the current instance of <see cref="IMenuItem"/>.
        /// </summary>
        ConsoleKey? ShortcutKey { get; set; }

        /// <summary>
        /// Gets or sets the command to be executed when the current instance is selected.
        /// </summary>
        ICommand Command { get; set; }

        /// <summary>
        /// Gets or sets the menu that contains the current instance.
        /// </summary>
        ScrollableMenu ParentMenu { get; set; }

        /// <summary>
        /// Gets or sets the padding applied to the left of the text.
        /// The padding is part of the menu item's width.
        /// </summary>
        int PaddingLeft { get; set; }

        /// <summary>
        /// Gets or sets the padding applied to the right of the text.
        /// The padding is part of the menu item's width.
        /// </summary>
        int PaddingRight { get; set; }

        /// <summary>
        /// Gets the location in the console where the current instance was last rendered.
        /// If the current instance was never rendered, this value is <c>null</c>.
        /// </summary>
        Location? Location { get; }

        /// <summary>
        /// Gets the size necessary for the current instance to render.
        /// </summary>
        Size Size { get; }

        /// <summary>
        /// Event raised before the current instance is selected.
        /// It gives the oportunity for a subscriber to cancel the selection of the menu item.
        /// </summary>
        event EventHandler<CancelEventArgs> BeforeSelect;

        /// <summary>
        /// Displays the current instance to the Console starting from the current location of the cursor.
        /// </summary>
        /// <param name="size">The size in which the current instance must be displayed.</param>
        /// <param name="highlighted">A value that specifies if the menu item must be displayed highlighted.</param>
        void Display(Size size, bool highlighted);

        /// <summary>
        /// Selects the current instance and executes the associated <see cref="Command"/>.
        /// </summary>
        /// <returns><c>true</c> if the menu item was successfully selected; <c>false</c> otherwise.</returns>
        bool Select();
    }
}