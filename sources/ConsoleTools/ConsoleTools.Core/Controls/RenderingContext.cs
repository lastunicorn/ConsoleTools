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

public class RenderingContext
{
    private int currentLineLength;
    private readonly IDisplay display;

    public ControlLayout ControlLayout { get; }

    /// <summary>
    /// Gets or sets a value that specifies if the underlying control is displayed as root or
    /// it is a child of a larger control.
    /// </summary>
    ///
    /// <remarks>
    /// <para>
    /// If the underlying control is root, each displayed line of the control will also represent
    /// the entire line being written to the display. So, an end-line will be written at the end.
    /// </para>
    /// <para>
    /// If the underlying control is embedded into a parent control, after each line being written,
    /// we allow the parent control to continue write its content. So, no end-line will be written
    /// at the end.
    /// </para>
    /// </remarks>
    public bool IsRoot { get; set; }

    /// <summary>
    /// Gets or sets the length of a line.
    /// If the content being written is bigger than this value, it will be truncated.
    /// If it is shorter, it will be filled in with spaces by te <see cref="EndLine"/> method.
    /// </summary>
    public int? LineLength { get; set; }

    /// <summary>
    /// Gets the number of lines that were written so far.
    /// The line in progress is not included in the count.
    /// </summary>
    public int LineCount { get; private set; }

    /// <summary>
    /// Gets or sets the color used for writing the text.
    /// </summary>
    public ConsoleColor ForegroundColor
    {
        get => display.ForegroundColor;
        set => display.ForegroundColor = value;
    }

    /// <summary>
    /// Gets or sets the color used for writing the background of the text.
    /// </summary>
    public ConsoleColor BackgroundColor
    {
        get => display.BackgroundColor;
        set => display.BackgroundColor = value;
    }

    public Action<int> OnLineWritten { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RenderingContext"/> class.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown if one of the parameters is <c>null</c>.</exception>
    public RenderingContext(IDisplay display, ControlLayout controlLayout)
    {
        this.display = display ?? throw new ArgumentNullException(nameof(display));
        this.ControlLayout = controlLayout ?? throw new ArgumentNullException(nameof(controlLayout));
    }

    /// <summary>
    /// Starts a new line and automatically writes the necessary left margins and paddings.
    /// If another line is in progress. It is automatically ended before starting the new line.
    /// </summary>
    public void StartLine()
    {
        if (currentLineLength > 0)
            EndLine();

        if (ControlLayout != null)
        {
            if (ControlLayout.EmptySpace.Left > 0)
                WriteSpaces(ControlLayout.EmptySpace.Left, null, null);

            if (ControlLayout.Margin.Left > 0)
                WriteSpaces(ControlLayout.Margin.Left, null, null);

            if (ControlLayout.Padding.Left > 0)
                WriteSpaces(ControlLayout.Padding.Left, null, BackgroundColor);
        }
    }

    /// <summary>
    /// Writes the specified character to the display.
    /// If the current line already exceeded the <see cref="LineLength"/>, the character is
    /// silently ignored.
    /// </summary>
    public void Write(char c, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null)
    {
        if (currentLineLength >= LineLength)
            return;

        ConsoleColor? fg = foregroundColor ?? ForegroundColor;
        ConsoleColor? bg = backgroundColor ?? BackgroundColor;

        display.Write(c, fg, bg);

        currentLineLength++;
    }

    /// <summary>
    /// Writes the specified text to the display.
    /// If the length of the text exceeds the remaining available space for the current line, the
    /// text is trimmed.
    /// If the current line already exceeded the <see cref="LineLength"/>, the text is silently
    /// ignored.
    /// </summary>
    public void Write(string text, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null)
    {
        if (text == null)
            return;

        int availableCharacterCount = LineLength.HasValue
            ? LineLength.Value - currentLineLength
            : int.MaxValue;

        if (availableCharacterCount <= 0)
            return;

        string textToWrite = text.Length <= availableCharacterCount
            ? text
            : text.Substring(0, availableCharacterCount);

        ConsoleColor? fg = foregroundColor ?? ForegroundColor;
        ConsoleColor? bg = backgroundColor ?? BackgroundColor;

        display.Write(textToWrite, fg, bg);

        currentLineLength += textToWrite.Length;
    }

    /// <summary>
    /// Ends the current line, by filling in spaces and writing the right padding and margins.
    /// By te end, the entire length of the line is <see cref="LineLength"/>.
    /// </summary>
    public void EndLine()
    {
        if (ControlLayout != null)
        {
            FillContentEmptySpace();

            if (ControlLayout.Padding.Right > 0)
                WriteSpaces(ControlLayout.Padding.Right, null, BackgroundColor);

            if (ControlLayout.Margin.Right > 0)
                WriteSpaces(ControlLayout.Margin.Right, null, null);

            if (!IsRoot && ControlLayout.EmptySpace.Right > 0)
                WriteSpaces(ControlLayout.EmptySpace.Right, null, null);
        }

        if (IsRoot)
            display.EndLine();

        LineCount++;
        OnLineWritten?.Invoke(currentLineLength);
        currentLineLength = 0;
    }

    private void FillContentEmptySpace()
    {
        if (ControlLayout == null)
            return;

        if (currentLineLength >= ControlLayout.ActualFullWidth)
            return;

        int marginLeft = ControlLayout.Margin.Left;
        int paddingLeft = ControlLayout.Padding.Left;
        int contentWidth = ControlLayout.ContentSize.Width;

        int remainingContentLength = marginLeft + paddingLeft + contentWidth - currentLineLength;

        if (remainingContentLength <= 0)
            return;

        string text = new(' ', remainingContentLength);
        display.Write(text);
        currentLineLength += text.Length;
    }

    public void WriteLine(string text = null, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null)
    {
        StartLine();
        Write(text, foregroundColor, backgroundColor);
        EndLine();
    }

    public void WriteMarginLine()
    {
        if (IsRoot)
        {
            display.EndLine();
        }
        else
        {
            StartLine();
            EndLine();
        }
    }

    public void WritePaddingLine()
    {
        StartLine();
        EndLine();
    }

    private void WriteSpaces(int count, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor)
    {
        int availableCharacterCount = LineLength.HasValue
            ? LineLength.Value - currentLineLength
            : int.MaxValue;

        int spacesCount = Math.Min(count, availableCharacterCount);

        if (spacesCount > 0)
        {
            string text = new(' ', spacesCount);
            display.Write(text, foregroundColor, backgroundColor);

            currentLineLength += spacesCount;
        }
    }

    public IRenderer CreateChildRenderer(BlockControl control, ChildRenderingOptions renderingOptions)
    {
        RenderingOptions options = new()
        {
            AvailableWidth = renderingOptions.AvailableWidth,
            IsRoot = false,
            OnWrite = count =>
            {
                currentLineLength += count;
            }
        };

        return control.GetRenderer(display, options);
    }
}