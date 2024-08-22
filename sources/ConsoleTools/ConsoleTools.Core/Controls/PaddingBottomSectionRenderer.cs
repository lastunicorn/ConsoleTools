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

internal class PaddingBottomSectionRenderer : SectionRenderer
{
    private int lineIndex;

    public override bool HasMoreLines => lineIndex < RenderingContext.ControlLayout.Padding.Bottom;

    public PaddingBottomSectionRenderer(RenderingContext renderingContext)
        : base(renderingContext)
    {
    }

    public override void RenderNextLine()
    {
        if (lineIndex >= RenderingContext.ControlLayout.Padding.Bottom)
            return;

        RenderingContext.WritePaddingLine();

        lineIndex++;
    }

    public override void Reset()
    {
        lineIndex = 0;
    }
}