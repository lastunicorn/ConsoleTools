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

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DustInTheWind.ConsoleTools.Demo.Core;

namespace DustInTheWind.ConsoleTools.Demo;

internal class Program
{
    private static void Main(string[] args)
    {
        DemoApplication demoApplication = new();

        IEnumerable<Assembly> demoAssemblies = GetDemoAssemblies();
        demoApplication.AddPackagesFrom(demoAssemblies);

        demoApplication.Run();
    }

    private static IEnumerable<Assembly> GetDemoAssemblies()
    {
        Assembly entryAssembly = Assembly.GetEntryAssembly();
        string applicationRootDirectoryPath = Path.GetDirectoryName(entryAssembly.Location);

        return Directory.GetFiles(applicationRootDirectoryPath, "*.dll")
            .Select(Assembly.LoadFrom);
    }
}