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
using DustInTheWind.ConsoleTools.Controls.Tables.Printers;

namespace DustInTheWind.ConsoleTools.Controls;

/// <summary>
/// Provides base functionality for a block control like top and bottom margins, paddings, etc.
/// A block control does not accept other controls on the same horizontal space.
/// It also force the rendering to start from the beginning of the next line if the cursor is
/// in the middle of a line.
/// </summary>
public abstract partial class BlockControl : Control
{
    ///// <summary>
    ///// Gets an instance that represents the display available for the control to write on.
    ///// It also provides helper methods to write partial or entire rows.
    ///// </summary>
    //protected IDisplay ControlDisplay { get; private set; }

    ///// <summary>
    ///// Gets the calculated layout for the current instance.
    ///// This value is calculated at the beginning of the display process and it is available throughout
    ///// the entire display process.
    ///// Before and after the display has unknown value.
    ///// </summary>
    //protected ControlLayout Layout { get; private set; }

    /// <summary>
    /// Gets or sets a value that specifies who should be considered the parent if none is specified.
    /// This is useful when calculating the alignment.
    /// Default value: ConsoleWindow
    /// </summary>
    public DefaultParent DefaultParent { get; set; } = DefaultParent.ConsoleWindow;

    /// <summary>
    /// Displays the margins and the content of the control.
    /// It also ensures that the control is displayed starting from a new line.
    /// </summary>
    protected override void DoDisplay()
    {
        //MoveToNextLineIfNecessary();

        ControlLayout controlLayout = CalculateLayout();
        IDisplay display = CreateControlDisplay(controlLayout);

        //WriteTopMargin();
        //WriteTopPadding();

        DoDisplayContent(display);

        //WriteBottomPadding();
        //WriteBottomMargin();
    }

    private static void MoveToNextLineIfNecessary()
    {
        if (Console.CursorLeft != 0)
            Console.WriteLine();
    }

    private ControlLayout CalculateLayout()
    {
        ControlLayout layout = new()
        {
            Control = this,
            AvailableWidth = AvailableWidth,
            DesiredContentWidth = DesiredContentWidth
        };

        layout.Calculate();

        return layout;
    }

    private IDisplay CreateControlDisplay(ControlLayout controlLayout)
    {
        ConsoleDisplay controlDisplay = new()
        {
            Layout = controlLayout
        };

        if (ForegroundColor.HasValue)
            controlDisplay.ForegroundColor = ForegroundColor.Value;

        if (BackgroundColor.HasValue)
            controlDisplay.BackgroundColor = BackgroundColor.Value;

        return controlDisplay;
    }

    /// <summary>
    /// Displays the control to the Console.
    /// The default implementation is doing the display using the <see cref="IRenderer"/> returned
    /// by the <see cref="Control.GetRenderer"/> method.
    /// </summary>
    protected virtual void DoDisplayContent(IDisplay display, RenderingOptions renderingOptions = null)
    {
        IRenderer renderer = GetRenderer(renderingOptions);

        while (renderer.HasMoreLines)
            renderer.RenderNextLine(display);

        display.Flush();
    }
}