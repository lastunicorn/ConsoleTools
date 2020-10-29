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

using System.Globalization;

namespace DustInTheWind.ConsoleTools
{
    /// <summary>
    /// Represents a location in the 2D plane.
    /// Immutable.
    /// </summary>
    public struct Location
    {
        /// <summary>
        /// Gets the left component.
        /// </summary>
        public int Left { get; }

        /// <summary>
        /// Gets the top component.
        /// </summary>
        public int Top { get; }

        /// <summary>
        /// Gets the origin location: left = 0, top = 0
        /// </summary>
        public static Location Origin { get; } = new Location(0, 0);

        /// <summary>
        /// Gets a value that specifies if the current instance is the origin (left = 0, top = 0)
        /// </summary>
        public bool IsOrigin => Left == 0 && Top == 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> structure with
        /// the left and top values.
        /// </summary>
        /// <param name="left">The left component of the location.</param>
        /// <param name="top">The top component of the location.</param>
        public Location(int left, int top)
        {
            Left = left;
            Top = top;
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">Another object to compare to.</param>
        /// <returns>true if obj and this instance are the same type and represent the same value; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Location))
                return false;

            Location location = (Location) obj;
            return location.Left == Left && location.Top == Top;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return Left ^ Top;
        }

        /// <summary>
        /// Return the string representation of the current instance.
        /// </summary>
        /// <returns>The string representation of the current instance.</returns>
        public override string ToString()
        {
            string widthAsString = Left.ToString(CultureInfo.CurrentCulture);
            string heightAsString = Top.ToString(CultureInfo.CurrentCulture);

            return $"{{Left={widthAsString}, Top={heightAsString}}}";
        }
    }
}