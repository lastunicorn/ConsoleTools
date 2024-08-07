// ConsoleTools
// Copyright (C) 2017-2024 Dust in the Wind
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

namespace DustInTheWind.ConsoleTools.Controls;

/// <summary>
/// Specifies the horizontal position of an element in a container.
/// </summary>
public enum HorizontalAlignment
{
    /// <summary>
    /// The container's default value will be used.
    /// </summary>
    Default = 0,

    /// <summary>
    /// The element is displayed in the left side of the container.
    /// </summary>
    Left,

    /// <summary>
    /// The element is displayed in the center of the container.
    /// </summary>
    Center,

    /// <summary>
    /// The element is displayed in the right side of the container.
    /// </summary>
    Right,

    /// <summary>
    /// The element is stretched to fill the entire horizontal space.
    /// </summary>
    Stretch
}