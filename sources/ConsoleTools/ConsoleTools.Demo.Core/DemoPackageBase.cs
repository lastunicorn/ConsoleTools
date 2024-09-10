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
using System.Linq;
using DustInTheWind.ConsoleTools.Controls;

namespace DustInTheWind.ConsoleTools.Demo.Utils;

public abstract class DemoPackageBase : IDemo
{
    public abstract string Title { get; }

    public virtual MultilineText Description => null;

    public List<IDemo> Demos { get; } = new();

    public bool HasSubPackages => Demos.Any(x => x is DemoPackageBase);

    public bool ForceDisplayMenu { get; set; }

    public void Execute()
    {
        if (HasSubPackages || ForceDisplayMenu)
            DisplayMenu();
        else
            RunAllDemos();
    }

    private void DisplayMenu()
    {
        ControlRepeater controlRepeater = new()
        {
            Content = new StackPanel
            {
                Children =
                {
                    new TextBlock(Description)
                    {
                        IsVisible = !string.IsNullOrEmpty(Description),
                        Margin = (0, 1),
                        ForegroundColor = ConsoleColor.DarkGray
                    },
                    new PackageMenu(this)
                }
            },
            RepeatCount = -1,
            Margin = (0, 2, 0, 0)
        };

        controlRepeater.Display();
    }

    private void RunAllDemos()
    {
        DisplayDemoTitle();

        IEnumerable<IDemo> notNullDemos = Demos.Where(x => x != null);

        foreach (IDemo demo in notNullDemos)
            demo.Execute();
    }

    private void DisplayDescription()
    {
        TextBlock textBlock = new(Description)
        {
            Margin = (0, 2, 0, 0) ,
            ForegroundColor = ConsoleColor.DarkGray
        };

        textBlock.Display();
    }

    private void DisplayDemoTitle()
    {
        StackPanel stackPanel = new()
        {
            Margin = (0, 2, 0, 0)
        };

        stackPanel.Children.Add(new TextBlock(new string('=', 79))
        {
            ForegroundColor = ConsoleColor.Magenta
        });

        stackPanel.Children.Add(new TextBlock
        {
            Text = $"{Title}",
            ForegroundColor = ConsoleColor.Magenta
        });

        if (Description is { IsEmpty: false })
        {
            stackPanel.Children.Add(new TextBlock
            {
                Text = Description,
                ForegroundColor = ConsoleColor.DarkGray,
                Margin = (0, 1, 0, 0),
                MaxWidth = 79
            });
        }

        stackPanel.Children.Add(new TextBlock(new string('=', 79))
        {
            ForegroundColor = ConsoleColor.Magenta
        });

        stackPanel.Display();
    }
}