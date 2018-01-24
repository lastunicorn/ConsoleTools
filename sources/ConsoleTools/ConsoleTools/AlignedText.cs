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
using System.Text;

namespace DustInTheWind.ConsoleTools
{
    /// <summary>
    /// Represents a text that can be horizontaly aligned into a space.
    /// </summary>
    public class AlignedText
    {
        /// <summary>
        /// Gets or sets the text that is aligned.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the horizontal alignment to be applied on the text.
        /// </summary>
        public HorizontalAlignment HorizontalAlignment { get; set; }

        /// <summary>
        /// Gets or sets the horizontal alignment to be applied if the <see cref="HorizontalAlignment"/> property
        /// is set on <see cref="HorizontalAlignment.Default"/>.
        /// </summary>
        public HorizontalAlignment DefaultHorizontalAlignment { get; set; } = HorizontalAlignment.Left;

        /// <summary>
        /// Gets or sets the width into which the text must be aligned.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets the amount of space to be displayed in the left of the <see cref="Text"/>.
        /// </summary>
        public int SpaceLeft
        {
            get
            {
                HorizontalAlignment calculatedHorizontalAlignment = CalculateHorizontalAlignment();

                switch (calculatedHorizontalAlignment)
                {
                    case HorizontalAlignment.Left:
                        return 0;

                    case HorizontalAlignment.Center:
                        int totalSpaces = Width - Text.Length;
                        return (int) Math.Floor((double) totalSpaces / 2);

                    case HorizontalAlignment.Right:
                        return Width - Text.Length;

                    default:
                        throw new ApplicationException("Invalid HorizontalAlignment value.");
                }
            }
        }

        private HorizontalAlignment CalculateHorizontalAlignment()
        {
            HorizontalAlignment horizontalAlignment = HorizontalAlignment;

            if (horizontalAlignment == HorizontalAlignment.Default)
            {
                horizontalAlignment = DefaultHorizontalAlignment;

                if (horizontalAlignment == HorizontalAlignment.Default)
                    horizontalAlignment = HorizontalAlignment.Left;
            }

            return horizontalAlignment;
        }

        /// <summary>
        /// Returns the text aligned in the specified space.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(Text))
                return new string(' ', Width);

            HorizontalAlignment calculatedHorizontalAlignment = CalculateHorizontalAlignment();

            switch (calculatedHorizontalAlignment)
            {
                default:
                    return AlignLeft(Text, Width);

                case HorizontalAlignment.Center:
                    return AlignCenter(Text, Width);

                case HorizontalAlignment.Right:
                    return AlignRight(Text, Width);
            }
        }

        private static string AlignLeft(string text, int width)
        {
            return text.PadRight(width);
        }

        private static string AlignRight(string text, int width)
        {
            return text.PadLeft(width);
        }

        private static string AlignCenter(string text, int width)
        {
            int totalSpaces = width - text.Length;
            double halfSpaces = (double) totalSpaces / 2;

            int leftSpaces = (int) Math.Floor(halfSpaces);
            int rightSpaces = (int) Math.Ceiling(halfSpaces);

            StringBuilder sb = new StringBuilder();

            sb.Append(new string(' ', leftSpaces));
            sb.Append(text);
            sb.Append(new string(' ', rightSpaces));

            return sb.ToString();
        }

        /// <summary>
        /// Aligns a text, in the specified width as is specified by the horizontalAlignemnt value.
        /// </summary>
        public static string QuickAlign(string text, HorizontalAlignment horizontalAlignment, int width)
        {
            AlignedText alignedText = new AlignedText
            {
                Text = text,
                HorizontalAlignment = horizontalAlignment,
                Width = width
            };

            return alignedText.ToString();
        }
    }
}