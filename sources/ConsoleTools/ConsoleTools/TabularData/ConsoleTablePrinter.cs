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

namespace DustInTheWind.ConsoleTools.TabularData
{
    public class ConsoleTablePrinter : ITablePrinter
    {
        public ConsoleColor BorderColor { get; set; }
        public ConsoleColor TitleColor { get; set; }
        public ConsoleColor HeaderColor { get; set; }
        public ConsoleColor NormalColor { get; set; }

        public ConsoleTablePrinter()
        {
            BorderColor = ConsoleColor.Gray;
            TitleColor = ConsoleColor.White;
            HeaderColor = ConsoleColor.White;
            NormalColor = ConsoleColor.Gray;
        }

        public void WriteBorder(string text)
        {
            CustomConsole.Write(BorderColor, text);
        }

        public void WriteBorder(char c)
        {
            CustomConsole.Write(BorderColor, c);
        }

        public void WriteLineBorder(string text)
        {
            CustomConsole.WriteLine(BorderColor, text);
        }

        public void WriteLineBorder(char c)
        {
            CustomConsole.WriteLine(BorderColor, c);
        }

        public void WriteTitle(string text)
        {
            CustomConsole.Write(TitleColor, text);
        }

        public void WriteHeader(string text)
        {
            CustomConsole.Write(HeaderColor, text);
        }

        public void WriteNormal(string text)
        {
            CustomConsole.Write(NormalColor, text);
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }
    }
}