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
using DustInTheWind.ConsoleTools.Demo.Utils;

namespace DustInTheWind.ConsoleTools.Demo.StackPanelDemo.Demo.ChildHorizontalAlignment;

internal class TwoChildrenAlignRight : DemoBase
{
    public override string Title => "Two Child Controls - Align Right";

    protected override void DoExecute()
    {
        StackPanel stackPanel = new()
        {
            Children =
            {
                new TextBlock("This is a text")
                {
                    BackgroundColor = ConsoleColor.Blue,
                    ForegroundColor = ConsoleColor.DarkBlue,
                    Padding = 1,
                    Margin = 1,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    TextHorizontalAlignment = HorizontalAlignment.Right
                },
                new TextBlock("This is a different text")
                {
                    BackgroundColor = ConsoleColor.Green,
                    ForegroundColor = ConsoleColor.DarkGreen,
                    Padding = 1,
                    Margin = 1
                }
            }
        };

        stackPanel.Display();
    }
}