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

internal abstract class SectionRenderer : IRenderer
{
    protected RenderingContext RenderingContext { get; }

    /// <summary>
    /// Gets a value specifying if the rendered still has more lines to render.
    /// </summary>
    public abstract bool HasMoreLines { get; }

    protected SectionRenderer(RenderingContext renderingContext)
    {
        RenderingContext = renderingContext ?? throw new ArgumentNullException(nameof(renderingContext));
    }

    /// <summary>
    /// Renders the next line using the underlying <see cref="IDisplay"/>.
    /// </summary>
    public abstract void RenderNextLine();

    public abstract void Reset();
}