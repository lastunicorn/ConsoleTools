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
using System.Diagnostics;

namespace DustInTheWind.ConsoleTools.Controls.Rendering;

/// <summary>
/// Writes the parts of a <see cref="Control"/> instance to the <see cref="Console"/>,
/// using different colors for each part.
/// </summary>
public class ConsoleDisplay : IInteractiveDisplay
{
    private readonly ICursorVisibility cursorVisibility;

    /// <summary>
    /// Gets or sets the color used for writing the text to the console.
    /// </summary>
    public ConsoleColor ForegroundColor
    {
        get => Console.ForegroundColor;
        set => Console.ForegroundColor = value;
    }

    /// <summary>
    /// Gets or sets the color used for writing the background of the text to the console.
    /// </summary>
    public ConsoleColor BackgroundColor
    {
        get => Console.BackgroundColor;
        set => Console.BackgroundColor = value;
    }

    /// <summary>
    /// Gets or sets a value that specifies if the cursor is visible in the console.
    /// </summary>
    public bool IsCursorVisible
    {
        get => cursorVisibility.IsVisible();
        set
        {
            if (value)
                cursorVisibility.ShowCursor();
            else
                cursorVisibility.HideCursor();
        }
    }


    /// <summary>
    /// Gets the width of the console's buffer.
    /// </summary>
    public int? MaxWidth => Console.BufferWidth - 1;

    /// <summary>
    /// Gets a value specifying if the cursor is placed at the beginning of a new line.
    /// </summary>
    public bool IsNewLine => Console.CursorLeft == 0;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConsoleDisplay"/> class
    /// as root printer.
    /// </summary>
    public ConsoleDisplay()
    {
        cursorVisibility = CursorVisibility.Create();
    }

    /// <summary>
    /// Writes the specified character to the console, using the specified colors.
    /// </summary>
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

    /// <summary>
    /// Writes the specified text to the console, using the specified colors.
    /// </summary>
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

    /// <summary>
    /// Writes the line terminator to the console and moves the cursor to a new line.
    /// </summary>
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