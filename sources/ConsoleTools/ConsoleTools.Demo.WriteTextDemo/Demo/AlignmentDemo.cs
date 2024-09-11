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
using DustInTheWind.ConsoleTools.Demo.Utils;

namespace DustInTheWind.ConsoleTools.Demo.WriteTextDemo.Demo;

internal class AlignmentDemo : DemoBase
{
    public override string Title => "Alignment Demo";

    public override MultilineText Description => "The text alignment was obtained using the CustomConsole static class.";

    protected override void DoExecute()
    {
        CustomConsole.WriteLine(HorizontalAlignment.Left, "This is a text aligned to left.");
        CustomConsole.WriteLine(HorizontalAlignment.Left, "This is another text aligned to left.");

        CustomConsole.WriteLine();
        CustomConsole.WriteLine(HorizontalAlignment.Center, "This is a text aligned to center.");
        CustomConsole.WriteLine(HorizontalAlignment.Center, "This is another text aligned to center.");

        CustomConsole.WriteLine();
        CustomConsole.WriteLine(HorizontalAlignment.Right, "This is a text aligned to right.");
        CustomConsole.WriteLine(HorizontalAlignment.Right, "This is another text aligned to right.");
    }
}