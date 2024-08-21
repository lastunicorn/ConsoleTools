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

namespace DustInTheWind.ConsoleTools.Controls;

internal class HorizontalLineRenderer : BlockControlRenderer<HorizontalLine>
{
    private string text;

    public HorizontalLineRenderer(HorizontalLine horizontalLine, IDisplay display, RenderingOptions renderingOptions)
        : base(horizontalLine, display, renderingOptions)
    {
    }

    protected override bool InitializeContentRendering()
    {
        int width = ControlLayout.ContentSize.Width;

        if (width <= 0)
            return false;

        text = new string(Control.Character, width);

        return true;
    }

    protected override bool RenderNextContentLine()
    {
        RenderingContext.WriteLine(text);

        return false;
    }
}