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

using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Demo.Core;

namespace DustInTheWind.ConsoleTools.Demo.HorizontalLineDemo
{
    public class DemoPackage : IDemoPackage
    {
        private ControlRepeater menuRepeater;

        public string Name => "HorizontalLine Demo";

        public void ExecuteDemo()
        {
            DisplayApplicationHeader();

            menuRepeater = new ControlRepeater
            {
                Control = new MainMenu(this)
            };

            menuRepeater.Display();
        }

        private static void DisplayApplicationHeader()
        {
            ApplicationHeader applicationHeader = new ApplicationHeader
            {
                Appendix = "YesNoQuestion Demo"
            };
            applicationHeader.Display();
        }

        public void RequestStop()
        {
            menuRepeater.RequestClose();
        }
    }
}