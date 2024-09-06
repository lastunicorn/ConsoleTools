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

namespace DustInTheWind.ConsoleTools.Controls.Rendering;

/// <summary>
/// It is used by a <see cref="Control"/> to render itself into a <see cref="System.IO.Stream"/>.
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
    /// the <see cref="System.IO.Stream"/> into which the control will be written.
    /// </summary>
    public StreamDisplay(Stream stream)
    {
        Stream = stream ?? throw new ArgumentNullException(nameof(stream));

        streamWriter = new StreamWriter(stream);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StreamDisplay"/> class with
    /// the <see cref="System.IO.StreamWriter"/> into which the control will be written.
    /// </summary>
    public StreamDisplay(StreamWriter streamWriter)
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

    /// <summary>
    /// Writes the line terminator to the underlying stream.
    /// </summary>
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