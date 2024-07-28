// ConsoleTools
// Copyright (C) 2017-2022 Dust in the Wind
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

namespace DustInTheWind.ConsoleTools.Controls
{
    public partial class MultiColor : BlockControl
    {
        private readonly List<ColorItem?> colorItems = new List<ColorItem?>
        {
            new ColorItem { Color = ConsoleColor.Black, Name = "Black" },
            new ColorItem { Color = ConsoleColor.White, Name = "White" },
            new ColorItem { Color = ConsoleColor.Gray, Name = "Gray" },
            new ColorItem { Color = ConsoleColor.DarkGray, Name = "Dark Gray" },
            null,
            new ColorItem { Color = ConsoleColor.Blue, Name = "Blue" },
            new ColorItem { Color = ConsoleColor.Green, Name = "Green" },
            new ColorItem { Color = ConsoleColor.Cyan, Name = "Cyan" },
            new ColorItem { Color = ConsoleColor.Red, Name = "Red" },
            new ColorItem { Color = ConsoleColor.Magenta, Name = "Magenta" },
            new ColorItem { Color = ConsoleColor.Yellow, Name = "Yellow" },
            null,
            new ColorItem { Color = ConsoleColor.DarkBlue, Name = "Dark Blue" },
            new ColorItem { Color = ConsoleColor.DarkGreen, Name = "Dark Green" },
            new ColorItem { Color = ConsoleColor.DarkCyan, Name = "Dark Cyan" },
            new ColorItem { Color = ConsoleColor.DarkRed, Name = "Dark Red" },
            new ColorItem { Color = ConsoleColor.DarkMagenta, Name = "DarkMagenta" },
            new ColorItem { Color = ConsoleColor.DarkYellow, Name = "DarkYellow" }
        };

        public override IEnumerator<Line> GetLineEnumerator(IDisplay display)
        {
            return new Enumerator(this, display);
        }
    }
}
