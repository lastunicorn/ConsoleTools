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

using System;

namespace DustInTheWind.ConsoleTools.Controls;

public abstract class InteractiveControl : BlockControl
{
    private bool? originalCursorVisibility;

    /// <summary>
    /// Gets or sets a value that specifies if the cursor is visible while the control is displayed.
    /// Default value: <c>true</c>
    /// </summary>
    public bool? CursorVisibility { get; set; }

    /// <summary>
    /// Gets or sets a value that specifies if the visibility of the cursor should be set back
    /// to the value it was before displaying the control.
    /// </summary>
    protected bool RestoreCursorVisibilityAfterDisplay { get; set; } = true;

    public event EventHandler<AfterInteractiveDisplayEventArgs> AfterInteractiveDisplay;

    protected override void OnBeforeDisplay(BeforeDisplayEventArgs e)
    {
        if (CursorVisibility.HasValue)
        {
            originalCursorVisibility = Console.CursorVisible;
            Console.CursorVisible = CursorVisibility.Value;
        }
        else
        {
            originalCursorVisibility = null;
        }

        base.OnBeforeDisplay(e);
    }

    protected override void OnAfterDisplay()
    {
        base.OnAfterDisplay();

        if (originalCursorVisibility.HasValue && RestoreCursorVisibilityAfterDisplay)
            Console.CursorVisible = originalCursorVisibility.Value;
    }

    /// <summary>
    /// Displays the control to the Console.
    /// The default implementation is doing the display using the <see cref="IRenderer"/> returned
    /// by the <see cref="Control.GetRenderer"/> method.
    /// </summary>
    protected override void DoRender(IDisplay display, RenderingOptions renderingOptions = null)
    {
        if (display is IInteractiveDisplay interactiveDisplay)
            DoRender(interactiveDisplay);

        throw new ArgumentException(nameof(display), $"The display must be of type {typeof(IInteractiveDisplay).FullName}");
    }

    /// <summary>
    /// Displays the control to the Console.
    /// The default implementation is doing the display using the <see cref="IRenderer"/> returned
    /// by the <see cref="Control.GetRenderer"/> method.
    /// </summary>
    protected virtual void DoRender(IInteractiveDisplay display, RenderingOptions renderingOptions = null)
    {
        IRenderer renderer = GetRenderer(display, renderingOptions);

        while (renderer.HasMoreLines)
            renderer.RenderNextLine();

        display.Flush();

        AfterInteractiveDisplayEventArgs afterInteractiveDisplayEventArgs = new()
        {
            Renderer = renderer
        };
        OnAfterInteractiveDisplay(afterInteractiveDisplayEventArgs);
    }

    protected virtual void OnAfterInteractiveDisplay(AfterInteractiveDisplayEventArgs e)
    {
        AfterInteractiveDisplay?.Invoke(this, e);
    }
}