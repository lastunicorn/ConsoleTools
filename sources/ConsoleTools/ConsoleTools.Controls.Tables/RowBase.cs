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
using System.Collections;
using System.Collections.Generic;

namespace DustInTheWind.ConsoleTools.Controls.Tables;

/// <summary>
/// The base class for a data grid row.
/// </summary>
public abstract class RowBase : IEnumerable<CellBase>
{
    /// <summary>
    /// Gets or sets the <see cref="DataGrid"/> instance that contains the current instance.
    /// </summary>
    public DataGrid ParentDataGrid { get; internal set; }

    /// <summary>
    /// Gets or sets the foreground color applied to all the cells in the row.
    /// Default value: <c>null</c>
    /// </summary>
    public ConsoleColor? ForegroundColor { get; set; }

    /// <summary>
    /// Gets or sets the background color applied to all the cells in the row.
    /// Default value: <c>null</c>
    /// </summary>
    public ConsoleColor? BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the horizontal alignment for the content of the cells contained by the current
    /// instance.
    /// </summary>
    public HorizontalAlignment CellHorizontalAlignment { get; set; } = HorizontalAlignment.Default;

    /// <summary>
    /// Gets or sets the padding applied to the left side of every cell.
    /// </summary>
    public int? CellPaddingLeft { get; set; }

    /// <summary>
    /// Gets or sets the padding applied to the right side of every cell contained by the current
    /// instance.
    /// </summary>
    public int? CellPaddingRight { get; set; }

    /// <summary>
    /// Gets the number of cells contained by the row.
    /// </summary>
    public abstract int CellCount { get; }

    /// <summary>
    /// Gets or sets a value that specifies if the row is displayed.
    /// Default value: <c>true</c>
    /// </summary>
    public bool IsVisible { get; set; } = true;

    /// <summary>
    /// Gets or sets a value that specifies the visibility of the border.
    /// By default the border is visible.
    /// </summary>
    public BorderVisibility? BorderVisibility { get; set; }

    /// <summary>
    /// Gets or sets the template to be used when rendering the border.
    /// </summary>
    public BorderTemplate BorderTemplate { get; set; }

    /// <summary>
    /// Gets or sets the foreground color for the border.
    /// Default value: <c>null</c>
    /// </summary>
    public ConsoleColor? BorderForegroundColor { get; set; }

    /// <summary>
    /// Gets or sets the background color for the border.
    /// Default value: <c>null</c>
    /// </summary>
    public ConsoleColor? BorderBackgroundColor { get; set; }

    /// <summary>
    /// Returns the index of the specified cell.
    /// If the cell is not part of the current row instance, returns <c>null</c>.
    /// </summary>
    public abstract int? IndexOfCell(CellBase cell);

    /// <summary>
    /// Enumerates the visible header cells contained by the current instance.
    /// The cells from the hidden columns are excluded.
    /// </summary>
    /// <returns>An enumeration of the visible cells contained by the current instance.</returns>
    public abstract IEnumerable<CellBase> EnumerateVisibleCells();

    /// <summary>
    /// When implemented by an inheritor, enumerates all the cells contained by the current instance.
    /// </summary>
    /// <returns>An enumeration of all the cell contained by the current instance.</returns>
    public abstract IEnumerator<CellBase> GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}