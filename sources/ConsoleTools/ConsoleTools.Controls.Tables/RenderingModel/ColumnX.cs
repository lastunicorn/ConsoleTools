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

internal class ColumnX
{
    private int minWidth;
    private int actualWidth;

    public int MinWidth
    {
        get => minWidth;
        set
        {
            minWidth = value;

            if (ActualWidth < value)
                ActualWidth = value;
        }
    }

    public int ActualWidth
    {
        get => actualWidth;
        set
        {
            if (value == actualWidth || (value < actualWidth && !AllowToShrink))
                return;

            actualWidth = value;
        }
    }

    public bool AllowToShrink { get; set; } = true;

    public List<ColumnSpanX> Spans { get; } = new();

    public int FlexibleWidthDelta => AllowToShrink
        ? ActualWidth - MinWidth
        : 0;

    public void AccomodateCell(CellX cellX)
    {
        if (cellX.ColumnSpan > 1)
        {
            ColumnSpanX columnSpanX = new()
            {
                Span = cellX.ColumnSpan,
                MinWidth = cellX.PreferredSize.Width
            };

            Spans.Add(columnSpanX);
        }
        else
        {
            if (ActualWidth < cellX.PreferredSize.Width)
                ActualWidth = cellX.PreferredSize.Width;
        }

        if (cellX.OverflowBehavior == OverflowBehavior.PreserveOverflow)
        {
            if (MinWidth < cellX.PreferredSize.Width)
                minWidth = cellX.PreferredSize.Width;
        }
    }

    public void ShrinkToMinimum()
    {
        if (AllowToShrink)
            ActualWidth = minWidth;
    }

    public void ShrinkBy(int delta)
    {
        if (!AllowToShrink)
            return;

        int newWidth = ActualWidth - delta;
        ActualWidth = Math.Max(minWidth, newWidth);
    }
}