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

namespace DustInTheWind.ConsoleTools.Controls.Tables.Printers;

/// <summary>
/// Writes the parts of a <see cref="Control"/> instance to the <see cref="Console"/>,
/// using different colors for each part.
/// </summary>
public class ConsoleDisplay : IInteractiveDisplay
{
    public ConsoleColor ForegroundColor
    {
        get => Console.ForegroundColor;
        set => Console.ForegroundColor = value;
    }

    public ConsoleColor BackgroundColor
    {
        get => Console.BackgroundColor;
        set => Console.BackgroundColor = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ConsoleDisplay"/> class
    /// as root printer.
    /// </summary>
    public ConsoleDisplay()
    {
    }

    public void Write(char c, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor)
    {
        if (foregroundColor.HasValue)
        {
            if (backgroundColor.HasValue)
                CustomConsole.Write(foregroundColor.Value, backgroundColor.Value, c);
            else
                CustomConsole.Write(foregroundColor.Value, c);
        }
        else
        {
            if (backgroundColor.HasValue)
                CustomConsole.WriteBackgroundColor(backgroundColor.Value, c);
            else
                CustomConsole.Write(c);
        }
    }

    public void Write(string text, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor)
    {
        if (foregroundColor.HasValue)
        {
            if (backgroundColor.HasValue)
                CustomConsole.Write(foregroundColor.Value, backgroundColor.Value, text);
            else
                CustomConsole.Write(foregroundColor.Value, text);
        }
        else
        {
            if (backgroundColor.HasValue)
                CustomConsole.WriteBackgroundColor(backgroundColor.Value, text);
            else
                CustomConsole.Write(text);
        }
    }

    public void EndLine()
    {
        Console.WriteLine();
    }

    /// <summary>
    /// Writes all the buffered data into the underlying stream of the console.
    /// </summary>
    public void Flush()
    {
        Console.Out.Flush();
    }
}