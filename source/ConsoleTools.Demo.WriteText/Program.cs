// ConsoleTools
// Copyright (C) 2017 Dust in the Wind
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

namespace DustInTheWind.ConsoleTools.Demo.WriteText
{
    internal static class Program
    {
        private static void Main()
        {
            DisplayApplicationHeader();

            try
            {
                CustomConsole.WriteLine("Normal: This is a normal line of text.");
                CustomConsole.WriteLine();
                CustomConsole.WriteLineEmphasies("Emphasies: But I can also write an emphasized text.");
                CustomConsole.WriteLine();
                CustomConsole.WriteLineSuccess("Success: And everything is ok if it finishes well :)");
                CustomConsole.WriteLine();
                CustomConsole.WriteLineWarning("Warning: But I have to warn you about the consequences of something not being done correctly.");
                CustomConsole.WriteLine();
                CustomConsole.WriteLineError("Error: If some error occures and the application will crush with an exception, I will display it on the screen immediately.");

                throw new Exception("Some demo exception occured.");
            }
            catch (Exception ex)
            {
                CustomConsole.WriteLine();
                CustomConsole.WriteLineError(ex);
            }
            finally
            {
                CustomConsole.WriteLine();
                CustomConsole.WriteLine("Did you like our demo? :)");
                Pause.DisplayDefault();
            }
        }

        private static void DisplayApplicationHeader()
        {
            CustomConsole.WriteLineEmphasies("ConsoleTools Demo - Write Normal/Emphasized/Warning/Error");
            CustomConsole.WriteLineEmphasies("===============================================================================");
            CustomConsole.WriteLine();
        }
    }
}
