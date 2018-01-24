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
// Bugs or feature requests
// --------------------------------------------------------------------------------
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new

using System;

namespace DustInTheWind.ConsoleTools.MenuControl
{
    /// <summary>
    /// Represent a menu item displayed by the <see cref="TextMenu"/>.
    /// </summary>
    public class TextMenuItem
    {
        /// <summary>
        /// Gets or sets the id of the menu item.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the text displayed by the current instance.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets a value that specifies if the current instance is displayed.
        /// Default value: <c>true</c>
        /// </summary>
        public bool IsVisible { get; set; } = true;

        /// <summary>
        /// Gets or sets a value that specifies if the current instance can be selected.
        /// This value is ignored if the <see cref="Command"/> property is set.
        /// Default value: <c>true</c>
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets the command to be executed when the current instance is selected.
        /// </summary>
        public ICommand Command { get; set; }

        /// <summary>
        /// Gets the size in characters necessary for the item to be rendered.
        /// </summary>
        public Size Size => Text == null
            ? Size.Empty
            : new Size(Text.Length, 1);

        /// <summary>
        /// Gets a value that specifies if the current instance can be selected.
        /// </summary>
        public bool CanBeSelected()
        {
            return (Command == null && IsEnabled) || (Command != null && Command.IsActive);
        }

        /// <summary>
        /// Displays the current instance to the Console starting from the current location of the cursor.
        /// </summary>
        public void Display()
        {
            if (CanBeSelected())
                CustomConsole.Write($"{Id} - {Text}");
            else
                CustomConsole.Write(ConsoleColor.DarkGray, $"{Id} - {Text}");
        }

        /// <summary>
        /// Selects the current instance and executes the associated <see cref="Command"/>.
        /// </summary>
        /// <returns><c>true</c> if the menu item was successfully selected; <c>false</c> otherwise.</returns>
        public bool Select()
        {
            Command?.Execute();

            return true;
        }
    }
}