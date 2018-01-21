﻿// ConsoleTools
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

namespace DustInTheWind.ConsoleTools.InputControls
{
    public class Int32ListOutput : ListOutput<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Int32ListOutput" /> class.
        /// </summary>
        public Int32ListOutput()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Int32ListOutput"/> class with
        /// the label text to be displayed before the list of values.
        /// </summary>
        /// <param name="label">The label text to be displayed before the list of values.</param>
        public Int32ListOutput(string label)
        {
            Label = label;
        }
    }
}