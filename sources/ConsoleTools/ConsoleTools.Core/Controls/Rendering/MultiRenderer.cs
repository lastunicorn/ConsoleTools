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
using System.Collections.ObjectModel;
using System.Linq;

namespace DustInTheWind.ConsoleTools.Controls.Rendering;

/// <summary>
/// A collection of renderers that are rendered one after the other.
/// </summary>
public class MultiRenderer : Collection<IRenderer>, IRenderer, IDisposable
{
    private bool isInitialized;
    private IEnumerator<IRenderer> enumerator;
    private bool hasMoreLines;

    protected override void InsertItem(int index, IRenderer item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        base.InsertItem(index, item);

        isInitialized = false;
    }

    protected override void SetItem(int index, IRenderer item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        base.SetItem(index, item);

        isInitialized = false;
    }

    protected override void RemoveItem(int index)
    {
        base.RemoveItem(index);

        isInitialized = false;
    }

    protected override void ClearItems()
    {
        base.ClearItems();

        isInitialized = false;
    }

    /// <summary>
    /// Adds a collection of renderers to the end of the list of renderers.
    /// </summary>
    public void AddRange(IEnumerable<IRenderer> renderers)
    {
        if (renderers == null) throw new ArgumentNullException(nameof(renderers));

        IEnumerable<IRenderer> nonNullRenderers = renderers.Where(x => x != null);

        foreach (IRenderer renderer in nonNullRenderers)
            Add(renderer);

        isInitialized = false;
    }

    /// <summary>
    /// Gets a value specifying if there are more lines to be rendered.
    /// </summary>
    public bool HasMoreLines
    {
        get
        {
            if (!isInitialized)
                Initialize();

            return hasMoreLines;
        }
    }

    private void Initialize()
    {
        enumerator?.Dispose();

        enumerator = Items.GetEnumerator();
        MoveToNextSection();

        isInitialized = true;
    }

    /// <summary>
    /// Renders the next available line.
    /// </summary>
    public void RenderNextLine()
    {
        if (!isInitialized)
            Initialize();

        if (!hasMoreLines)
            return;

        enumerator.Current!.RenderNextLine();

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
                enumerator.Current!.Reset();

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

    /// <summary>
    /// Resets the rendering process. Next time when the <see cref="RenderNextLine"/> is called
    /// it will render the first line of the first renderer in the list.
    /// </summary>
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