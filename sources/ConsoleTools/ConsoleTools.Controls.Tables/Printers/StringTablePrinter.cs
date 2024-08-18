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
using System.Text;

namespace DustInTheWind.ConsoleTools.Controls.Tables.Printers;

/// <summary>
/// Collects the rendered parts of a <see cref="DataGrid"/> instance as a plain text that is later
/// returned by the <see cref="ToString"/> method.
/// </summary>
public class StringTablePrinter : ITablePrinter
{
    private readonly StringBuilder stringBuilder;
    private readonly bool isRoot;

    /// <summary>
    /// This property is ignored.
    /// </summary>
    public ConsoleColor? ForegroundColor { get; set; }

    /// <summary>
    /// This property is ignored.
    /// </summary>
    public ConsoleColor? BackgroundColor { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="StringTablePrinter"/> class as root.
    /// </summary>
    public StringTablePrinter()
    {
        stringBuilder = new StringBuilder();
        isRoot = true;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StringTablePrinter"/> class as child.
    /// </summary>
    public StringTablePrinter(StringBuilder stringBuilder)
    {
        this.stringBuilder = stringBuilder ?? throw new ArgumentNullException(nameof(stringBuilder));
        isRoot = false;
    }

    /// <summary>
    /// It is not used by the current instance.
    /// Writes nothing to the output stream.
    /// </summary>
    public void StartLine()
    {
    }

    /// <summary>
    /// Stores the specified character in the internal <see cref="StringBuilder"/>.
    /// </summary>
    public void Write(char c, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor)
    {
        stringBuilder.Append(c);
    }

    /// <summary>
    /// Stores the specified text in the internal <see cref="StringBuilder"/>.
    /// </summary>
    public void Write(string text, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor)
    {
        stringBuilder.Append(text);
    }

    /// <summary>
    /// Stores the line terminator in the internal <see cref="StringBuilder"/>.
    /// </summary>
    public void EndLine()
    {
        if (isRoot)
            stringBuilder.AppendLine();
    }

    /// <summary>
    /// Does nothing. The underlying <see cref="StringBuilder"/> does not have a buffer to be flushed.
    /// </summary>
    public void Flush()
    {
    }

    public ITablePrinter CreateChild()
    {
        return new StringTablePrinter(stringBuilder);
    }

    /// <summary>
    /// Returns the <see cref="string"/> built until now. 
    /// </summary>
    public override string ToString()
    {
        return stringBuilder.ToString();
    }
}