using DustInTheWind.ConsoleTools.Demo.Utils;

namespace DustInTheWind.ConsoleTools.Demo.BorderDemo.Demo.TemplateDemo;

internal class TemplateDemoPackage : DemoPackageBase
{
    public override string Title => "Border Templates";

    public TemplateDemoPackage()
    {
        Demos.AddRange(new IDemo[]
        {
            new DefaultBorderDemo(),
            new SingleLineBorderDemo(),
            new DoubleLineBorderDemo()
        });
    }
}