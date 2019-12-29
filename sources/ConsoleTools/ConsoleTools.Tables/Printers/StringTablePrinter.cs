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
using System.Text;

namespace DustInTheWind.ConsoleTools.TabularData.Printers
{
    /// <summary>
    /// Collects the rendered parts of a <see cref="DataGrid"/> instance as a plain text that is later
    /// returnd by the <see cref="ToString"/> method.
    /// </summary>
    public class StringTablePrinter : ITablePrinter
    {
        private readonly StringBuilder sb;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringTablePrinter"/> class.
        /// </summary>
        public StringTablePrinter()
        {
            sb = new StringBuilder();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringTablePrinter"/> class with
        /// the <see cref="StringBuilder"/> into which tp store the texts.
        /// </summary>
        public StringTablePrinter(StringBuilder sb)
        {
            if (sb == null) throw new ArgumentNullException(nameof(sb));
            this.sb = sb;
        }

        /// <summary>
        /// Stores the specified text in the internal <see cref="StringBuilder"/>.
        /// </summary>
        public void WriteBorder(string text)
        {
            sb.Append(text);
        }

        /// <summary>
        /// Stores the specified character in the internal <see cref="StringBuilder"/>.
        /// </summary>
        public void WriteBorder(char c)
        {
            sb.Append(c);
        }

        /// <summary>
        /// Stores the specified text in the internal <see cref="StringBuilder"/>, followed by a line terminator.
        /// </summary>
        public void WriteLineBorder(string text)
        {
            sb.AppendLine(text);
        }

        /// <summary>
        /// Stores the specified character in the internal <see cref="StringBuilder"/>, followed by a line terminator.
        /// </summary>
        public void WriteLineBorder(char c)
        {
            sb.AppendLine(c.ToString());
        }

        /// <summary>
        /// Stores the specified text in the internal <see cref="StringBuilder"/>.
        /// </summary>
        public void WriteTitle(string text)
        {
            sb.Append(text);
        }

        /// <summary>
        /// Stores the specified text in the internal <see cref="StringBuilder"/>.
        /// </summary>
        public void WriteHeader(string text)
        {
            sb.Append(text);
        }

        /// <summary>
        /// Stores the specified text in the internal <see cref="StringBuilder"/>.
        /// </summary>
        public void WriteNormal(string text)
        {
            sb.Append(text);
        }

        /// <summary>
        /// Stores the line terminator in the internal <see cref="StringBuilder"/>.
        /// </summary>
        public void WriteLine()
        {
            sb.AppendLine();
        }

        /// <summary>
        /// Returns the <see cref="string"/> built until now. 
        /// </summary>
        public override string ToString()
        {
            return sb.ToString();
        }
    }
}