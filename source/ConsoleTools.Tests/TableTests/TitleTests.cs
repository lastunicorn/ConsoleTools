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

using DustInTheWind.ConsoleTools.TabularData;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.TableTests
{
    [TestFixture]
    public class TitleTests
    {
        [Test]
        public void title_is_shorter_than_row()
        {
            Table table = new Table();
            table.Title = new MultilineText("My Title");
            table.AddRow(new[] { "asd", "qwe", "zxczxc" });

            string actual = table.ToString();

            string expected =
@"+--------------------+
| My Title           |
+-----+-----+--------+
| asd | qwe | zxczxc |
+-----+-----+--------+
";
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void title_is_longer_than_row()
        {
            Table table = new Table();
            table.Title = new MultilineText("My Title My Title My Title My Title");
            table.AddRow(new[] { "asd", "qwe", "zxczxc" });

            string expected =
@"+-------------------------------------+
| My Title My Title My Title My Title |
+-----------+-----------+-------------+
| asd       | qwe       | zxczxc      |
+-----------+-----------+-------------+
";

            Assert.That(table.ToString(), Is.EqualTo(expected));
        }

        [Test]
        public void multiline_title()
        {
            Table table = new Table();
            table.Title = new MultilineText(@"My Title1
My Title2
My Title3
My Title4");
            table.AddRow(new[] { "asd", "qwe", "zxczxc" });

            string expected =
@"+--------------------+
| My Title1          |
| My Title2          |
| My Title3          |
| My Title4          |
+-----+-----+--------+
| asd | qwe | zxczxc |
+-----+-----+--------+
";

            Assert.That(table.ToString(), Is.EqualTo(expected));
        }
    }
}
