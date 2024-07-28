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

namespace DustInTheWind.ConsoleTools.Controls.Spinners.Templates;

/// <summary>
/// Specifies what the <see cref="FillSpinnerTemplate"/> should do when it is full.
/// </summary>
public enum FilledBehavior
{
    /// <summary>
    /// Specifies that the <see cref="FillSpinnerTemplate"/> should empty itself when full.
    /// </summary>
    SuddenEmpty,

    /// <summary>
    /// Specifies that the <see cref="FillSpinnerTemplate"/> should decreasing its load
    /// starting with the last character displayed.
    /// </summary>
    EmptyFromEnd,

    /// <summary>
    /// Specifies that the <see cref="FillSpinnerTemplate"/> should decreasing its load
    /// starting with the first character displayed.
    /// </summary>
    EmptyFromStart
}