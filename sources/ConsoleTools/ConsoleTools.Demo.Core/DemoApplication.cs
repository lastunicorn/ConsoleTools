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
using System.Collections.Generic;
using System.Reflection;
using DustInTheWind.ConsoleTools.Controls;

namespace DustInTheWind.ConsoleTools.Demo.Core;

public class DemoApplication
{
    private static ControlRepeater controlRepeater;
    private readonly DemoPackages demoPackages = new();

    public void AddPackagesFrom(IEnumerable<Assembly> assemblies)
    {
        demoPackages.LoadFrom(assemblies);
    }

    public void Run()
    {
        //new StackPanel
        //{
        //    Children =
        //    {
        //        new TextBlock("This is a text")
        //        {
        //            BackgroundColor = ConsoleColor.Blue,
        //            ForegroundColor = ConsoleColor.DarkBlue,
        //            Padding = 1,
        //            Margin = 1,
        //            HorizontalAlignment = HorizontalAlignment.Left,
        //            TextHorizontalAlignment = HorizontalAlignment.Right
        //        },
        //        new TextBlock("This is a text")
        //        {
        //            BackgroundColor = ConsoleColor.Green,
        //            ForegroundColor = ConsoleColor.DarkGreen,
        //            Padding = "2 1",
        //            //Margin = "1 1"
        //        },

        //        new ApplicationHeader
        //        {
        //            Title = "Console Tools Demo",
        //            BackgroundColor = ConsoleColor.Blue,
        //            ForegroundColor = ConsoleColor.DarkBlue,
        //            //Padding = 1,
        //            //Margin = 0
        //        }
        //    },
        //    BackgroundColor = ConsoleColor.DarkCyan,
        //    Padding = new Thickness(1, 2, 3, 4),
        //    Margin = "1"
        //}.Display();

        //new ControlRepeater
        //    {
        //        Content = new StackPanel
        //        {
        //            Children =
        //            {
        //                new TextBlock("This is a text")
        //                {
        //                    BackgroundColor = ConsoleColor.Blue,
        //                    ForegroundColor = ConsoleColor.DarkBlue,
        //                    Padding = 1
        //                    //Margin = 1
        //                },
        //                new TextBlock("This is a text")
        //                {
        //                    BackgroundColor = ConsoleColor.Green,
        //                    ForegroundColor = ConsoleColor.DarkGreen,
        //                    Padding = 1
        //                    //Margin = "1 0"
        //                }

        //                //new ApplicationHeader
        //                //{
        //                //    Title = "Console Tools Demo",
        //                //    BackgroundColor = ConsoleColor.Blue,
        //                //    ForegroundColor = ConsoleColor.DarkBlue,
        //                //    Padding = 1,
        //                //    Margin = 1
        //                //}
        //            },
        //            BackgroundColor = ConsoleColor.DarkCyan,
        //            //Padding = 1,
        //            Margin = 1
        //        },
        //        RepeatCount = 3,
        //        Margin = 1
        //    }
        //    .Display();

        controlRepeater = new ControlRepeater
        {
            Content = new StackPanel
            {
                Children =
                {
                    new ApplicationHeader
                    {
                        Title = "Console Tools Demo"
                    },
                    new MainMenu(demoPackages, this)
                }
            },
            RepeatCount = -1
        };

        controlRepeater.Display();
    }

    public void RequestExit()
    {
        controlRepeater.RequestClose();
    }
}