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

namespace DustInTheWind.ConsoleTools.Advanced;

public class ConsoleInputMode
{
    private readonly IntPtr consoleHandle;

    private const int ENABLE_PROCESSED_INPUT = 0x0001;
    private const int ENABLE_LINE_INPUT = 0x0002;
    private const int ENABLE_ECHO_INPUT = 0x0004;
    private const int ENABLE_WINDOW_INPUT = 0x0008;
    private const int ENABLE_MOUSE_INPUT = 0x0010;
    private const int ENABLE_INSERT_MODE = 0x0020;
    private const int ENABLE_QUICK_EDIT_MODE = 0x0040;
    private const int ENABLE_VIRTUAL_TERMINAL_INPUT = 0x0200;

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

    public bool IsEnableProcessedInput
    {
        get => GetFlagValue(ENABLE_PROCESSED_INPUT);
        set => SetFlagValue(ENABLE_PROCESSED_INPUT, value);
    }

    public bool IsEnableLineInput
    {
        get => GetFlagValue(ENABLE_LINE_INPUT);
        set => SetFlagValue(ENABLE_LINE_INPUT, value);
    }

    public bool IsEnableEchoInput
    {
        get => GetFlagValue(ENABLE_ECHO_INPUT);
        set => SetFlagValue(ENABLE_ECHO_INPUT, value);
    }

    public bool IsEnableWindowInput
    {
        get => GetFlagValue(ENABLE_WINDOW_INPUT);
        set => SetFlagValue(ENABLE_WINDOW_INPUT, value);
    }

    public bool IsEnableMouseInput
    {
        get => GetFlagValue(ENABLE_MOUSE_INPUT);
        set => SetFlagValue(ENABLE_MOUSE_INPUT, value);
    }

    public bool IsEnableInsertMode
    {
        get => GetFlagValue(ENABLE_INSERT_MODE);
        set => SetFlagValue(ENABLE_INSERT_MODE, value);
    }

    public bool IsEnableQuickEditMode
    {
        get => GetFlagValue(ENABLE_QUICK_EDIT_MODE);
        set => SetFlagValue(ENABLE_QUICK_EDIT_MODE, value);
    }

    public bool IsEnableVirtualTerminalInput
    {
        get => GetFlagValue(ENABLE_VIRTUAL_TERMINAL_INPUT);
        set => SetFlagValue(ENABLE_VIRTUAL_TERMINAL_INPUT, value);
    }

    public ConsoleInputMode()
    {
        consoleHandle = Kernel32.GetStdHandle(StandardHandleType.STD_INPUT_HANDLE);

        // check for null

        // check for errors
        // if consoleHandle == INVALID_HANDLE_VALUE;
        //  GetLastError
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