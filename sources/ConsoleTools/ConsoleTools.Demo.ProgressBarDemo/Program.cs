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
using DustInTheWind.ConsoleTools.Demo.NetCore;
using DustInTheWind.ConsoleTools.Demo.ProgressBarkDemo.NetCore.PresentationLayer;

namespace DustInTheWind.ConsoleTools.Demo.ProgressBarkDemo.NetCore
{
    /// <summary>
    /// This is an example of a asynchronous job that is running in the background.
    /// Its progress is displayed in the Console using a progress bar.
    /// </summary>
    internal class Program : IDemoPackage
    {
        public string ShortDescription => "ProgressBar Demo";


        public void ExecuteDemo()
        {
            DisplayApplicationHeader();

            // The most important class is DataProcessingView.
            // It will demonstrate the usage of the ProgressBar class.

            DataProcessingView view = new DataProcessingView();
            view.Show();

            Pause.QuickDisplay();
        }

        private static void DisplayApplicationHeader()
        {
            ApplicationHeader applicationHeader = new ApplicationHeader()
            {
                Appendix = "ProgressBar Demo",
                Description = "This demo shows the usage of the ProgressBar control."
            };
            applicationHeader.Display();
        }
    }
}