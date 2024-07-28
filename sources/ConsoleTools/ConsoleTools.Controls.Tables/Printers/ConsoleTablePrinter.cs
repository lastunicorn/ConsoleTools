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
/// Writes the parts of a <see cref="DataGrid"/> instance to the <see cref="Console"/>,
/// using different colors for each type of part.
/// </summary>
public class ConsoleTablePrinter : ITablePrinter
{
    /// <summary>
    /// Writes the specified character to the <see cref="Console"/>.
    /// </summary>
    public void Write(char c, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor)
    {
        Write(foregroundColor, backgroundColor, c);
    }

    /// <summary>
    /// Writes the specified text to the <see cref="Console"/>.
    /// </summary>
    public void Write(string text, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor)
    {
        Write(foregroundColor, backgroundColor, text);
    }

    /// <summary>
    /// Writes the specified text to the <see cref="Console"/>, followed by a line terminator.
    /// </summary>
    public void WriteLine(string text, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor)
    {
        WriteLine(foregroundColor, backgroundColor, text);
    }

    /// <summary>
    /// Writes the line terminator to the <see cref="Console"/>.
    /// </summary>
    public void WriteLine()
    {
        Console.WriteLine();
    }

    private static void Write(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, string text)
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

    private static void WriteLine(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, string text)
    {
        if (foregroundColor.HasValue)
        {
            if (backgroundColor.HasValue)
                CustomConsole.WriteLine(foregroundColor.Value, backgroundColor.Value, text);
            else
                CustomConsole.WriteLine(foregroundColor.Value, text);
        }
        else
        {
            if (backgroundColor.HasValue)
                CustomConsole.WriteLineBackgroundColor(backgroundColor.Value, text);
            else
                CustomConsole.WriteLine(text);
        }
    }

    private static void Write(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, char c)
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

    private static void WriteLine(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, char c)
    {
        if (foregroundColor.HasValue)
        {
            if (backgroundColor.HasValue)
                CustomConsole.WriteLine(foregroundColor.Value, backgroundColor.Value, c);
            else
                CustomConsole.WriteLine(foregroundColor.Value, c);
        }
        else
        {
            if (backgroundColor.HasValue)
                CustomConsole.WriteLineBackgroundColor(backgroundColor.Value, c);
            else
                CustomConsole.WriteLine(c);
        }
    }
}