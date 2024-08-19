using System;

namespace DustInTheWind.ConsoleTools.Controls;

internal class TopMarginRenderer : IRenderer
{
    private readonly ControlLayout controlLayout;
    private int actualCount;

    public bool HasMoreLines => actualCount < controlLayout.Margin.Top;

    public TopMarginRenderer(ControlLayout controlLayout)
    {
        this.controlLayout = controlLayout ?? throw new ArgumentNullException(nameof(controlLayout));
    }

    public void RenderNextLine(IDisplay display)
    {
        if (actualCount >= controlLayout.Margin.Top)
            return;

        display.WriteLine();

        actualCount++;
    }
}