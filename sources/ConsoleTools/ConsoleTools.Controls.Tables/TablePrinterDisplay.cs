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

namespace DustInTheWind.ConsoleTools.Controls.Tables
{
    internal class TablePrinterDisplay : DisplayBase
    {
        private readonly ITablePrinter tablePrinter;

        private ConsoleColor? temporaryForegroundColor;
        private ConsoleColor? temporaryBackgroundColor;

        public TablePrinterDisplay(ITablePrinter tablePrinter)
        {
            this.tablePrinter = tablePrinter ?? throw new ArgumentNullException(nameof(tablePrinter));
        }

        public override bool IsCursorVisible { get; set; } = Console.CursorVisible;

        public override int AvailableWidth { get; } = Console.WindowWidth;

        protected override void SetRowForegroundColor(ConsoleColor? foregroundColor)
        {
            temporaryForegroundColor = foregroundColor;
        }

        protected override void SetRowBackgroundColor(ConsoleColor? backgroundColor)
        {
            temporaryBackgroundColor = backgroundColor;
        }

        protected override void ResetRowForegroundColor()
        {
            temporaryForegroundColor = null;
        }

        protected override void ResetRowBackgroundColor()
        {
            temporaryBackgroundColor = null;
        }

        protected override void WriteNewLineInternal()
        {
            tablePrinter.WriteLine();
        }

        protected override void WriteInternal(string text)
        {
            if (string.IsNullOrEmpty(text))
                return;

            ConsoleColor? actualForegroundColor = temporaryForegroundColor ?? ForegroundColor;
            ConsoleColor? actualBackgroundColor = temporaryBackgroundColor ?? BackgroundColor;
            tablePrinter.Write(text, actualForegroundColor, actualBackgroundColor);
        }

        protected override void WriteInternal(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, string text)
        {
            if (string.IsNullOrEmpty(text))
                return;

            ConsoleColor? actualForegroundColor = foregroundColor ?? ForegroundColor;
            ConsoleColor? actualBackgroundColor = backgroundColor ?? BackgroundColor;
            tablePrinter.Write(text, actualForegroundColor, actualBackgroundColor);
        }

        protected override void WriteInternal(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, char c)
        {
            ConsoleColor? actualForegroundColor = foregroundColor ?? ForegroundColor;
            ConsoleColor? actualBackgroundColor = backgroundColor ?? BackgroundColor;
            tablePrinter.Write(c.ToString(), actualForegroundColor, actualBackgroundColor);
        }
    }
}