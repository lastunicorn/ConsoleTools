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
    public class ConstructorTests
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
    }
}
