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

namespace DustInTheWind.ConsoleTools.Demo.Core;

public abstract class DemoBase : IDemo
{
    public abstract string Title { get; }

    public virtual MultilineText Description { get; }

    public void Execute()
    {
        DisplayDemoTitle();
        DoExecute();
    }

    private void DisplayDemoTitle()
    {
        StackPanel stackPanel = new()
        {
            Margin = (0, 2)
        };

        stackPanel.Children.Add(new TextBlock(new string('-', 79))
        {
            ForegroundColor = ConsoleColor.DarkYellow
        });

        stackPanel.Children.Add(new TextBlock
        {
            Text = $"{Title}",
            ForegroundColor = ConsoleColor.DarkYellow
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

        stackPanel.Children.Add(new TextBlock(new string('-', 79))
        {
            ForegroundColor = ConsoleColor.DarkYellow
        });

        stackPanel.Display();
    }

    protected abstract void DoExecute();
}