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

namespace DustInTheWind.ConsoleTools.Controls.Tables;

/// <summary>
/// The implementors of this interface represent the target where a <see cref="DataGrid"/> is rendered.
/// They provide methods to sequentially render the parts of the <see cref="DataGrid"/> instance.
/// </summary>
public interface ITablePrinter
{
    /// <summary>
    /// Writes the specified character, using the specified colors.
    /// </summary>
    void Write(char c, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor);

    /// <summary>
    /// Writes the specified text, using the specified colors.
    /// </summary>
    void Write(string text, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor);

    /// <summary>
    /// Writes the specified text, using the specified colors,
    /// followed by the current line terminator.
    /// </summary>
    void WriteLine(string text, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor);

    /// <summary>
    /// Writes the current line terminator.
    /// </summary>
    void WriteLine();

    /// <summary>
    /// Writes all the buffered data into the output.
    /// </summary>
    void Flush();
}