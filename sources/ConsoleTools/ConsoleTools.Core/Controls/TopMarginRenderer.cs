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

internal class TopMarginRenderer : IRenderer
{
    private readonly ControlDisplay controlDisplay;
    private readonly ControlLayout controlLayout;
    private int actualCount;

    public bool HasMoreLines => actualCount < controlLayout.Margin.Top;

    public TopMarginRenderer(ControlDisplay controlDisplay, ControlLayout controlLayout)
    {
        this.controlDisplay = controlDisplay ?? throw new ArgumentNullException(nameof(controlDisplay));
        this.controlLayout = controlLayout ?? throw new ArgumentNullException(nameof(controlLayout));
    }

    public void RenderNextLine()
    {
        if (actualCount >= controlLayout.Margin.Top)
            return;

        controlDisplay.WriteLine();

        actualCount++;
    }
}