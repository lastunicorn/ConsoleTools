// ConsoleTools
// Copyright (C) 2017-2022 Dust in the Wind
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
using System.Collections.Generic;

namespace DustInTheWind.ConsoleTools.Controls.Tables.RenderingModel
{
    internal class CellX
    {
        private IEnumerator<string> lineEnumerator;

        public MultilineText Content { get; set; }

        public Size Size { get; set; }

        public ConsoleColor? ForegroundColor { get; set; }

        public ConsoleColor? BackgroundColor { get; set; }

        public int PaddingLeft { get; set; }

        public int PaddingRight { get; set; }

        public HorizontalAlignment HorizontalAlignment { get; set; }

        public int HorizontalMerge { get; set; }

        public void CalculateLayout()
        {
            Size = CalculatePreferredSize();
        }

        private Size CalculatePreferredSize()
        {
            int cellWidth;
            int cellHeight;

            bool isEmpty = Content == null || Content.IsEmpty;

            if (isEmpty)
            {
                cellWidth = PaddingLeft + PaddingRight;
                cellHeight = 0;
            }
            else
            {
                cellWidth = PaddingLeft + Content.Size.Width + PaddingRight;
                cellHeight = Content.Size.Height;
            }

            return new Size(cellWidth, cellHeight);
        }

        public void RenderNextLine(ITablePrinter tablePrinter, Size actualSize)
        {
            if (lineEnumerator == null)
                lineEnumerator = RenderContent(actualSize).GetEnumerator();

            string content = lineEnumerator.MoveNext()
                ? lineEnumerator.Current
                : null;

            tablePrinter.Write(content, ForegroundColor, BackgroundColor);
        }

        private IEnumerable<string> RenderContent(Size size)
        {
            for (int i = 0; i < size.Height; i++)
                yield return RenderLine(i, size.Width);
        }

        private string RenderLine(int lineIndex, int width)
        {
            int cellContentWidth = width - PaddingLeft - PaddingRight;

            bool existsContentLine = lineIndex < Content.Size.Height;
            if (!existsContentLine)
                return new string(' ', width);

            // Build inner content.

            string innerContent = Content.Lines[lineIndex];

            innerContent = AlignedText.QuickAlign(innerContent, HorizontalAlignment, cellContentWidth);

            // Build paddings.

            string paddingLeft = new string(' ', PaddingLeft);
            string paddingRight = new string(' ', PaddingRight);

            // Concatenate everything.

            return paddingLeft + innerContent + paddingRight;
        }

        public static CellX CreateFrom(CellBase cellBase)
        {
            CellX cellX = new CellX
            {
                ForegroundColor = cellBase.CalculateForegroundColor(),
                BackgroundColor = cellBase.CalculateBackgroundColor(),
                PaddingLeft = cellBase.CalculatePaddingLeft(),
                PaddingRight = cellBase.CalculatePaddingRight(),
                HorizontalAlignment = cellBase.CalculateHorizontalAlignment(),
                Size = cellBase.CalculatePreferredSize(),
                Content = cellBase.Content
            };

            cellX.CalculateLayout();

            return cellX;
        }
    }
}