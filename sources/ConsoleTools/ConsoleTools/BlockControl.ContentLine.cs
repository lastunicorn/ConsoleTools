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

namespace DustInTheWind.ConsoleTools
{
    partial class BlockControl
    {
        protected void WriteTextLine(string text)
        {
            StartTextLine();

            if (text.Length < ActualContentWidth)
                text += new string(' ', ActualContentWidth - text.Length);

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
            if (ActualFullWidth % Console.BufferWidth != 0)
                Console.WriteLine();
        }

        private void WriteLeftEmptySpace()
        {
            int availableWidth = AvailableWidth;
            int fullWidth = ActualFullWidth;

            switch (HorizontalAlignment)
            {
                case HorizontalAlignment.Default:
                case HorizontalAlignment.Left:
                    break;

                case HorizontalAlignment.Center:
                    {
                        int allSpaces = availableWidth - fullWidth;
                        double halfSpaces = (double)allSpaces / 2;
                        int leftSpaces = (int)Math.Floor(halfSpaces);
                        Console.Write(new string(' ', leftSpaces));
                        break;
                    }

                case HorizontalAlignment.Right:
                    {
                        int allSpaces = availableWidth - fullWidth;
                        Console.Write(new string(' ', allSpaces));
                        break;
                    }

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void WriteRightEmptySpace()
        {
            int availableWidth = AvailableWidth;
            int fullWidth = ActualFullWidth;

            switch (HorizontalAlignment)
            {
                case HorizontalAlignment.Default:
                case HorizontalAlignment.Left:
                    {
                        int allSpaces = availableWidth - fullWidth;
                        Console.Write(new string(' ', allSpaces));
                        break;
                    }

                case HorizontalAlignment.Center:
                    {
                        int allSpaces = availableWidth - fullWidth;
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

        /// <summary>
        /// Helper method that writes the specified text to the console using the
        /// <see cref="ForegroundColor"/> and <see cref="BackgroundColor"/> values.
        /// </summary>
        /// <param name="text">The text to be written to the console.</param>
        protected void WriteText(string text)
        {
            if (!ForegroundColor.HasValue && !BackgroundColor.HasValue)
                CustomConsole.Write(text);
            else if (ForegroundColor.HasValue && BackgroundColor.HasValue)
                CustomConsole.Write(ForegroundColor.Value, BackgroundColor.Value, text);
            else if (ForegroundColor.HasValue)
                CustomConsole.Write(ForegroundColor.Value, text);
            else
                CustomConsole.WriteBackgroundColor(BackgroundColor.Value, text);
        }
    }
}