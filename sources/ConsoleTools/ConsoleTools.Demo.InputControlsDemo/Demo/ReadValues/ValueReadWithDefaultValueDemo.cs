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

using DustInTheWind.ConsoleTools.Controls.InputControls;
using DustInTheWind.ConsoleTools.Demo.Utils;

namespace DustInTheWind.ConsoleTools.Demo.InputControlsDemo.Demo.ReadValues;

internal class ValueReadWithDefaultValueDemo : DemoBase
{
    public override string Title => "Value Read - With default value";

    protected override void DoExecute()
    {
        RunExample();
    }

    private static void RunExample()
    {
        ValueControl<int> numberControl = new("Number ({0}):");
        numberControl.AcceptDefaultValue = true;
        numberControl.DefaultValue = 42;

        CustomConsole.WriteLine("Just hit enter. The default value, 42, is returned by the ValueControl control.");
        CustomConsole.WriteLine();

        int number = numberControl.Read();

        CustomConsole.WriteLine();
        CustomConsole.WriteLine("You selected {0}.", number);
    }
}