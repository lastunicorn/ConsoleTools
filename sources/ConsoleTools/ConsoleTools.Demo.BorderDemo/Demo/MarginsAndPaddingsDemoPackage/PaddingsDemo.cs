using System;
using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Demo.Core;

namespace DustInTheWind.ConsoleTools.Demo.BorderDemo.Demo.MarginsAndPaddingsDemoPackage;

internal class PaddingsDemo : DemoBase
{
    public override string Title => "Paddings (1, 1, 1, 1)";

    protected override void DoExecute()
    {
        Border border = new()
        {
            Padding = 1,
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