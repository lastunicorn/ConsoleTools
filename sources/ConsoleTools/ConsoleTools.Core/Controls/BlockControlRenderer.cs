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

public abstract class BlockControlRenderer<TControl> : IRenderer
    where TControl : BlockControl
{
    private RenderingStep step;
    private IRenderer currentRenderer;

    protected ControlDisplay Display { get; }

    /// <summary>
    /// Gets the control being rendered.
    /// </summary>
    protected TControl Control { get; }

    /// <summary>
    /// Gets the calculated layout information of the control being rendered.
    /// </summary>
    protected ControlLayout ControlLayout { get; }

    /// <summary>
    /// Gets a value specifying if there are still lines awaiting to be rendered.
    /// </summary>
    public bool HasMoreLines => currentRenderer?.HasMoreLines ?? false;

    /// <summary>
    /// Initializes a new instance of teh <see cref="BlockControlRenderer{TControl}"/> class with
    /// the control being rendered and rendering options.
    /// </summary>
    ///
    /// <param name="control">The control being rendered.</param>
    ///
    /// <param name="renderingOptions">
    /// Rendering options based on which the <see cref="IRenderer"/> is created.
    /// If <c>null</c> is provided, the renderer is created using default options.
    /// </param>
    ///
    /// <exception cref="ArgumentNullException">
    /// Thrown if the provided <see cref="control"/> is null.
    /// </exception>
    protected BlockControlRenderer(TControl control, IDisplay display, RenderingOptions renderingOptions)
    {
        Control = control ?? throw new ArgumentNullException(nameof(control));

        ControlLayout = new ControlLayout
        {
            Control = control,
            AvailableWidth = renderingOptions?.AvailableWidth,
            DesiredContentWidth = control.DesiredContentWidth
        };

        ControlLayout.Calculate();

        Display = new ControlDisplay(display, ControlLayout)
        {
            MaxLineLength = renderingOptions?.AvailableWidth,
            IsRoot = renderingOptions?.IsRoot ?? true
        };

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
        currentRenderer = new TopMarginRenderer(Display, ControlLayout);
    }

    private void MoveToTopPadding()
    {
        step = RenderingStep.TopPadding;
        currentRenderer = new TopPaddingRenderer(Display, ControlLayout);
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
        currentRenderer = new BottomPaddingRenderer(Display, ControlLayout);
    }

    private void MoveToBottomMargin()
    {
        step = RenderingStep.BottomMargin;
        currentRenderer = new BottomMarginRenderer(Display, ControlLayout);
    }

    private void MoveToEnd()
    {
        step = RenderingStep.End;
        currentRenderer = null;
    }

    public void RenderNextLine()
    {
        if (currentRenderer == null)
            return;

        currentRenderer.RenderNextLine();
        //display.DoWriteRootEndLine();

        MoveNext();
    }

    protected abstract bool DoInitializeContentRendering();

    protected abstract bool DoRenderNextContentLine();

    //protected void OnAfterStartLine()
    //{
    //    Display.WriteSpaces(ControlLayout.EmptySpace.Left, null, null);
    //    Display.WriteSpaces(ControlLayout.Margin.Left, null, null);
    //    Display.WritePadding(ControlLayout.Padding.Left);
    //}

    //protected void OnBeforeEndLine()
    //{
    //    Display.WritePadding(ControlLayout.Padding.Right);
    //    Display.WriteSpaces(ControlLayout.Margin.Right, null, null);
    //}
}