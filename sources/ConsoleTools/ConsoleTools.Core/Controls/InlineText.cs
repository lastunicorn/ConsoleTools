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
using DustInTheWind.ConsoleTools.Controls.Rendering;

namespace DustInTheWind.ConsoleTools.Controls;

/// <summary>
/// Represents a text to be displayed in the console.
/// </summary>
public class InlineText : InlineControl
{
    /// <summary>
    /// Gets or sets the text.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Gets or sets a format string for the <see cref="Text"/> value.
    /// </summary>
    public string TextFormat { get; set; }

    public override int NaturalContentWidth => Text?.Length ?? 0;

    /// <summary>
    /// Initializes a new instance of the <see cref="InlineText"/> class.
    /// </summary>
    public InlineText()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InlineText"/> class with
    /// the text.
    /// </summary>
    public InlineText(string text)
    {
        Text = text;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InlineText"/> class with
    /// the text and the foreground color.
    /// </summary>
    public InlineText(string text, ConsoleColor foregroundColor)
    {
        Text = text;
        ForegroundColor = foregroundColor;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InlineText"/> class with
    /// the text, the foreground color and the background color.
    /// </summary>
    public InlineText(string text, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
    {
        Text = text;
        ForegroundColor = foregroundColor;
        BackgroundColor = backgroundColor;
    }

    /// <summary>
    /// Displays the <see cref="Text"/> value.
    /// </summary>
    protected override void DoDisplayContent()
    {
        if (Text == null)
            return;

        string formattedText = TextFormat == null
            ? Text
            : string.Format(TextFormat, Text);

        WriteText(formattedText);
    }

    /// <summary>
    /// Calculates and returns the length of the control, including the margins.
    /// </summary>
    /// <returns>The number of characters representing the length of the control including the margins.</returns>
    public int CalculateOuterLength()
    {
        int length = MarginLeft;

        if (Text != null)
            length += Text.Length;

        length += MarginRight;

        return length;
    }

    public override IRenderer GetRenderer(IDisplay display, RenderingOptions renderingOptions = null)
    {
        return new InlineTextRenderer(this, display, renderingOptions);
    }

    /// <summary>
    /// Converts a simple string into a <see cref="InlineText"/> object containing that string.
    /// </summary>
    /// <param name="text">The text to be converted.</param>
    public static implicit operator InlineText(string text)
    {
        return new InlineText(text);
    }

    /// <summary>
    /// Converts a <see cref="InlineText"/> object int a string by returning its text.
    /// </summary>
    /// <param name="inlineText">The <see cref="InlineText"/> object to be converted.</param>
    public static implicit operator string(InlineText inlineText)
    {
        return inlineText.Text;
    }
}