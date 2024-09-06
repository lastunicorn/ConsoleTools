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
using DustInTheWind.ConsoleTools.Controls.Rendering;

namespace DustInTheWind.ConsoleTools.Controls;

/// <summary>
/// Base class for a control that supports input from the user.
/// </summary>
public abstract class InteractiveControl : BlockControl, ICloseSupport
{
    private volatile bool isClosed;
    private bool? originalCursorVisibility;

    /// <summary>
    /// Gets a value that specifies if the control was requested to close.
    /// </summary>
    public bool IsClosed => isClosed;

    /// <summary>
    /// Gets or sets a value that specifies if the cursor is visible while the control is displayed.
    /// Default value: <c>true</c>
    /// </summary>
    public bool? CursorVisibility { get; set; } = true;

    /// <summary>
    /// Gets or sets a value that specifies if the visibility of the cursor should be set back
    /// to the original value (before displaying the control).
    /// Default value: <c>true</c>
    /// </summary>
    protected bool RestoreCursorVisibilityAfterDisplay { get; set; } = true;

    public event EventHandler<AfterInteractiveDisplayEventArgs> AfterInteractiveDisplay;

    /// <summary>
    /// Event raised when the control is requested to close itself.
    /// </summary>
    public event EventHandler CloseRequested;

    /// <summary>
    /// Method called before the control is rendered.
    /// </summary>
    protected override void OnBeforeRender(BeforeRenderEventArgs e)
    {
        if (CursorVisibility.HasValue)
        {
            originalCursorVisibility = e.Display.IsCursorVisible;
            e.Display.IsCursorVisible = CursorVisibility.Value;
        }
        else
        {
            originalCursorVisibility = null;
        }

        base.OnBeforeRender(e);
    }

    /// <summary>
    /// Raises the <see cref="Control.AfterRender"/> event. This method is called at the very end of the
    /// rendering process.
    /// </summary>
    protected override void OnAfterRender(AfterRenderEventArgs e)
    {
        base.OnAfterRender(e);

        if (originalCursorVisibility.HasValue && RestoreCursorVisibilityAfterDisplay)
            e.Display.IsCursorVisible = originalCursorVisibility.Value;
    }

    /// <summary>
    /// Call this method to announce the control that it should end its process.
    /// This method does not force the control to close.
    /// </summary>
    public void RequestClose()
    {
        isClosed = true;
        OnCloseRequested();
    }

    /// <summary>
    /// Resets the "closed" state of the control and allows it to be rendered again.
    /// </summary>
    protected void ResetClosed()
    {
        isClosed = false;
    }

    /// <summary>
    /// Raises the <see cref="CloseRequested"/> event.
    /// </summary>
    protected virtual void OnCloseRequested()
    {
        CloseRequested?.Invoke(this, EventArgs.Empty);
    }
}