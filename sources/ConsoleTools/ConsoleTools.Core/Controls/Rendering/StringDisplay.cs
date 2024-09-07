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

namespace DustInTheWind.ConsoleTools.Controls.Rendering;

/// <summary>
/// It is used by a <see cref="Control"/> to render itself into as a string.
/// </summary>
public class StringDisplay : IDisplay
{
    private readonly StringBuilder stringBuilder;

    /// <summary>
    /// This property is ignored.
    /// </summary>
    public ConsoleColor ForegroundColor { get; set; }

    /// <summary>
    /// This property is ignored.
    /// </summary>
    public ConsoleColor BackgroundColor { get; set; }

    /// <summary>
    /// This property is ignored.
    /// </summary>
    public bool IsCursorVisible { get; set; }

    /// <summary>
    /// Gets <c>null</c>. The string does not have a maximum width.
    /// </summary>
    public int? MaxWidth => null;

    /// <summary>
    /// Initializes a new instance of the <see cref="StringDisplay"/> class as root.
    /// </summary>
    public StringDisplay()
    {
        stringBuilder = new StringBuilder();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StringDisplay"/> class as child.
    /// </summary>
    public StringDisplay(StringBuilder stringBuilder)
    {
        this.stringBuilder = stringBuilder ?? throw new ArgumentNullException(nameof(stringBuilder));
    }

    public void Write(char c, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor)
    {
        stringBuilder.Append(c);
    }

    public void Write(string text, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor)
    {
        stringBuilder.Append(text);
    }

    public void EndLine()
    {
        stringBuilder.AppendLine();
    }

    /// <summary>
    /// Does nothing. The underlying <see cref="StringBuilder"/> does not have a buffer to be flushed.
    /// </summary>
    public void Flush()
    {
    }

    /// <summary>
    /// Returns the <see cref="string"/> built until now. 
    /// </summary>
    public override string ToString()
    {
        return stringBuilder.ToString();
    }
}