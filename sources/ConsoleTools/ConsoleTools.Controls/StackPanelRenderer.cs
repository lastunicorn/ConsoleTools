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

using System.Collections.Generic;
using System.Linq;
using DustInTheWind.ConsoleTools.Controls.Rendering;

namespace DustInTheWind.ConsoleTools.Controls;

internal class StackPanelRenderer : BlockRenderer<StackPanel>
{
    private readonly MultiRenderer multiRenderer = new();

    public StackPanelRenderer(StackPanel stackPanel, IDisplay display, RenderingOptions renderingOptions)
        : base(stackPanel, display, renderingOptions)
    {
    }

    protected override bool InitializeContentRendering()
    {
        multiRenderer.Clear();
        
        IEnumerable<IRenderer> childRenderers = Control.Children
            .Select(x => RenderingContext.CreateChildRenderer(x, new ChildRenderingOptions()
            {
                AvailableWidth = ControlLayout.ActualContentWidth
            }))
            .ToList();

        multiRenderer.AddRange(childRenderers);
        multiRenderer.Reset();

        return multiRenderer.HasMoreLines;
    }

    protected override bool RenderNextContentLine()
    {
        RenderingContext.StartLine();
        multiRenderer.RenderNextLine();
        RenderingContext.EndLine();

        return multiRenderer.HasMoreLines;
    }
}