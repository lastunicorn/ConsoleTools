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

namespace DustInTheWind.ConsoleTools.Controls.Menus;

internal class EmptySection : SectionRenderer
{
    private readonly int lineCount;
    private int actualLineCount;

    public override bool HasMoreLines => actualLineCount < lineCount;

    public EmptySection(RenderingContext renderingContext, int lineCount)
        : base(renderingContext)
    {
        this.lineCount = lineCount;
    }

    public override void RenderNextLine()
    {
        RenderingContext.WriteLine();
        actualLineCount++;
    }

    public override void Reset()
    {
        actualLineCount = 0;
    }
}