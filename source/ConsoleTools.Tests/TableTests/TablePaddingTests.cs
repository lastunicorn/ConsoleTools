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
    public class TablePaddingTests
    {
        private Table table;

        [SetUp]
        public void SetUp()
        {
            table = new Table("My Title");

            table.AddRow(new[] { "1234567", "123456", "one two" });
            table.AddRow(new[] { "1", "asd", "asas" });
            table.AddRow(new[] { "12", "a", "errr" });
        }

        [Test]
        public void added_a_padding_left_of_2()
        {
            table.PaddingLeft = 2;

            const string expected =
@"+-------------------------------+
|  My Title                     |
+----------+---------+----------+
|  1234567 |  123456 |  one two |
|  1       |  asd    |  asas    |
|  12      |  a      |  errr    |
+----------+---------+----------+
";

            Assert.That(table.PaddingLeft, Is.EqualTo(2));
            Assert.That(table.PaddingRight, Is.EqualTo(1));
            Assert.That(table.Padding, Is.EqualTo(-1));
            Assert.That(table.ToString(), Is.EqualTo(expected));
        }

        [Test]
        public void added_a_padding_right_of_2()
        {
            table.PaddingRight = 2;

            const string expected =
@"+-------------------------------+
| My Title                      |
+----------+---------+----------+
| 1234567  | 123456  | one two  |
| 1        | asd     | asas     |
| 12       | a       | errr     |
+----------+---------+----------+
";

            Assert.That(table.PaddingLeft, Is.EqualTo(1));
            Assert.That(table.PaddingRight, Is.EqualTo(2));
            Assert.That(table.Padding, Is.EqualTo(-1));
            Assert.That(table.ToString(), Is.EqualTo(expected));
        }

        [Test]
        public void added_a_padding_of_2()
        {
            table.Padding = 2;

            const string expected =
@"+----------------------------------+
|  My Title                        |
+-----------+----------+-----------+
|  1234567  |  123456  |  one two  |
|  1        |  asd     |  asas     |
|  12       |  a       |  errr     |
+-----------+----------+-----------+
";

            Assert.That(table.PaddingLeft, Is.EqualTo(2));
            Assert.That(table.PaddingRight, Is.EqualTo(2));
            Assert.That(table.Padding, Is.EqualTo(2));
            Assert.That(table.ToString(), Is.EqualTo(expected));
        }
    }
}