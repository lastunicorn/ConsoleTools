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

namespace DustInTheWind.ConsoleTools.Demo.BorderDemo.Demo.MarginsAndPaddingsDemoPackage;

internal class PaddingsDemo : DemoBase
{
    public override string Title => "Paddings (1, 1, 1, 1)";

    protected override void DoExecute()
    {
        Border border = new()
        {
            Padding = 1,
            ForegroundColor = ConsoleColor.DarkBlue,
            BackgroundColor = ConsoleColor.Blue,
            Content = new TextBlock
            {
                ForegroundColor = ConsoleColor.DarkGreen,
                BackgroundColor = ConsoleColor.Green,
                Text = new[]
                {
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    "Sed sollicitudin non enim sit amet interdum.",
                    "Nullam quis nisl a dolor convallis rhoncus at sit amet eros."
                }
            }
        };

        border.Display();
    }
}