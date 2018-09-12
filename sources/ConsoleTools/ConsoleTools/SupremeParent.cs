// ConsoleTools
// Copyright (C) 2017-2018 Dust in the Wind
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
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new

namespace DustInTheWind.ConsoleTools
{
    public class SupremeParent : BlockControl
    {
        ///// <summary>
        ///// Gets or sets a value that specifies who should be considered the parent if none is specified.
        ///// This is useful for calculating the alignment for example.
        ///// </summary>
        //public DefaultParent DefaultParent { get; set; } = DefaultParent.ConsoleWindow;

        ///// <summary>
        ///// Gets or sets the amount of space that should be empty outside the control.
        ///// </summary>
        //public Thickness Margin { get; set; }

        ///// <summary>
        ///// Gets or sets the amount of space between the content and the margin of the control.
        ///// </summary>
        //public Thickness Padding { get; set; }

        ///// <summary>
        ///// Gets the width available for the control to render itself.
        ///// </summary>
        ///// <remarks>
        ///// The parent's control is deciding this space.
        ///// </remarks>
        //public int AvailableWidth
        //{
        //    get
        //    {
        //        switch (DefaultParent)
        //        {
        //            case DefaultParent.ConsoleBuffer:
        //                return Console.BufferWidth;

        //            case DefaultParent.ConsoleWindow:
        //                return Console.WindowWidth;

        //            default:
        //                throw new ArgumentOutOfRangeException();
        //        }
        //    }
        //}

        ///// <summary>
        ///// Gets the actual width of the control including the left and right margins.
        ///// </summary>
        ///// <remarks>
        ///// This value is equal to the available width if the control is stretched.
        ///// </remarks>
        //public int ActualFullWidth => AvailableWidth;

        ///// <summary>
        ///// Gets the actual width of the control without the left and right margins.
        ///// </summary>
        //public int ActualWidth => ActualFullWidth - Margin.Left - Margin.Right;

        ///// <summary>
        ///// Gets the actual width of the client area (without the left and right paddings).
        ///// </summary>
        //public int ActualClientWidth => ActualWidth - Padding.Left - Padding.Right;

        private SupremeParent()
        {
        }

        protected override void DoDisplayContent()
        {
        }

        public static SupremeParent ConsoleWindow { get; } = new SupremeParent
        {
            DefaultParent = DefaultParent.ConsoleWindow
        };

        public static SupremeParent ConsoleBuffer { get; } = new SupremeParent
        {
            DefaultParent = DefaultParent.ConsoleBuffer
        };
    }
}