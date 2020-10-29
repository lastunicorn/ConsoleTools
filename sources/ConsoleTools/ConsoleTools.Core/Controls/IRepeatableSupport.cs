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
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new/choose

using System;

namespace DustInTheWind.ConsoleTools
{
    /// <summary>
    /// Must be implemented by a control in order to offer additional support when used in the <see cref="ControlRepeater"/>.
    /// </summary>
    public interface IRepeatableSupport
    {
        /// <summary>
        /// Event raised when the control cannot be displayed anymore and it is in the "Closed" state.
        /// The <see cref="ControlRepeater"/> must also end its display loop.
        /// </summary>
        event EventHandler Closed;

        /// <summary>
        /// The <see cref="ControlRepeater"/> calls this method to announce the control that it should end its process.
        /// </summary>
        void RequestClose();
    }
}