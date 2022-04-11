﻿// ConsoleTools
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
using DustInTheWind.ConsoleTools.Demo.Core;

namespace DustInTheWind.ConsoleTools.Demo.DataGridDemo
{
    internal class DemoPackage : IDemoPackage
    {
        public string ShortDescription => "DataGrid Demo";
        
        public void ExecuteDemo()
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
        }
    }
}