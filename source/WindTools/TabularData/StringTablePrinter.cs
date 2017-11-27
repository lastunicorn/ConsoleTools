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

using System.Text;

namespace DustInTheWind.ConsoleTools.TabularData
{
    public class StringTablePrinter : ITablePrinter
    {
        private readonly StringBuilder sb;

        public StringTablePrinter()
        {
            sb = new StringBuilder();
        }

        public void WriteBorder(string text)
        {
            sb.Append(text);
        }

        public void WriteLineBorder(string text)
        {
            sb.AppendLine(text);
        }

        public void WriteTitle(string text)
        {
            sb.Append(text);
        }

        public void WriteLineTitle(string text)
        {
            sb.AppendLine(text);
        }

        public void WriteHeader(string text)
        {
            sb.Append(text);
        }

        public void WriteLineHeader(string text)
        {
            sb.AppendLine(text);
        }

        public void WriteNormal(string text)
        {
            sb.Append(text);
        }

        public void WriteLineNormal(string text)
        {
            sb.AppendLine(text);
        }

        public void WriteLine()
        {
            sb.AppendLine();
        }

        public override string ToString()
        {
            return sb.ToString();
        }
    }
}