// ConsoleTools
// Copyright (C) 2017-2018 Dust in the Wind
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

            Console.SetBufferSize(80, 1024);

            Pause pause = new Pause();

            CustomConsole.WriteLine("some text");
            CustomConsole.WriteLine("some text");
            CustomConsole.WriteLine("some text");

            pause.EraseTextAfterUnlock = false;
            pause.Display();

            CustomConsole.WriteLine("some text");
            CustomConsole.WriteLine("some text");
            CustomConsole.WriteLine("some text");

            pause.EraseTextAfterUnlock = true;
            pause.Display();

            CustomConsole.WriteLine("some text");
            CustomConsole.WriteLine("some text");
            CustomConsole.WriteLine("some text");

            try
            {
                CustomConsole.WriteLine();
                CustomConsole.WriteLine("-------------------------------------------------------------------------------");
                CustomConsole.WriteLine("Colors Demo");
                CustomConsole.WriteLine("-------------------------------------------------------------------------------");
                CustomConsole.WriteLine();

                RunColorExample();
                Pause.QuickPause();

                CustomConsole.WriteLine();
                CustomConsole.WriteLine("-------------------------------------------------------------------------------");
                CustomConsole.WriteLine("Alignment Demo");
                CustomConsole.WriteLine("-------------------------------------------------------------------------------");
                CustomConsole.WriteLine();


                RunAlignmentExample();
            }

            finally
            {
                Pause.QuickPause();
            }
        }

        private static void RunAlignmentExample()
        {
            CustomConsole.WriteLine(HorizontalAlignment.Left, "This is a text aligned to left.");
            CustomConsole.WriteLine(HorizontalAlignment.Left, "This is anoter text aligned to left.");
            CustomConsole.WriteLine();
            CustomConsole.WriteLine(HorizontalAlignment.Center, "This is a text aligned to center.");
            CustomConsole.WriteLine(HorizontalAlignment.Center, "This is another text aligned to center.");
            CustomConsole.WriteLine();
            CustomConsole.WriteLine(HorizontalAlignment.Right, "This is a text aligned to right.");
            CustomConsole.WriteLine(HorizontalAlignment.Right, "This is another text aligned to right.");
        }

        private static void RunColorExample()
        {
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
        }

        private static void DisplayApplicationHeader()
        {
            CustomConsole.WriteLineEmphasies("ConsoleTools Demo - Write Normal/Emphasized/Warning/Error");
            CustomConsole.WriteLineEmphasies("===============================================================================");
            CustomConsole.WriteLine();
        }
    }
}
