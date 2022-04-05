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
    internal class TablePrinterDisplay : IDisplay
    {
        private readonly ITablePrinter tablePrinter;

        private string temporaryContent;
        private ConsoleColor? temporaryForegroundColor;
        private ConsoleColor? temporaryBackgroundColor;

        public int DisplayedRowCount { get; }

        public ControlLayout Layout { get; set; }

        public ConsoleColor? ForegroundColor { get; set; }

        public ConsoleColor? BackgroundColor { get; set; }

        public TablePrinterDisplay(ITablePrinter tablePrinter)
        {
            this.tablePrinter = tablePrinter;
        }

        public void WriteRow(string text)
        {
            tablePrinter.WriteLine(text, ForegroundColor, BackgroundColor);
        }

        public void WriteRow(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, string text)
        {
            tablePrinter.WriteLine(text, foregroundColor, backgroundColor);
        }

        public void WriteRow()
        {
            tablePrinter.WriteLine();
        }

        public void StartRow()
        {
            temporaryContent = string.Empty;
            temporaryForegroundColor = ForegroundColor;
            temporaryBackgroundColor = BackgroundColor;
        }

        public void StartRow(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor)
        {
            temporaryContent = string.Empty;
            temporaryForegroundColor = foregroundColor;
            temporaryBackgroundColor = backgroundColor;
        }

        public void EndRow()
        {
            tablePrinter.WriteLine(temporaryContent, temporaryForegroundColor, temporaryBackgroundColor);

            temporaryContent = null;
            temporaryForegroundColor = null;
            temporaryBackgroundColor = null;
        }

        public void Write(string text)
        {
            tablePrinter.Write(text, ForegroundColor, BackgroundColor);
        }

        public void Write(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, string text)
        {
            tablePrinter.Write(text, foregroundColor, backgroundColor);
        }

        public void Write(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, char c)
        {
            tablePrinter.Write(c.ToString(), foregroundColor, backgroundColor);
        }
    }
}