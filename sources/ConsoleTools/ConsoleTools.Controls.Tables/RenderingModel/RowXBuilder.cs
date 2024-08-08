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
using System.Linq;

namespace DustInTheWind.ConsoleTools.Controls.Tables.RenderingModel;

internal class RowXBuilder
{
    private readonly RowBase rowBase;

    public RowXBuilder(RowBase rowBase)
    {
        this.rowBase = rowBase ?? throw new ArgumentNullException(nameof(rowBase));
    }

    public RowX Build()
    {
        RowX rowX = new()
        {
            Border = rowBase.ParentDataGrid == null || rowBase.ParentDataGrid.AreBordersAllowed
                ? RowBorderX.CreateFrom(rowBase)
                : null,
            Cells = CreateCells()
        };

        rowX.CalculateLayout();

        return rowX;
    }

    private List<CellX> CreateCells()
    {
        switch (rowBase)
        {
            case TitleRow titleRow:
            {
                CellX cellX = CellX.CreateFor(titleRow.TitleCell);
                cellX.ColumnSpan = int.MaxValue;
                return new List<CellX> { cellX };
            }

            case ContentRow contentRow:
            {
                return contentRow.EnumerateVisibleCells()
                    .Select(CellX.CreateFor)
                    .ToList();
            }

            case HeaderRow headerRow:
            {
                return headerRow.EnumerateVisibleCells()
                    .Select(CellX.CreateFor)
                    .ToList();
            }

            case FooterRow footerRow:
            {
                CellX cellX = CellX.CreateFor(footerRow.FooterCell);
                cellX.ColumnSpan = int.MaxValue;
                return new List<CellX> { cellX };
            }

            default:
                return new List<CellX>();
        }
    }

    public static RowXBuilder CreateFor(RowBase rowBase)
    {
        return new RowXBuilder(rowBase);
    }
}