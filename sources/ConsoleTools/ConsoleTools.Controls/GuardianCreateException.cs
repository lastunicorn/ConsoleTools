﻿// ConsoleTools
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

namespace DustInTheWind.ConsoleTools.Controls
{
    /// <summary>
    /// This exception is thrown by the <see cref="MachineLevelGuardian"/> if it cannot be successfully created and
    /// gain exclusive access to the mutex.
    /// </summary>
    public class GuardianCreateException : Exception
    {
        private const string DefaultMessage = "The guardian could not be created.";

        /// <summary>
        /// Initializes a new instance of the <see cref="GuardianCreateException"/> class.
        /// </summary>
        /// <exception cref="ApplicationException"></exception>
        public GuardianCreateException(Exception innerException)
            : base(DefaultMessage, innerException)
        {
        }
    }
}