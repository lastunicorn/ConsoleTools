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

using System;
using System.Collections.Generic;

namespace DustInTheWind.ConsoleTools.TabularData
{
    /// <summary>
    /// Represents a table cell that contains data.
    /// </summary>
    public abstract class CellBase
    {
        /// <summary>
        /// Gets or sets the content of the cell.
        /// </summary>
        public MultilineText Content { get; set; }

        /// <summary>
        /// Gets a value that specified if the cell contains no data.
        /// </summary>
        public bool IsEmpty => Content == null || Content.IsEmpty;

        /// <summary>
        /// Gets or sets the horizontal alignment of the content displayed in the cell.
        /// </summary>
        public HorizontalAlignment HorizontalAlignment { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CellBase" /> class with
        /// empty content.
        /// </summary>
        protected CellBase()
        {
            Content = MultilineText.Empty;
            HorizontalAlignment = HorizontalAlignment.Default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CellBase" /> class with
        /// the text contained by it.
        /// </summary>
        /// <param name="text">The text displayed in the cell.</param>
        protected CellBase(string text)
        {
            Content = new MultilineText(text);
            HorizontalAlignment = HorizontalAlignment.Default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CellBase" /> class with
        /// the text contained by it and its horizontal alignment.
        /// </summary>
        /// <param name="text">The text displayed in the cell.</param>
        /// <param name="horizontalAlignment">The horizontal alignment of the content of the new cell.</param>
        protected CellBase(string text, HorizontalAlignment horizontalAlignment)
        {
            Content = new MultilineText(text);
            HorizontalAlignment = horizontalAlignment;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CellBase" /> class with
        /// the text contained by it.
        /// </summary>
        /// <param name="text"></param>
        protected CellBase(MultilineText text)
        {
            Content = text;
            HorizontalAlignment = HorizontalAlignment.Default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CellBase" /> class with
        /// the text contained by it and its horizontal alignment.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="horizontalAlignment">The horizontal alignment of the content of the new cell.</param>
        protected CellBase(MultilineText text, HorizontalAlignment horizontalAlignment)
        {
            Content = text;
            HorizontalAlignment = horizontalAlignment;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CellBase" /> class with
        /// an object representing the content.
        /// </summary>
        protected CellBase(object content)
        {
            Content = new MultilineText(content.ToString());
            HorizontalAlignment = HorizontalAlignment.Default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CellBase" /> class with
        /// an object representing the content and its horizontal alignment.
        /// </summary>
        protected CellBase(object content, HorizontalAlignment horizontalAlignment)
        {
            Content = new MultilineText(content.ToString());
            HorizontalAlignment = horizontalAlignment;
        }

        /// <summary>
        /// Returns the size of the cell, including the padding.
        /// </summary>
        public Size CalculateDimensions()
        {
            int cellWidth;
            int cellHeight;

            int paddingLeft = CalculatePaddingLeft();
            int paddingRight = CalculatePaddingRight();

            if (IsEmpty)
            {
                cellWidth = paddingLeft + paddingRight;
                cellHeight = 0;
            }
            else
            {
                cellWidth = paddingLeft + Content.Size.Width + paddingRight;
                cellHeight = Content.Size.Height;
            }

            return new Size(cellWidth, cellHeight);
        }

        protected abstract int CalculatePaddingLeft();

        protected abstract int CalculatePaddingRight();

        /// <summary>
        /// Returns the string representation of the content of the cell.
        /// </summary>
        public override string ToString()
        {
            return Content?.ToString() ?? string.Empty;
        }

        public List<string> Render(int minWidth, int minHeight)
        {
            List<string> lines = new List<string>();

            int minLineCount = Math.Max(Content.Size.Height, minHeight);

            for (int i = 0; i < minLineCount; i++)
            {
                string line = RenderLine(i, minWidth);
                lines.Add(line);
            }

            return lines;
        }

        /// <summary>
        /// Returns a single line from the cell including the paddings.
        /// </summary>
        /// <param name="lineIndex">The index of the line to be generated.</param>
        /// <param name="minWidth">The minimum width of the cell.</param>
        /// <returns>A <see cref="string"/> representing a single line from the cell.</returns>
        public string RenderLine(int lineIndex, int minWidth)
        {
            int paddingLeftLength = CalculatePaddingLeft();
            int paddingRightLength = CalculatePaddingRight();

            int cellContentWidth = minWidth - paddingLeftLength - paddingRightLength;

            bool existsContentLine = lineIndex < Content.Size.Height;
            if (!existsContentLine)
                return new string(' ', minWidth);

            // Build inner content.

            string innerContent = Content.Lines[lineIndex];

            HorizontalAlignment alignment = CalculateHorizontalAlignment();

            switch (alignment)
            {
                case HorizontalAlignment.Left:
                    innerContent = innerContent.PadRight(cellContentWidth, ' ');
                    break;

                case HorizontalAlignment.Right:
                    innerContent = innerContent.PadLeft(cellContentWidth, ' ');
                    break;

                case HorizontalAlignment.Center:
                    int totalSpaces = cellContentWidth - Content.Size.Width;
                    int rightSpaces = (int)Math.Ceiling((double)totalSpaces / 2);
                    innerContent = innerContent
                        .PadLeft(cellContentWidth - rightSpaces, ' ')
                        .PadRight(cellContentWidth, ' ');
                    break;

                default:
                    throw new ApplicationException("Internal error: Invalid calculated horizontal alignment.");
            }

            // Build paddings.

            string leftPadding = new string(' ', paddingLeftLength);
            string rightPadding = new string(' ', paddingRightLength);

            // Concatenate everything.

            return leftPadding + innerContent + rightPadding;
        }

        protected abstract HorizontalAlignment CalculateHorizontalAlignment();
    }
}