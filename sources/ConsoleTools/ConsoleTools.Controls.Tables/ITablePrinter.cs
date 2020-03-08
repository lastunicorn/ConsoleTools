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
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new

namespace DustInTheWind.ConsoleTools.Controls.Tables
{
    /// <summary>
    /// The implementors of this interface represent the target where a <see cref="DataGrid"/> is rendered.
    /// They provide methods to sequentially render the parts of the <see cref="DataGrid"/> instance.
    /// </summary>
    public interface ITablePrinter
    {
        /// <summary>
        /// Writes the specified text, applying the formatting specific for a border.
        /// </summary>
        void WriteBorder(string text);

        /// <summary>
        /// Writes the specified text, applying the formatting specific for a border,
        /// followed by the current line terminator.
        /// </summary>
        void WriteLineBorder(string text);

        /// <summary>
        /// Writes the specified character, applying the formatting specific for a border.
        /// </summary>
        void WriteBorder(char c);

        /// <summary>
        /// Writes the specified character, applying the formatting specific for a border,
        /// followed by the current line terminator.
        /// </summary>
        void WriteLineBorder(char c);

        /// <summary>
        /// Writes the specified text, applying the formatting specific for the title.
        /// </summary>
        void WriteTitle(string text);

        /// <summary>
        /// Writes the specified text, applying the formatting specific for the column headers.
        /// </summary>
        void WriteHeader(string text);

        /// <summary>
        /// Writes the specified text, applying the default formatting.
        /// </summary>
        void WriteNormal(string text);

        /// <summary>
        /// Writes the current line terminator.
        /// </summary>
        void WriteLine();
    }
}