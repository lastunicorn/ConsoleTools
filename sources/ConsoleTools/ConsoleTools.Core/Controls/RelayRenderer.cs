using System;

namespace DustInTheWind.ConsoleTools.Controls;

public class RelayRenderer : IRenderer
{
    public bool HasMoreLines { get; private set; }

    public Func<IDisplay, bool> RenderNextLineAction { get; set; }

    public RelayRenderer(Func<bool> initializeAction)
    {
        bool? hasMoreLines = initializeAction?.Invoke();

        HasMoreLines = hasMoreLines ?? false;
    }

    public void RenderNextLine(IDisplay display)
    {
        bool? hasMoreLines = RenderNextLineAction?.Invoke(display);

        HasMoreLines = hasMoreLines ?? false;
    }
}