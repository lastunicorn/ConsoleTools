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

using System;
using DustInTheWind.ConsoleTools.Controls;

namespace DustInTheWind.ConsoleTools.Demo.TabularData
{
    internal class Program
    {
        private static void Main()
        {
            try
            {
                DisplayApplicationHeader();

                DemoApplication demoApplication = new DemoApplication();
                demoApplication.Run();
            }
            catch (Exception ex)
            {
                CustomConsole.WriteError(ex);
                Pause.QuickDisplay();
            }
        }

        private static void DisplayApplicationHeader()
        {
            ApplicationHeader applicationHeader = new ApplicationHeader()
            {
                Appendix = "DataGrid Demo",
                Description = "This demo shows how to display data in tables."
            };
            applicationHeader.Display();

            //CustomConsole.WriteLineEmphasized("ConsoleTools Demo - TabularData");
            //CustomConsole.WriteLineEmphasized(new string('=', 79));
            //CustomConsole.WriteLine();
            //CustomConsole.WriteLine("This demo shows how to display data in tables.");
            //CustomConsole.WriteLine();
        }
    }
}