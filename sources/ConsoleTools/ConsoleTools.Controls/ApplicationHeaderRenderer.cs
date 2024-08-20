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

internal class ApplicationHeaderRenderer : BlockControlRenderer<ApplicationHeader>
{
    private string text;
    private ApplicationHeaderRenderingStep step;

    public ApplicationHeaderRenderer(ApplicationHeader control, IDisplay display, RenderingOptions renderingOptions)
        : base(control, display, renderingOptions)
    {
    }

    protected override bool DoInitializeContentRendering()
    {
        text = Control.BuildTitleRow();
        step = ApplicationHeaderRenderingStep.TitleText;
        return true;
    }

    protected override bool DoRenderNextContentLine()
    {
        return step switch
        {
            ApplicationHeaderRenderingStep.TitleText => ExecuteTitleTextStep(),
            ApplicationHeaderRenderingStep.Separator => ExecuteSeparatorStep(),
            _ => false
        };
    }

    private bool ExecuteTitleTextStep()
    {
        Display.WriteLine(text);

        if (Control.ShowSeparator)
        {
            step = ApplicationHeaderRenderingStep.Separator;
            return true;
        }

        step = ApplicationHeaderRenderingStep.End;
        return false;
    }

    private bool ExecuteSeparatorStep()
    {
        string separatorText = new('=', Console.WindowWidth - 1);
        Display.WriteLine(separatorText);

        step = ApplicationHeaderRenderingStep.End;
        return false;
    }
}