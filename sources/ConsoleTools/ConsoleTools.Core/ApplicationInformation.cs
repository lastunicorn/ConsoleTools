// ConsoleTools
// Copyright (C) 2017-2020 Dust in the Wind
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
using System.Linq;
using System.Reflection;

namespace DustInTheWind.ConsoleTools
{
    /// <summary>
    /// Provides information about the current application like product name, version, etc.
    /// </summary>
    public class ApplicationInformation
    {
        private readonly Assembly assembly;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationInformation"/> class.
        /// </summary>
        public ApplicationInformation()
        {
            assembly = Assembly.GetEntryAssembly();
        }

        /// <summary>
        /// Returns the version of the current application.
        /// It is retrieved from the entry assembly.
        /// </summary>
        /// <returns>A <see cref="Version"/> instance containing version information for the current application.</returns>
        public Version GetVersion()
        {
            AssemblyName assemblyName = assembly.GetName();
            return assemblyName.Version;
        }

        /// <summary>
        /// Returns the name of the application.
        /// It is retrieved from the entry assembly.
        /// </summary>
        /// <returns>The name of the current application.</returns>
        public string GetProductName()
        {
            AssemblyProductAttribute assemblyProductAttribute = assembly.GetCustomAttributes(typeof(AssemblyProductAttribute))
                .Cast<AssemblyProductAttribute>()
                .FirstOrDefault();

            return assemblyProductAttribute?.Product;
        }
    }
}