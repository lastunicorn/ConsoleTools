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
using System.Collections.Generic;

namespace DustInTheWind.ConsoleTools
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// The content of the control is filled with the <see cref="Character"/> character.
    /// The control will always be one line height and, by default, it will stretch to fill the parent's client width.
    /// The <see cref="Width"/> property can be used to specify a smaller width if necessary.
    /// </remarks>
    public class HorizontalLine : BlockControl
    {
        /// <summary>
        /// Gets or sets the character to be used to fill the content of the control.
        /// </summary>
        public char Character { get; set; } = '-';

        /// <summary>
        /// Gets or sets the width of the control. It does not including the left and right margins.
        /// </summary>
        public int? Width { get; set; }

        public HorizontalAlignment HorizontalAlignment { get; set; }

        /// <summary>
        /// Gets or sets the number of spaces to be written before the text (to the left).
        /// Default value: 0
        /// </summary>
        public int MarginLeft { get; set; }

        /// <summary>
        /// Gets or sets the number of spaces to be written after the text (to the right).
        /// Default value: 0
        /// </summary>
        public int MarginRight { get; set; }

        /// <summary>
        /// Gets or sets the space outside the control that should be empty.
        /// </summary>
        public Thickness Margin
        {
            get => new Thickness(MarginLeft, MarginTop, MarginRight, MarginBottom);
            set
            {
                MarginLeft = value.Left;
                MarginTop = value.Top;
                MarginRight = value.Right;
                MarginBottom = value.Bottom;
            }
        }

        /// <summary>
        /// Gets or sets the space between the content and the margin of the control.
        /// </summary>
        public Thickness Padding { get; set; }

        private int ActualOuterWidth
        {
            get
            {
                if (Width.HasValue)
                    return Width.Value + MarginLeft + MarginRight;

                switch (DefaultParent)
                {
                    case DefaultParent.ConsoleBuffer:
                        return Console.BufferWidth;

                    case DefaultParent.ConsoleWindow:
                        return Console.WindowWidth;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private int ActualInnerWidth
        {
            get
            {
                if (Width.HasValue)
                    return Width.Value;

                switch (DefaultParent)
                {
                    case DefaultParent.ConsoleBuffer:
                        return Console.BufferWidth - MarginLeft - MarginRight;

                    case DefaultParent.ConsoleWindow:
                        return Console.WindowWidth - MarginLeft - MarginRight;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private int ActualContentWidth
        {
            get
            {
                if (Width.HasValue)
                    return Width.Value - Padding.Left - Padding.Right;

                switch (DefaultParent)
                {
                    case DefaultParent.ConsoleBuffer:
                        return Console.BufferWidth - MarginLeft - MarginRight - Padding.Left - Padding.Right;

                    case DefaultParent.ConsoleWindow:
                        return Console.WindowWidth - MarginLeft - MarginRight - Padding.Left - Padding.Right;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HorizontalLine"/> class.
        /// </summary>
        public HorizontalLine()
        {
            MarginTop = 1;
            MarginBottom = 1;
        }

        protected override void DoDisplayContent()
        {
            WriteTopPadding();

            string text = GenerateText();
            WriteTextLine(text);

            WriteBottomPadding();
        }

        private void WriteTopPadding()
        {
            if (Padding.Top <= 0)
                return;

            string text = new string(' ', ActualContentWidth);

            for (int i = 0; i < Padding.Top; i++)
                WriteTextLine(text);
        }

        private void WriteBottomPadding()
        {
            if (Padding.Bottom <= 0)
                return;

            string text = new string(' ', ActualContentWidth);

            for (int i = 0; i < Padding.Bottom; i++)
                WriteTextLine(text);
        }

        private MultilineText GenerateText()
        {
            return new string(Character, ActualContentWidth);
        }

        private void WriteTextLine(string text)
        {
            StartTextLine();
            WriteText(text);
            EndTextLine();
        }

        private void StartTextLine()
        {
            WriteLeftEmptySpace();
            WriteLeftMargin();
            WriteLeftPadding();
        }

        private void EndTextLine()
        {
            WriteRightPadding();
            WriteRightMargin();
            WriteRightEmptySpace();

            // Decide if new line is needed.
            if (ActualOuterWidth % Console.BufferWidth != 0)
                Console.WriteLine();
        }

        private void WriteLeftEmptySpace()
        {
            int availableWidth = DefaultParent == DefaultParent.ConsoleWindow
                ? Console.WindowWidth
                : Console.BufferWidth;

            int outerWidth = ActualOuterWidth;

            switch (HorizontalAlignment)
            {
                case HorizontalAlignment.Stretch:
                case HorizontalAlignment.Default:
                case HorizontalAlignment.Left:
                    break;

                case HorizontalAlignment.Center:
                    {
                        int allSpaces = availableWidth - outerWidth;
                        double halfSpaces = (double)allSpaces / 2;
                        int leftSpaces = (int)Math.Floor(halfSpaces);
                        Console.Write(new string(' ', leftSpaces));
                        break;
                    }

                case HorizontalAlignment.Right:
                    {
                        int allSpaces = availableWidth - outerWidth;
                        Console.Write(new string(' ', allSpaces));
                        break;
                    }

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void WriteRightEmptySpace()
        {
            int availableWidth = DefaultParent == DefaultParent.ConsoleWindow
                ? Console.WindowWidth
                : Console.BufferWidth;

            int outerWidth = ActualOuterWidth;

            switch (HorizontalAlignment)
            {
                case HorizontalAlignment.Stretch:
                case HorizontalAlignment.Default:
                case HorizontalAlignment.Left:
                {
                    int allSpaces = availableWidth - outerWidth;
                    Console.Write(new string(' ', allSpaces));
                    break;
                }

                case HorizontalAlignment.Center:
                {
                    int allSpaces = availableWidth - outerWidth;
                    double halfSpaces = (double)allSpaces / 2;
                    int leftSpaces = (int)Math.Ceiling(halfSpaces);
                    Console.Write(new string(' ', leftSpaces));
                    break;
                }

                case HorizontalAlignment.Right:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void WriteLeftMargin()
        {
            if (Margin.Left <= 0)
                return;

            string text = new string(' ', Margin.Left);
            Console.Write(text);
        }

        private void WriteRightMargin()
        {
            if (Margin.Right <= 0)
                return;

            string text = new string(' ', Margin.Right);
            Console.Write(text);
        }

        private void WriteLeftPadding()
        {
            if (Padding.Left <= 0)
                return;

            string text = new string(' ', Padding.Left);
            WriteText(text);
        }

        private void WriteRightPadding()
        {
            if (Padding.Right <= 0)
                return;

            string text = new string(' ', Padding.Right);
            WriteText(text);
        }
    }
}