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
using System.Linq;

namespace DustInTheWind.ConsoleTools.Controls.Rendering;

public class MultiRenderer : IRenderer
{
    private readonly List<IRenderer> renderers = new();
    private IEnumerator<IRenderer> enumerator;

    public bool HasMoreLines => enumerator != null;

    public void Add(IRenderer renderer)
    {
        if (enumerator != null)
            throw new Exception("No renderer can be added once the rendering was started.");

        if (renderer == null) throw new ArgumentNullException(nameof(renderer));

        renderers.Add(renderer);
    }

    public void AddRange(IEnumerable<IRenderer> renderers)
    {
        if (renderers == null) throw new ArgumentNullException(nameof(renderers));

        IEnumerable<IRenderer> nonNullRenderers = renderers.Where(x => x != null);

        foreach (IRenderer renderer in nonNullRenderers)
            this.renderers.Add(renderer);
    }

    public void Clear()
    {
        renderers.Clear();
    }

    public void RenderNextLine()
    {
        if (enumerator == null)
            return;

        enumerator.Current.RenderNextLine();

        if (!enumerator.Current.HasMoreLines)
            MoveToNextSection();
    }

    private void MoveToNextSection()
    {
        if (enumerator == null)
            return;

        while (true)
        {
            bool success = enumerator.MoveNext();

            if (success)
            {
                enumerator.Current.Reset();

                if (enumerator.Current.HasMoreLines)
                    return;
            }
            else
            {
                enumerator = null;
                return;
            }
        }
    }

    public void Reset()
    {
        enumerator = renderers.GetEnumerator();
        MoveToNextSection();
    }
}