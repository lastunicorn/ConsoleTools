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

namespace DustInTheWind.ConsoleTools.Spinners.Templates
{
    /// <summary>
    /// A template for the <see cref="Spinner"/> that displays a "boomerang" spinning.
    /// It has 4 frames.
    /// </summary>
    public class BoomerangTemplate : SequenceTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BoomerangTemplate"/> class.
        /// </summary>
        public BoomerangTemplate()
            : base(new[] { "v", "<", "^", ">" })
        {
        }
    }
}