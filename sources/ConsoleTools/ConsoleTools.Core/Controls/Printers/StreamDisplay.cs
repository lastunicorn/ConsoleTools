// ConsoleTools
// Copyright (C) 2017-2024 Dust in the Wind
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
using System.IO;

namespace DustInTheWind.ConsoleTools.Controls.Tables.Printers;

/// <summary>
/// Stores the rendered parts of a <see cref="DataGrid"/> instance in a <see cref="System.IO.Stream"/>.
/// </summary>
public class StreamDisplay : IDisplay, IDisposable
{
    private bool isDisposed;

    private readonly StreamWriter streamWriter;

    /// <summary>
    /// This property is ignored.
    /// </summary>
    public ConsoleColor ForegroundColor { get; set; }

    /// <summary>
    /// This property is ignored.
    /// </summary>
    public ConsoleColor BackgroundColor { get; set; }

    /// <summary>
    /// Gets the underlying stream into which the data is written.
    /// </summary>
    public Stream Stream { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="StreamDisplay"/> class with
    /// the <see cref="System.IO.Stream"/> into which the table will be written.
    /// </summary>
    public StreamDisplay(Stream stream)
    {
        this.Stream = stream ?? throw new ArgumentNullException(nameof(stream));

        streamWriter = new StreamWriter(stream);
    }

    private StreamDisplay(StreamWriter streamWriter)
    {
        this.streamWriter = streamWriter ?? throw new ArgumentNullException(nameof(streamWriter));
    }

    public void Write(char c, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor)
    {
        streamWriter.Write(c);
    }

    public void Write(string text, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor)
    {
        streamWriter.Write(text);
    }

    public void EndLine()
    {
        streamWriter.WriteLine();
    }

    /// <summary>
    /// Writes all the buffered data into the underlying stream.
    /// </summary>
    public void Flush()
    {
        streamWriter.Flush();
    }

    /// <summary>
    /// Disposes the underlying <see cref="System.IO.Stream"/>.
    /// </summary>
    public void Dispose()
    {
        if (isDisposed)
            return;

        if (streamWriter != null)
        {
            streamWriter?.Flush();
            streamWriter?.Dispose();
        }

        Stream?.Dispose();

        isDisposed = true;
    }
}


///// <summary>
///// Stores the rendered parts of a <see cref="DataGrid"/> instance in a <see cref="Stream"/>.
///// </summary>
//public class StreamDisplay : IDisplay, IDisposable
//{
//    private bool isDisposed;

//    private readonly Stream stream;
//    private readonly StreamWriter streamWriter;
//    private readonly bool isRoot;

//    /// <summary>
//    /// This property is ignored.
//    /// </summary>
//    public ConsoleColor? ForegroundColor { get; set; }

//    /// <summary>
//    /// This property is ignored.
//    /// </summary>
//    public ConsoleColor? BackgroundColor { get; set; }

//    /// <summary>
//    /// Initializes a new instance of the <see cref="StreamDisplay"/> class with
//    /// the <see cref="Stream"/> into which the table will be written.
//    /// </summary>
//    public StreamDisplay(Stream stream)
//    {
//        this.stream = stream ?? throw new ArgumentNullException(nameof(stream));

//        streamWriter = new StreamWriter(stream);
//        isRoot = true;
//    }

//    private StreamDisplay(StreamWriter streamWriter)
//    {
//        this.streamWriter = streamWriter ?? throw new ArgumentNullException(nameof(streamWriter));
//        isRoot = false;
//    }

//    /// <summary>
//    /// It is not used by the current instance.
//    /// Writes nothing to the output stream.
//    /// </summary>
//    public void StartLine()
//    {
//    }

//    /// <summary>
//    /// Writes the specified character in the underlying <see cref="Stream"/>.
//    /// </summary>
//    public void Write(char c, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor)
//    {
//        streamWriter.Write(c);
//    }

//    /// <summary>
//    /// Writes the specified text in the underlying <see cref="Stream"/>.
//    /// </summary>
//    public void Write(string text, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor)
//    {
//        streamWriter.Write(text);
//    }

//    /// <summary>
//    /// Writes the line terminator in the underlying <see cref="Stream"/>.
//    /// </summary>
//    public void EndLine()
//    {
//        if (isRoot)
//            streamWriter.WriteLine();
//    }

//    public void WriteLine(string text, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null)
//    {
//        StartLine();
//        Write(text, foregroundColor, backgroundColor);
//        EndLine();
//    }

//    /// <summary>
//    /// Writes all the buffered data into the underlying stream.
//    /// </summary>
//    public void Flush()
//    {
//        streamWriter.Flush();
//    }

//    /// <summary>
//    /// Creates a new child instance of the current <see cref="StreamDisplay"/> instance that
//    /// writes in the same <see cref="Stream"/> as the current instance.
//    /// </summary>
//    /// <returns>The newly created <see cref="StreamDisplay"/> instance.</returns>
//    public IDisplay CreateChild()
//    {
//        return new StreamDisplay(streamWriter);
//    }

//    /// <summary>
//    /// Disposes the underlying <see cref="Stream"/>.
//    /// </summary>
//    public void Dispose()
//    {
//        if (isDisposed)
//            return;

//        if (streamWriter != null)
//        {
//            streamWriter?.Flush();
//            streamWriter?.Dispose();
//        }

//        stream?.Dispose();

//        isDisposed = true;
//    }
//}