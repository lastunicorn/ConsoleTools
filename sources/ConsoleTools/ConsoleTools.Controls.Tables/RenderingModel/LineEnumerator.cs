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

using System.Collections;
using System.Collections.Generic;

namespace DustInTheWind.ConsoleTools.Controls.Tables.RenderingModel;

internal class LineEnumerator : IEnumerator<string>
{
    private int cellContentWidth;
    private IEnumerator<string> contentLineEnumerator;
    private int lineIndex;

    public MultilineText Content { get; set; }

    public int PaddingLeft { get; set; }

    public int PaddingRight { get; set; }

    public HorizontalAlignment HorizontalAlignment { get; set; }

    public Size Size { get; set; }

    public string Current { get; private set; }

    object IEnumerator.Current => Current;

    public void Reset()
    {
        cellContentWidth = Size.Width - PaddingLeft - PaddingRight;

        contentLineEnumerator = Content
            .GetLines(cellContentWidth, OverflowBehavior.WordWrap)
            .GetEnumerator();

        lineIndex = -1;
    }

    public bool MoveNext()
    {
        lineIndex++;

        if (lineIndex == Size.Height)
        {
            Current = null;
            return false;
        }

        bool success = contentLineEnumerator.MoveNext();

        if (success)
        {
            // Build inner content.

            string innerContent = contentLineEnumerator.Current;

            innerContent = AlignedText.QuickAlign(innerContent, HorizontalAlignment, cellContentWidth);

            // Build paddings.

            string paddingLeft = new(' ', PaddingLeft);
            string paddingRight = new(' ', PaddingRight);

            // Concatenate everything.

            Current = paddingLeft + innerContent + paddingRight;
        }
        else
        {
            // return empty line.

            Current = new string(' ', Size.Width);
        }

        return true;
    }

    public void Dispose()
    {
    }
}