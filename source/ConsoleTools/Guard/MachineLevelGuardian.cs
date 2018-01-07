// ConsoleTools
// Copyright (C) 2017-2018 Dust in the Wind
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
using System.Threading;

namespace DustInTheWind.ConsoleTools.Guard
{
    public class MachineLevelGuardian
    {
        /// <summary>
        /// The <see cref="Mutex"/> object used to ensure that only one instance
        /// of the class is created on the current machine. (Machine level)
        /// </summary>
        private Mutex mutex;

        /// <summary>
        /// Gets the name of the current instance.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MachineLevelGuardian"/> class with
        /// the name that identifies it and 
        /// the level at which will have efect.
        /// </summary>
        /// <param name="name">The name that identifies the instance that will be created.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ApplicationException"></exception>
        public MachineLevelGuardian(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            Name = name;

            // Create the mutex.
            mutex = new Mutex(false, name);

            // Gain exclusive access to the mutex.
            bool access = mutex.WaitOne(0, true);

            if (!access)
                throw new ApplicationException(string.Format("Another instance with the name '{0}' already exists.", name));
        }

        private bool isDisposed;

        /// <summary>
        /// Releases all resources used by the current instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases all resources used by the current instance.
        /// </summary>
        /// <remarks>
        /// <para>Dispose(bool disposing) executes in two distinct scenarios.</para>
        /// <para>If the method has been called directly or indirectly by a user's code managed and unmanaged resources can be disposed.</para>
        /// <para>If the method has been called by the runtime from inside the finalizer you should not reference other objects. Only unmanaged resources can be disposed.</para>
        /// </remarks>
        /// <param name="disposing">Specifies if the method has been called by a user's code (true) or by the runtime from inside the finalizer (false).</param>
        private void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!isDisposed)
            {
                // If disposing equals true, dispose all managed resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    // ...

                    mutex?.Close();
                    mutex = null;
                }

                // Call the appropriate methods to clean up unmanaged resources here.
                // ...

                isDisposed = true;
            }
        }

        /// <summary>
        /// Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.
        /// </summary>
        ~MachineLevelGuardian()
        {
            Dispose(false);
        }
    }
}