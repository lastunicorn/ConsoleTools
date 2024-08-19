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
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.ConsoleTools.Controls.Tables.Printers;

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

    public override int DesiredContentWidth => Text?.Size.Width ?? 0;

    /// <summary>
    /// Returns the string representation of the current instance.
    /// </summary>
    /// <returns>The string representation of the current instance.</returns>
    public override string ToString()
    {
        StringDisplay display = new();
        DoDisplayContent(display);

        return display.ToString();
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

    public override IRenderer GetRenderer(RenderingOptions renderingOptions = null)
    {
        return new TextBlockRenderer(this, renderingOptions);
    }

    private class TextBlockRenderer : BlockControlRenderer<TextBlock>
    {
        private IEnumerator<string> chunksEnumerator;

        public TextBlockRenderer(TextBlock textBlock, RenderingOptions renderingOptions)
            : base(textBlock, renderingOptions)
        {
        }

        protected override bool DoInitializeContentRendering()
        {
            if (Control.Text == null)
            {
                chunksEnumerator = Enumerable.Empty<string>().GetEnumerator();
                return false;
            }

            int contentWidth = ControlLayout.ActualContentWidth;
            chunksEnumerator = Control.Text.GetLines(contentWidth, OverflowBehavior.CutChar)
                .GetEnumerator();

            return chunksEnumerator.MoveNext();
        }

        protected override bool DoRenderNextContentLine(IDisplay display)
        {
            display.WriteLine(chunksEnumerator.Current);

            return chunksEnumerator.MoveNext();
        }
    }
}