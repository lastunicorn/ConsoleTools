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
using System.ComponentModel.Design;
using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Demo.Utils;

namespace DustInTheWind.ConsoleTools.Demo.WriteTextDemo.Demo;

internal class ColorsDemo : DemoBase
{
    public override string Title => "Colors";

    public override MultilineText Description => "CustomConsole static class is used. Colors are selected automatically based on the type of the text being written: Normal, Emphasized, Success, Warning or Error";

    protected override void DoExecute()
    {
        CustomConsole.WriteLine("Normal: This is a normal line of text.");

        CustomConsole.WriteLine();
        CustomConsole.WriteLineEmphasized("Emphasized: But I can also write an emphasized text.");

        CustomConsole.WriteLine();
        CustomConsole.WriteLineSuccess("Success: And everything is ok if it finishes well :)");

        CustomConsole.WriteLine();
        CustomConsole.WriteLineWarning("Warning: But I have to warn you about the consequences of something not being done correctly.");

        CustomConsole.WriteLine();
        CustomConsole.WriteLineError("Error: If some error occurred and the application will crush with an exception, I will display it on the screen immediately.");

        try
        {
            throw new Exception("Some demo exception occurred.");
        }
        catch (Exception ex)
        {
            CustomConsole.WriteLine();
            CustomConsole.WriteLineError("Exception:");
            CustomConsole.WriteLineError(ex);
        }
    }
}