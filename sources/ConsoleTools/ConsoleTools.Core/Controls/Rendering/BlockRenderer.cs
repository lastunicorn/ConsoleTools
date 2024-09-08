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

namespace DustInTheWind.ConsoleTools.Controls.Rendering;

/// <summary>
/// This base renderer provides support for rendering a <see cref="BlockControl"/>.
/// </summary>
/// <typeparam name="TControl"></typeparam>
public abstract class BlockRenderer<TControl> : IRenderer
    where TControl : BlockControl
{
    private bool isInitialized;
    private RenderingStep step;
    private IRenderer sectionRenderer;

    /// <summary>
    /// Provides a context for the rendering process.
    /// It contains the control being rendered, its layout information and the display.
    /// </summary>
    protected RenderingContext RenderingContext { get; }

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
    public bool HasMoreLines
    {
        get
        {
            if (!isInitialized)
                Initialize();

            return sectionRenderer?.HasMoreLines ?? false;
        }
    }

    /// <summary>
    /// Initializes a new instance of teh <see cref="BlockRenderer{TControl}"/> class with
    /// the control being rendered and rendering options.
    /// </summary>
    /// 
    /// <param name="control">
    /// The control being rendered.
    /// </param>
    ///
    /// <param name="display">
    /// The display into which the control is rendered.
    /// </param>
    /// 
    /// <param name="renderingOptions">
    /// Rendering options based on which the <see cref="IRenderer"/> is created.
    /// If <c>null</c> is provided, the renderer is created using default options.
    /// </param>
    /// 
    /// <exception cref="ArgumentNullException">
    /// Thrown if the provided <see cref="control"/> is null.
    /// </exception>
    protected BlockRenderer(TControl control, IDisplay display, RenderingOptions renderingOptions)
    {
        Control = control ?? throw new ArgumentNullException(nameof(control));

        ControlLayout = new ControlLayout
        {
            Control = control,
            AllocatedWidth = renderingOptions?.AvailableWidth ?? display.MaxWidth
        };

        ControlLayout.Calculate();

        RenderingContext = new RenderingContext(display, ControlLayout)
        {
            LineLength = renderingOptions?.AvailableWidth,
            IsRoot = renderingOptions?.IsRoot ?? true,
            OnLineRendered = renderingOptions?.OnLineRendered,
            ForegroundColor = Control.ForegroundColor,
            BackgroundColor = Control.BackgroundColor
        };

        step = RenderingStep.Start;
        OnRenderingStart();
    }

    private void Initialize()
    {
        step = RenderingStep.Start;
        MoveNext();

        isInitialized = true;
    }

    private void MoveNext()
    {
        if (step == RenderingStep.Start)
            MoveToTopMargin();

        if (step == RenderingStep.TopMargin)
        {
            if (sectionRenderer.HasMoreLines)
                return;

            MoveToTopPadding();
        }

        if (step == RenderingStep.TopPadding)
        {
            if (sectionRenderer.HasMoreLines)
                return;

            MoveToContent();
        }

        if (step == RenderingStep.Content)
        {
            if (sectionRenderer.HasMoreLines)
                return;

            MoveToBottomPadding();
        }

        if (step == RenderingStep.BottomPadding)
        {
            if (sectionRenderer.HasMoreLines)
                return;

            MoveToBottomMargin();
        }

        if (step == RenderingStep.BottomMargin)
        {
            if (sectionRenderer.HasMoreLines)
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
        sectionRenderer = new MarginTopSectionRenderer(RenderingContext);
    }

    private void MoveToTopPadding()
    {
        step = RenderingStep.TopPadding;
        sectionRenderer = new PaddingTopSectionRenderer(RenderingContext);
    }

    private void MoveToContent()
    {
        step = RenderingStep.Content;

        sectionRenderer = new RelayRenderer(InitializeContentRendering)
        {
            RenderNextLineAction = RenderNextContentLine
        };
    }

    private void MoveToBottomPadding()
    {
        step = RenderingStep.BottomPadding;
        sectionRenderer = new PaddingBottomSectionRenderer(RenderingContext);
    }

    private void MoveToBottomMargin()
    {
        step = RenderingStep.BottomMargin;
        sectionRenderer = new MarginBottomSectionRenderer(RenderingContext);
    }

    private void MoveToEnd()
    {
        step = RenderingStep.End;
        sectionRenderer = null;

        OnRenderingEnd();
    }

    /// <summary>
    /// Renders the next line of the control into the display provided on the constructor.
    /// </summary>
    public void RenderNextLine()
    {
        if (!isInitialized)
            Initialize();

        if (sectionRenderer == null)
            return;

        sectionRenderer.RenderNextLine();

        MoveNext();
    }

    protected virtual void OnRenderingStart()
    {
    }

    protected virtual void OnRenderingEnd()
    {
    }

    /// <summary>
    /// This method is called just before the content is rendered.
    /// When implemented by an inheritor, it allows to perform any needed initialization for
    /// rendering the content.
    /// </summary>
    /// 
    /// <returns>
    /// <c>true</c> if the content has lines that can be serialized; <c>false</c> otherwise.
    /// </returns>
    protected abstract bool InitializeContentRendering();

    /// <summary>
    /// When implemented by an inheritor, it renders the next line of the content.
    /// </summary>
    /// 
    /// <returns>
    /// <c>true</c> if the content has more lines that can be serialized; <c>false</c> otherwise.
    /// </returns>
    protected abstract bool RenderNextContentLine();

    public void Reset()
    {
        if (!isInitialized)
            Initialize();

        step = RenderingStep.Start;
        MoveNext();
    }
}