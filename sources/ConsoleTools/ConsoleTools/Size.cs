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

using System.Globalization;

namespace DustInTheWind.ConsoleTools
{
    /// <summary>
    /// Represents the size of a rectangle.
    /// Imutable.
    /// </summary>
    public struct Size
    {
        /// <summary>
        /// Gets the width component.
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Gets the height component.
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Gets the empty size: width = 0, height = 0
        /// </summary>
        public static Size Empty { get; } = new Size(0, 0);

        /// <summary>
        /// Gets a value that specifies if the current instance represents the empty size (width = 0, height = 0)
        /// </summary>
        public bool IsEmpty => Width == 0 && Height == 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Size"/> structure with
        /// the width and height values.
        /// </summary>
        /// <param name="width">The width component of the size.</param>
        /// <param name="height">The height component of the size.</param>
        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">Another object to compare to.</param>
        /// <returns>true if obj and this instance are the same type and represent the same value; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Size))
                return false;

            Size size = (Size)obj;
            return size.Width == Width && size.Height == Height;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return Width ^ Height;
        }

        /// <summary>
        /// Return the string representation of the current instance.
        /// </summary>
        /// <returns>The string representation of the current instance.</returns>
        public override string ToString()
        {
            string widthAsString = Width.ToString(CultureInfo.CurrentCulture);
            string heightAsString = Height.ToString(CultureInfo.CurrentCulture);

            return $"{{Width={widthAsString}, Height={heightAsString}}}";
        }
    }
}
