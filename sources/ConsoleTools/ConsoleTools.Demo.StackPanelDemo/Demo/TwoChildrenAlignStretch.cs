using System;
using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Demo.Utils;

namespace DustInTheWind.ConsoleTools.Demo.StackPanelDemo.Demo;

internal class TwoChildrenAlignStretch : DemoBase
{
    public override string Title => "Two Child Controls - Align Stretch";

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
                    HorizontalAlignment = HorizontalAlignment.Stretch
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