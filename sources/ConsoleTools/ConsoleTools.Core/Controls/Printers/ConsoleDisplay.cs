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
public class ConsoleDisplay : DisplayBase
{
    public override ConsoleColor ForegroundColor
    {
        get => Console.ForegroundColor;
        set => Console.ForegroundColor = value;
    }

    public override ConsoleColor BackgroundColor
    {
        get => Console.BackgroundColor;
        set => Console.BackgroundColor = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ConsoleDisplay"/> class
    /// as root printer.
    /// </summary>
    public ConsoleDisplay()
        : base(true)
    {
    }

    private ConsoleDisplay(bool isRoot)
        : base(isRoot)
    {
    }

    public override void DoWrite(char c, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor)
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

    public override void DoWrite(string text, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor)
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

    public override void DoWriteRootEndLine()
    {
        Console.WriteLine();
    }

    /// <summary>
    /// Writes all the buffered data into the underlying stream of the console.
    /// </summary>
    public override void Flush()
    {
        Console.Out.Flush();
    }

    /// <summary>
    /// Creates a new child instance of the current <see cref="ConsoleDisplay"/> instance.
    /// </summary>
    /// <returns>The newly created <see cref="ConsoleDisplay"/> instance.</returns>
    public override IDisplay CreateChild()
    {
        return new ConsoleDisplay(false)
        {
            ForegroundColor = ForegroundColor,
            BackgroundColor = BackgroundColor
        };
    }
}


///// <summary>
///// Writes the parts of a <see cref="DataGrid"/> instance to the <see cref="Console"/>,
///// using different colors for each type of part.
///// </summary>
//public class ConsoleDisplay : IDisplay
//{
//    private readonly bool isRoot;

//    /// <summary>
//    /// Gets or sets the foreground color used to write the text.
//    /// Default value: <c>null</c>
//    /// </summary>
//    public ConsoleColor? ForegroundColor { get; set; }

//    /// <summary>
//    /// Gets or sets the background color used to write the text.
//    /// Default value: <c>null</c>
//    /// </summary>
//    public ConsoleColor? BackgroundColor { get; set; }

//    /// <summary>
//    /// Initializes a new instance of the <see cref="ConsoleDisplay"/> class
//    /// as root printer.
//    /// </summary>
//    public ConsoleDisplay()
//    {
//        isRoot = true;
//    }

//    private ConsoleDisplay(bool isRoot)
//    {
//        this.isRoot = isRoot;
//    }

//    /// <summary>
//    /// It is not used by the current instance.
//    /// Writes nothing to the output stream.
//    /// </summary>
//    public void StartLine()
//    {
//    }

//    /// <summary>
//    /// Writes the specified character to the <see cref="Console"/>.
//    /// </summary>
//    public void Write(char c, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null)
//    {
//        ConsoleColor? fg = foregroundColor ?? ForegroundColor;
//        ConsoleColor? bg = backgroundColor ?? BackgroundColor;

//        Write(fg, bg, c);
//    }

//    /// <summary>
//    /// Writes the specified text to the <see cref="Console"/>.
//    /// </summary>
//    public void Write(string text, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null)
//    {
//        ConsoleColor? fg = foregroundColor ?? ForegroundColor;
//        ConsoleColor? bg = backgroundColor ?? BackgroundColor;

//        Write(fg, bg, text);
//    }

//    /// <summary>
//    /// Writes the line terminator to the <see cref="Console"/>.
//    /// </summary>
//    public void EndLine()
//    {
//        if (isRoot)
//            Console.WriteLine();
//    }

//    public void WriteLine(string text, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null)
//    {
//        StartLine();
//        Write(text, foregroundColor, backgroundColor);
//        EndLine();
//    }

//    /// <summary>
//    /// Writes all the buffered data into the underlying stream of the console.
//    /// </summary>
//    public void Flush()
//    {
//        Console.Out.Flush();
//    }

//    /// <summary>
//    /// Creates a new child instance of the current <see cref="ConsoleDisplay"/> instance.
//    /// </summary>
//    /// <returns>The newly created <see cref="ConsoleDisplay"/> instance.</returns>
//    public IDisplay CreateChild()
//    {
//        return new ConsoleDisplay(false)
//        {
//            ForegroundColor = ForegroundColor,
//            BackgroundColor = BackgroundColor
//        };
//    }

//    private static void Write(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, string text)
//    {
//        if (foregroundColor.HasValue)
//        {
//            if (backgroundColor.HasValue)
//                CustomConsole.Write(foregroundColor.Value, backgroundColor.Value, text);
//            else
//                CustomConsole.Write(foregroundColor.Value, text);
//        }
//        else
//        {
//            if (backgroundColor.HasValue)
//                CustomConsole.WriteBackgroundColor(backgroundColor.Value, text);
//            else
//                CustomConsole.Write(text);
//        }
//    }

//    private static void Write(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, char c)
//    {
//        if (foregroundColor.HasValue)
//        {
//            if (backgroundColor.HasValue)
//                CustomConsole.Write(foregroundColor.Value, backgroundColor.Value, c);
//            else
//                CustomConsole.Write(foregroundColor.Value, c);
//        }
//        else
//        {
//            if (backgroundColor.HasValue)
//                CustomConsole.WriteBackgroundColor(backgroundColor.Value, c);
//            else
//                CustomConsole.Write(c);
//        }
//    }
//}