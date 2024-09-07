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

using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Demo.Core;

namespace DustInTheWind.ConsoleTools.Demo.TextBlockDemo.DemoTasks
{
    internal class MultipleLongLinesDemoTask : DemoTaskBase
    {
        public override string Title => "Multiple long lines (text wrapping)";

        protected override void DoExecute()
        {
            TextBlock textBlock = new TextBlock
            {
                Text = new[]
                {
                    "1) Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam orci purus, luctus in est a, tempor luctus tortor. In tortor metus, lacinia vel sapien suscipit, commodo scelerisque metus.",
                    "2) Sed sollicitudin non enim sit amet interdum. Vivamus sem nisl, commodo in posuere sed, ultrices quis nulla. Etiam justo nibh, lacinia vel ornare a, luctus quis quam.",
                    "3) Nullam quis nisl a dolor convallis rhoncus at sit amet eros. Suspendisse quis ipsum et eros ornare placerat. Fusce euismod eros eu est ullamcorper eleifend."
                }
            };

            textBlock.Display();
        }
    }
}