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
using System.Text;

namespace DustInTheWind.ConsoleTools.Controls
{
    /// <summary>
    /// Collects the rendered parts of a <see cref="Control"/> instance as a plain text that is later
    /// returned by the <see cref="ToString"/> method.
    /// </summary>
    public class StringDisplay : DisplayBase
    {
        private readonly StringBuilder sb;
        private int availableWidth = 1024;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringDisplay"/> class.
        /// </summary>
        public StringDisplay()
        {
            sb = new StringBuilder();
        }

        public override bool IsCursorVisible { get; set; } = false;

        public override int AvailableWidth => availableWidth;

        protected override void SetRowForegroundColor(ConsoleColor? foregroundColor)
        {
        }

        protected override void SetRowBackgroundColor(ConsoleColor? backgroundColor)
        {
        }

        protected override void ResetRowForegroundColor()
        {
        }

        protected override void ResetRowBackgroundColor()
        {
        }

        public override IDisplay CreateChild(int availableWidth)
        {
            return new StringDisplay
            {
                availableWidth = availableWidth
            };
        }

        protected override void WriteNewLineInternal()
        {
            sb.AppendLine();
        }

        protected override void WriteInternal(string text)
        {
            sb.Append(text);
        }

        protected override void WriteInternal(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, string text)
        {
            sb.Append(text);
        }

        protected override void WriteInternal(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, char c)
        {
            sb.Append(c);
        }

        /// <summary>
        /// Returns the <see cref="string"/> built until now.
        /// </summary>
        public override string ToString()
        {
            return sb.ToString();
        }
    }
}