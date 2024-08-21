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

namespace DustInTheWind.ConsoleTools.Controls;

/// <summary>
/// This control displays a multiline text to the console.
/// </summary>
public class TextBlock : BlockControl
{
    /// <summary>
    /// Gets or sets the text to be displayed.
    /// </summary>
    public MultilineText Text { get; set; }

    /// <summary>
    /// Gets or sets a value specifying how to render the text that falls outside of the required
    /// available rendering space.
    /// The rendering space may be limited by the <see cref="BlockControl.Width"/>,
    /// <see cref="BlockControl.MaxWidth"/>, <see cref="BlockControl.MinWidth"/> or some external
    /// limitations from a possible parent control.
    /// </summary>
    public OverflowBehavior OverflowBehavior { get; set; } = OverflowBehavior.WrapWord;

    /// <summary>
    /// Gets the size of the underlying text when no restrictions are applied.
    /// </summary>
    public override int NaturalContentWidth => Text?.Size.Width ?? 0;

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
    /// Initializes a new instance of the <see cref="TextBlock"/> class
    /// with multiple text lines to be displayed.
    /// </summary>
    public TextBlock(params string[] lines)
    {
        Text = new MultilineText(lines);
    }

    /// <summary>
    /// Returns a renderer object that is able to render the current <see cref="Control"/>
    /// instance using a specified <see cref="IDisplay"/>.
    /// </summary>
    /// <returns>The <see cref="IRenderer"/> instance.</returns>
    public override IRenderer GetRenderer(IDisplay display, RenderingOptions renderingOptions = null)
    {
        return new TextBlockRenderer(this, display, renderingOptions);
    }

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