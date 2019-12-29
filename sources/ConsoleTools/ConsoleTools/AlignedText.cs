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
        private bool isAnalyzed;

        private string text;
        private int width;
        private HorizontalAlignment horizontalAlignment;

        private int spaceLeftCount;
        private int spaceRightCount;

        /// <summary>
        /// Gets or sets the text that is aligned.
        /// </summary>
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                isAnalyzed = false;
            }
        }

        /// <summary>
        /// Gets or sets the width into which the text must be aligned.
        /// </summary>
        public int Width
        {
            get { return width; }
            set
            {
                width = value;
                isAnalyzed = false;
            }
        }

        /// <summary>
        /// Gets or sets the horizontal alignment to be applied if the <see cref="HorizontalAlignment"/> property
        /// is set on <see cref="HorizontalAlignment.Default"/>.
        /// </summary>
        public HorizontalAlignment DefaultHorizontalAlignment { get; set; } = HorizontalAlignment.Left;

        /// <summary>
        /// Gets or sets the horizontal alignment to be applied on the text.
        /// </summary>
        public HorizontalAlignment HorizontalAlignment
        {
            get { return horizontalAlignment; }
            set
            {
                horizontalAlignment = value;
                isAnalyzed = false;
            }
        }

        /// <summary>
        /// Gets the amount of space to be displayed at the left of the <see cref="Text"/>
        /// (before the text).
        /// </summary>
        public int SpaceLeftCount
        {
            get
            {
                if (!isAnalyzed)
                    Analyze();

                return spaceLeftCount;
            }
        }

        /// <summary>
        /// Gets the amount of space to be displayed at the right of the <see cref="Text"/>
        /// (after the text).
        /// </summary>
        public int SpaceRightCount
        {
            get
            {
                if (!isAnalyzed)
                    Analyze();

                return spaceRightCount;
            }
        }

        private void Analyze()
        {
            if (width <= 0)
            {
                spaceLeftCount = 0;
                spaceRightCount = 0;
                return;
            }

            HorizontalAlignment calculatedHorizontalAlignment = CalculateHorizontalAlignment();
            int textLength = text?.Length ?? 0;

            switch (calculatedHorizontalAlignment)
            {
                default:
                    spaceLeftCount = 0;
                    spaceRightCount = Math.Max(width - textLength, 0);
                    break;

                case HorizontalAlignment.Center:
                    int totalSpaces = width - textLength;
                    double halfSpaces = (double)totalSpaces / 2;

                    spaceLeftCount = (int)Math.Floor(halfSpaces);
                    spaceRightCount = (int)Math.Ceiling(halfSpaces);
                    break;

                case HorizontalAlignment.Right:
                    spaceLeftCount = Math.Max(width - textLength, 0);
                    spaceRightCount = 0;
                    break;
            }
        }

        private HorizontalAlignment CalculateHorizontalAlignment()
        {
            HorizontalAlignment calculatedHorizontalAlignment = HorizontalAlignment;

            if (calculatedHorizontalAlignment == HorizontalAlignment.Default)
            {
                calculatedHorizontalAlignment = DefaultHorizontalAlignment;

                if (calculatedHorizontalAlignment == HorizontalAlignment.Default)
                    calculatedHorizontalAlignment = HorizontalAlignment.Left;
            }

            return calculatedHorizontalAlignment;
        }

        /// <summary>
        /// Returns the text aligned in the specified space.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(Text))
                return new string(' ', Width);

            Analyze();

            StringBuilder sb = new StringBuilder();

            if (spaceLeftCount > 0)
                sb.Append(new string(' ', spaceLeftCount));

            sb.Append(text);

            if (spaceRightCount > 0)
                sb.Append(new string(' ', spaceRightCount));

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

        public static implicit operator AlignedText(string text)
        {
            return new AlignedText
            {
                Text = text
            };
        }

        public static implicit operator string(AlignedText alignedText)
        {
            return alignedText.ToString();
        }
    }
}