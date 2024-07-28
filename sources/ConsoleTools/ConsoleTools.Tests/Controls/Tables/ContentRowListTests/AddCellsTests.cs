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

using System.Collections.Generic;
using DustInTheWind.ConsoleTools.Controls.Tables;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.ContentRowListTests
{
    [TestFixture]
    public class AddCellsTests
    {
        private DataGrid dataGrid;
        private ContentRowList contentRowList;

        [SetUp]
        public void SetUp()
        {
            dataGrid = new DataGrid();
            contentRowList = new ContentRowList(dataGrid);
        }

        [Test]
        public void HavingAnEmptyNormalRowList_WhenThreeCellsAreAdded_ThenRowCountIs1()
        {
            ContentCell contentCell1 = new ContentCell();
            ContentCell contentCell2 = new ContentCell();
            ContentCell contentCell3 = new ContentCell();

            contentRowList.Add(contentCell1, contentCell2, contentCell3);

            Assert.That(contentRowList.Count, Is.EqualTo(1));
        }

        [Test]
        public void HavingAnEmptyNormalRowList_WhenThreeCellsAreAdded_ThenRowContainsTheThreeCells()
        {
            ContentCell contentCell1 = new ContentCell();
            ContentCell contentCell2 = new ContentCell();
            ContentCell contentCell3 = new ContentCell();

            contentRowList.Add(contentCell1, contentCell2, contentCell3);

            List<ContentCell> expected = new List<ContentCell>
            {
                contentCell1,
                contentCell2,
                contentCell3
            };
            Assert.That(contentRowList[0], Is.EqualTo(expected));
        }

        [Test]
        public void HavingAnEmptyNormalRowList_WhenNullDataCellIsAdded_ThenRowContainsEmptyCell()
        {
            contentRowList.Add((ContentCell)null);

            Assert.That(contentRowList[0][0].IsEmpty, Is.True);
        }
    }
}