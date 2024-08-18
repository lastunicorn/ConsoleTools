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

using System.Collections.Generic;

namespace DustInTheWind.ConsoleTools.Controls;

/// <summary>
/// This control displays a multiline text to the console.
/// </summary>
public class TextBlock : BlockControl
{
    /// <summary>
    /// Gets or sets the text.
    /// </summary>
    public MultilineText Text { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TextBlock"/> class.
    /// </summary>
    public TextBlock()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TextBlock"/> class
    /// with the text to be displayed.
    /// </summary>
    public TextBlock(MultilineText text)
    {
        Text = text;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TextBlock"/> class
    /// with the text to be displayed.
    /// </summary>
    public TextBlock(string text)
    {
        Text = text;
    }

    /// <summary>
    /// Displays the lines of text together with the left and right margins.
    /// </summary>
    protected override void DoDisplayContent(IDisplay display)
    {
        if (Text == null)
            return;

        IEnumerable<string> chunks = Text.GetLines(Layout.ActualContentWidth, OverflowBehavior.CutChar);

        foreach (string chunk in chunks)
            display.WriteLine(chunk);
    }

    //protected override int ActualContentHeight => Text?.CalculateSize(ActualContentWidth).Height ?? 0;

    protected override int DesiredContentWidth => Text?.Size.Width ?? 0;

    /// <summary>
    /// Displays the specified text into the console.
    /// </summary>
    /// <param name="text">The text to be displayed to the console.</param>
    private static void QuickDisplay(string text)
    {
        TextBlock textBlock = new()
        {
            Text = text
        };
        textBlock.Display();
    }
}