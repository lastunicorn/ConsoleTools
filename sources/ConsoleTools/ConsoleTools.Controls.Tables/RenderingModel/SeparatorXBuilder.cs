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

internal class SeparatorXBuilder
{
    private readonly RowBase previousRow;
    private readonly RowBase currentRow;

    public SeparatorXBuilder(RowBase previousRow, RowBase currentRow)
    {
        this.previousRow = previousRow;
        this.currentRow = currentRow;
    }

    public SeparatorX Build()
    {
        return new SeparatorX
        {
            BorderTemplate = ComputeBorderTemplate(),
            ForegroundColor = ComputeBorderForegroundColor(),
            BackgroundColor = ComputeBorderBackgroundColor()
        };
    }

    private BorderTemplate ComputeBorderTemplate()
    {
        BorderTemplate template = currentRow?.BorderTemplate;

        if (template != null)
            return template;

        template = previousRow?.BorderTemplate;

        if (template != null)
            return template;

        template = currentRow?.ParentDataGrid?.BorderTemplate;

        if (template != null)
            return template;

        template = previousRow?.ParentDataGrid?.BorderTemplate;

        return template;
    }

    private ConsoleColor? ComputeBorderForegroundColor()
    {
        ConsoleColor? color = currentRow?.BorderForegroundColor;

        if (color != null)
            return color;

        color = previousRow?.BorderForegroundColor;

        if (color != null)
            return color;

        color = currentRow?.ParentDataGrid?.BorderForegroundColor;

        if (color != null)
            return color;

        color = previousRow?.ParentDataGrid?.BorderForegroundColor;

        return color;
    }

    private ConsoleColor? ComputeBorderBackgroundColor()
    {
        ConsoleColor? color = currentRow?.BorderBackgroundColor;

        if (color != null)
            return color;

        color = previousRow?.BorderBackgroundColor;

        if (color != null)
            return color;

        color = currentRow?.ParentDataGrid?.BorderBackgroundColor;

        if (color != null)
            return color;

        color = previousRow?.ParentDataGrid?.BorderBackgroundColor;

        return color;
    }
}