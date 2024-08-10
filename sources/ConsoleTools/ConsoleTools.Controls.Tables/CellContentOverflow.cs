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

namespace DustInTheWind.ConsoleTools.Controls.Tables;

/// <summary>
/// Specifies how t obe handled the content that is too big for the specified cell width.
/// </summary>
public enum CellContentOverflow
{
    /// <summary>
    /// The container's default value will be used.
    /// </summary>
    Default = 0,

    /// <summary>
    /// If the text is longer than the required cell width, the cell will not be allowed to shrink.
    /// </summary>
    PreserveOverflow,

    /// <summary>
    /// If the text is longer than the required cell width, the text is cut at the exact length,
    /// loosing the overflow part.
    /// </summary>
    CutChar,

    /// <summary>
    /// If the text is longer than the required cell width, it cuts the text after the last full
    /// word, loosing the overflow part.
    /// </summary>
    CutWord,

    /// <summary>
    /// If the text is longer than the required cell width, it cuts the text adds an ellipsis
    /// "..." at the end.
    /// The length of the remaining text together with the three ellipsis characters will be
    /// exactly the requested length.
    /// </summary>
    CutCharWithEllipsis,

    /// <summary>
    /// If the text is longer than the required cell width, it cuts the text after the last full
    /// word, and adds an ellipsis "..." at the end.
    /// The length of the remaining text together with the three ellipsis characters will be less
    /// or equal to the requested length.
    /// </summary>
    CutWordWithEllipsis,

    /// <summary>
    /// If the text is longer than the required cell width, it wraps the text by cutting each line
    /// at the exact required length in characters.
    /// </summary>
    WrapChar,

    /// <summary>
    /// If the text is longer than the required cell width, it wraps the text by cutting each line
    /// after the last full word, thus preserving the words.
    /// If a word is bigger than one full line it will fall back to <see cref="WrapChar"/> for that
    /// specific word.
    /// </summary>
    WrapWord
}