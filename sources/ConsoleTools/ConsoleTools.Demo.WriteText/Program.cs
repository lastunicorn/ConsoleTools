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

namespace DustInTheWind.ConsoleTools.Demo.WriteText
{
    internal static class Program
    {
        private static void Main()
        {
            DisplayApplicationHeader();

            Console.SetWindowSize(80, 24);
            Console.SetBufferSize(80, 1024);

            try
            {
                WriteTitle("Colors Demo");
                RunColorExample();
                Pause.QuickDisplay();

                WriteTitle("Alignment Demo");
                RunAlignmentExample();
            }

            finally
            {
                Pause.QuickDisplay();
            }
        }

        private static void DisplayApplicationHeader()
        {
            CustomConsole.WriteLineEmphasized("ConsoleTools Demo - Write Normal/Emphasized/Warning/Error");
            CustomConsole.WriteLineEmphasized("===============================================================================");
            CustomConsole.WriteLine();
        }

        private static void WriteTitle(string text)
        {
            CustomConsole.WriteLine();
            CustomConsole.WriteLine("-------------------------------------------------------------------------------");
            CustomConsole.WriteLine(text);
            CustomConsole.WriteLine("-------------------------------------------------------------------------------");
            CustomConsole.WriteLine();
        }

        private static void RunColorExample()
        {
            try
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

                throw new Exception("Some demo exception occurred.");
            }
            catch (Exception ex)
            {
                CustomConsole.WriteLine();
                CustomConsole.WriteLineError(ex);
            }
        }

        private static void RunAlignmentExample()
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
}