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

using DustInTheWind.ConsoleTools.Controls.Rendering;

namespace DustInTheWind.ConsoleTools.Controls;

internal class ControlRepeaterRenderer : BlockRenderer<ControlRepeater>
{
    private IRenderer childRenderer;
    private int count;
    private BlockControl content;

    public ControlRepeaterRenderer(ControlRepeater control, IDisplay display, RenderingOptions renderingOptions)
        : base(control, display, renderingOptions)
    {
    }

    protected override bool InitializeContentRendering()
    {
        if (Control.RepeatCount == 0 || Control?.Content == null)
        {
            content = null;
            childRenderer = null;
            return false;
        }

        content = Control.Content;

        count = 0;
        childRenderer = Control.IsRootControl
            ? RenderingContext.CreateRootRenderer(content, null)
            : RenderingContext.CreateChildRenderer(content, null);

        return childRenderer.HasMoreLines;
    }

    protected override bool RenderNextContentLine()
    {
        if (Control.IsClosed || content is ICloseSupport { IsClosed: true })
            return false;

        if (!Control.IsRootControl)
            RenderingContext.StartLine();

        childRenderer.RenderNextLine();

        if (!Control.IsRootControl)
            RenderingContext.EndLine();

        if (childRenderer.HasMoreLines)
            return true;

        count++;

        if (Control.RepeatCount < 0 || count < Control.RepeatCount)
        {
            childRenderer.Reset();
            return childRenderer.HasMoreLines;
        }

        return false;
    }
}