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
using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Controls.InputControls;
using DustInTheWind.ConsoleTools.Demo.Utils;

namespace DustInTheWind.ConsoleTools.Demo.InputControlsDemo.Demo.WriteList;

internal class ListWriteDemo : DemoBase
{
    public override string Title => "List Write - Custom";

    protected override void DoExecute()
    {
        string[] colors = Enum.GetNames(typeof(ConsoleColor));

        DisplayColors(colors);
    }

    /// <summary>
    /// By creating an instance of the <see cref="ListWrite{T}"/>, additional properties can be set.
    /// </summary>
    private static void DisplayColors(string[] colors)
    {
        ValueList<string> colorsWrite = new()
        {
            Label = new TextBlock
            {
                Text = "Colors:",
                ForegroundColor = ConsoleColor.Cyan
            },
            Values = colors.ToList(),
            Bullet = "#",
            Margin = "0 2"
            // etc...
        };

        colorsWrite.Write();
    }
}