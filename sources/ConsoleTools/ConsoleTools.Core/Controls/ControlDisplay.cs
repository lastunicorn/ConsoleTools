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

public class ControlDisplay
{
    private int currentLineLength;

    public IDisplay Display { get; }

    private ControlLayout ControlLayout { get; set; }

    public bool IsRoot { get; set; }

    public int? MaxLineLength { get; set; }

    public int LineCount { get; private set; }


    public ConsoleColor ForegroundColor
    {
        get => Display.ForegroundColor;
        set => Display.ForegroundColor = value;
    }

    public ConsoleColor BackgroundColor
    {
        get => Display.BackgroundColor;
        set => Display.BackgroundColor = value;
    }

    public ControlDisplay(IDisplay display, ControlLayout controlLayout)
    {
        Display = display ?? throw new ArgumentNullException(nameof(display));
        ControlLayout = controlLayout ?? throw new ArgumentNullException(nameof(controlLayout));
    }

    public void StartLine()
    {
        if (ControlLayout != null)
        {
            WriteSpaces(ControlLayout.EmptySpace.Left, null, null);
            WriteSpaces(ControlLayout.Margin.Left, null, null);
            WriteSpaces(ControlLayout.Padding.Left, null, BackgroundColor);
        }
    }

    public void Write(char c, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null)
    {
        if (currentLineLength >= MaxLineLength)
            return;

        ConsoleColor? fg = foregroundColor ?? ForegroundColor;
        ConsoleColor? bg = backgroundColor ?? BackgroundColor;

        Display.Write(c, fg, bg);

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

        Display.Write(textToWrite, fg, bg);

        currentLineLength += textToWrite.Length;
    }

    public void EndLine()
    {
        if (ControlLayout != null)
        {
            //FillContentEmptySpace();
            WriteSpaces(ControlLayout.Padding.Right, null, BackgroundColor);
            WriteSpaces(ControlLayout.Margin.Right, null, null);

            if (!IsRoot)
                WriteSpaces(ControlLayout.EmptySpace.Right, null, null);
        }

        if (IsRoot)
            Display.DoWriteRootEndLine();

        LineCount++;
        currentLineLength = 0;
    }

    public void WriteLine(string text = null, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null)
    {
        StartLine();
        Write(text, foregroundColor, backgroundColor);
        EndLine();
    }

    //private void FillContentEmptySpace()
    //{
    //    if (Layout == null)
    //        return;

    //    if (currentLineLength >= Layout.ActualFullWidth)
    //        return;

    //    int marginRight = Layout.Margin.Right;
    //    int paddingRight = Layout.Padding.Right;
    //    int emptySpaceRight = Layout.ActualFullWidth - currentLineLength - paddingRight - marginRight;

    //    if (emptySpaceRight <= 0)
    //        return;

    //    string text = new(' ', emptySpaceRight);
    //    DoWrite(text, null, null);
    //}

    public void WritePadding(int count)
    {
        WriteSpaces(count, null, null);
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
            Display.Write(text, foregroundColor, backgroundColor);

            currentLineLength += spacesCount;
        }
    }

    public void Flush()
    {
        Display.Flush();
    }
}