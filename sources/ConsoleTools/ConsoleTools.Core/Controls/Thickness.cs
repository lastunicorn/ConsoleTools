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
using System.Globalization;

namespace DustInTheWind.ConsoleTools
{
    /// <summary>
    /// Represents the thickness of a rectangle.
    /// There can be provided different values for the top, bottom, left and right thicknesses.
    /// </summary>
    public struct Thickness : IEquatable<Thickness>
    {
        /// <summary>
        /// Gets the top value.
        /// Default value: 0
        /// </summary>
        public int Top { get;  }

        /// <summary>
        /// Gets the bottom value.
        /// Default value: 0
        /// </summary>
        public int Bottom { get; }

        /// <summary>
        /// Gets the left value.
        /// Default value: 0
        /// </summary>
        public int Left { get; }

        /// <summary>
        /// Gets the right value.
        /// Default value: 0
        /// </summary>
        public int Right { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Thickness" /> structure that has specific lengths applied to each side of the rectangle.
        /// </summary>
        /// <param name="left">The thickness for the left side of the rectangle.</param>
        /// <param name="top">The thickness for the upper side of the rectangle.</param>
        /// <param name="right">The thickness for the right side of the rectangle</param>
        /// <param name="bottom">The thickness for the lower side of the rectangle.</param>
        public Thickness(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Thickness" /> structure that has specific lengths applied to horizontal and vertical sides of the rectangle.
        /// </summary>
        /// <param name="horizontal">The thickness for the horizontal sides of the rectangle.</param>
        /// <param name="vertical">The thickness for the vertical sides of the rectangle.</param>
        public Thickness(int horizontal, int vertical)
        {
            Left = horizontal;
            Top = vertical;
            Right = horizontal;
            Bottom = vertical;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Thickness" /> structure that has the specified uniform length on each side.
        /// </summary>
        /// <param name="uniformLength">The uniform length applied to all four sides of the bounding rectangle.</param>
        public Thickness(int uniformLength)
        {
            Left = uniformLength;
            Top = uniformLength;
            Right = uniformLength;
            Bottom = uniformLength;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Thickness" /> structure
        /// using a text representation of the thickness.
        /// </summary>
        /// <param name="text">Must be 4 integer values separated by spaces.</param>
        /// <exception cref="ArgumentNullException">Exception thrown if the 'text' parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Exception thrown if the 'text' parameter does not have the correct format.</exception>
        public Thickness(string text)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));

            string[] chunks = text.Split(' ');

            switch (chunks.Length)
            {
                case 4:
                    Left = int.Parse(chunks[0]);
                    Top = int.Parse(chunks[1]);
                    Right = int.Parse(chunks[2]);
                    Bottom = int.Parse(chunks[3]);
                    
                    break;
                
                case 2:
                    int marginHorizontal = int.Parse(chunks[0]);
                    int marginVertical = int.Parse(chunks[1]);

                    Left = marginHorizontal;
                    Top = marginVertical;
                    Right = marginHorizontal;
                    Bottom = marginVertical;

                    break;
                
                case 1:
                    int margin = int.Parse(chunks[0]);

                    Left = margin;
                    Top = margin;
                    Right = margin;
                    Bottom = margin;
                    
                    break;
                
                default:
                    throw new ArgumentException("Invalid string representation.", nameof(text));
            }
        }

        /// <summary>
        /// Returns the string representation of the current instance.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string[] values = {
                Left.ToString(CultureInfo.InvariantCulture),
                Top.ToString(CultureInfo.InvariantCulture),
                Right.ToString(CultureInfo.InvariantCulture),
                Bottom.ToString(CultureInfo.InvariantCulture)
            };

            return string.Join(" ", values);
        }

        /// <summary>
        /// Compares this <see cref="Thickness" /> structure to another <see cref="object" /> for equality.
        /// </summary>
        /// <returns>true if the two objects are equal; otherwise, false.</returns>
        /// <param name="obj">The object to compare.</param>
        public override bool Equals(object obj)
        {
            if (obj is Thickness thickness)
                return this == thickness;

            return false;
        }

        /// <summary>
        /// Compares this <see cref="Thickness" /> structure to another <see cref="Thickness" /> structure for equality.
        /// </summary>
        /// <returns>true if the two instances of <see cref="Thickness" /> are equal; otherwise, false.</returns>
        /// <param name="thickness">An instance of <see cref="Thickness" /> to compare for equality.</param>
        public bool Equals(Thickness thickness)
        {
            return this == thickness;
        }

        /// <summary>
        /// Returns the hash code of the structure.
        /// </summary>
        /// <returns>A hash code for this instance of <see cref="T:System.Windows.Thickness" />.</returns>
        public override int GetHashCode()
        {
            return Left.GetHashCode() ^ Top.GetHashCode() ^ Right.GetHashCode() ^ Bottom.GetHashCode();
        }

        /// <summary>
        /// Compares the value of two <see cref="Thickness" /> structures for equality.
        /// </summary>
        /// <returns>true if the two instances of <see cref="Thickness" /> are equal; otherwise, false.</returns>
        /// <param name="t1">The first structure to compare.</param>
        /// <param name="t2">The other structure to compare.</param>
        public static bool operator ==(Thickness t1, Thickness t2)
        {
            return t1.Left == t2.Left &&
                   t1.Top == t2.Top &&
                   t1.Right == t2.Right &&
                   t1.Bottom == t2.Bottom;
        }

        /// <summary>
        /// Compares two <see cref="T:System.Windows.Thickness" /> structures for inequality.
        /// </summary>
        /// <returns>true if the two instances of <see cref="T:System.Windows.Thickness" /> are not equal; otherwise, false.</returns>
        /// <param name="t1">The first structure to compare.</param>
        /// <param name="t2">The other structure to compare.</param>
        public static bool operator !=(Thickness t1, Thickness t2)
        {
            return !(t1 == t2);
        }

        /// <summary>
        /// Converts a <see cref="string"/> representation of margins into a <see cref="Thickness"/> object.
        /// </summary>
        public static implicit operator Thickness(string text)
        {
            return new Thickness(text);
        }

        /// <summary>
        /// Created a <see cref="Thickness"/> object having all the values equal to the specified <see cref="int"/> value.
        /// </summary>
        public static implicit operator Thickness(int value)
        {
            return new Thickness(value);
        }

        /// <summary>
        /// Converts a <see cref="string"/> object into its <see cref="Thickness"/> representation.
        /// </summary>
        public static implicit operator string(Thickness margins)
        {
            return margins.ToString();
        }
    }
}