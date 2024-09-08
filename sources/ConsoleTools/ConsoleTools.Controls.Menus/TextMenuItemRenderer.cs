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

using System;
using DustInTheWind.ConsoleTools.Controls.Rendering;

namespace DustInTheWind.ConsoleTools.Controls.Menus;

internal class TextMenuItemRenderer : IRenderer
{
    private readonly IDisplay display;
    private readonly RenderingOptions renderingOptions;

    public bool HasMoreLines { get; private set; }

    private TextMenuItem Control { get; }

    public TextMenuItemRenderer(TextMenuItem textMenuItem, IDisplay display, RenderingOptions renderingOptions)
    {
        Control = textMenuItem ?? throw new ArgumentNullException(nameof(textMenuItem));
        this.display = display ?? throw new ArgumentNullException(nameof(display));
        this.renderingOptions = renderingOptions;

        HasMoreLines = true;
    }

    public void RenderNextLine()
    {
        if (Control.CanBeSelected())
        {
            Write($"{Control.Id} - {Control.Text}", Control.ForegroundColor);
        }
        else
        {
            if (Control.DisabledForegroundColor.HasValue)
                Write($"{Control.Id} - {Control.Text}", Control.DisabledForegroundColor.Value);
            else
                Write($"{Control.Id} - {Control.Text}", Control.ForegroundColor);
        }

        HasMoreLines = false;
    }

    private void Write(string text, ConsoleColor? foregroundColor = null)
    {
        display.Write(text, foregroundColor);
        renderingOptions?.OnLineRendered?.Invoke(text.Length);
    }

    public void Reset()
    {
        HasMoreLines = true;
    }
}