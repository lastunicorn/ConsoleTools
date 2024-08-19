using System;

namespace DustInTheWind.ConsoleTools.Controls;

internal class BottomMarginRenderer : IRenderer
{
    private readonly ControlLayout controlLayout;
    private int actualCount;

    public bool HasMoreLines => actualCount < controlLayout.Margin.Bottom;

    public BottomMarginRenderer(ControlLayout controlLayout)
    {
        this.controlLayout = controlLayout ?? throw new ArgumentNullException(nameof(controlLayout));
    }

    public void RenderNextLine(IDisplay display)
    {
        if (actualCount >= controlLayout.Margin.Bottom)
            return;

        display.WriteLine();

        actualCount++;
    }
}