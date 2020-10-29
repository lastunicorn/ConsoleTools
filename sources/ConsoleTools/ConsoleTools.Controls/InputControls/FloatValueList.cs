// ConsoleTools
// Copyright (C) 2017-2020 Dust in the Wind
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

namespace DustInTheWind.ConsoleTools.Controls.InputControls
{
    /// <summary>
    /// Reads or writes a list of <see cref="float"/> values from the console.
    /// </summary>
    public class FloatValueList : ValueList<float>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FloatValueList"/> class.
        /// </summary>
        public FloatValueList()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FloatValueList"/> class with
        /// the label to be displayed when the user is requested to provide the values.
        /// </summary>
        /// <param name="label">The label to be displayed when the user is requested to provide the values.</param>
        public FloatValueList(string label)
            : base(label)
        {
        }
    }
}