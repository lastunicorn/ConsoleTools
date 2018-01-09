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

namespace DustInTheWind.ConsoleTools
{
    internal class AlignedText
    {
        public string Text { get; set; }
        public HorizontalAlignment HorizontalAlignment { get; set; }
        public HorizontalAlignment DefaultHorizontalAlignment { get; set; } = HorizontalAlignment.Left;
        public int Width { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Text))
                return new string(' ', Width);

            switch (HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    Text = AlignLeft(Text, Width);
                    break;

                case HorizontalAlignment.Right:
                    Text = AlignRight(Text, Width);
                    break;

                case HorizontalAlignment.Center:
                    Text = AlignCenter(Text, Width);
                    break;

                default:
                    {
                        switch (DefaultHorizontalAlignment)
                        {
                            case HorizontalAlignment.Right:
                                Text = AlignRight(Text, Width);
                                break;

                            case HorizontalAlignment.Center:
                                Text = AlignCenter(Text, Width);
                                break;

                            default:
                                Text = AlignLeft(Text, Width);
                                break;
                        }
                    }
                    break;
            }

            return Text;
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
            int rightSpaces = (int)Math.Ceiling((double)totalSpaces / 2);
            text = text
                .PadLeft(width - rightSpaces, ' ')
                .PadRight(width, ' ');
            return text;
        }

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