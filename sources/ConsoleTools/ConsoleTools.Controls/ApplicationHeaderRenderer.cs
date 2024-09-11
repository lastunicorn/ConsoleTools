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

using System.Collections.Generic;
using DustInTheWind.ConsoleTools.Controls.Rendering;

namespace DustInTheWind.ConsoleTools.Controls;

internal class ApplicationHeaderRenderer : BlockRenderer<ApplicationHeader>
{
    private readonly MultiRenderer multiRenderer = new();

    public ApplicationHeaderRenderer(ApplicationHeader control, IDisplay display, RenderingOptions renderingOptions)
        : base(control, display, renderingOptions)
    {
    }

    protected override bool InitializeContentRendering()
    {
        multiRenderer.Clear();

        multiRenderer.Add(new TextSectionRenderer(RenderingContext)
        {
            Text = BuildTitleRow()
        });

        if (Control.ShowSeparator)
        {
            multiRenderer.Add(new TextSectionRenderer(RenderingContext)
            {
                Text = new string('=', ControlLayout.ActualContentWidth)
            });
        }

        return multiRenderer.HasMoreLines;
    }

    private string BuildTitleRow()
    {
        List<string> parts = new();

        if (Control.ApplicationName != null)
            parts.Add(Control.ApplicationName);

        if (Control.ShowVersion)
            parts.Add(Control.ApplicationVersion.ToString(3));

        return string.Join(" ", parts);
    }

    protected override bool RenderNextContentLine()
    {
        multiRenderer.RenderNextLine();
        return multiRenderer.HasMoreLines;
    }
}