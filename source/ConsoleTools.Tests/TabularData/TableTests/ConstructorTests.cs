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
    public class ConstructorTests
    {
        [Test]
        public void TestConstructor1()
        {
            Table table = new Table();

            Assert.That(table.Title, Is.EqualTo(MultilineText.Empty));
            Assert.That(table.CellHorizontalAlignment, Is.EqualTo(HorizontalAlignment.Default));
            Assert.That(table.Padding, Is.EqualTo(1));
            Assert.That(table.PaddingLeft, Is.EqualTo(1));
            Assert.That(table.PaddingRight, Is.EqualTo(1));
            Assert.That(table.DisplayBorderBetweenRows, Is.False);
            Assert.That(table.ColumnCount, Is.EqualTo(0));
            Assert.That(table.RowCount, Is.EqualTo(0));
        }

        [Test]
        public void TestConstructor2()
        {
            Table table = new Table("My Title");

            Assert.That(table.Title, Is.EqualTo(new MultilineText("My Title")));
            Assert.That(table.CellHorizontalAlignment, Is.EqualTo(HorizontalAlignment.Default));
            Assert.That(table.Padding, Is.EqualTo(1));
            Assert.That(table.PaddingLeft, Is.EqualTo(1));
            Assert.That(table.PaddingRight, Is.EqualTo(1));
            Assert.That(table.DisplayBorderBetweenRows, Is.False);
            Assert.That(table.ColumnCount, Is.EqualTo(0));
            Assert.That(table.RowCount, Is.EqualTo(0));
        }

        [Test]
        public void TestConstructor3()
        {
            Table table = new Table(new MultilineText("My Title"));

            Assert.That(table.Title, Is.EqualTo(new MultilineText("My Title")));
            Assert.That(table.CellHorizontalAlignment, Is.EqualTo(HorizontalAlignment.Default));
            Assert.That(table.Padding, Is.EqualTo(1));
            Assert.That(table.PaddingLeft, Is.EqualTo(1));
            Assert.That(table.PaddingRight, Is.EqualTo(1));
            Assert.That(table.DisplayBorderBetweenRows, Is.False);
            Assert.That(table.ColumnCount, Is.EqualTo(0));
            Assert.That(table.RowCount, Is.EqualTo(0));
        }
    }
}
