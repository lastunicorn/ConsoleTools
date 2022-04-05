// ConsoleTools
// Copyright (C) 2017-2022 Dust in the Wind
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

namespace OperatingSystemDetection.Library
{
    public class Cursor
    {
        public static bool IsVisible
        {
            get => GetVisibility();
            set => SetVisibility(value);
        }

        private static bool GetVisibility()
        {
            if (OperatingSystem.IsWindows())
                return Console.CursorVisible;

            if (OperatingSystem.IsLinux())
                return true;

            if (OperatingSystem.IsMacOS())
                return Console.CursorVisible;

            throw new OperatingSystemNotSupportedException();
        }

        public static bool SetVisibility(bool value)
        {
            if (OperatingSystem.IsWindows())
                return SetVisibilityOnWindows(value);

            if (OperatingSystem.IsLinux())
                return SetVisibilityOnLinux(value);

            if (OperatingSystem.IsMacOS())
                return SetVisibilityOnMacOS(value);

            throw new OperatingSystemNotSupportedException();
        }

        private static bool SetVisibilityOnWindows(bool value)
        {
            bool initialValue = Console.CursorVisible;

            if (value != initialValue)
                Console.CursorVisible = value;

            return initialValue;
        }

        private static bool SetVisibilityOnLinux(bool value)
        {
            string parameter = value
                ? "cnorm"
                : "civis";

            Process.Start("tput", parameter);

            return true;
        }

        private static bool SetVisibilityOnMacOS(bool value)
        {
            bool initialValue = Console.CursorVisible;

            if (value != initialValue)
                Console.CursorVisible = value;

            return initialValue;
        }
    }
}