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
using System.IO;

namespace DustInTheWind.ConsoleTools.TabularData.Printers
{
    /// <summary>
    /// Stores the rendered parts of a <see cref="DataGrid"/> instance in a <see cref="Stream"/>.
    /// </summary>
    public class StreamTablePrinter : ITablePrinter, IDisposable
    {
        private bool isDisposed;

        private readonly Stream stream;
        private readonly StreamWriter streamWriter;

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamTablePrinter"/> class with
        /// the <see cref="Stream"/> into which the table will be written.
        /// </summary>
        public StreamTablePrinter(Stream stream)
        {
            this.stream = stream ?? throw new ArgumentNullException(nameof(stream));

            streamWriter = new StreamWriter(stream);
        }

        /// <summary>
        /// Writes the specified text in the underlying <see cref="Stream"/>.
        /// </summary>
        public void WriteBorder(string text)
        {
            streamWriter.Write(text);
        }

        /// <summary>
        /// Writes the specified character in the underlying <see cref="Stream"/>.
        /// </summary>
        public void WriteBorder(char c)
        {
            streamWriter.Write(c);
        }

        /// <summary>
        /// Writes the specified text in the underlying <see cref="Stream"/>, followed by a line terminator.
        /// </summary>
        public void WriteLineBorder(string text)
        {
            streamWriter.WriteLine(text);
        }

        /// <summary>
        /// Writes the specified text in the underlying <see cref="Stream"/>, followed by a line terminator.
        /// </summary>
        public void WriteLineBorder(char c)
        {
            streamWriter.WriteLine(c.ToString());
        }

        /// <summary>
        /// Writes the specified text in the underlying <see cref="Stream"/>.
        /// </summary>
        public void WriteTitle(string text)
        {
            streamWriter.Write(text);
        }

        /// <summary>
        /// Writes the specified text in the underlying <see cref="Stream"/>.
        /// </summary>
        public void WriteHeader(string text)
        {
            streamWriter.Write(text);
        }

        /// <summary>
        /// Writes the specified text in the underlying <see cref="Stream"/>.
        /// </summary>
        public void WriteNormal(string text)
        {
            streamWriter.Write(text);
        }

        /// <summary>
        /// Writes the line terminator in the underlying <see cref="Stream"/>.
        /// </summary>
        public void WriteLine()
        {
            streamWriter.WriteLine();
        }

        /// <summary>
        /// Disposes the underlying <see cref="Stream"/>.
        /// </summary>
        public void Dispose()
        {
            if (isDisposed)
                return;

            stream?.Dispose();
            streamWriter?.Dispose();

            isDisposed = true;
        }
    }
}