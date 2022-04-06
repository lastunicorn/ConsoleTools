// ConsoleTools
// Copyright (C) 2017-2022 Dust in the Wind
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

namespace DustInTheWind.ConsoleTools.Controls
{
    /// <summary>
    /// Represents the display available for a control to write on.
    /// It also provides helper methods to write partial or entire rows.
    /// </summary>
    public class ControlDisplay : DisplayBase
    {
        private ConsoleColor? initialForegroundColor;
        private ConsoleColor? initialBackgroundColor;

        public override bool IsCursorVisible
        {
            get => Console.CursorVisible;
            set => Console.CursorVisible = value;
        }

        /// <summary>
        /// Gets or sets a value that specifies who should be considered the parent if none is specified.
        /// This is useful when calculating the alignment.
        /// Default value: ConsoleWindow
        /// </summary>
        public DefaultParent DefaultParent { get; set; } = DefaultParent.ConsoleWindow;

        /// <summary>
        /// Gets the width available for the control to render itself.
        /// </summary>
        /// <remarks>
        /// The parent's control is deciding this space.
        /// </remarks>
        public override int AvailableWidth
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


        protected override void SetRowForegroundColor(ConsoleColor? foregroundColor)
        {
            ConsoleColor? calculatedForegroundColor = foregroundColor ?? ForegroundColor;

            if (calculatedForegroundColor.HasValue)
            {
                initialForegroundColor = Console.ForegroundColor;
                Console.ForegroundColor = calculatedForegroundColor.Value;
            }
            else
            {
                initialForegroundColor = null;
            }
        }

        protected override void SetRowBackgroundColor(ConsoleColor? backgroundColor)
        {
            ConsoleColor? calculatedBackgroundColor = backgroundColor ?? BackgroundColor;

            if (calculatedBackgroundColor.HasValue)
            {
                initialBackgroundColor = Console.BackgroundColor;
                Console.BackgroundColor = calculatedBackgroundColor.Value;
            }
            else
            {
                initialBackgroundColor = null;
            }
        }

        protected override void ResetRowForegroundColor()
        {
            if (initialForegroundColor.HasValue)
                Console.ForegroundColor = initialForegroundColor.Value;
        }

        protected override void ResetRowBackgroundColor()
        {
            if (initialBackgroundColor.HasValue)
                Console.BackgroundColor = initialBackgroundColor.Value;
        }

        protected override void WriteNewLineInternal()
        {
            Console.WriteLine();
        }

        protected override void WriteInternal(string text)
        {
            CustomConsole.Write(text);
        }

        protected override void WriteInternal(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, string text)
        {
            if (foregroundColor.HasValue)
            {
                if (backgroundColor.HasValue)
                    CustomConsole.Write(foregroundColor.Value, backgroundColor.Value, text);
                else
                    CustomConsole.Write(foregroundColor.Value, text);
            }
            else
            {
                if (backgroundColor.HasValue)
                    CustomConsole.WriteBackgroundColor(backgroundColor.Value, text);
                else
                    CustomConsole.Write(text);
            }
        }

        protected override void WriteInternal(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, char c)
        {
            if (foregroundColor.HasValue)
            {
                if (backgroundColor.HasValue)
                    CustomConsole.Write(foregroundColor.Value, backgroundColor.Value, c);
                else
                    CustomConsole.Write(foregroundColor.Value, c);
            }
            else
            {
                if (backgroundColor.HasValue)
                    CustomConsole.WriteBackgroundColor(backgroundColor.Value, c);
                else
                    CustomConsole.Write(c);
            }
        }
    }
}