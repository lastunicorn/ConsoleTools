// ConsoleTools
// Copyright (C) 2017 Dust in the Wind
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
            Write(text, BorderColor);
        }

        public void WriteLineBorder(string text)
        {
            WriteLine(text, BorderColor);
        }

        public void WriteTitle(string text)
        {
            Write(text, TitleColor);
        }

        public void WriteLineTitle(string text)
        {
            WriteLine(text, TitleColor);
        }

        public void WriteHeader(string text)
        {
            Write(text, HeaderColor);
        }

        public void WriteLineHeader(string text)
        {
            WriteLine(text, HeaderColor);
        }

        public void WriteNormal(string text)
        {
            Write(text, NormalColor);
        }

        public void WriteLineNormal(string text)
        {
            WriteLine(text, NormalColor);
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }

        private static void Write(string text, ConsoleColor color)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = oldColor;
        }

        private static void WriteLine(string text, ConsoleColor color)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = oldColor;
        }
    }
}