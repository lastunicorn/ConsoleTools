// ConsoleTools
// Copyright (C) 2017 Dust in the Wind
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

namespace DustInTheWind.ConsoleTools.Guard
{
    /// <summary>
    /// This class ensures that an instance of itself with the same name
    /// can not be created twice.
    /// </summary>
    public class Guardian : IGuardian
    {
        private IGuardian underlyingGuardian;

        /// <summary>
        /// Gets the name of the current instance.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets a value that specifies the level at which the current instance of the
        /// <see cref="Guardian"/> class will have efect.
        /// </summary>
        public GuardLevel GuardLevel { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Guardian"/> class with
        /// the name that identifies it and 
        /// the level at which will have efect.
        /// </summary>
        /// <param name="name">The name that identifies the instance that will be created.</param>
        /// <param name="guardLevel">The level at which the new instance will have efect.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ApplicationException"></exception>
        public Guardian(string name, GuardLevel guardLevel)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            Name = name;
            GuardLevel = guardLevel;

            switch (guardLevel)
            {
                case GuardLevel.Application:
                    underlyingGuardian = new ApplicationLevelGuardian(name);
                    break;

                case GuardLevel.Machine:
                    underlyingGuardian = new MachineLevelGuardian(name);
                    break;

                default:
                    throw new ArgumentException("Invalid guard level.", nameof(guardLevel));
            }
        }

        private bool disposed;

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
            if (!disposed)
            {
                // If disposing equals true, dispose all managed resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    // ...

                    underlyingGuardian?.Dispose();
                    underlyingGuardian = null;
                }

                // Call the appropriate methods to clean up unmanaged resources here.
                // ...

                disposed = true;
            }
        }

        ~Guardian()
        {
            Dispose(false);
        }
    }
}