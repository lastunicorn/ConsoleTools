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

namespace DustInTheWind.ConsoleTools.Demo.BorderDemo
{
    internal class DemoPackage : IDemoPackage
    {
        public string ShortDescription => "Border Demo";

        public void ExecuteDemo()
        {
            DisplayApplicationHeader();
            DisplayTextBlocWithBorder();
        }

        private static void DisplayApplicationHeader()
        {
            ApplicationHeader applicationHeader = new ApplicationHeader()
            {
                Appendix = "Border Demo"
            };
            applicationHeader.Display();
        }

        private static void DisplayTextBlocWithBorder()
        {
            TextBlock textBlock = new TextBlock
            {
                Margin = 1,
                Padding = 1,
                BackgroundColor = ConsoleColor.DarkGray,
                Text = new[] { "something", "something", "something" }
            };

            Border border = new Border
            {
                Margin = 1,
                Padding = 1,
                BackgroundColor = ConsoleColor.Gray,
                Control = textBlock
            };

            border.Display();
        }
    }
}