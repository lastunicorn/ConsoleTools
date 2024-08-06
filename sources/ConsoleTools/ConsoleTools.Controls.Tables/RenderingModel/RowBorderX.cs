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
            Template = rowBase.ComputeBorderTemplate(),
            ForegroundColor = rowBase.ComputeBorderForegroundColor(),
            BackgroundColor = rowBase.ComputeBorderBackgroundColor()
        };
    }

    private static bool ComputeVerticalBorderVisibility(RowBase rowBase)
    {
        if (rowBase.BorderVisibility == null)
            return rowBase.ParentDataGrid == null || rowBase.ParentDataGrid.IsBorderVisible;

        if (rowBase.BorderVisibility.Value.Left == null && rowBase.BorderVisibility.Value.Inside == null && rowBase.BorderVisibility.Value.Right == null)
            return rowBase.ParentDataGrid == null || rowBase.ParentDataGrid.IsBorderVisible;

        return rowBase.BorderVisibility.Value.Left == true || rowBase.BorderVisibility.Value.Inside == true || rowBase.BorderVisibility.Value.Right == true;
    }
}