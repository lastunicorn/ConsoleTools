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
using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Demo.Utils;

namespace DustInTheWind.ConsoleTools.Demo.TextBlockDemo.Demo.HorizontalAlignmentDemo;

internal class HorizontalAlignmentStretchDemo : DemoBase
{
    public override string Title => "Custom HorizontalAlignment Stretch (Width = 50)";

    protected override void DoExecute()
    {
        TextBlock textBlock = new()
        {
            Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam orci purus, luctus in est a, tempor luctus tortor. In tortor metus, lacinia vel sapien suscipit, commodo scelerisque metus.",
            Width = 50,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            BackgroundColor = ConsoleColor.DarkGray
        };

        textBlock.Display();
    }
}