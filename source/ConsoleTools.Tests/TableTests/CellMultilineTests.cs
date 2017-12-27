﻿// ConsoleTools
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

using DustInTheWind.ConsoleTools.TabularData;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.TableTests
{
    [TestFixture]
    public class CellMultilineTests
    {
        [Test]
        public void TestMultilineCellContent()
        {
            Table table = new Table();
            table.Title = new MultilineText(@"My Title");
            table.AddRow(new[] { "asd\nas", "qwe", "zxczxc\nasas\nerrr r r" });

            string expected =
@"+----------------------+
| My Title             |
+-----+-----+----------+
| asd | qwe | zxczxc   |
| as  |     | asas     |
|     |     | errr r r |
+-----+-----+----------+
";

            Assert.That(table.ToString(), Is.EqualTo(expected));
        }
    }
}
