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
using System.Collections.Generic;

namespace DustInTheWind.ConsoleTools.Controls.Rendering;

/// <summary>
/// This base renderer provides support for rendering a <see cref="BlockControl"/>.
/// </summary>
/// <typeparam name="TControl"></typeparam>
public abstract class BlockRenderer<TControl> : IRenderer
    where TControl : BlockControl
{
    private readonly MultiRenderer multiRenderer = new();

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
            if (!Control.IsVisible)
                return false;

            return multiRenderer?.HasMoreLines ?? false;
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
            BackgroundColor = Control.BackgroundColor,
            ParentForegroundColor = renderingOptions?.ParentForegroundColor,
            ParentBackgroundColor = renderingOptions?.ParentBackgroundColor
        };

        multiRenderer.AddRange(new IRenderer[]
        {
            new MarginTopSectionRenderer(RenderingContext),
            new PaddingTopSectionRenderer(RenderingContext),
            new RelayRenderer(InitializeContentRendering)
            {
                RenderNextLineAction = RenderNextContentLine
            },
            new PaddingBottomSectionRenderer(RenderingContext),
            new MarginBottomSectionRenderer(RenderingContext)
        });
    }

    /// <summary>
    /// Renders the next line of the control into the display provided on the constructor.
    /// </summary>
    public void RenderNextLine()
    {
        if (!Control.IsVisible)
            return;

        multiRenderer?.RenderNextLine();
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

    /// <summary>
    /// Resets the rendering process. Next time when the <see cref="RenderNextLine"/> is called
    /// it will render the first line.
    /// </summary>
    public void Reset()
    {
        multiRenderer.Reset();
    }
}