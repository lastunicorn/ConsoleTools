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
using System.Linq;
using DustInTheWind.ConsoleTools.Controls.Rendering;

namespace DustInTheWind.ConsoleTools.Controls;

internal class StackPanelRenderer : BlockRenderer<StackPanel>
{
    private IEnumerator<IRenderer> enumerator;

    public StackPanelRenderer(StackPanel stackPanel, IDisplay display, RenderingOptions renderingOptions)
        : base(stackPanel, display, renderingOptions)
    {
    }

    protected override bool InitializeContentRendering()
    {
        enumerator = Control.Children
            .Select(x => RenderingContext.CreateChildRenderer(x))
            .GetEnumerator();

        return enumerator.MoveNext() && (enumerator.Current?.HasMoreLines ?? false);
    }

    protected override bool RenderNextContentLine()
    {
        if (enumerator?.Current == null)
            return false;

        RenderingContext.StartLine();
        enumerator.Current.RenderNextLine();
        RenderingContext.EndLine();

        return MoveNext();
    }

    private bool MoveNext()
    {
        while (enumerator.Current == null || enumerator.Current.HasMoreLines == false)
        {
            bool success = enumerator.MoveNext();

            if (!success)
                return false;
        }

        return true;
    }
}