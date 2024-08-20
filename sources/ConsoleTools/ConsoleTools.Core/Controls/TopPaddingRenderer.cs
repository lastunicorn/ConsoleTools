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

namespace DustInTheWind.ConsoleTools.Controls;

internal class TopPaddingRenderer : IRenderer
{
    private int lineIndex;
    private readonly ControlDisplay controlDisplay;
    private readonly ControlLayout controlLayout;

    public bool HasMoreLines => lineIndex < controlLayout.Padding.Top;

    public TopPaddingRenderer(ControlDisplay controlDisplay, ControlLayout controlLayout)
    {
        this.controlDisplay = controlDisplay ?? throw new ArgumentNullException(nameof(controlDisplay));
        this.controlLayout = controlLayout ?? throw new ArgumentNullException(nameof(controlLayout));
    }

    public void RenderNextLine()
    {
        if (lineIndex >= controlLayout.Padding.Top)
            return;

        if (controlLayout.Padding.Left > 0)
        {
            string text = new(' ', controlLayout.Padding.Left);
            controlDisplay.Write(text);
        }

        if (controlLayout.ContentSize.Width > 0)
        {
            string text = new(' ', controlLayout.ContentSize.Width);
            controlDisplay.Write(text);
        }

        if (controlLayout.Padding.Right > 0)
        {
            string text = new(' ', controlLayout.Padding.Right);
            controlDisplay.Write(text);
        }

        lineIndex++;
    }
}