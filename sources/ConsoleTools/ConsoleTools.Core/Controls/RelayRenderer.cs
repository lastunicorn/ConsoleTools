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

public class RelayRenderer : IRenderer
{
    public bool HasMoreLines { get; private set; }

    public Func<bool> RenderNextLineAction { get; set; }

    public RelayRenderer(Func<bool> initializeAction)
    {
        bool? hasMoreLines = initializeAction?.Invoke();
        HasMoreLines = hasMoreLines ?? false;
    }

    public void RenderNextLine()
    {
        bool? hasMoreLines = RenderNextLineAction?.Invoke();

        HasMoreLines = hasMoreLines ?? false;
    }
}