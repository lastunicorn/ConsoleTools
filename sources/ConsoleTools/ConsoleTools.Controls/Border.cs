﻿// ConsoleTools
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

using DustInTheWind.ConsoleTools.Controls.Rendering;

namespace DustInTheWind.ConsoleTools.Controls;

/// <summary>
/// Displays a border around other controls.
/// </summary>
public class Border : BlockControl
{
    /// <summary>
    /// Gets or sets the control contained inside the border.
    /// </summary>
    public BlockControl Content { get; set; }

    /// <summary>
    /// Gets or sets the template used for the border. It provides the characters to be used as
    /// border.
    /// </summary>
    public BorderTemplate Template { get; set; } = BorderTemplate.PlusMinusBorderTemplate;

    /// <summary>
    /// Gets the width of the content (no margins, no paddings) when it is not restricted by
    /// external factors.
    /// </summary>
    protected override int NaturalContentWidth => Content.CalculateNaturalWidth(true, true) + 2;

    /// <summary>
    /// Returns an object capable of rendering the current control into a display.
    /// </summary>
    public override IRenderer GetRenderer(IDisplay display, RenderingOptions renderingOptions = null)
    {
        TextBlock textBlock = new();
        textBlock.AddBorder(x =>
        {
            x.Template = BorderTemplate.DoubleLineBorderTemplate;
        });

        return new BorderRenderer(this, display, renderingOptions);
    }
}