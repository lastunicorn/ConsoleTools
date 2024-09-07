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

namespace DustInTheWind.ConsoleTools.Controls.Menus;

internal class TextMenuItemsSection : SectionRenderer
{
    private readonly TextMenu textMenu;
    private readonly MultiRenderer multiRenderer = new();

    public override bool HasMoreLines => multiRenderer.HasMoreLines;

    public TextMenuItemsSection(TextMenu textMenu, RenderingContext renderingContext)
        : base(renderingContext)
    {
        this.textMenu = textMenu ?? throw new ArgumentNullException(nameof(textMenu));

        Initialize();
    }

    private void Initialize()
    {
        IEnumerable<IRenderer> childRenderers = textMenu.MenuItems
            .Where(x => x.IsVisible)
            .Select(x => RenderingContext.CreateChildRenderer(x))
            .ToList();

        multiRenderer.AddRange(childRenderers);
        multiRenderer.Reset();
    }

    public override void RenderNextLine()
    {
        RenderingContext.StartLine();
        multiRenderer.RenderNextLine();
        RenderingContext.EndLine();
    }

    public override void Reset()
    {
        multiRenderer.Reset();
    }
}