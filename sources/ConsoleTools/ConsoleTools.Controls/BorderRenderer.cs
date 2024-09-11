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

using DustInTheWind.ConsoleTools.Controls.Rendering;

namespace DustInTheWind.ConsoleTools.Controls;

internal class BorderRenderer : BlockRenderer<Border>
{
    private IRenderer contentRenderer;
    private BorderRendererStep step;

    public BorderRenderer(Border border, IDisplay display, RenderingOptions renderingOptions)
        : base(border, display, renderingOptions)
    {
    }

    protected override bool InitializeContentRendering()
    {
        int contentWidth = Control.CalculateNaturalWidth(false, false) - 2;

        ChildRenderingOptions renderingOptions = new()
        {
            AvailableWidth = contentWidth,
            ParentForegroundColor = Control.ForegroundColor,
            ParentBackgroundColor = Control.BackgroundColor
        };

        contentRenderer = RenderingContext.CreateChildRenderer(Control.Content, renderingOptions);

        step = BorderRendererStep.Top;
        return true;
    }

    protected override bool RenderNextContentLine()
    {
        if (step == BorderRendererStep.Top)
        {
            string text = Control.Template.GenerateTopBorder(ControlLayout.ContentSize.Width - 2);
            RenderingContext.WriteLine(text);

            step = BorderRendererStep.Content;
            return true;
        }

        if (step == BorderRendererStep.Content)
        {
            RenderingContext.BeginLine();

            char left = Control.Template.Left;
            RenderingContext.Write(left);

            contentRenderer.RenderNextLine();

            char right = Control.Template.Left;
            RenderingContext.Write(right);

            RenderingContext.EndLine();

            if (!contentRenderer.HasMoreLines)
                step = BorderRendererStep.Bottom;

            return true;
        }

        if (step == BorderRendererStep.Bottom)
        {
            string text = Control.Template.GenerateBottomBorder(ControlLayout.ContentSize.Width - 2);
            RenderingContext.WriteLine(text);

            step = BorderRendererStep.End;
            return false;
        }

        return false;
    }
}