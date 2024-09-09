using System;
using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Demo.Utils;

namespace DustInTheWind.ConsoleTools.Demo.BorderDemo.Demo.MarginsAndPaddingsDemoPackage;

internal class MarginsDemo : DemoBase
{
    public override string Title => "Margins (1, 1, 1, 1)";

    protected override void DoExecute()
    {
        Border border = new()
        {
            Margin = 1,
            ForegroundColor = ConsoleColor.DarkBlue,
            BackgroundColor = ConsoleColor.Blue,
            Content = new TextBlock
            {
                ForegroundColor = ConsoleColor.DarkGreen,
                BackgroundColor = ConsoleColor.Green,
                Text = new[]
                {
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    "Sed sollicitudin non enim sit amet interdum.",
                    "Nullam quis nisl a dolor convallis rhoncus at sit amet eros."
                }
            }
        };

        border.Display();
    }
}