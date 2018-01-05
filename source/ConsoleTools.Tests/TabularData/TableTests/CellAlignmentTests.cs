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

using DustInTheWind.ConsoleTools.TabularData;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.TabularData.TableTests
{
    [TestFixture]
    public class CellAlignmentTests
    {
        [Test]
        public void by_default_cell_content_is_aligned_to_left()
        {
            Table table = new Table("This is a cell alignment test");
            table.AddRow(new[] { "0,0", "0,1", "0,2" });

            string expected =
@"+-------------------------------+
| This is a cell alignment test |
+----------+----------+---------+
| 0,0      | 0,1      | 0,2     |
+----------+----------+---------+
";
            CustomAssert.TableRender(table, expected);
        }

        [Test]
        public void cell_0_1_is_alignment_Default_after_cell_creation()
        {
            Table table = new Table("This is a cell alignment test");
            table.AddRow(new[] { "0,0", "0,1", "0,2" });
            table[0][1].HorizontalAlignment = HorizontalAlignment.Default;

            string expected =
@"+-------------------------------+
| This is a cell alignment test |
+----------+----------+---------+
| 0,0      | 0,1      | 0,2     |
+----------+----------+---------+
";
            CustomAssert.TableRender(table, expected);
        }

        [Test]
        public void cell_0_1_is_alignment_Left_after_cell_creation()
        {
            Table table = new Table("This is a cell alignment test");
            table.AddRow(new[] { "0,0", "0,1", "0,2" });
            table[0][1].HorizontalAlignment = HorizontalAlignment.Left;

            string expected =
@"+-------------------------------+
| This is a cell alignment test |
+----------+----------+---------+
| 0,0      | 0,1      | 0,2     |
+----------+----------+---------+
";
            CustomAssert.TableRender(table, expected);
        }

        [Test]
        public void cell_0_1_is_alignment_Center_after_cell_creation()
        {
            Table table = new Table("This is a cell alignment test");
            table.AddRow(new[] { "0,0", "0,1", "0,2" });
            table[0][1].HorizontalAlignment = HorizontalAlignment.Center;

            string expected =
@"+-------------------------------+
| This is a cell alignment test |
+----------+----------+---------+
| 0,0      |   0,1    | 0,2     |
+----------+----------+---------+
";
            CustomAssert.TableRender(table, expected);
        }

        [Test]
        public void cell_0_1_is_alignment_Right_after_cell_creation()
        {
            Table table = new Table("This is a cell alignment test");
            table.AddRow(new[] { "0,0", "0,1", "0,2" });
            table[0][1].HorizontalAlignment = HorizontalAlignment.Right;

            string expected =
@"+-------------------------------+
| This is a cell alignment test |
+----------+----------+---------+
| 0,0      |      0,1 | 0,2     |
+----------+----------+---------+
";
            CustomAssert.TableRender(table, expected);
        }

        [Test]
        public void cell_1_1_is_alignment_Default_at_cell_creation()
        {
            Table table = new Table("This is a cell alignment test");
            table.AddRow(new[] { "0,0", "0,1", "0,2" });
            table.AddRow(new DataCell[] { new DataCell("1,0", HorizontalAlignment.Default), "1,1", "1,2" });
            table.AddRow(new[] { "2,0", "2,1", "2,2" });

            string expected =
@"+-------------------------------+
| This is a cell alignment test |
+----------+----------+---------+
| 0,0      | 0,1      | 0,2     |
| 1,0      | 1,1      | 1,2     |
| 2,0      | 2,1      | 2,2     |
+----------+----------+---------+
";

            CustomAssert.TableRender(table, expected);
        }

        [Test]
        public void cell_1_1_is_alignment_Left_at_cell_creation()
        {
            Table table = new Table("This is a cell alignment test");
            table.AddRow(new[] { "0,0", "0,1", "0,2" });
            table.AddRow(new DataCell[] { new DataCell("1,0", HorizontalAlignment.Left), "1,1", "1,2" });
            table.AddRow(new[] { "2,0", "2,1", "2,2" });

            string expected =
@"+-------------------------------+
| This is a cell alignment test |
+----------+----------+---------+
| 0,0      | 0,1      | 0,2     |
| 1,0      | 1,1      | 1,2     |
| 2,0      | 2,1      | 2,2     |
+----------+----------+---------+
";

            CustomAssert.TableRender(table, expected);
        }

        [Test]
        public void cell_1_1_is_alignment_Center_at_cell_creation()
        {
            Table table = new Table("This is a cell alignment test");
            table.AddRow(new[] { "0,0", "0,1", "0,2" });
            table.AddRow(new DataCell[] { new DataCell("1,0", HorizontalAlignment.Center), "1,1", "1,2" });
            table.AddRow(new[] { "2,0", "2,1", "2,2" });

            string expected =
@"+-------------------------------+
| This is a cell alignment test |
+----------+----------+---------+
| 0,0      | 0,1      | 0,2     |
|   1,0    | 1,1      | 1,2     |
| 2,0      | 2,1      | 2,2     |
+----------+----------+---------+
";

            CustomAssert.TableRender(table, expected);
        }

        [Test]
        public void cell_1_1_is_alignment_Right_at_cell_creation()
        {
            Table table = new Table("This is a cell alignment test");
            table.AddRow(new[] { "0,0", "0,1", "0,2" });
            table.AddRow(new DataCell[] { new DataCell("1,0", HorizontalAlignment.Right), "1,1", "1,2" });
            table.AddRow(new[] { "2,0", "2,1", "2,2" });

            string expected =
@"+-------------------------------+
| This is a cell alignment test |
+----------+----------+---------+
| 0,0      | 0,1      | 0,2     |
|      1,0 | 1,1      | 1,2     |
| 2,0      | 2,1      | 2,2     |
+----------+----------+---------+
";

            CustomAssert.TableRender(table, expected);
        }

        [Test]
        public void TestCellHorizontalAlign2()
        {
            Table table = new Table();
            table.Title = "My Title";

            table.AddRow(new[] { "1234567", "123456", "one two" });
            table.AddRow(new[] { new DataCell("1", HorizontalAlignment.Center), new DataCell("asd", HorizontalAlignment.Center), new DataCell("asas") });
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

            CustomAssert.TableRender(table, expected);
        }
    }
}