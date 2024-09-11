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

namespace DustInTheWind.ConsoleTools.Controls;

public static class BlockControlExtensions
{
    /// <summary>
    /// Creates a new <see cref="Border"/> control around the provided control.
    /// </summary>
    /// 
    /// <param name="control">
    /// The control to be placed inside the border.
    /// </param>
    /// 
    /// <param name="borderConfigurator">
    /// An optional action allowing the caller to configure the newly created <see cref="Border"/>
    /// instance.
    /// </param>
    /// <returns></returns>
    public static Border AddBorder(this BlockControl control, Action<Border> borderConfigurator = null)
    {
        if (control == null) throw new ArgumentNullException(nameof(control));

        Border border = new()
        {
            Content = control
        };

        borderConfigurator?.Invoke(border);

        return border;
    }
}