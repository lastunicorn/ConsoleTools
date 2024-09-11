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
using System.Linq;
using DustInTheWind.ConsoleTools.Controls.Rendering;

namespace DustInTheWind.ConsoleTools.Controls;

/// <summary>
/// Displays a list of controls in a vertical stack, one after the other.
/// </summary>
public class StackPanel : BlockControl, ICloseSupport
{
    private bool isClosed;
    private bool suppressCloseEvent;

    /// <summary>
    /// Gets the list of children to be displayed.
    /// </summary>
    public BlockControlCollection Children { get; } = new();

    public bool IsClosed => isClosed || Children.IsClosed;

    /// <summary>
    /// Gets the width of the largest child control's content calculated when there are no other
    /// space restrictions applied to it.
    /// </summary>
    protected override int NaturalContentWidth => Children
        .Select(x => x.CalculateNaturalWidth())
        .Max();

    public event EventHandler CloseStateChanged;

    public StackPanel()
    {
        Children.CloseStateChanged += HandleCloseStateChanged;
    }

    private void HandleCloseStateChanged(object sender, EventArgs e)
    {
        if (suppressCloseEvent)
            return;

        if (isClosed)
            return;

        OnCloseStateChanged();
    }

    /// <summary>
    /// Returns a renderer object that is able to render the current <see cref="StackPanel"/>
    /// instance using the specified <see cref="IDisplay"/>.
    /// </summary>
    /// <returns>The <see cref="IRenderer"/> instance.</returns>
    public override IRenderer GetRenderer(IDisplay display, RenderingOptions renderingOptions = null)
    {
        return new StackPanelRenderer(this, display, renderingOptions);
    }

    public void RequestClose()
    {
        bool initialCloseState = IsClosed;
        suppressCloseEvent = true;

        try
        {
            isClosed = true;
            Children.RequestClose();
        }
        finally
        {
            suppressCloseEvent = false;

            if (IsClosed != initialCloseState)
                OnCloseStateChanged();
        }
    }

    public void ResetClose()
    {
        bool initialCloseState = IsClosed;
        suppressCloseEvent = true;

        try
        {
            isClosed = false;
            Children.ResetClose();
        }
        finally
        {
            suppressCloseEvent = false;

            if (IsClosed != initialCloseState)
                OnCloseStateChanged();
        }
    }

    protected virtual void OnCloseStateChanged()
    {
        CloseStateChanged?.Invoke(this, EventArgs.Empty);
    }
}