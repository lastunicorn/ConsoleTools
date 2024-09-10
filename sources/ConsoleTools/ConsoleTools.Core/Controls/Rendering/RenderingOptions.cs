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

using System;

namespace DustInTheWind.ConsoleTools.Controls.Rendering;

/// <summary>
/// Provides options for configuring an instance of <see cref="IRenderer"/>.
/// </summary>
public class RenderingOptions
{
    /// <summary>
    /// Gets or sets the available with into which the control should be rendered.
    /// If <c>null</c> no restriction is imposed. Infinite available space is assumed.
    /// </summary>
    public int? AvailableWidth { get; set; }

    /// <summary>
    /// Gets or sets a value specifying if the requested renderer should be a root or a child.
    /// Default value: true (Root).
    /// </summary>
    public bool IsRoot { get; set; } = true;

    /// <summary>
    /// Method called after each line that is rendered.
    /// </summary>
    public Action<int> OnLineRendered { get; set; }

    public ConsoleColor? ParentBackgroundColor { get; set; }
}