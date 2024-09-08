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

public class MultiRenderer : IRenderer, IDisposable
{
    private bool isInitialized;
    private readonly List<IRenderer> renderers = new();
    private IEnumerator<IRenderer> enumerator;
    private bool hasMoreLines;

    public bool HasMoreLines
    {
        get
        {
            if (!isInitialized)
                Initialize();

            return hasMoreLines;
        }
    }

    public void Add(IRenderer renderer)
    {
        if (enumerator != null)
            throw new Exception("No renderer can be added once the rendering was started.");

        if (renderer == null) throw new ArgumentNullException(nameof(renderer));

        renderers.Add(renderer);

        isInitialized = false;
    }

    public void AddRange(IEnumerable<IRenderer> renderers)
    {
        if (renderers == null) throw new ArgumentNullException(nameof(renderers));

        IEnumerable<IRenderer> nonNullRenderers = renderers.Where(x => x != null);

        foreach (IRenderer renderer in nonNullRenderers)
            this.renderers.Add(renderer);

        isInitialized = false;
    }

    public void Clear()
    {
        renderers.Clear();

        isInitialized = false;
    }

    private void Initialize()
    {
        enumerator?.Dispose();

        enumerator = renderers.GetEnumerator();
        MoveToNextSection();

        isInitialized = true;
    }

    public void RenderNextLine()
    {
        if (!isInitialized)
            Initialize();

        if (!hasMoreLines)
            return;

        enumerator.Current.RenderNextLine();

        if (!enumerator.Current.HasMoreLines)
            MoveToNextSection();
    }

    private void MoveToNextSection()
    {
        while (true)
        {
            bool success = enumerator.MoveNext();

            if (success)
            {
                enumerator.Current.Reset();

                if (enumerator.Current.HasMoreLines)
                {
                    hasMoreLines = true;
                    return;
                }
            }
            else
            {
                hasMoreLines = false;
                return;
            }
        }
    }

    public void Reset()
    {
        if (!isInitialized)
            Initialize();

        enumerator.Reset();
        MoveToNextSection();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            enumerator?.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}