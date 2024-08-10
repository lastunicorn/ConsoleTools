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

internal static class CellContentOverflowExtensions
{
    public static OverflowBehavior ToOverflowBehavior(this CellContentOverflow cellContentOverflow)
    {
        return cellContentOverflow switch
        {
            CellContentOverflow.Default => throw new ArgumentOutOfRangeException(nameof(cellContentOverflow), cellContentOverflow, null),
            CellContentOverflow.PreserveOverflow => OverflowBehavior.PreserveOverflow,
            CellContentOverflow.CutChar => OverflowBehavior.CutChar,
            CellContentOverflow.CutWord => OverflowBehavior.CutWord,
            CellContentOverflow.CutCharWithEllipsis => OverflowBehavior.CutCharWithEllipsis,
            CellContentOverflow.CutWordWithEllipsis => OverflowBehavior.CutWordWithEllipsis,
            CellContentOverflow.WrapChar => OverflowBehavior.WrapChar,
            CellContentOverflow.WrapWord => OverflowBehavior.WrapWord,
            _ => throw new ArgumentOutOfRangeException(nameof(cellContentOverflow), cellContentOverflow, null)
        };
    }
}