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
using DustInTheWind.ConsoleTools.Controls.InputControls;
using DustInTheWind.ConsoleTools.Demo.Utils;

namespace DustInTheWind.ConsoleTools.Demo.InputControlsDemo.Demo.WriteValues;

internal class ValueWriteQuickDemo : DemoBase
{
    public override string Title => "Value Write - Quick (static method)";

    public override MultilineText Description => new[]
    {
        "The QuickDisplay static method can be used to display values. It is more convenient, but less flexible.",
        "There are two ways to access this method. Here it's an example for a string value:",
        "  - StringValue.QuickWrite(...)",
        "  - ValueControl<string>.QuickWrite(...)"
    };

    protected override void DoExecute()
    {
        StringValue.QuickWrite("First Name:", "John");
        StringValue.QuickWrite("Last Name:", "Doe");
        Int32Value.QuickWrite("Age:", 25);

        Console.WriteLine();

        ValueControl<string>.QuickWrite("First Name:", "John");
        ValueControl<string>.QuickWrite("Last Name:", "Doe");
        ValueControl<int>.QuickWrite("Age:", 25);
    }
}