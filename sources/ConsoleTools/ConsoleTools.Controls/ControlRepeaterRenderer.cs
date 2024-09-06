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

namespace DustInTheWind.ConsoleTools.Controls;

internal class ControlRepeaterRenderer : BlockControlRenderer<ControlRepeater>
{
    private volatile bool closeWasRequested;
    private bool isRunning;
    private IRenderer childRenderer;
    private int count;

    public ControlRepeaterRenderer(ControlRepeater control, IDisplay display, RenderingOptions renderingOptions)
        : base(control, display, renderingOptions)
    {
    }

    protected override bool InitializeContentRendering()
    {
        if (Control.RepeatCount == 0 || Control?.Content == null)
        {
            childRenderer = null;
            return false;
        }

        closeWasRequested = false;

        if (Control.Content is IRepeatableSupport repeatableControl)
            repeatableControl.Closed += HandleControlClosed;

        count = 0;
        childRenderer = Control.RenderContentAsRoot
            ? RenderingContext.CreateRenderer(Control.Content, null)
            : RenderingContext.CreateChildRenderer(Control.Content, null);
        return childRenderer.HasMoreLines;
    }

    private void HandleControlClosed(object sender, EventArgs e)
    {
        closeWasRequested = true;
    }

    protected override bool RenderNextContentLine()
    {
        if (closeWasRequested)
            return false;

        if (!Control.RenderContentAsRoot)
            RenderingContext.StartLine();

        childRenderer.RenderNextLine();

        if (!Control.RenderContentAsRoot)
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

    protected override void OnRenderingStart()
    {
        isRunning = true;

        base.OnRenderingStart();
    }

    protected override void OnRenderingEnd()
    {
        base.OnRenderingEnd();

        isRunning = false;
    }
}