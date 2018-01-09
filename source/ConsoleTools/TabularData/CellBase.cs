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

        /// <summary>
        /// Returns the number of spaces representing the left padding.
        /// </summary>
        protected abstract int CalculatePaddingLeft();

        /// <summary>
        /// Returns the number of spaces representing the right padding.
        /// </summary>
        protected abstract int CalculatePaddingRight();

        /// <summary>
        /// Returns the string representation of the content of the cell.
        /// </summary>
        public override string ToString()
        {
            return Content?.ToString() ?? string.Empty;
        }

        /// <summary>
        /// Returns a list of text lines that represent the string representation of the cell.
        /// Every line from the list has the same length, and the length is equal or greater than the specified minWidth value.
        /// The number of lines in the list is equal or greater than the specified minHeight value.
        /// </summary>
        /// <param name="minWidth">The minimum width (characters) into which the cell must be rendered.</param>
        /// <param name="minHeight">The minimum height (lines) into which the cell must be rendered.</param>
        /// <returns></returns>
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

            innerContent = AlignedText.QuickAlign(innerContent, alignment, cellContentWidth);

            // Build paddings.

            string leftPadding = new string(' ', paddingLeftLength);
            string rightPadding = new string(' ', paddingRightLength);

            // Concatenate everything.

            return leftPadding + innerContent + rightPadding;
        }

        /// <summary>
        /// Returns the calculated horizontal alignment for the content of the current instance.
        /// The value is calculated based on the <see cref="HorizontalAlignment"/> property of the current instance,
        /// and the values specified by the parents.
        /// It should never return <see cref="HorizontalAlignment.Default"/>.
        /// </summary>
        protected abstract HorizontalAlignment CalculateHorizontalAlignment();
    }
}