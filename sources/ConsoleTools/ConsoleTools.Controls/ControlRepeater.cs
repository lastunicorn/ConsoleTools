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
/// Displays a control repeatedly until the <see cref="RequestClose"/> method is called.
/// </summary>
public class ControlRepeater : BlockControl, ICloseSupport
{
    private volatile bool isClosed;
    private volatile bool allowClose = true;
    private Control content;

    /// <summary>
    /// Gets or sets a value that specifies if the control is rendered as a root control.
    /// A root control is not embedded in another parent control.
    /// </summary>
    public bool IsRootControl { get; set; } = true;

    /// <summary>
    /// Gets the natural width of the control.
    /// The natural width is the width of the control when no external constraints are applied.
    /// </summary>
    protected override int NaturalContentWidth => Content?.CalculateNaturalWidth() ?? 0;

    /// <summary>
    /// Gets or sets the control that is to be displayed repeatedly.
    /// </summary>
    public Control Content
    {
        get => content;
        set
        {
            if (content is ICloseSupport contentWithCloseSupport1)
                contentWithCloseSupport1.CloseStateChanged -= HandleContentCloseStateChanged;

            content = value;

            if (content is ICloseSupport contentWithCloseSupport2)
                contentWithCloseSupport2.CloseStateChanged += HandleContentCloseStateChanged;
        }
    }

    private void HandleContentCloseStateChanged(object sender, EventArgs e)
    {
        if (!allowClose)
            return;

        if (sender is not ICloseSupport contentWithCloseSupport)
            return;

        isClosed = contentWithCloseSupport.IsClosed;
        OnCloseStateChanged();
    }

    /// <summary>
    /// Gets a value that specifies if the control was requested to close.
    /// </summary>
    public bool IsClosed => isClosed;

    /// <summary>
    /// Gets or sets the number of times that the content should be repeated.
    /// A negative value means the content is repeated an infinite number of times.
    /// Zero will not render the content at all.
    /// Default value = 0
    /// </summary>
    public int RepeatCount { get; set; }

    /// <summary>
    /// Event raised when the control is requested to close itself.
    /// If the <see cref="Content"/> has <see cref="ICloseSupport"/> it is also requested to close.
    /// The repeat loop is then interrupted and the rendering process stopped.
    /// </summary>
    public event EventHandler CloseStateChanged;

    /// <summary>
    /// Returns a renderer object that is able to render the current <see cref="ControlRepeater"/>
    /// instance using the specified <see cref="IDisplay"/>.
    /// </summary>
    /// <returns>The <see cref="IRenderer"/> instance.</returns>
    public override IRenderer GetRenderer(IDisplay display, RenderingOptions renderingOptions = null)
    {
        return new ControlRepeaterRenderer(this, display, renderingOptions);
    }

    /// <summary>
    /// Call this method to announce the control that it should end its process.
    /// This method does not force the control to close.
    /// Instead it will wait for the current iteration of the <see cref="Content"/> to end its
    /// rendering than also ends its own repeat loop.
    /// If the <see cref="Content"/> has <see cref="ICloseSupport"/> it is also requested to close,
    /// to speed up the close process.
    /// </summary>
    public void RequestClose()
    {
        isClosed = true;

        if (Content is ICloseSupport contentWithCloseSupport)
        {
            allowClose = false;

            try
            {
                contentWithCloseSupport.RequestClose();
            }
            finally
            {
                allowClose = true;
            }
        }

        OnCloseStateChanged();
    }

    /// <summary>
    /// Resets the "closed" state of the control and allows it to be rendered again.
    /// </summary>
    public void ResetClose()
    {
        isClosed = false;

        if (Content is ICloseSupport contentWithCloseSupport)
        {
            allowClose = false;

            try
            {
                contentWithCloseSupport.ResetClose();
            }
            finally
            {
                allowClose = true;
            }
        }

        OnCloseStateChanged();
    }

    /// <summary>
    /// Raises the <see cref="CloseStateChanged"/> event.
    /// </summary>
    protected virtual void OnCloseStateChanged()
    {
        CloseStateChanged?.Invoke(this, EventArgs.Empty);
    }
}