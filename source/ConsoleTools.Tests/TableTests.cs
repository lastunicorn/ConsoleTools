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
using NUnit.Framework.SyntaxHelpers;

namespace DustInTheWind.ConsoleTools.Tests
{
    [TestFixture]
    public class TableTests
    {
        [Test]
        public void TestConstructor1()
        {
            Table table = new Table();

            Assert.That(table.Title, Is.EqualTo(MultilineText.Empty));
            Assert.That(table.HorizontalAlignment, Is.EqualTo(HorizontalAlignment.Left));
            Assert.That(table.Padding, Is.EqualTo(1));
            Assert.That(table.PaddingLeft, Is.EqualTo(1));
            Assert.That(table.PaddingRight, Is.EqualTo(1));
            Assert.That(table.DrawLinesBetweenRows, Is.False);
            Assert.That(table.ColumnCount, Is.EqualTo(0));
            Assert.That(table.RowCount, Is.EqualTo(0));
        }

        [Test]
        public void TestConstructor2()
        {
            string tableTitle = "My Title";
            Table table = new Table(tableTitle);

            Assert.That(table.Title, Is.EqualTo(new MultilineText(tableTitle)));
            Assert.That(table.HorizontalAlignment, Is.EqualTo(HorizontalAlignment.Left));
            Assert.That(table.Padding, Is.EqualTo(1));
            Assert.That(table.PaddingLeft, Is.EqualTo(1));
            Assert.That(table.PaddingRight, Is.EqualTo(1));
            Assert.That(table.DrawLinesBetweenRows, Is.False);
            Assert.That(table.ColumnCount, Is.EqualTo(0));
            Assert.That(table.RowCount, Is.EqualTo(0));
        }

        [Test]
        public void TestConstructor3()
        {
            MultilineText tableTitle = new MultilineText("My Title");

            Table table = new Table(tableTitle);

            Assert.That(table.Title, Is.EqualTo(tableTitle));
            Assert.That(table.HorizontalAlignment, Is.EqualTo(HorizontalAlignment.Left));
            Assert.That(table.Padding, Is.EqualTo(1));
            Assert.That(table.PaddingLeft, Is.EqualTo(1));
            Assert.That(table.PaddingRight, Is.EqualTo(1));
            Assert.That(table.DrawLinesBetweenRows, Is.False);
            Assert.That(table.ColumnCount, Is.EqualTo(0));
            Assert.That(table.RowCount, Is.EqualTo(0));
        }


        [Test]
        public void Title_is_less_than_row()
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
        public void title_longer_than_row()
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
        public void TestMultilineTableTitle()
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

        [Test]
        public void TestMultilineCellContent()
        {
            Table table = new Table();
            table.Title = new MultilineText(@"My Title1
My Title2
My Title3
My Title4");
            table.AddRow(new[] { "asd\nas", "qwe", "zxczxc\nasas\nerrr r r" });

            string expected =
@"+----------------------+
| My Title1            |
| My Title2            |
| My Title3            |
| My Title4            |
+-----+-----+----------+
| asd | qwe | zxczxc   |
| as  |     | asas     |
|     |     | errr r r |
+-----+-----+----------+
";

            Assert.That(table.ToString(), Is.EqualTo(expected));
        }

        [Test]
        public void three_rows()
        {
            Table table = new Table();
            table.Title = new MultilineText("My Title");
            table.AddRow(new[] { "one", "ichi", "eins" });
            table.AddRow(new[] { "two", "ni", "zwei" });
            table.AddRow(new[] { "three", "san", "drei" });

            string expected =
@"+---------------------+
| My Title            |
+-------+------+------+
| one   | ichi | eins |
| two   | ni   | zwei |
| three | san  | drei |
+-------+------+------+
";

            Assert.That(table.ToString(), Is.EqualTo(expected));
        }

        [Test]
        public void three_rows_with_lines_between_them()
        {
            Table table = new Table();
            table.Title = new MultilineText("My Title");
            table.DrawLinesBetweenRows = true;

            table.AddRow(new[] { "one", "ichi", "eins" });
            table.AddRow(new[] { "two", "ni", "zwei" });
            table.AddRow(new[] { "three", "san", "drei" });

            string expected =
@"+---------------------+
| My Title            |
+-------+------+------+
| one   | ichi | eins |
+-------+------+------+
| two   | ni   | zwei |
+-------+------+------+
| three | san  | drei |
+-------+------+------+
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
