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
    /// Represents the size of a rectangle.
    /// Immutable.
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
        /// Creates a new <see cref="Size"/> object having the <see cref="Width"/> and <see cref="Height"/>
        /// incremented with the specified value.
        /// </summary>
        /// <param name="value">The value to be added to the width and height of the current instance.</param>
        /// <returns>A new <see cref="Size"/> object.</returns>
        public Size Inflate(int value)
        {
            return new Size(Width + value, Height + value);
        }

        /// <summary>
        /// Creates a new <see cref="Size"/> object having its <see cref="Width"/> and <see cref="Height"/>
        /// incremented with the specified values.
        /// </summary>
        /// <param name="width">The value to be added to the width of the size.</param>
        /// <param name="height">The value to be added to the height of the current instance.</param>
        /// <returns>A new <see cref="Size"/> object.</returns>
        public Size Inflate(int width, int height)
        {
            return new Size(Width + width, Height + height);
        }

        /// <summary>
        /// Creates a new <see cref="Size"/> object having the <see cref="Width"/> and <see cref="Height"/>
        /// values equal to the sum of the <see cref="Width"/> and <see cref="Height"/> values of the
        /// current instance and the one specified as parameter.
        /// </summary>
        /// <param name="size">The <see cref="Size"/> object to be added to the current instance.</param>
        /// <returns>A new <see cref="Size"/> object.</returns>
        public Size Inflate(Size size)
        {
            return new Size(Width + size.Width, Height + size.Height);
        }

        /// <summary>
        /// Creates a new <see cref="Size"/> object having only the <see cref="Width"/>
        /// incremented with the specified value.
        /// </summary>
        /// <param name="value">The value to be added to the width of the current instance.</param>
        /// <returns>A new <see cref="Size"/> object.</returns>
        public Size InflateWidth(int value)
        {
            return new Size(Width + value, Height);
        }

        /// <summary>
        /// Creates a new <see cref="Size"/> object having only the <see cref="Height"/>
        /// incremented with the specified value.
        /// </summary>
        /// <param name="value">The value to be added to the height of the current instance.</param>
        /// <returns>A new <see cref="Size"/> object.</returns>
        public Size InflateHeight(int value)
        {
            return new Size(Width, Height + value);
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

        /// <summary>
        /// Creates a new <see cref="Size"/> instance having the width and height equal to the sums
        /// of the widths and heights of the instances received as parameters.
        /// </summary>
        public static Size operator +(Size size1, Size size2)
        {
            return new Size(size1.Width + size2.Width, size1.Height + size2.Height);
        }

        /// <summary>
        /// Creates a new <see cref="Size"/> instance having the width and height equal to the difference
        /// of the widths and heights of the instances received as parameters.
        /// </summary>
        public static Size operator -(Size size1, Size size2)
        {
            return new Size(size1.Width - size2.Width, size1.Height - size2.Height);
        }

        /// <summary>
        /// Creates a new <see cref="Size"/> instance having the width and height equal to the original values
        /// to which the integer value is added.
        /// </summary>
        public static Size operator +(Size size, int value)
        {
            return new Size(size.Width + value, size.Height + value);
        }

        /// <summary>
        /// Creates a new <see cref="Size"/> instance having the width and height equal to the original values
        /// from which the integer value is subtracted.
        /// </summary>
        public static Size operator -(Size size, int value)
        {
            return new Size(size.Width - value, size.Height - value);
        }
    }
}