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

using System;

namespace DustInTheWind.ConsoleTools.InputControls
{
    /// <summary>
    /// Represents a text to be displayed in the console.
    /// </summary>
    public class Label
    {
        /// <summary>
        /// Gets or sets the number of spaces to be written before the text (to the left).
        /// </summary>
        public int MarginLeft { get; set; } = 0;

        /// <summary>
        /// Gets or sets the number of spaces to be written after the text (to the right).
        /// </summary>
        public int MarginRight { get; set; } = 1;

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the foreground color used to write the text.
        /// </summary>
        public ConsoleColor? ForegroundColor { get; set; }

        /// <summary>
        /// Gets or sets the background color used to write the text.
        /// </summary>
        public ConsoleColor? BackgroundColor { get; set; }

        /// <summary>
        /// Displays the control in the Console.
        /// </summary>
        public void Display()
        {
            if (MarginLeft > 0)
                DisplayLeftMargin();

            if (!ForegroundColor.HasValue && !BackgroundColor.HasValue)
                CustomConsole.Write(Text);
            else if (ForegroundColor.HasValue && BackgroundColor.HasValue)
                CustomConsole.WriteColor(ForegroundColor.Value, BackgroundColor.Value, Text);
            else if (ForegroundColor.HasValue)
                CustomConsole.WriteColor(ForegroundColor.Value, Text);
            else
                CustomConsole.WriteBackgroundColor(BackgroundColor.Value, Text);

            if (MarginRight > 0)
                DisplayRightMargin();
        }

        private void DisplayLeftMargin()
        {
            string space = new string(' ', MarginLeft);
            Console.Write(space);
        }

        private void DisplayRightMargin()
        {
            string space = new string(' ', MarginRight);
            Console.Write(space);
        }
    }
}