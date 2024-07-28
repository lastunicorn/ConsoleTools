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
using System.Collections.Generic;
using DustInTheWind.ConsoleTools;

namespace ConsoleTools.Demo.PauseDemo.NetCore
{
    internal class KeyValueControl<TKey, TValue> : BlockControl
    {
        public List<KeyValuePair<TKey, TValue>> Items { get; set; }

        protected override void DoDisplayContent(ControlDisplay display)
        {
            foreach ((TKey key, TValue value) in Items)
            {
                DisplayLine(display, key, value);
            }
        }

        private static void DisplayLine(ControlDisplay display, object key, object value)
        {
            display.StartRow();
            display.Write($"{key}: ");

            if (value is bool boolValue)
            {
                ConsoleColor foregroundColor = boolValue
                    ? CustomConsole.SuccessColor
                    : CustomConsole.ErrorColor;

                display.Write(foregroundColor, null, value.ToString());
            }
            else
            {
                display.Write(value.ToString());
            }

            display.EndRow();
        }
    }
}