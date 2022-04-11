// ConsoleTools
// Copyright (C) 2017-2022 Dust in the Wind
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

namespace DustInTheWind.ConsoleTools.Demo.SimpleTextDemo
{
    internal class DemoPackage : IDemoPackage
    {
        private static ControlRepeater menuRepeater;

        public string Name => "Simple Text Demo";

        public void ExecuteDemo()
        {
            DisplayApplicationHeader();

            menuRepeater = new ControlRepeater
            {
                Control = new MainMenu()
            };

            menuRepeater.Display();
        }

        private static void DisplayApplicationHeader()
        {
            ApplicationHeader applicationHeader = new ApplicationHeader()
            {
                Appendix = "Simple Text Demo"
            };
            applicationHeader.Display();
        }

        public static void RequestStop()
        {
            menuRepeater.RequestClose();
        }
    }

    //internal class DemoPackage : IDemoPackage
    //{
    //    public string Name => "Write Simple Text Demo";

    //    public void ExecuteDemo()
    //    {
    //        DisplayApplicationHeader();
    //        DisplayExamples();
    //    }

    //    private static void DisplayExamples()
    //    {
    //        CustomConsole.WriteLine();
    //        CustomConsole.WriteLine("-------------------------------------------------------------------------------");
    //        CustomConsole.WriteLine("Colors Demo");
    //        CustomConsole.WriteLine("-------------------------------------------------------------------------------");
    //        CustomConsole.WriteLine();

    //        RunColorExample();

    //        CustomConsole.WriteLine();
    //        CustomConsole.WriteLine("-------------------------------------------------------------------------------");
    //        CustomConsole.WriteLine("Alignment Demo");
    //        CustomConsole.WriteLine("-------------------------------------------------------------------------------");
    //        CustomConsole.WriteLine();

    //        RunAlignmentExample();
    //    }

    //    private static void RunAlignmentExample()
    //    {
    //        CustomConsole.WriteLine(HorizontalAlignment.Left, "This is a text aligned to left.");
    //        CustomConsole.WriteLine(HorizontalAlignment.Left, "This is another text aligned to left.");
    //        CustomConsole.WriteLine();
    //        CustomConsole.WriteLine(HorizontalAlignment.Center, "This is a text aligned to center.");
    //        CustomConsole.WriteLine(HorizontalAlignment.Center, "This is another text aligned to center.");
    //        CustomConsole.WriteLine();
    //        CustomConsole.WriteLine(HorizontalAlignment.Right, "This is a text aligned to right.");
    //        CustomConsole.WriteLine(HorizontalAlignment.Right, "This is another text aligned to right.");
    //    }

    //    private static void RunColorExample()
    //    {
    //        try
    //        {
    //            CustomConsole.WriteLine("Normal: This is a normal line of text.");
    //            CustomConsole.WriteLine();
    //            CustomConsole.WriteLineEmphasized("Emphasized: But I can also write an emphasized text.");
    //            CustomConsole.WriteLine();
    //            CustomConsole.WriteLineSuccess("Success: And everything is ok if it finishes well :)");
    //            CustomConsole.WriteLine();
    //            CustomConsole.WriteLineWarning("Warning: But I have to warn you about the consequences of something not being done correctly.");
    //            CustomConsole.WriteLine();
    //            CustomConsole.WriteLineError("Error: If some error occurred and the application will crush with an exception, I will display it on the screen immediately.");

    //            throw new Exception("Some demo exception occurred.");
    //        }
    //        catch (Exception ex)
    //        {
    //            CustomConsole.WriteLine();
    //            CustomConsole.WriteLineError(ex);
    //        }
    //    }

    //    private static void DisplayApplicationHeader()
    //    {
    //        ApplicationHeader applicationHeader = new ApplicationHeader()
    //        {
    //            Appendix = "Write Normal/Emphasized/Warning/Error"
    //        };
    //        applicationHeader.Display();
    //    }
    //}
}