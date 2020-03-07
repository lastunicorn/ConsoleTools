// ConsoleTools
// Copyright (C) 2017-2020 Dust in the Wind
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

namespace DustInTheWind.ConsoleTools.Menues
{
    /// <summary>
    /// Contains the data for the <see cref="MenuItemCollection.CurrentIndexChanged"/> event.
    /// </summary>
    public class CurrentIndexChangedEventArgs : EventArgs
    {
        /// <summary>
        /// The previously selected index.
        /// </summary>
        public int? PreviousIndex { get; }

        /// <summary>
        /// The newly selected index.
        /// </summary>
        public int? CurrentIndex { get; }

        /// <summary>
        /// Initializes a new instance for the <see cref="CurrentIndexChangedEventArgs"/> class.
        /// </summary>
        /// <param name="previousIndex">The previously selected index.</param>
        /// <param name="currentIndex">The newly selected index.</param>
        public CurrentIndexChangedEventArgs(int? previousIndex, int? currentIndex)
        {
            PreviousIndex = previousIndex;
            CurrentIndex = currentIndex;
        }
    }
}