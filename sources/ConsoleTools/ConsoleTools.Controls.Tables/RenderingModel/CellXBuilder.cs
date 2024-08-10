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

namespace DustInTheWind.ConsoleTools.Controls.Tables.RenderingModel;

internal class CellXBuilder
{
    private readonly CellBase cellBase;

    public CellXBuilder(CellBase cellBase)
    {
        this.cellBase = cellBase ?? throw new ArgumentNullException(nameof(cellBase));
    }

    public CellX Build()
    {
        CellX cellX = new()
        {
            ForegroundColor = cellBase.CalculateForegroundColor(),
            BackgroundColor = cellBase.CalculateBackgroundColor(),
            PaddingLeft = cellBase.CalculatePaddingLeft(),
            PaddingRight = cellBase.CalculatePaddingRight(),
            PaddingTop = cellBase.ComputePaddingTop(),
            PaddingBottom = cellBase.ComputePaddingBottom(),
            HorizontalAlignment = cellBase.CalculateHorizontalAlignment(),
            Content = cellBase.Content,
            ColumnSpan = ComputeColumnSpan(),
            OverflowBehavior = cellBase.OverflowBehavior
        };

        cellX.CalculateLayout();

        return cellX;
    }

    private int ComputeColumnSpan()
    {
        return cellBase switch
        {
            TitleCell => int.MaxValue,
            HeaderCell => 1,
            ContentCell contentCell => contentCell.ColumnSpan,
            FooterCell => int.MaxValue,
            _ => 1
        };
    }

    public static CellXBuilder CreateFor(CellBase cellBase)
    {
        return new CellXBuilder(cellBase);
    }
}