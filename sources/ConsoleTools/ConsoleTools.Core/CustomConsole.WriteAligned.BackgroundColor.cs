﻿// ConsoleTools
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
using DustInTheWind.ConsoleTools.Controls;

namespace DustInTheWind.ConsoleTools;

public static partial class CustomConsole
{
    /// <summary>
    /// Writes the specified text to the Console.
    /// Additional parameters can be specified for the background color and the horizontal alignment in the Console's buffer.
    /// </summary>
    /// <param name="horizontalAlignment">The horizontal alignment in the Console's buffer.</param>
    /// <param name="backgroundColor">The background color used to write the text.</param>
    /// <param name="text">The text to be written to the Console.</param>
    public static void WriteBackgroundColor(HorizontalAlignment horizontalAlignment, ConsoleColor backgroundColor, string text)
    {
        Console.CursorLeft = CalculateStartPosition(text, horizontalAlignment);
        Write(backgroundColor, text);
    }

    /// <summary>
    /// Writes the text representation of the specified array of objects to the Console using the specified format information.
    /// Additional parameters can be specified for the background color and the horizontal alignment in the Console's buffer.
    /// </summary>
    /// <param name="horizontalAlignment">The horizontal alignment in the Console's buffer.</param>
    /// <param name="backgroundColor">The background color used to write the text.</param>
    /// <param name="format">A composite format string.</param>
    /// <param name="arg">An array of objects to write using format.</param>
    public static void WriteBackgroundColor(HorizontalAlignment horizontalAlignment, ConsoleColor backgroundColor, string format, params object[] arg)
    {
        string text = string.Format(format, arg);

        Console.CursorLeft = CalculateStartPosition(text, horizontalAlignment);
        Write(backgroundColor, text);
    }

    /// <summary>
    /// Writes the text representation of the specified object to the Console.
    /// Additional parameters can be specified for the background color and the horizontal alignment in the Console's buffer.
    /// </summary>
    /// <param name="horizontalAlignment">The horizontal alignment in the Console's buffer.</param>
    /// <param name="backgroundColor">The background color used to write the text.</param>
    /// <param name="o">The value to write.</param>
    public static void WriteBackgroundColor(HorizontalAlignment horizontalAlignment, ConsoleColor backgroundColor, object o)
    {
        string text = o?.ToString() ?? string.Empty;

        Console.CursorLeft = CalculateStartPosition(text, horizontalAlignment);
        Write(backgroundColor, text);
    }

    /// <summary>
    /// Writes the specified text to the Console, followed by the current line terminator.
    /// Additional parameters can be specified for the background color and the horizontal alignment in the Console's buffer.
    /// </summary>
    /// <param name="horizontalAlignment">The horizontal alignment in the Console's buffer.</param>
    /// <param name="backgroundColor">The background color used to write the text.</param>
    /// <param name="text">The text to be written to the Console.</param>
    public static void WriteLineBackgroundColor(HorizontalAlignment horizontalAlignment, ConsoleColor backgroundColor, string text)
    {
        Console.CursorLeft = CalculateStartPosition(text, horizontalAlignment);
        WriteLine(backgroundColor, text);
    }

    /// <summary>
    /// Writes the text representation of the specified array of objects, followed by the current line terminator,
    /// to the Console, using the specified format information.
    /// Additional parameters can be specified for the background color and the horizontal alignment in the Console's buffer.
    /// </summary>
    /// <param name="horizontalAlignment">The horizontal alignment in the Console's buffer.</param>
    /// <param name="backgroundColor">The background color used to write the text.</param>
    /// <param name="format">A composite format string.</param>
    /// <param name="arg">An array of objects to write using format.</param>
    public static void WriteLineBackgroundColor(HorizontalAlignment horizontalAlignment, ConsoleColor backgroundColor, string format, params object[] arg)
    {
        string text = string.Format(format, arg);

        Console.CursorLeft = CalculateStartPosition(text, horizontalAlignment);
        WriteLine(backgroundColor, text);
    }

    /// <summary>
    /// Writes the text representation of the specified object, followed by the current line terminator, to the Console.
    /// Additional parameters can be specified for the background color and the horizontal alignment in the Console's buffer.
    /// </summary>
    /// <param name="horizontalAlignment">The horizontal alignment in the Console's buffer.</param>
    /// <param name="backgroundColor">The background color used to write the text.</param>
    /// <param name="o">The value to write.</param>
    public static void WriteLineBackgroundColor(HorizontalAlignment horizontalAlignment, ConsoleColor backgroundColor, object o)
    {
        string text = o?.ToString() ?? string.Empty;

        Console.CursorLeft = CalculateStartPosition(text, horizontalAlignment);
        WriteLine(backgroundColor, text);
    }
}