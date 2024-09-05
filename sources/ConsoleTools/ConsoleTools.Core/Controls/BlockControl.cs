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

/// <summary>
/// Provides base functionality for a block control like top and bottom margins, paddings, etc.
/// A block control does not accept other controls on the same horizontal space.
/// It also force the rendering to start from the beginning of the next line if the cursor is
/// in the middle of a line.
/// </summary>
public abstract class BlockControl : Control
{
    /// <summary>
    /// Gets or sets a value that specifies who should be considered the parent if none is
    /// specified.
    /// This is useful when calculating the alignment.
    /// Default value: ConsoleWindow
    /// </summary>
    public DefaultParent DefaultParent { get; set; } = DefaultParent.ConsoleWindow;

    /// <summary>
    /// Gets or sets the amount of space that should be empty outside the control.
    /// </summary>
    public Thickness Margin { get; set; }

    /// <summary>
    /// Gets or sets the amount of space between the content and the margin of the control.
    /// </summary>
    public Thickness Padding { get; set; }

    /// <summary>
    /// Gets or sets the width of the control. The margins are not included.
    /// 
    /// If <see cref="MinWidth"/> is set and the <see cref="Width"/> is smaller,
    /// than <see cref="MinWidth"/> is used.
    /// 
    /// If <see cref="MaxWidth"/> is set and the <see cref="Width"/> is larger,
    /// than <see cref="MaxWidth"/> is used.
    /// </summary>
    public int? Width { get; set; }

    /// <summary>
    /// Gets or sets the minimum width allowed for the control, including the paddings.
    /// </summary>
    public int? MinWidth { get; set; }

    /// <summary>
    /// Gets or sets the maximum width allowed for the control, including the paddings.
    /// </summary>
    public int? MaxWidth { get; set; }

    /// <summary>
    /// Gets or sets a value that specifies the horizontal position of the control in respect to
    /// its parent container.
    /// </summary>
    public HorizontalAlignment? HorizontalAlignment { get; set; }

    /// <summary>
    /// Gets the width available for the control to render itself.
    /// </summary>
    /// <remarks>
    /// The parent's control is deciding this space.
    /// </remarks>
    protected int AvailableWidth
    {
        get
        {
            switch (DefaultParent)
            {
                case DefaultParent.ConsoleBuffer:
                    return Console.BufferWidth - 1;

                case DefaultParent.ConsoleWindow:
                    return Console.WindowWidth - 1;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    /// <summary>
    /// When implemented by an inheritor, gets the width of the control's content calculated when
    /// there are no other space restrictions applied to it. Not even the control's
    /// <see cref="Width"/>, <see cref="MinWidth"/> or <see cref="MaxWidth"/> limitations.
    /// </summary>
    public abstract int NaturalContentWidth { get; }

    /// <summary>
    /// Gets the width of the content's natural width including paddings.
    /// </summary>
    public int NaturalWidth => NaturalContentWidth + Padding.Left + Padding.Right;

    /// <summary>
    /// Gets the width of the content's natural width including paddings and margins.
    /// </summary>
    public int NaturalFullWidth => NaturalWidth + Margin.Left + Margin.Right;

    /// <summary>
    /// Calculates the natural width of the content when control's limitations like
    /// <see cref="Width"/>, <see cref="MinWidth"/> and <see cref="MaxWidth"/> are applied.
    /// </summary>
    public int CalculateNaturalContentWidth()
    {
        int contentWidth = Width.HasValue
            ? Width.Value - Padding.Left - Padding.Right
            : NaturalContentWidth;

        if (MinWidth.HasValue)
        {
            int minWidth = MinWidth.Value - Padding.Left - Padding.Right;
            contentWidth = Math.Max(contentWidth, minWidth);
        }

        if (MaxWidth.HasValue)
        {
            int maxWidth = MaxWidth.Value - Padding.Left - Padding.Right;
            contentWidth = Math.Min(contentWidth, maxWidth);
        }

        return contentWidth;
    }

    private static void MoveToNextLineIfNecessary()
    {
        if (Console.CursorLeft != 0)
            Console.WriteLine();
    }

    /// <summary>
    /// Method called before the control is displayed.
    /// </summary>
    protected override void OnBeforeRender(BeforeRenderEventArgs e)
    {
        //e.RenderingOptions.AvailableWidth = AvailableWidth;

        base.OnBeforeRender(e);
    }
}