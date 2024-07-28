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

using System.Reflection;

namespace DustInTheWind.ConsoleTools.Tests.Utils;

public static class TestResources
{
    public static string ReadTextFile(string resourceFileName, Type relativeType)
    {
        if (relativeType == null) throw new ArgumentNullException(nameof(relativeType));

        Assembly assembly = relativeType.Assembly;
        string callerNamespace = relativeType.Namespace;

        using Stream stream = assembly.GetManifestResourceStream(callerNamespace + "." + resourceFileName);

        if (stream == null)
            throw new Exception($"The resource file was not found: '{resourceFileName}'.");

        using StreamReader streamReader = new(stream);

        return streamReader.ReadToEnd();
    }

    public static string ReadTextFile(string resourceFileName, Assembly assembly)
    {
        using Stream stream = assembly.GetManifestResourceStream(resourceFileName);

        if (stream == null)
            throw new Exception($"The resource file was not found: '{resourceFileName}'.");

        using StreamReader streamReader = new(stream);

        return streamReader.ReadToEnd();
    }
}