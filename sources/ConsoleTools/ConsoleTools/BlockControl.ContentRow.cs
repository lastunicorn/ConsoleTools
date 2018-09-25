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
        protected void WriteControlRow(string text)
        {
            StartControlRow();

            CustomConsole.WithColors(ForegroundColor, BackgroundColor, () =>
            {
                CustomConsole.Write(text);

                if (text.Length < controlLayout.ActualContentWidth)
                {
                    string rightContentEmptySpace = new string(' ', controlLayout.ActualContentWidth - text.Length);
                    CustomConsole.Write(rightContentEmptySpace);
                }
            });

            EndControlRow();
        }

        private void StartControlRow()
        {
            WriteLeftEmptySpace();
            WriteLeftMargin();
            WriteLeftPadding();
        }

        private void EndControlRow()
        {
            WriteRightPadding();
            WriteRightMargin();
            WriteRightEmptySpace();

            // Decide if new line is needed.
            if (controlLayout.ActualFullWidth % Console.BufferWidth != 0)
                Console.WriteLine();
        }

        private void WriteLeftEmptySpace()
        {
            int spaces = controlLayout.EmptySpaceLeft;
            Console.Write(new string(' ', spaces));
        }

        private void WriteRightEmptySpace()
        {
            int spaces = controlLayout.EmptySpaceLeft;
            Console.Write(new string(' ', spaces));
        }
    }
}