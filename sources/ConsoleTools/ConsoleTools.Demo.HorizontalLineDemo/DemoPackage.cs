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

using DustInTheWind.ConsoleTools.Demo.HorizontalLineDemo.Demos.ColorsDemo;
using DustInTheWind.ConsoleTools.Demo.HorizontalLineDemo.Demos.HorizontalAlignmentDemo;
using DustInTheWind.ConsoleTools.Demo.HorizontalLineDemo.Demos.MarginAndPaddingDemo;
using DustInTheWind.ConsoleTools.Demo.HorizontalLineDemo.Demos.MiscellaneousDemo;
using DustInTheWind.ConsoleTools.Demo.Utils;

namespace DustInTheWind.ConsoleTools.Demo.HorizontalLineDemo;

public class DemoPackage : DemoPackageBase
{
    public override string Title => "Horizontal Line";

    public DemoPackage()
    {
        Demos.AddRange(new IDemo[]
        {
            new MiscellaneousDemoPackage(),
            new MarginAndPaddingDemoPackage(),
            new ColorsDemoPackage(),
            new HorizontalAlignmentDemoPackage()
        });
    }
}