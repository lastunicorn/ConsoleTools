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

namespace DustInTheWind.ConsoleTools.Spinners
{
    /// <summary>
    /// In the<see cref="ProgressBar"/>, it specifies the alignmet of the value inside tha space allocated for that value.
    /// </summary>
    public enum ValueAlignment
    {
        /// <summary>
        /// No alignment specified.
        /// </summary>
        None,

        /// <summary>
        /// Aligned to the left.
        /// </summary>
        Left,

        /// <summary>
        /// Aligned to the right.
        /// </summary>
        Right
    }
}