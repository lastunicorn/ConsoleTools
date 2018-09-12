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
using System.IO;
using System.Runtime.CompilerServices;

namespace DustInTheWind.ConsoleTools.Tests
{
    internal class ExpectedOutput
    {
        private Type testClassType;
        private readonly string basePath;

        public ExpectedOutput(Type testClassType, string basePath)
        {
            this.testClassType = testClassType ?? throw new ArgumentNullException(nameof(testClassType));
            this.basePath = basePath ?? throw new ArgumentNullException(nameof(basePath));
        }

        public string GeExpectedtOut([CallerMemberName] string testName = "")
        {
            string directoryName = testClassType.Name + ".out";
            string outFileName = testName + ".out";
            string expectedFilePath = Path.Combine(basePath, directoryName, outFileName);

            return File.Exists(expectedFilePath)
                ? File.ReadAllText(expectedFilePath)
                : null;
        }
    }
}