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

namespace DustInTheWind.ConsoleTools.Controls.Rendering;

public class RenderingContext
{
    private int currentLineLength;
    private readonly IDisplay display;

    /// <summary>
    /// Gets the layout values pre-calculated for the control being displayed.
    /// </summary>
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
    public ConsoleColor? ForegroundColor { get; set; }

    /// <summary>
    /// Gets or sets the color used for writing the background of the text.
    /// </summary>
    public ConsoleColor? BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the foreground color used for the text when no explicit color is provided.
    /// </summary>
    public ConsoleColor? ParentForegroundColor { get; set; }

    /// <summary>
    /// Gets or sets the background color used for the "transparent" parts of the control: margins
    /// and empty space to be displayed around the control.
    /// </summary>
    public ConsoleColor? ParentBackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets a function that will be called after each line is finished rendering.
    /// </summary>
    public Action<int> OnLineRendered { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RenderingContext"/> class.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown if one of the parameters is <c>null</c>.</exception>
    public RenderingContext(IDisplay display, ControlLayout controlLayout)
    {
        this.display = display ?? throw new ArgumentNullException(nameof(display));
        ControlLayout = controlLayout ?? throw new ArgumentNullException(nameof(controlLayout));
    }

    /// <summary>
    /// Starts a new line and automatically writes the necessary left margins and paddings.
    /// If another line is in progress. It is automatically ended before starting the new line.
    /// </summary>
    public void BeginLine()
    {
        if (ControlLayout == null)
            return;

        if (currentLineLength > 0)
            EndLine();

        if(IsRoot && !display.IsNewLine)
            display.EndLine();

        WriteBeginOfLine();
    }

    private void WriteBeginOfLine()
    {
        if (ControlLayout.EmptySpace.Left > 0)
            WriteSpaces(ControlLayout.EmptySpace.Left, ParentBackgroundColor);

        if (ControlLayout.Margin.Left > 0)
            WriteSpaces(ControlLayout.Margin.Left, ParentBackgroundColor);

        if (ControlLayout.Padding.Left > 0)
            WriteSpaces(ControlLayout.Padding.Left, BackgroundColor ?? ParentBackgroundColor);
    }

    /// <summary>
    /// Ends the current line, by filling in spaces and writing the right padding and margins.
    /// By te end, the entire length of the line is <see cref="LineLength"/>.
    /// </summary>
    public void EndLine()
    {
        if (ControlLayout == null)
            return;

        FillContentEmptySpace();
        WriteEndOfLine();
        CloseLine();
    }

    private void WriteEndOfLine()
    {
        if (ControlLayout.Padding.Right > 0)
            WriteSpaces(ControlLayout.Padding.Right, BackgroundColor ?? ParentBackgroundColor);

        if (!IsRoot && ControlLayout.Margin.Right > 0)
            WriteSpaces(ControlLayout.Margin.Right, ParentBackgroundColor);

        if (!IsRoot && ControlLayout.EmptySpace.Right > 0)
            WriteSpaces(ControlLayout.EmptySpace.Right, ParentBackgroundColor);
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

        ConsoleColor? fg = foregroundColor ?? ForegroundColor ?? ParentForegroundColor;
        ConsoleColor? bg = backgroundColor ?? BackgroundColor ?? ParentBackgroundColor;

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

        ConsoleColor? fg = foregroundColor ?? ForegroundColor ?? ParentForegroundColor;
        ConsoleColor? bg = backgroundColor ?? BackgroundColor ?? ParentBackgroundColor;

        display.Write(textToWrite, fg, bg);

        currentLineLength += textToWrite.Length;
    }

    private void CloseLine()
    {
        if (IsRoot)
            display.EndLine();

        LineCount++;
        OnLineRendered?.Invoke(currentLineLength);
        currentLineLength = 0;
    }

    private void FillContentEmptySpace()
    {
        if (currentLineLength >= ControlLayout.ActualFullWidth)
            return;

        int emptyLeft = ControlLayout.EmptySpace.Left;
        int marginLeft = ControlLayout.Margin.Left;
        int paddingLeft = ControlLayout.Padding.Left;
        int contentWidth = ControlLayout.ContentSize.Width;

        int remainingContentLength = emptyLeft + marginLeft + paddingLeft + contentWidth - currentLineLength;

        if (remainingContentLength > 0)
            WriteSpaces(remainingContentLength, BackgroundColor ?? ParentBackgroundColor);
    }

    /// <summary>
    /// Starts a new line, writes the specified text and ends the line.
    /// If the specified text is shorter than the length of a line, empty spaces are used to fill it.
    /// If the specified text is longer than the length of a line, it is truncated.
    /// </summary>
    /// <param name="text">The text to be written as the content of the line.</param>
    /// <param name="foregroundColor">The color of the text.</param>
    /// <param name="backgroundColor">The color of the text's background.</param>
    public void WriteLine(string text = null, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null)
    {
        BeginLine();
        Write(text, foregroundColor, backgroundColor);
        EndLine();
    }

    /// <summary>
    /// Writes a top/bottom margin line.
    /// What a top/bottom margin means depends on the control being rendered as root or block.
    /// - If the control is rendered as root, the top or bottom margin is not written. The cursor
    /// is just moved to the next line.
    /// - If the control is rendered as block, a top or a bottom margin is a line filled with
    /// spaces.
    /// </summary>
    public void WriteMarginLine()
    {
        if (currentLineLength > 0)
            EndLine();

        if (!IsRoot)
            WriteSpaces(ControlLayout.ActualFullWidth, ParentBackgroundColor);

        CloseLine();
    }
    /// <summary>
    /// Writes a top/bottom padding line.
    /// What a top/bottom padding means depends on the control being rendered as root or block and
    /// on the control's background color.
    /// - If the control is rendered as root and has no background color, the top or bottom padding
    /// is not written. The cursor is moved to the next line.
    /// - If the control is rendered as block, the top or bottom padding is a line filled with
    /// spaces colored with the control's background color or its parent's background color.
    /// </summary>
    public void WritePaddingLine()
    {
        if (currentLineLength > 0)
            EndLine();

        BeginLine();
        EndLine();
    }

    private void WriteSpaces(int count, ConsoleColor? backgroundColor)
    {
        int availableCharacterCount = LineLength.HasValue
            ? LineLength.Value - currentLineLength
            : int.MaxValue;

        int spacesCount = Math.Min(count, availableCharacterCount);

        if (spacesCount > 0)
        {
            string text = new(' ', spacesCount);
            display.Write(text, null, backgroundColor);

            currentLineLength += spacesCount;
        }
    }

    /// <summary>
    /// Creates and returns a new root renderer for the specified control having the same display
    /// as output.
    /// </summary>
    public IRenderer CreateRootRenderer(Control control, RenderingOptions renderingOptions = null)
    {
        return control.GetRenderer(display, renderingOptions);
    }


    /// <summary>
    /// Creates and returns a new child renderer for the specified control.
    /// The new child renderer is reporting back to the parent whenever a new line is written, so
    /// that the parent can be aware of the amount of text written so far.
    /// </summary>
    public IRenderer CreateChildRenderer(Control control, ChildRenderingOptions renderingOptions = null)
    {
        RenderingOptions options = new()
        {
            AvailableWidth = renderingOptions?.AvailableWidth,
            IsRoot = false,
            OnLineRendered = count =>
            {
                currentLineLength += count;
            },
            ParentForegroundColor = renderingOptions?.ParentForegroundColor,
            ParentBackgroundColor = renderingOptions?.ParentBackgroundColor
        };

        return control.GetRenderer(display, options);
    }
}