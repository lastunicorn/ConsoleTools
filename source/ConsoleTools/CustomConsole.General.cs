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

namespace DustInTheWind.ConsoleTools
{
    /// <summary>
    /// Contains a set of methods that help to write text to the standard output stream.
    /// </summary>
    public static partial class CustomConsole
    {
        /// <summary>
        /// Gets or sets the color used to write Success messages.
        /// </summary>
        public static ConsoleColor SuccessColor { get; set; } = ConsoleColor.Green;

        /// <summary>
        /// Gets or sets the color used to write Warning messages.
        /// </summary>
        public static ConsoleColor WarningColor { get; set; } = ConsoleColor.Yellow;

        /// <summary>
        /// Gets or sets the color used to write Error messages.
        /// </summary>
        public static ConsoleColor ErrorColor { get; set; } = ConsoleColor.Red;

        /// <summary>
        /// Gets or sets the color used to write Emphasiesed messages.
        /// </summary>
        public static ConsoleColor EmphasiesColor { get; set; } = ConsoleColor.White;
    }
}