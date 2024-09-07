using System;

namespace DustInTheWind.ConsoleTools.Controls.Rendering;

public abstract class InlineRenderer<TControl> : IRenderer
    where TControl : InlineControl
{
    public bool HasMoreLines { get; }

    /// <summary>
    /// Gets the control being rendered.
    /// </summary>
    protected TControl Control { get; }

    protected InlineRenderer(TControl control, IDisplay display, RenderingOptions renderingOptions)
    {
        Control = control ?? throw new ArgumentNullException(nameof(control));
    }

    public void RenderNextLine()
    {
    }

    public void Reset()
    {
    }
}