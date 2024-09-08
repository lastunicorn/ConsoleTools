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
using DustInTheWind.ConsoleTools.Controls.Rendering;

namespace DustInTheWind.ConsoleTools.Controls;

/// <summary>
/// Displays a list of controls in a vertical stack, one after the other.
/// </summary>
public class StackPanelWithCloseSupport : StackPanel, ICloseSupport
{
    /// <summary>
    /// Gets the list of children to be displayed.
    /// </summary>
    public List<BlockControl> Children { get; } = new();

    /// <summary>
    /// Gets the width of the largest child control's content calculated when there are no other
    /// space restrictions applied to it.
    /// </summary>
    public override int NaturalContentWidth => Children
        .Select(x => x.CalculateNaturalWidth())
        .Max();

    public bool IsClosed => Children
        .OfType<ICloseSupport>()
        .Any(x => x.IsClosed);

    public event EventHandler CloseRequested;

    /// <summary>
    /// Returns a renderer object that is able to render the current <see cref="StackPanel"/>
    /// instance using the specified <see cref="IDisplay"/>.
    /// </summary>
    /// <returns>The <see cref="IRenderer"/> instance.</returns>
    public override IRenderer GetRenderer(IDisplay display, RenderingOptions renderingOptions = null)
    {
        return new StackPanelRenderer(this, display, renderingOptions);
    }

    public void RequestClose()
    {
        IEnumerable<ICloseSupport> childrenWithCloseSupports = Children
            .OfType<ICloseSupport>();

        foreach (ICloseSupport closeSupport in childrenWithCloseSupports) 
            closeSupport.RequestClose();
    }
}