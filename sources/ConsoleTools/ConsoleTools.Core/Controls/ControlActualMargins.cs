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

namespace DustInTheWind.ConsoleTools.Controls;

internal class ControlActualMargins
{
    private readonly Control control;
    private readonly Size availableSpace;

    public ControlActualMargins(Control control, Size availableSpace)
    {
        this.control = control ?? throw new ArgumentNullException(nameof(control));
        this.availableSpace = availableSpace;
    }

    public Thickness Compute()
    {
        return control switch
        {
            BlockControl blockControl => ComputeActualMargins(blockControl),
            InlineControl inlineControl => ComputeActualMargins(inlineControl),
            _ => Thickness.Zero
        };
    }

    private Thickness ComputeActualMargins(BlockControl blockControl)
    {
        Size actualSpace = Size.Empty;

        int availableWidth = availableSpace.Width - actualSpace.Width;
        int left = Math.Min(availableWidth, blockControl.Margin.Left);
        actualSpace = actualSpace.InflateWidth(left);

        availableWidth = availableSpace.Width - actualSpace.Width;
        int right = Math.Min(availableWidth, blockControl.Margin.Right);
        actualSpace = actualSpace.InflateWidth(right);

        int availableHeight = availableSpace.Height - actualSpace.Height;
        int top = Math.Min(availableHeight, blockControl.Margin.Top);
        actualSpace = actualSpace.InflateHeight(top);

        availableHeight = availableSpace.Height - actualSpace.Height;
        int bottom = Math.Min(availableHeight, blockControl.Margin.Bottom);
        actualSpace = actualSpace.InflateHeight(bottom);

        return new Thickness(left, top, right, bottom);
    }

    private Thickness ComputeActualMargins(InlineControl inlineControl)
    {
        Size actualSpace = Size.Empty;

        int availableWidth = availableSpace.Width - actualSpace.Width;
        int left = Math.Min(availableWidth, inlineControl.MarginLeft);
        actualSpace = actualSpace.InflateWidth(left);

        availableWidth = availableSpace.Width - actualSpace.Width;
        int right = Math.Min(availableWidth, inlineControl.MarginRight);
        actualSpace = actualSpace.InflateWidth(right);

        return new Thickness(left, 0, right, 0);
    }
}