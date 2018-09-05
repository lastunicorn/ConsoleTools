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
    public class HorizontalLine : BlockControl
    {
        public char Character { get; set; } = '-';

        public int? Width { get; set; }

        public HorizontalAlignment HorizontalAlignment { get; set; }

        private int InnerWidth
        {
            get
            {
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
            MultilineText multilineText = BuildLineText(InnerWidth);

            IEnumerable<string> lines = multilineText.GetLines(InnerWidth);

            foreach (string line in lines)
            {
                AlignedText alignedText = new AlignedText
                {
                    Text = line,
                    Width = InnerWidth,
                    HorizontalAlignment = HorizontalAlignment
                };

                string alignedLine = alignedText.ToString();

                WriteText(alignedLine);

                if (alignedLine.Length % Console.BufferWidth != 0)
                    Console.WriteLine();
            }
        }

        private MultilineText BuildLineText(int innerWidth)
        {
            int lineWidth = Width ?? innerWidth;
            return new string(Character, lineWidth);
        }
    }
}