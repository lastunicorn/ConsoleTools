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

namespace DustInTheWind.ConsoleTools
{
    /// <summary>
    /// Provided base functionality for a control.
    /// </summary>
    public abstract class Control
    {
        /// <summary>
        /// Gets or sets the number of empty lines displayed before the pause text.
        /// Default value: 0
        /// </summary>
        public int MarginTop { get; set; }

        /// <summary>
        /// Gets or sets the number of empty lines displayed after the pause text, after the pause was ended.
        /// Default value: 0
        /// </summary>
        public int MarginBottom { get; set; }

        /// <summary>
        /// Displays the pause text and waits for the user to press a key.
        /// </summary>
        public void Display()
        {
            WriteTopMargin();

            OnDisplay();

            WriteBottomMargin();
        }

        /// <summary>
        /// When implemented by an inheritor it displays the content of the control to the console.
        /// </summary>
        protected abstract void OnDisplay();

        private void WriteTopMargin()
        {
            for (int i = 0; i < MarginTop; i++)
                Console.WriteLine();
        }

        private void WriteBottomMargin()
        {
            for (int i = 0; i < MarginBottom; i++)
                Console.WriteLine();
        }
    }
}