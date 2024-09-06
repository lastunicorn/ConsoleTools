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

using System;
using System.Collections.Generic;
using System.Text;

namespace DustInTheWind.ConsoleTools.Controls.Rendering;

internal class StringLinesDisplay : IDisplay
{
    private readonly List<string> lines = new();

    private readonly StringBuilder buffer = new();
    private bool shouldReplaceLastLine;

    /// <summary>
    /// This property is ignored.
    /// </summary>
    public ConsoleColor ForegroundColor { get; set; }

    /// <summary>
    /// This property is ignored.
    /// </summary>
    public ConsoleColor BackgroundColor { get; set; }

    public void Write(char c, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor)
    {
        buffer.Append(c);
    }

    public void Write(string text, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor)
    {
        buffer.Append(text);
    }

    public void EndLine()
    {
        if (buffer.Length > 0)
        {
            if (shouldReplaceLastLine)
            {
                lines[lines.Count - 1] = buffer.ToString();
                shouldReplaceLastLine = false;
            }
            else
            {
                lines.Add(buffer.ToString());
            }

            buffer.Clear();
        }
        else
        {
            lines.Add(string.Empty);
        }
    }

    public void Flush()
    {
        if (buffer.Length <= 0)
            return;

        if (shouldReplaceLastLine)
            lines[lines.Count - 1] = buffer.ToString();
        else
            lines.Add(buffer.ToString());

        shouldReplaceLastLine = true;
    }

    public IReadOnlyCollection<string> GetLines()
    {
        return lines;
    }
}