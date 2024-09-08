﻿// ConsoleTools
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
using DustInTheWind.ConsoleTools.Demo.Core;

namespace DustInTheWind.ConsoleTools.Demo.HorizontalLineDemo.Demos.MarginAndPaddingDemo;

internal class PaddingDemo : DemoBase
{
    public override string Title => "Custom Paddings (3 3 3 3)";

    public override MultilineText Description => "The background color was added to easier see the paddings.";

    protected override void DoExecute()
    {
        HorizontalLine horizontalLine = new()
        {
            Padding = 3,
            BackgroundColor = ConsoleColor.DarkGray // Added to easier see the paddings.
        };

        horizontalLine.Display();
    }
}