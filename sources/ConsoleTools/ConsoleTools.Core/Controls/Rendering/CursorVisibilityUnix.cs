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
using System.Diagnostics;

namespace DustInTheWind.ConsoleTools.Controls.Rendering;

internal class CursorVisibilityUnix : ICursorVisibility
{
    private bool isCursorVisible;

    public CursorVisibilityUnix()
    {
        isCursorVisible = true;
    }

    public CursorVisibilityUnix(bool isCursorVisible)
    {
        this.isCursorVisible = isCursorVisible;
    }

    public bool IsVisible()
    {
        return isCursorVisible;
    }

    public void ShowCursor()
    {
        //Process process = Process.Start("tput", "cnorm");
        //process?.WaitForExit();

        Process.Start("tput", "cnorm");

        isCursorVisible = true;
    }

    public void HideCursor()
    {
        //Process process = Process.Start("tput", "civis");
        //process?.WaitForExit();

        Process.Start("tput", "civis");

        isCursorVisible = false;
    }
}