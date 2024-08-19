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

namespace DustInTheWind.ConsoleTools.Controls;

partial class BlockControl
{
    /// <summary>
    /// Gets or sets the amount of space that should be empty outside the control.
    /// </summary>
    public Thickness Margin { get; set; }

    /// <summary>
    /// Gets or sets the amount of space between the content and the margin of the control.
    /// </summary>
    public Thickness Padding { get; set; }

    /// <summary>
    /// Event raised immediately before writing the top margin.
    /// </summary>
    public event EventHandler BeforeTopMargin;

    /// <summary>
    /// Event raised immediately after writing the bottom margin.
    /// </summary>
    public event EventHandler AfterBottomMargin;

    private void WriteTopMargin(ControlLayout layout)
    {
        OnBeforeTopMargin();

        if (layout.Margin.Top <= 0)
            return;

        for (int i = 0; i < layout.Margin.Top; i++)
            CustomConsole.WriteLine();
    }

    private void WriteBottomMargin(ControlLayout layout)
    {
        if (layout.Margin.Bottom > 0)
        {
            for (int i = 0; i < layout.Margin.Bottom; i++)
                CustomConsole.WriteLine();
        }

        OnAfterBottomMargin();
    }

    private void WriteTopPadding(IDisplay display, ControlLayout layout)
    {
        if (layout.Padding.Top <= 0)
            return;

        string text = new(' ', layout.ContentSize.Width);

        for (int i = 0; i < layout.Padding.Top; i++)
            display.WriteLine(text);
    }

    private void WriteBottomPadding(IDisplay display, ControlLayout layout)
    {
        if (layout.Padding.Bottom <= 0)
            return;

        string text = new(' ', layout.ContentSize.Width);

        for (int i = 0; i < layout.Padding.Bottom; i++)
            display.WriteLine(text);
    }

    /// <summary>
    /// Method called immediately before writing the top margin.
    /// </summary>
    protected virtual void OnBeforeTopMargin()
    {
        BeforeTopMargin?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Method called immediately after writing the bottom margin.
    /// </summary>
    protected virtual void OnAfterBottomMargin()
    {
        AfterBottomMargin?.Invoke(this, EventArgs.Empty);
    }
}