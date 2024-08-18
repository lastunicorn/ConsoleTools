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

public abstract class DisplayBase : IDisplay
{
    private readonly bool isRoot;
    private int currentLineLength;

    public ControlLayout Layout { get; set; }

    public ConsoleColor? ForegroundColor { get; set; }

    public ConsoleColor? BackgroundColor { get; set; }

    public int? MaxLineLength { get; set; }

    public int LineCount { get; private set; }

    protected DisplayBase(bool isRoot)
    {
        this.isRoot = isRoot;
    }

    public void StartLine()
    {
        WriteSpaces(Layout.OuterEmptySpaceLeft, null, null);
        WriteSpaces(Layout.MarginLeft, null, null);
        WriteSpaces(Layout.PaddingLeft, null, BackgroundColor);
    }

    public void Write(char c, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null)
    {
        if (currentLineLength >= MaxLineLength)
            return;

        ConsoleColor? fg = foregroundColor ?? ForegroundColor;
        ConsoleColor? bg = backgroundColor ?? BackgroundColor;

        DoWrite(c, fg, bg);

        currentLineLength++;
    }

    public void Write(string text, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null)
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

        ConsoleColor? fg = foregroundColor ?? ForegroundColor;
        ConsoleColor? bg = backgroundColor ?? BackgroundColor;

        DoWrite(textToWrite, fg, bg);

        currentLineLength += textToWrite.Length;
    }

    public void EndLine()
    {
        FillContentEmptySpace();
        WriteSpaces(Layout.PaddingRight, null, BackgroundColor);

        WriteSpaces(Layout.MarginRight, null, null);
        WriteSpaces(Layout.OuterEmptySpaceRight, null, null);

        if (isRoot)
            DoWriteRootEndLine();

        LineCount++;
        currentLineLength = 0;
    }

    public void WriteLine(string text = null, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null)
    {
        StartLine();
        Write(text, foregroundColor, backgroundColor);
        EndLine();
    }

    private void FillContentEmptySpace()
    {
        if (Layout == null)
            return;

        if (currentLineLength >= Layout.ActualFullWidth)
            return;

        int marginRight = Layout.MarginRight;
        int paddingRight = Layout.PaddingRight;
        int emptySpaceRight = Layout.ActualFullWidth - currentLineLength - paddingRight - marginRight;

        if (emptySpaceRight <= 0)
            return;

        string text = new(' ', emptySpaceRight);
        DoWrite(text, null, null);
    }

    private void WriteSpaces(int count, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor)
    {
        int availableCharacterCount = MaxLineLength.HasValue
            ? MaxLineLength.Value - currentLineLength
            : int.MaxValue;

        int spacesCount = Math.Min(count, availableCharacterCount);

        if (spacesCount > 0)
        {
            string text = new(' ', spacesCount);
            DoWrite(text, foregroundColor, backgroundColor);

            currentLineLength += spacesCount;
        }
    }

    protected abstract void DoWrite(char c, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor);

    protected abstract void DoWrite(string text, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor);

    protected abstract void DoWriteRootEndLine();

    public abstract void Flush();

    public abstract IDisplay CreateChild();
}