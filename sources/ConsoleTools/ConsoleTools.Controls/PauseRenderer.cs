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

internal class PauseRenderer : BlockRenderer<Pause>
{
    private IEnumerator<string> linesEnumerator;

    public PauseRenderer(Pause control, IDisplay display, RenderingOptions renderingOptions)
        : base(control, display, renderingOptions)
    {
    }

    protected override bool InitializeContentRendering()
    {
        if (Control.Text == null)
            return false;

        linesEnumerator = Control.Text.GetLines(ControlLayout.ActualContentWidth, OverflowBehavior.CutChar)
            .GetEnumerator();

        return linesEnumerator.MoveNext();
    }

    protected override bool RenderNextContentLine()
    {
        RenderingContext.StartLine();
        RenderingContext.Write(linesEnumerator.Current);
        bool hasMoreLines = linesEnumerator.MoveNext();

        if (!hasMoreLines) 
            WaitForUnlockKey();

        RenderingContext.EndLine();

        return hasMoreLines;
    }

    private void WaitForUnlockKey()
    {
        bool isCorrectKey = false;

        while (!isCorrectKey)
        {
            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);

            isCorrectKey = !Control.UnlockKey.HasValue || consoleKeyInfo.Key == Control.UnlockKey;
        }
    }
}