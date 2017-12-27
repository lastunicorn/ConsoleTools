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
    public class CellAlignmentTests
    {
        [Test]
        public void default_alignment()
        {
            Table table = new Table("Cell Alignment Tests");
            table.AddRow(new[] { "1", "2", "3" });

            string expected =
@"+----------------------+
| Cell Alignment Tests |
+-------+-------+------+
| 1     | 2     | 3    |
+-------+-------+------+
";
            Assert.That(table.ToString(), Is.EqualTo(expected));
        }

        [Test]
        public void cell_1_1_has_alignment_Left()
        {
            Table table = new Table("Cell Alignment Tests");
            table.AddRow(new[] { "1", "2", "3" });
            table[0][1].HorizontalAlignment = HorizontalAlignment.Left;

            string expected =
@"+----------------------+
| Cell Alignment Tests |
+-------+-------+------+
| 1     | 2     | 3    |
+-------+-------+------+
";
            Assert.That(table.ToString(), Is.EqualTo(expected));
        }

        [Test]
        public void cell_1_1_has_alignment_Center()
        {
            Table table = new Table("Cell Alignment Tests");
            table.AddRow(new[] { "1", "2", "3" });
            table[0][1].HorizontalAlignment = HorizontalAlignment.Center;

            string expected =
@"+----------------------+
| Cell Alignment Tests |
+-------+-------+------+
| 1     |   2   | 3    |
+-------+-------+------+
";
            Assert.That(table.ToString(), Is.EqualTo(expected));
        }

        [Test]
        public void cell_1_1_has_alignment_Right()
        {
            Table table = new Table("Cell Alignment Tests");
            table.AddRow(new[] { "1", "2", "3" });
            table[0][1].HorizontalAlignment = HorizontalAlignment.Right;

            string expected =
@"+----------------------+
| Cell Alignment Tests |
+-------+-------+------+
| 1     |     2 | 3    |
+-------+-------+------+
";
            Assert.That(table.ToString(), Is.EqualTo(expected));
        }

        [Test]
        public void TestCellHorizontalAlign1()
        {
            Table table = new Table
            {
                Title = new MultilineText("My Title")
            };

            table.AddRow(new[] { "123", "qwe", "one two" });
            table.AddRow(new[] { new Cell("1", HorizontalAlignment.Right), new Cell("asd"), new Cell("asas") });
            table.AddRow(new[] { "12", "a", "errr" });

            string expected =
@"+---------------------+
| My Title            |
+-----+-----+---------+
| 123 | qwe | one two |
|   1 | asd | asas    |
| 12  | a   | errr    |
+-----+-----+---------+
";

            Assert.That(table.ToString(), Is.EqualTo(expected));
        }

        [Test]
        public void TestColumnHorizontalAlign()
        {
            Table table = new Table();
            table.Title = new MultilineText("My Title");

            Column col = new Column("Col1");
            col.HorizontalAlignment = HorizontalAlignment.Right;
            table.Columns.Add(col);

            col = new Column("Col2");
            table.Columns.Add(col);

            col = new Column("Col3");
            table.Columns.Add(col);

            table.AddRow(new[] { "123", "qwe", "one two" });
            table.AddRow(new[] { new Cell("1", HorizontalAlignment.Left), new Cell("asd"), new Cell("asas") });
            table.AddRow(new[] { "12", "a", "errr" });

            string expected =
@"+---------------------+
| My Title            |
+-----+-----+---------+
| 123 | qwe | one two |
| 1   | asd | asas    |
|  12 | a   | errr    |
+-----+-----+---------+
";

            Assert.That(table.ToString(), Is.EqualTo(expected));
        }

        [Test]
        public void TestCellHorizontalAlign2()
        {
            Table table = new Table();
            table.Title = new MultilineText("My Title");

            table.AddRow(new[] { "1234567", "123456", "one two" });
            table.AddRow(new[] { new Cell("1", HorizontalAlignment.Center), new Cell("asd", HorizontalAlignment.Center), new Cell("asas") });
            table.AddRow(new[] { "12", "a", "errr" });

            string expected =
@"+----------------------------+
| My Title                   |
+---------+--------+---------+
| 1234567 | 123456 | one two |
|    1    |  asd   | asas    |
| 12      | a      | errr    |
+---------+--------+---------+
";

            Assert.That(table.ToString(), Is.EqualTo(expected));
        }
    }
}