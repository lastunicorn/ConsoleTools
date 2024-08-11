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
            Cells = rowBase.EnumerateVisibleCells()
                .Select(x => CellXBuilder.CreateFor(x).Build())
                .ToList()
        };

        bool areBordersAllowed = rowBase.ParentDataGrid == null || rowBase.ParentDataGrid.AreBordersAllowed;
        if (areBordersAllowed)
        {
            bool areVerticalBorderVisible = ComputeVerticalBorderVisibility(rowBase);
            if (areVerticalBorderVisible)
            {
                rowX.BorderTemplate = ComputeBorderTemplate(rowBase);
                rowX.BorderForegroundColor = ComputeBorderForegroundColor(rowBase);
                rowX.BorderBackgroundColor = ComputeBorderBackgroundColor(rowBase);
            }
            ;
        }

        rowX.CalculateLayout();

        return rowX;
    }

    private static bool ComputeVerticalBorderVisibility(RowBase currentRow)
    {
        if (currentRow.BorderVisibility == null)
            return currentRow.ParentDataGrid == null || currentRow.ParentDataGrid.IsBorderVisible;

        if (currentRow.BorderVisibility.Value.Left == null && currentRow.BorderVisibility.Value.Inside == null && currentRow.BorderVisibility.Value.Right == null)
            return currentRow.ParentDataGrid == null || currentRow.ParentDataGrid.IsBorderVisible;

        return currentRow.BorderVisibility.Value.Left == true || currentRow.BorderVisibility.Value.Inside == true || currentRow.BorderVisibility.Value.Right == true;
    }

    private static BorderTemplate ComputeBorderTemplate(RowBase rowBase)
    {
        BorderTemplate template = rowBase.BorderTemplate;

        if (template != null)
            return template;

        template = rowBase.ParentDataGrid?.BorderTemplate;

        return template;
    }

    private static ConsoleColor? ComputeBorderForegroundColor(RowBase rowBase)
    {
        ConsoleColor? color = rowBase.BorderForegroundColor;

        if (color != null)
            return color;

        color = rowBase.ParentDataGrid?.BorderForegroundColor;

        return color;
    }

    private static ConsoleColor? ComputeBorderBackgroundColor(RowBase rowBase)
    {
        ConsoleColor? color = rowBase.BorderBackgroundColor;

        if (color != null)
            return color;

        color = rowBase.ParentDataGrid?.BorderBackgroundColor;

        return color;
    }

    public static RowXBuilder CreateFor(RowBase rowBase)
    {
        return new RowXBuilder(rowBase);
    }
}