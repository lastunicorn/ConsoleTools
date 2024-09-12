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

using System.Linq;
using DustInTheWind.ConsoleTools.Controls.Rendering;

namespace DustInTheWind.ConsoleTools.Controls;

internal class MultiColorRenderer : BlockRenderer<MultiColor>
{
    private readonly MultiRenderer multiRenderer = new();

    public MultiColorRenderer(MultiColor control, IDisplay display, RenderingOptions renderingOptions)
        : base(control, display, renderingOptions)
    {
    }

    protected override bool InitializeContentRendering()
    {
        if (Control.Text == null || Control.Colors == null || Control.Colors.Count == 0)
            return false;

        multiRenderer.Clear();

        multiRenderer.AddRange(Control.Colors
            .Select(x => new TextSectionRenderer(RenderingContext)
            {
                Text = Control.Text,
                ForegroundColor = x
            }));

        return multiRenderer.HasMoreLines;
    }

    protected override bool RenderNextContentLine()
    {
        if (multiRenderer == null)
            return false;

        multiRenderer.RenderNextLine();
        return multiRenderer.HasMoreLines;
    }
}