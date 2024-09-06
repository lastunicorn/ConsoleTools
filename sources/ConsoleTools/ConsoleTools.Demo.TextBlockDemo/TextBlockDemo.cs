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

using DustInTheWind.ConsoleTools.Demo.Core;
using DustInTheWind.ConsoleTools.Demo.TextBlockDemo.DemoTasks;

namespace DustInTheWind.ConsoleTools.Demo.TextBlockDemo
{
    internal class TextBlockDemo : DemoBase
    {
        public override string Name => "TextBlock Demo";

        public TextBlockDemo()
        {
            DemoTasks.AddRange(new DemoTaskBase[]
            {
                new SingleShortLineDemoTask(),
                new MultipleShortLinesDemoTask(),
                new SingleLongLineDemoTask(),
                new MultipleLongLinesDemoTask(),
                new MarginsDemoTask(),
                new PaddingsDemoTask(),
                new ForegroundColorDemoTask(),
                new BackgroundColorDemoTask(),
                new MinWidthDemoTask(),
                new MaxWidthDemoTask(),
                new WidthDemoTask(),
                new HorizontalAlignmentCenterDemoTask(),
                new HorizontalAlignmentRightDemoTask()
            });
        }
    }
}