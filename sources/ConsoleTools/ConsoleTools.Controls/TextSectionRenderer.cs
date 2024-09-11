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

namespace DustInTheWind.ConsoleTools.Controls;

internal class TextSectionRenderer : SectionRenderer
{
    private bool isInitialized;
    private IEnumerator<string> enumerator;
    private MultilineText text;
    private bool hasMoreLines;

    public override bool HasMoreLines => hasMoreLines;

    public MultilineText Text
    {
        get => text;
        set
        {
            text = value;
            isInitialized = false;
        }
    }

    public ConsoleColor? ForegroundColor { get; set; }

    public TextSectionRenderer(RenderingContext renderingContext)
        : base(renderingContext)
    {
    }

    public override void RenderNextLine()
    {
        if (!isInitialized)
            Initialize();

        if (enumerator?.Current == null)
            return;

        RenderingContext.WriteLine(enumerator.Current, ForegroundColor);
        hasMoreLines = enumerator.MoveNext();
    }

    private void Initialize()
    {
        if (text == null)
        {
            enumerator = null;
            hasMoreLines = false;
        }
        else
        {
            enumerator = text.GetEnumerator();
            hasMoreLines = enumerator.MoveNext();
        }

        isInitialized = true;
    }

    public override void Reset()
    {
        if (!isInitialized)
            Initialize();

        enumerator.Reset();
        hasMoreLines = enumerator.MoveNext();
    }
}