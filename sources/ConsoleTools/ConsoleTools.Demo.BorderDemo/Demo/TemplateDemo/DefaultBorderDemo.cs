using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Demo.Core;

namespace DustInTheWind.ConsoleTools.Demo.BorderDemo.Demo.TemplateDemo;

internal class DefaultBorderDemo : DemoBase
{
    public override string Title => "Default Border";

    protected override void DoExecute()
    {
        Border border = new()
        {
            Content = new TextBlock
            {
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