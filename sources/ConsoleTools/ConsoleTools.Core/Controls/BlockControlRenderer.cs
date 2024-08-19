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

public abstract class BlockControlRenderer<TControl> : ControlRendererBase<TControl>
    where TControl : BlockControl
{
    private RenderingStep step;
    private IRenderer currentRenderer;

    public override bool HasMoreLines => currentRenderer?.HasMoreLines ?? false;

    protected BlockControlRenderer(TControl control, RenderingOptions renderingOptions)
        : base(control, renderingOptions)
    {
        step = RenderingStep.Start;
        MoveNext();
    }

    private void MoveNext()
    {
        if (step == RenderingStep.Start)
        {
            MoveToTopMargin();
        }

        if (step == RenderingStep.TopMargin)
        {
            if (currentRenderer.HasMoreLines)
                return;

            MoveToTopPadding();
        }

        if (step == RenderingStep.TopPadding)
        {
            if (currentRenderer.HasMoreLines)
                return;

            MoveToContent();
        }

        if (step == RenderingStep.Content)
        {
            if (currentRenderer.HasMoreLines)
                return;

            MoveToBottomPadding();
        }


        if (step == RenderingStep.BottomPadding)
        {
            if (currentRenderer.HasMoreLines)
                return;

            MoveToBottomMargin();
        }

        if (step == RenderingStep.BottomMargin)
        {
            if (currentRenderer.HasMoreLines)
                return;

            MoveToEnd();
        }

        if (step == RenderingStep.End)
            return;

        throw new ArgumentOutOfRangeException();
    }

    private void MoveToTopMargin()
    {
        step = RenderingStep.TopMargin;
        currentRenderer = new TopMarginRenderer(ControlLayout);
    }

    private void MoveToTopPadding()
    {
        step = RenderingStep.TopPadding;
        currentRenderer = new PaddingRenderer
        {
            Height = ControlLayout.Padding.Top,
            PaddingLeft = ControlLayout.Padding.Left,
            PaddingRight = ControlLayout.Padding.Right,
            ContentWidth = ControlLayout.ContentSize.Width
        };
    }

    private void MoveToContent()
    {
        step = RenderingStep.Content;
        currentRenderer = new RelayRenderer(DoInitializeContentRendering)
        {
            RenderNextLineAction = DoRenderNextContentLine
        };
    }

    private void MoveToBottomPadding()
    {
        step = RenderingStep.BottomPadding;
        currentRenderer = new PaddingRenderer
        {
            Height = ControlLayout.Padding.Bottom,
            PaddingLeft = ControlLayout.Padding.Left,
            PaddingRight = ControlLayout.Padding.Right,
            ContentWidth = ControlLayout.ContentSize.Width
        };
    }

    private void MoveToBottomMargin()
    {
        step = RenderingStep.BottomMargin;
        currentRenderer = new BottomMarginRenderer(ControlLayout);
    }

    private void MoveToEnd()
    {
        step = RenderingStep.End;
        currentRenderer = null;
    }

    public override void RenderNextLine(IDisplay display)
    {
        if (currentRenderer == null)
            return;

        currentRenderer.RenderNextLine(display);
        MoveNext();
    }

    protected abstract bool DoInitializeContentRendering();

    protected abstract bool DoRenderNextContentLine(IDisplay display);

    protected override void OnAfterStartLine(IDisplay display)
    {
        WriteSpaces(display, ControlLayout.EmptySpace.Left, null, null);
        WriteSpaces(display, ControlLayout.Margin.Left, null, null);
        WriteSpaces(display, ControlLayout.Padding.Left, null, display.BackgroundColor);
    }

    protected override void OnBeforeEndLine(IDisplay display)
    {
        WriteSpaces(display, ControlLayout.Padding.Right, null, display.BackgroundColor);
        WriteSpaces(display, ControlLayout.Margin.Right, null, null);
    }
}