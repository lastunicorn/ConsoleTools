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

namespace DustInTheWind.ConsoleTools.Controls;

/// <summary>
/// Renders the underlying control line by line into the specified <see cref="IDisplay"/>.
/// </summary>
/// 
/// <remarks>
/// This approach of rendering line by line is necessary when the control being rendered is
/// embedded into another one.
/// The parent control will need to render each child control's line into its own lines.
/// </remarks>
public interface IRenderer
{
    /// <summary>
    /// Gets a value specifying if the rendered still has more lines to render.
    /// </summary>
    bool HasMoreLines { get; }

    /// <summary>
    /// Renders the next line using the underlying <see cref="IDisplay"/>.
    /// </summary>
    void RenderNextLine();

    void Reset();
}