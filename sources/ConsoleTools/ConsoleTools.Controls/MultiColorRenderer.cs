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

internal class MultiColorRenderer : BlockControlRenderer<MultiColor>
{
    private int index;

    public MultiColorRenderer(MultiColor control, IDisplay display, RenderingOptions renderingOptions)
        : base(control, display, renderingOptions)
    {
    }

    protected override bool DoInitializeContentRendering()
    {
        if (Control.Text == null)
            return false;

        index = 0;
        return index < Control.Colors?.Count;
    }

    protected override bool DoRenderNextContentLine()
    {
        if (Control.Text == null || Control.Colors == null || index >= Control.Colors.Count)
            return false;

        string text = Control.Text;
        ConsoleColor color = Control.Colors[index];

        Display.WriteLine(text, color);

        index++;
        return index < Control.Colors.Count;
    }
}