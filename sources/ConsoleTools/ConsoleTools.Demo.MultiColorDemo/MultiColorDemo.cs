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

using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Demo.Core;

namespace DustInTheWind.ConsoleTools.Demo.MultiColorDemo;

public class MultiColorDemo : DemoBase
{
    public override string Title => "Multi Color Text";

    public override MultilineText Description => "A dummy control that displays a text in multiple colors, just for fun.";

    protected override void DoExecute()
    {
        MultiColor multiColor = new()
        {
            Text = "This is a dummy text."
        };

        multiColor.Display();
    }
}