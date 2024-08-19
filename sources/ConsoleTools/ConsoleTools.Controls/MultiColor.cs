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
using System.Collections.Generic;

namespace DustInTheWind.ConsoleTools.Controls;

public class MultiColor : BlockControl
{
    public string Text { get; set; }

    public List<ConsoleColor> Colors { get; set; } = new()
    {
        ConsoleColor.Black,
        ConsoleColor.White,
        ConsoleColor.Gray,
        ConsoleColor.DarkGray,

        ConsoleColor.Blue,
        ConsoleColor.Green,
        ConsoleColor.Cyan,
        ConsoleColor.Red,
        ConsoleColor.Magenta,
        ConsoleColor.Yellow,

        ConsoleColor.DarkBlue,
        ConsoleColor.DarkGreen,
        ConsoleColor.DarkCyan,
        ConsoleColor.DarkRed,
        ConsoleColor.DarkMagenta,
        ConsoleColor.DarkYellow
    };

    public override IRenderer GetRenderer(RenderingOptions renderingOptions = null)
    {
        return new MultiColorRenderer(this, renderingOptions);
    }

    private class MultiColorRenderer : BlockControlRenderer<MultiColor>
    {
        private int index;

        public MultiColorRenderer(MultiColor control, RenderingOptions renderingOptions)
            : base(control, renderingOptions)
        {
        }

        protected override bool DoInitializeContentRendering()
        {
            if (Control.Text == null)
                return false;

            index = 0;
            return index < Control.Colors?.Count;
        }

        protected override bool DoRenderNextContentLine(IDisplay display)
        {
            if (Control.Text == null || Control.Colors == null || index >= Control.Colors.Count)
                return false;

            string text = Control.Text;
            ConsoleColor color = Control.Colors[index];

            display.WriteLine(text, color);

            index++;
            return index < Control.Colors.Count;
        }
    }
}