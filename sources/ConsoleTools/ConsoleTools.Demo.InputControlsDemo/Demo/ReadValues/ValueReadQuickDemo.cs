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

using DustInTheWind.ConsoleTools.Controls.InputControls;
using DustInTheWind.ConsoleTools.Demo.Utils;

namespace DustInTheWind.ConsoleTools.Demo.InputControlsDemo.Demo.ReadValues;

internal class ValueReadQuickDemo : DemoBase
{
    public override string Title => "Value Read - Quick (static method)";

    protected override void DoExecute()
    {
        RunExample();
    }

    /// <summary>
    /// The QuickRead static method is used to read values from the console.
    /// </summary>
    private static void RunExample()
    {
        string firstName = ValueControl<string>.QuickRead("First Name:");
        string lastName = ValueControl<string>.QuickRead("Last Name:");

        // Display the read values.
        CustomConsole.WriteLine();
        CustomConsole.WriteLine("Hi, {0} {1}!", firstName, lastName);
    }
}