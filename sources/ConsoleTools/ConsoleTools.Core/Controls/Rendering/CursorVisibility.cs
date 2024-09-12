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

internal static class CursorVisibility
{
    public static ICursorVisibility Create()
    {
        return Environment.OSVersion.Platform switch
        {
            PlatformID.Win32NT => new CursorVisibilityWindows(),
            PlatformID.Win32S => new CursorVisibilityWindows(),
            PlatformID.Win32Windows => new CursorVisibilityWindows(),
            PlatformID.WinCE => new CursorVisibilityWindows(),
            PlatformID.Unix => new CursorVisibilityUnix(),
            PlatformID.MacOSX => new CursorVisibilityDummy(),
            PlatformID.Xbox => new CursorVisibilityDummy(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}