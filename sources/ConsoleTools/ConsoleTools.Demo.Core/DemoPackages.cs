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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DustInTheWind.ConsoleTools.Demo.Core
{
    public class DemoPackages : IEnumerable<IDemo>
    {
        private readonly List<IDemo> demoPackages = new List<IDemo>();

        public void LoadFrom(IEnumerable<Assembly> assemblies)
        {
            IEnumerable<IDemo> newDemoPackages = assemblies
                .SelectMany(x => x.GetTypes())
                .Where(x => x.IsClass && !x.IsAbstract)
                .Where(x => typeof(IDemo).IsAssignableFrom(x))
                .Select(Activator.CreateInstance)
                .Cast<IDemo>()
                .ToList();

            demoPackages.AddRange(newDemoPackages);
        }

        public IEnumerator<IDemo> GetEnumerator()
        {
            return demoPackages.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}