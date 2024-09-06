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
using DustInTheWind.ConsoleTools.Controls.Rendering;

namespace DustInTheWind.ConsoleTools.Controls.Menus;

internal class TextMenuTitleSection : SectionRenderer
{
    private readonly TextMenu textMenu;
    private IEnumerator<string> enumerator;

    public override bool HasMoreLines => enumerator != null;

    public TextMenuTitleSection(TextMenu textMenu, RenderingContext renderingContext)
        : base(renderingContext)
    {
        this.textMenu = textMenu ?? throw new ArgumentNullException(nameof(textMenu));

        enumerator = GetLines().GetEnumerator();
        MoveNext();
    }

    private IEnumerable<string> GetLines()
    {
        yield return textMenu.TitleText;
        yield return string.Empty;
        yield return string.Empty;
    }

    public override void RenderNextLine()
    {
        RenderingContext.WriteLine(enumerator.Current, textMenu.TitleForegroundColor, textMenu.TitleBackgroundColor);
        MoveNext();
    }

    private void MoveNext()
    {
        if (enumerator == null)
            return;

        while (true)
        {
            bool success = enumerator.MoveNext();

            if (!success)
            {
                enumerator.Dispose();
                enumerator = null;
            }

            return;
        }
    }

    public override void Reset()
    {
        enumerator = GetLines().GetEnumerator();
        MoveNext();
    }
}