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
/// A section renderer is a child of a bigger renderer. It will use its parent
/// <see cref="RenderingContext"/> instance.
/// </summary>
public abstract class SectionRenderer : IRenderer
{
    /// <summary>
    /// Gets the rendering context used during the rendering process.
    /// This is the instance received from its parent.
    /// </summary>
    protected RenderingContext RenderingContext { get; }

    /// <summary>
    /// Gets a value specifying if the rendered still has more lines to render.
    /// </summary>
    public abstract bool HasMoreLines { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SectionRenderer"/> class
    /// with the parent's rendering context.
    /// </summary>
    /// <param name="renderingContext">The parent's rendering context.</param>
    /// <exception cref="ArgumentNullException">Thrown if the received rendering context is null.</exception>
    protected SectionRenderer(RenderingContext renderingContext)
    {
        RenderingContext = renderingContext ?? throw new ArgumentNullException(nameof(renderingContext));
    }

    /// <summary>
    /// Renders the next line.
    /// </summary>
    public abstract void RenderNextLine();

    /// <summary>
    /// Resets the rendering process. Next call to <see cref="RenderNextLine"/> will render the
    /// first line.
    /// </summary>
    public abstract void Reset();
}