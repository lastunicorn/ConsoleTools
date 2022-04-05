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

        protected override void RestoreForegroundColor()
        {
            if (initialForegroundColor.HasValue)
                Console.ForegroundColor = initialForegroundColor.Value;
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

        protected override void RestoreBackgroundColor()
        {
            if (initialBackgroundColor.HasValue)
                Console.BackgroundColor = initialBackgroundColor.Value;
        }

        protected override void WriteInternal(string text)
        {
            CustomConsole.Write(text);
        }

        protected override void WriteInternal(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string text)
        {
            CustomConsole.Write(foregroundColor, backgroundColor, text);
        }

        protected override void WriteInternal(ConsoleColor foregroundColor, ConsoleColor backgroundColor, char c)
        {
            CustomConsole.Write(foregroundColor, backgroundColor, c);
        }
    }
}