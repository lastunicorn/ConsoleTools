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

// --------------------------------------------------------------------------------
// Bugs or feature requests
// --------------------------------------------------------------------------------
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new/choose

using System;
using System.Collections.Generic;

namespace DustInTheWind.ConsoleTools.Controls.Tables.RenderingModel;

internal class CellX
{
    private IEnumerator<string> lineEnumerator;

    public MultilineText Content { get; set; }

    public Size PreferredSize { get; set; }

    public Size ActualContentSize { get; set; }

    public ConsoleColor? ForegroundColor { get; set; }

    public ConsoleColor? BackgroundColor { get; set; }

    public int PaddingLeft { get; set; }

    public int PaddingRight { get; set; }

    public HorizontalAlignment HorizontalAlignment { get; set; }

    public int ColumnSpan { get; set; }

    public void CalculateLayout()
    {
        PreferredSize = CalculateSize(-1);
    }

    public void InitializeRendering(int desiredWidth)
    {
        ActualContentSize = CalculateSize(desiredWidth);

        lineEnumerator = new LineEnumerator
        {
            Content = Content,
            PaddingLeft = PaddingLeft,
            PaddingRight = PaddingRight,
            HorizontalAlignment = HorizontalAlignment,
            Size = ActualContentSize
        };
        lineEnumerator.Reset();
    }

    private Size CalculateSize(int desiredWidth)
    {
        int cellWidth;
        int cellHeight;

        bool isEmpty = Content == null || Content.IsEmpty;

        if (isEmpty)
        {
            cellWidth = PaddingLeft + PaddingRight;
            cellHeight = 0;
        }
        else
        {
            Size contentSize = Content.CalculateSize(desiredWidth);

            cellWidth = PaddingLeft + contentSize.Width + PaddingRight;
            cellHeight = contentSize.Height;
        }

        if (desiredWidth >= 0 && cellWidth != desiredWidth)
            cellWidth = desiredWidth;

        return new Size(cellWidth, cellHeight);
    }

    public void RenderNextLine(ITablePrinter tablePrinter)
    {
        if (lineEnumerator == null)
            throw new Exception("The cell rendering was not initialized yet.");

        string content = lineEnumerator.MoveNext()
            ? lineEnumerator.Current
            : new string(' ', ActualContentSize.Width);

        tablePrinter.Write(content, ForegroundColor, BackgroundColor);
    }

    public static CellX CreateFrom(CellBase cellBase)
    {
        CellX cellX = new()
        {
            ForegroundColor = cellBase.CalculateForegroundColor(),
            BackgroundColor = cellBase.CalculateBackgroundColor(),
            PaddingLeft = cellBase.CalculatePaddingLeft(),
            PaddingRight = cellBase.CalculatePaddingRight(),
            HorizontalAlignment = cellBase.CalculateHorizontalAlignment(),
            PreferredSize = cellBase.CalculatePreferredSize(),
            Content = cellBase.Content,
            ColumnSpan = 1
        };

        cellX.CalculateLayout();

        return cellX;
    }
}