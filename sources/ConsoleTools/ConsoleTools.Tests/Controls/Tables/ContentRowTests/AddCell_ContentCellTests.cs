// ConsoleTools
// Copyright (C) 2017-2022 Dust in the Wind
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

using DustInTheWind.ConsoleTools.Controls.Tables;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.ContentRowTests
{
    [TestFixture]
    public class AddCell_ContentCellTests
    {
        [Test]
        public void HavingEmptyRow_WhenAContentCellIsAdded_ThenRowContainsOneCell()
        {
            ContentRow row = new();
            ContentCell cell = new("cell 1");

            row.AddCell(cell);

            Assert.That(row.CellCount, Is.EqualTo(1));
        }

        [Test]
        public void HavingEmptyRow_WhenAContentCellIsAdded_ThenRowContainsTheAddedCell()
        {
            ContentRow row = new();
            ContentCell cell = new("cell 1");

            row.AddCell(cell);

            Assert.That(row[0], Is.SameAs(cell));
        }

        [Test]
        public void HavingEmptyRow_WhenAContentCellIsAdded_ThenParentRowIsSet()
        {
            ContentRow row = new();
            ContentCell cell = new("cell 1");

            row.AddCell(cell);

            Assert.That(cell.ParentRow, Is.SameAs(row));
        }

        [Test]
        public void HavingEmptyRow_WhenAContentCellIsAdded_ThenAddedCellIsReturned()
        {
            ContentRow row = new();
            ContentCell cell = new("cell 1");

            ContentCell returnedCell = row.AddCell(cell);

            Assert.That(returnedCell, Is.SameAs(cell));
        }

        [Test]
        public void HavingEmptyRow_WhenANullContentCellIsAdded_ThenRowContainsOneCell()
        {
            ContentRow row = new();

            row.AddCell(null as ContentCell);

            Assert.That(row.CellCount, Is.EqualTo(1));
        }

        [Test]
        public void HavingEmptyRow_WhenANullContentCellIsAdded_ThenRowContainsANotNullCell()
        {
            ContentRow row = new();

            row.AddCell(null as ContentCell);

            Assert.That(row[0], Is.Not.Null);
        }

        [Test]
        public void HavingEmptyRow_WhenANullContentCellIsAdded_ThenParentRowIsSetToTheNewContentCell()
        {
            ContentRow row = new();

            row.AddCell(null as ContentCell);

            Assert.That(row[0].ParentRow, Is.SameAs(row));
        }

        [Test]
        public void HavingEmptyRow_WhenANullContentCellIsAdded_ThenAddedCellIsSameAsTheAddedOne()
        {
            ContentRow row = new();

            ContentCell returnedCell = row.AddCell(null as ContentCell);

            Assert.That(returnedCell, Is.SameAs(row[0]));
        }
    }
}