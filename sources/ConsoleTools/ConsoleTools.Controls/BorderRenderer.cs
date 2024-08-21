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

namespace DustInTheWind.ConsoleTools.Controls;

internal class BorderRenderer : BlockControlRenderer<Border>
{
    private IRenderer contentRenderer;
    private BorderRendererStep step;

    public BorderRenderer(Border border, IDisplay display, RenderingOptions renderingOptions)
        : base(border, display, renderingOptions)
    {
    }

    protected override bool DoInitializeContentRendering()
    {
        int naturalContentWidth = Control.ComputeNaturalContentWidth();
        int contentWidth = naturalContentWidth - Control.Padding.Left - Control.Padding.Right - 2;

        ChildRenderingOptions renderingOptions = new()
        {
            AvailableWidth = contentWidth
        };

        contentRenderer = RenderingContext.CreateChildRenderer(Control.Content, renderingOptions);

        step = BorderRendererStep.Top;
        return true;
    }

    protected override bool DoRenderNextContentLine()
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
            RenderingContext.StartLine();

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