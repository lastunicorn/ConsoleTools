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

using System.Collections.Generic;

namespace DustInTheWind.ConsoleTools.Controls.Tables.RenderingModel;

internal class ColumnX
{
    private int minWidth;

    public int MinWidth
    {
        get => minWidth;
        set
        {
            minWidth = value;

            if (Width < value)
                Width = value;
        }
    }

    public int Width { get; set; }

    public List<ColumnSpanX> Spans { get; } = new();

    public void AccomodateCell(CellX cellX)
    {
        int width = cellX.PreferredSize.Width;
        int span = cellX.ColumnSpan;

        if (span > 1)
        {
            ColumnSpanX columnSpanX = new()
            {
                Span = span,
                MinWidth = width
            };

            Spans.Add(columnSpanX);
        }
        else
        {
            if (width > Width)
                Width = width;
        }
    }
}