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

using System;

namespace DustInTheWind.ConsoleTools.Controls;

/// <summary>
/// The implementors of this interface represent the target where a <see cref="Control"/> is rendered.
/// They provide methods to sequentially render the parts of the <see cref="Control"/> instance.
/// </summary>
public interface IDisplay
{
    /// <summary>
    /// Gets or sets the foreground color used to write the text.
    /// Default value: <c>null</c>
    /// </summary>
    ConsoleColor ForegroundColor { get; set; }

    /// <summary>
    /// Gets or sets the background color used to write the text.
    /// Default value: <c>null</c>
    /// </summary>
    ConsoleColor BackgroundColor { get; set; }

    /// <summary>
    /// Writes the specified character, using the specified colors.
    /// </summary>
    void Write(char c, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null);

    /// <summary>
    /// Writes the specified text, using the specified colors.
    /// </summary>
    void Write(string text, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null);

    /// <summary>
    /// Writes the line terminator.
    /// </summary>
    void EndLine();

    /// <summary>
    /// Writes all the buffered data into the output.
    /// </summary>
    void Flush();
}