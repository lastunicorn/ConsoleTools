﻿// ConsoleTools
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
using DustInTheWind.ConsoleTools.CommandLine;

namespace DustInTheWind.ConsoleTools.Controls.Menus;

/// <summary>
/// Provides data for UnhandledCommand event.
/// </summary>
public class UnhandledCommandEventArgs : EventArgs
{
    /// <summary>
    /// Gets the unhandled <see cref="CliCommand"/> instance.
    /// </summary>
    public CliCommand Command { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UnhandledCommandEventArgs"/> class.
    /// </summary>
    public UnhandledCommandEventArgs(CliCommand command)
    {
        Command = command;
    }
}