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
using DustInTheWind.ConsoleTools.Controls.InputControls;
using DustInTheWind.ConsoleTools.Demo.Utils;

namespace DustInTheWind.ConsoleTools.Demo.InputControlsDemo.Demo.ReadValues;

internal class ValueReadStringDemo : DemoBase
{
    public override string Title => "Value Read - Strings";

    protected override void DoExecute()
    {
        RunExample();
    }

    /// <summary>
    /// This example creates instances for each input value and sets different label colors.
    /// Each instance reads a different type of value (string, int, DateTime, float)
    /// </summary>
    private static void RunExample()
    {
        // Create the input controls

        ValueControl<string> firstNameControl = new("First Name:");
        firstNameControl.Label.ForegroundColor = ConsoleColor.Cyan;

        ValueControl<string> lastNameControl = new("Last Name:");
        lastNameControl.Label.ForegroundColor = ConsoleColor.Cyan;

        // Read values using the input controls
        string firstName = firstNameControl.Read();
        string lastName = lastNameControl.Read();

        // Display the read values.
        CustomConsole.WriteLine();
        CustomConsole.WriteLine("Hi, {0} {1}!", firstName, lastName);
    }
}