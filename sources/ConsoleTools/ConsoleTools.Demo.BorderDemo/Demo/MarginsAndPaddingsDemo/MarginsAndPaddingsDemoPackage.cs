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
using DustInTheWind.ConsoleTools.Demo.Utils;

namespace DustInTheWind.ConsoleTools.Demo.BorderDemo.Demo.MarginsAndPaddingsDemo;

internal class MarginsAndPaddingsDemoPackage : DemoPackageBase
{
    public override string Title => "Margins and Paddings";

    public override MultilineText Description => new[]
    {
        "Background color was added to to the border and the child control to easier see the margins and the paddings."
    };

    public MarginsAndPaddingsDemoPackage()
    {
        Demos.AddRange(new IDemo[]
        {
            new PaddingsDemo(),
            new MarginsDemo()
        });
    }
}