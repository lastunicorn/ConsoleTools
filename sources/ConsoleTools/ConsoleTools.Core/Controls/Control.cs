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

// --------------------------------------------------------------------------------
// Bugs or feature requests
// --------------------------------------------------------------------------------
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new/choose

using System;
using DustInTheWind.ConsoleTools.Controls.Rendering;

namespace DustInTheWind.ConsoleTools.Controls;

/// <summary>
/// Provides base functionality for a control.
/// </summary>
public abstract class Control : IRenderable
{
    /// <summary>
    /// Gets or sets the foreground color used to write the text of the control.
    /// Default value: <c>null</c>
    /// </summary>
    public ConsoleColor? ForegroundColor { get; set; }

    /// <summary>
    /// Gets or sets the background color of the text.
    /// Default value: <c>null</c>
    /// </summary>
    public ConsoleColor? BackgroundColor { get; set; }

    /// <summary>
    /// Event raised at the beginning of the rendering process.
    /// </summary>
    public virtual event EventHandler<BeforeRenderEventArgs> BeforeRender;

    /// <summary>
    /// Event raised at the very end of the rendering process.
    /// </summary>
    public virtual event EventHandler<AfterRenderEventArgs> AfterRender;

    /// <summary>
    /// Displays the control in the console as a root control (not embedded into another parent
    /// control).
    /// </summary>
    public void Display()
    {
        ConsoleDisplay consoleDisplay = new();
        DoRender(consoleDisplay);
        consoleDisplay.Flush();
    }

    /// <summary>
    /// Renders the current instance using the specified <see cref="IDisplay"/>.
    /// Optionally, some rendering options may be provided.
    /// </summary>
    /// <param name="display">The <see cref="IDisplay"/> instance used to render the data.</param>
    /// <param name="renderingOptions">A set of options based on which the renderer is created.</param>
    public void Render(IDisplay display, RenderingOptions renderingOptions = null)
    {
        if (display == null) throw new ArgumentNullException(nameof(display));

        DoRender(display, renderingOptions);
    }

    /// <summary>
    /// Returns the string representation of the current instance.
    /// </summary>
    /// <returns>The string representation of the current instance.</returns>
    public override string ToString()
    {
        StringDisplay stringDisplay = new();
        DoRender(stringDisplay);
        stringDisplay.Flush();

        return stringDisplay.ToString();
    }

    /// <summary>
    /// Returns a renderer object that is able to render the current <see cref="Control"/>
    /// instance using the specified <see cref="IDisplay"/>.
    /// </summary>
    /// <returns>The <see cref="IRenderer"/> instance.</returns>
    public abstract IRenderer GetRenderer(IDisplay display, RenderingOptions renderingOptions = null);

    /// <summary>
    /// Renders the control into the specified <see cref="IDisplay"/>.
    /// The default implementation is doing the display using the <see cref="IRenderer"/> returned
    /// by the <see cref="GetRenderer"/> method.
    /// </summary>
    protected virtual void DoRender(IDisplay display, RenderingOptions renderingOptions = null)
    {
        renderingOptions ??= new RenderingOptions();

        BeforeRenderEventArgs beforeRenderEventArgs = new()
        {
            Display = display,
            RenderingOptions = renderingOptions
        };
        OnBeforeRender(beforeRenderEventArgs);

        try
        {
            IRenderer renderer = GetRenderer(display, renderingOptions);

            while (renderer.HasMoreLines)
                renderer.RenderNextLine();

            display.Flush();
        }
        finally
        {
            AfterRenderEventArgs afterRenderEventArgs = new()
            {
                Display = display
            };
            OnAfterRender(afterRenderEventArgs);
        }
    }

    /// <summary>
    /// Raises the <see cref="BeforeRender"/> event. This method is called at the beginning of the
    /// rendering process.
    /// When overwritten, the base method must be called in order to allow the event to be raised.
    /// </summary>
    protected virtual void OnBeforeRender(BeforeRenderEventArgs e)
    {
        BeforeRender?.Invoke(this, e);
    }

    /// <summary>
    /// Raises the <see cref="AfterRender"/> event. This method is called at the very end of the
    /// rendering process.
    /// When overwritten, the base method must be called in order to allow the event to be raised.
    /// </summary>
    protected virtual void OnAfterRender(AfterRenderEventArgs e)
    {
        AfterRender?.Invoke(this, e);
    }
}