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

namespace DustInTheWind.ConsoleTools.Controls;

/// <summary>
/// Displays a horizontal line by repeating a specific character.
/// Multiple aspects can be configured like width, horizontal alignment, etc.
/// </summary>
/// <remarks>
/// The content of the control is filled with the <see cref="Character"/> character.
/// The control will always be one line height and, by default, it will stretch to fill the parent's client width.
/// The <see cref="BlockControl.Width"/> property can be used to specify a smaller width if necessary.
/// </remarks>
public class HorizontalLine : BlockControl
{
    /// <summary>
    /// Gets or sets the character to be used to fill the content of the control.
    /// </summary>
    public char Character { get; set; } = '-';

    /// <summary>
    /// Gets the <see cref="int.MaxValue"/> value.
    /// The horizontal line is willing to be as wide as necessary.
    /// </summary>
    protected override int DesiredContentWidth => int.MaxValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="HorizontalLine"/> class.
    /// </summary>
    public HorizontalLine()
    {
        Margin = "0 1";
    }

    /// <summary>
    /// Displays the horizontal line.
    /// </summary>
    protected override void DoDisplayContent(IDisplay display)
    {
        int actualContentWidth = Layout.ActualContentWidth;
        string text = new(Character, actualContentWidth);
        display.WriteLine(text);
    }

    /// <summary>
    /// Displays a horizontal line with default settings.
    /// </summary>
    public static void QuickDisplay()
    {
        HorizontalLine horizontalLine = new();
        horizontalLine.Display();
    }

    /// <summary>
    /// Displays a horizontal line constructed using the specified character.
    /// </summary>
    public static void QuickDisplay(char character)
    {
        HorizontalLine horizontalLine = new()
        {
            Character = character
        };
        horizontalLine.Display();
    }

    /// <summary>
    /// Displays a horizontal line constructed using the specified <see cref="P:character"/> and
    /// the specified <see cref="P:foregroundColor"/>.
    /// </summary>
    public static void QuickDisplay(char character, ConsoleColor foregroundColor)
    {
        HorizontalLine horizontalLine = new()
        {
            Character = character,
            ForegroundColor = foregroundColor
        };
        horizontalLine.Display();
    }

    /// <summary>
    /// Creates a new instance of a <see cref="HorizontalLine"/> and, before displaying it to the console,
    /// it allows the caller to make adjustments to it.
    /// </summary>
    /// <param name="action">The action called before displaying the control, allowing the caller to make adjustments to it.</param>
    public static void QuickDisplay(Action<HorizontalLine> action)
    {
        HorizontalLine horizontalLine = new();
        action?.Invoke(horizontalLine);
        horizontalLine.Display();
    }

    /// <summary>
    /// Builds a string repeating the specified character and having the length equal to the width of the console's window.
    /// </summary>
    public static string WindowAsString(char c = '-')
    {
        return new string(c, Console.WindowWidth);
    }

    /// <summary>
    /// Builds a string repeating the specified character and having the length equal to the width of the console's buffer.
    /// </summary>
    public static string BufferAsString(char c = '-')
    {
        return new string(c, Console.BufferWidth);
    }
}