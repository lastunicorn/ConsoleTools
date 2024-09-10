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

// --------------------------------------------------------------------------------
// Bugs or feature requests
// --------------------------------------------------------------------------------
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new/choose

using System;
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.ConsoleTools.Controls.Rendering;

namespace DustInTheWind.ConsoleTools.Controls;

/// <summary>
/// Displays a text multiple times, each time with a different color.
/// </summary>
public class MultiColor : BlockControl
{
    /// <summary>
    /// Gets or sets the text to be displayed in multiple colors.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Gets or sets the colors to be used for displaying the text.
    /// The text will be displayed once per each color.
    /// </summary>
    public List<ConsoleColor> Colors { get; set; }

    /// <summary>
    /// Gets the width of the control's content calculated when there are no other space
    /// restrictions applied to it.
    /// </summary>
    public override int NaturalContentWidth => Text.Length;

    public MultiColor()
    {
        Colors = Enum.GetValues(typeof(ConsoleColor))
            .Cast<ConsoleColor>()
            .ToList();
    }

    /// <summary>
    /// Returns a renderer object that is able to render the current <see cref="MultiColor"/>
    /// instance using the specified <see cref="IDisplay"/>.
    /// </summary>
    /// <returns>The <see cref="IRenderer"/> instance.</returns>
    public override IRenderer GetRenderer(IDisplay display, RenderingOptions renderingOptions = null)
    {
        return new MultiColorRenderer(this, display, renderingOptions);
    }
}