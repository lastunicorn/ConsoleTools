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

public abstract class ControlRendererBase<TControl> : IRenderer
    where TControl : BlockControl
{
    private int currentLineLength;

    public abstract bool HasMoreLines { get; }

    protected TControl Control { get; }

    protected ControlLayout ControlLayout { get; }

    public int? MaxLineLength { get; set; }

    public int LineCount { get; protected set; }

    protected ControlRendererBase(TControl control, RenderingOptions renderingOptions)
    {
        Control = control ?? throw new ArgumentNullException(nameof(control));

        ControlLayout = new ControlLayout
        {
            Control = control,
            AvailableWidth = renderingOptions?.AvailableWidth,
            DesiredContentWidth = control.DesiredContentWidth
        };

        ControlLayout.Calculate();

    }

    public abstract void RenderNextLine(IDisplay display);

    protected void Write(IDisplay display, string text, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null)
    {
        if (text == null)
            return;

        int availableCharacterCount = MaxLineLength.HasValue
            ? MaxLineLength.Value - currentLineLength
            : int.MaxValue;

        if (availableCharacterCount <= 0)
            return;

        string textToWrite = text.Length <= availableCharacterCount
            ? text
            : text.Substring(0, availableCharacterCount);

        ConsoleColor? fg = foregroundColor ?? display.ForegroundColor;
        ConsoleColor? bg = backgroundColor ?? display.BackgroundColor;

        display.DoWrite(textToWrite, fg, bg);

        currentLineLength += textToWrite.Length;
    }

    protected void WriteSpaces(IDisplay display, int count, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor)
    {
        int availableCharacterCount = MaxLineLength.HasValue
            ? MaxLineLength.Value - currentLineLength
            : int.MaxValue;

        int spacesCount = Math.Min(count, availableCharacterCount);

        if (spacesCount > 0)
        {
            string text = new(' ', spacesCount);
            display.DoWrite(text, foregroundColor, backgroundColor);

            currentLineLength += spacesCount;
        }
    }

    protected void WriteLine(IDisplay display, string text = null, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null)
    {
        StartLine(display);
        Write(display, text, foregroundColor, backgroundColor);
        EndLine(display);
    }

    protected void StartLine(IDisplay display)
    {
        OnBeforeStartLine(display);
        OnAfterStartLine(display);
    }

    protected virtual void OnBeforeStartLine(IDisplay display)
    {
    }

    protected virtual void OnAfterStartLine(IDisplay display)
    {
    }

    protected virtual void EndLine(IDisplay display)
    {
        OnBeforeEndLine(display);

        //if (isRoot)
        display.DoWriteRootEndLine();

        LineCount++;
        currentLineLength = 0;

        OnAfterEndLine(display);
    }

    protected virtual void OnBeforeEndLine(IDisplay display)
    {
    }

    protected virtual void OnAfterEndLine(IDisplay display)
    {
    }
}