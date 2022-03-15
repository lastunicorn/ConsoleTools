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
    public class AddCell_StringContentTests
    {
        [Test]
        public void HavingEmptyRow_WhenAStringIsAdded_ThenRowContainsOneCell()
        {
            ContentRow row = new();

            row.AddCell("cell 1");

            Assert.That(row.CellCount, Is.EqualTo(1));
        }

        [Test]
        public void HavingEmptyRow_WhenAStringIsAdded_ThenCellFromRowIsNotNull()
        {
            ContentRow row = new();

            row.AddCell("cell 1");

            Assert.That(row[0], Is.Not.Null);
        }

        [Test]
        public void HavingEmptyRow_WhenAStringIsAdded_ThenParentRowIsSetToTheNewContentCell()
        {
            ContentRow row = new();

            row.AddCell("cell 1");

            Assert.That(row[0].ParentRow, Is.SameAs(row));
        }

        [Test]
        public void HavingEmptyRow_WhenAStringIsAdded_ThenNewCellContainsTheString()
        {
            ContentRow row = new();

            row.AddCell("cell 1");

            Assert.That(row[0].Content.ToString(), Is.EqualTo("cell 1"));
        }

        [Test]
        public void HavingEmptyRow_WhenAStringIsAdded_ThenAddedCellIsSameAsTheAddedOne()
        {
            ContentRow row = new();

            ContentCell returnedCell = row.AddCell("cell 1");

            Assert.That(returnedCell, Is.SameAs(row[0]));
        }
    }
}