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
using System.Threading;

namespace DustInTheWind.ConsoleTools.Controls
{
    public sealed class MachineLevelGuardian : IDisposable
    {
        /// <summary>
        /// The <see cref="Mutex"/> object used to ensure that only one instance
        /// of the class is created on the current machine. (Machine level)
        /// </summary>
        private readonly Mutex mutex;

        /// <summary>
        /// Gets the name of the current instance.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MachineLevelGuardian"/> class.
        /// </summary>
        /// <exception cref="GuardianCreateException"></exception>
        public MachineLevelGuardian()
        {
            try
            {
                ApplicationInformation applicationInformation = new ApplicationInformation();
                Name = applicationInformation.GetProductName();

                // Create the mutex.
                mutex = new Mutex(false, Name);

                // Gain exclusive access to the mutex.
                bool access = mutex.WaitOne(0, true);

                if (!access)
                    throw new GuardianException();
            }
            catch (GuardianException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new GuardianCreateException(ex);
            }
        }

        /// <summary>
        /// Releases all resources used by the current instance.
        /// </summary>
        public void Dispose()
        {
            mutex?.Dispose();
        }
    }
}