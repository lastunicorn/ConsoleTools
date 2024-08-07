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

namespace DustInTheWind.ConsoleTools.Controls.Tables.RenderingModel;

internal class RowBorderX
{
    public BorderTemplate Template { get; set; }

    public ConsoleColor? ForegroundColor { get; set; }

    public ConsoleColor? BackgroundColor { get; set; }

    public void RenderRowLeftBorder(ITablePrinter tablePrinter)
    {
        if (Template != null)
            tablePrinter.Write(Template.Left, ForegroundColor, BackgroundColor);
    }

    public void RenderRowRightBorder(ITablePrinter tablePrinter)
    {
        if (Template != null)
            tablePrinter.Write(Template.Right, ForegroundColor, BackgroundColor);
    }

    public void RenderRowInsideBorder(ITablePrinter tablePrinter)
    {
        if (Template != null)
            tablePrinter.Write(Template.Vertical, ForegroundColor, BackgroundColor);
    }

    public static RowBorderX CreateFrom(RowBase rowBase)
    {
        if (rowBase == null)
            return null;

        bool areVerticalBorderVisible = ComputeVerticalBorderVisibility(rowBase);

        if (!areVerticalBorderVisible)
            return null;

        return new RowBorderX
        {
            Template = ComputeBorderTemplate(rowBase),
            ForegroundColor = ComputeBorderForegroundColor(rowBase),
            BackgroundColor = ComputeBorderBackgroundColor(rowBase)
        };
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
}