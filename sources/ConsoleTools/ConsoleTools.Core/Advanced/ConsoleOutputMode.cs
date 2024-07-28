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

// --------------------------------------------------------------------------------
// Bugs or feature requests
// --------------------------------------------------------------------------------
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new/choose

using System;

namespace DustInTheWind.ConsoleTools.Advanced
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/console/console-handles
    /// 
    /// https://docs.microsoft.com/en-us/windows/console/getstdhandle
    /// https://docs.microsoft.com/en-us/windows/console/getconsolemode
    /// https://docs.microsoft.com/en-us/windows/console/setconsolemode
    /// </summary>
    public class ConsoleOutputMode
    {
        private readonly IntPtr consoleHandle;

        private const uint ENABLE_PROCESSED_OUTPUT = 0x0001;
        private const uint ENABLE_WRAP_AT_EOL_OUTPUT = 0x0002;
        private const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;
        private const uint DISABLE_NEWLINE_AUTO_RETURN = 0x0008;
        private const uint ENABLE_LVB_GRID_WORLDWIDE = 0x0010;

        public uint Value
        {
            get
            {
                uint rawValue = 0;
                Kernel32.GetConsoleMode(consoleHandle, ref rawValue);

                return rawValue;
            }
            set => Kernel32.SetConsoleMode(consoleHandle, value);
        }

        public bool IsEnableProcessedOutput
        {
            get => GetFlagValue(ENABLE_PROCESSED_OUTPUT);
            set => SetFlagValue(ENABLE_PROCESSED_OUTPUT, value);
        }

        public bool IsEnableWrapAtEolOutput
        {
            get => GetFlagValue(ENABLE_WRAP_AT_EOL_OUTPUT);
            set => SetFlagValue(ENABLE_WRAP_AT_EOL_OUTPUT, value);
        }

        public bool IsEnableVirtualTerminalProcessing
        {
            get => GetFlagValue(ENABLE_VIRTUAL_TERMINAL_PROCESSING);
            set => SetFlagValue(ENABLE_VIRTUAL_TERMINAL_PROCESSING, value);
        }

        public bool IsDisabledNewLineAutoReturn
        {
            get => GetFlagValue(DISABLE_NEWLINE_AUTO_RETURN);
            set => SetFlagValue(DISABLE_NEWLINE_AUTO_RETURN, value);
        }

        public bool IsEnableLvbGridWorldwide
        {
            get => GetFlagValue(ENABLE_LVB_GRID_WORLDWIDE);
            set => SetFlagValue(ENABLE_LVB_GRID_WORLDWIDE, value);
        }

        public ConsoleOutputMode()
        {
            consoleHandle = Kernel32.GetStdHandle(StandardHandleType.STD_OUTPUT_HANDLE);
        }

        private bool GetFlagValue(uint flag)
        {
            return (Value & flag) == flag;
        }

        private void SetFlagValue(uint flag, bool value)
        {
            if (value)
                Value |= flag;
            else
                Value &= ~flag;
        }
    }
}