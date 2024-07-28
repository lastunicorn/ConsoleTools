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
using System.Linq;

namespace DustInTheWind.ConsoleTools.Themes;

public class DefaultColorTheme : IColorTheme
{
    private readonly TextType[] textTypes;

    public DefaultColorTheme()
    {
        textTypes = new[]
        {
            new TextType(DefaultTextType.Normal, Console.ForegroundColor, Console.BackgroundColor),
            new TextType(DefaultTextType.Inverted, Console.BackgroundColor, Console.ForegroundColor),
            new TextType(DefaultTextType.Emphasized, ConsoleColor.White, null),
            new TextType(DefaultTextType.Success, ConsoleColor.Green, null),
            new TextType(DefaultTextType.Warning, ConsoleColor.Yellow, null),
            new TextType(DefaultTextType.Error, ConsoleColor.Red, null)
        };
    }

    public TextType this[string id]
    {
        get { return textTypes.FirstOrDefault(x => x.Id == id); }
    }
}