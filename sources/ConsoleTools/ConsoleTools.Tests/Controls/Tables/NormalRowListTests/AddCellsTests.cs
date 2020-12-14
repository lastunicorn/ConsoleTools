// ConsoleTools
// Copyright (C) 2017-2020 Dust in the Wind
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

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.NormalRowListTests
{
    [TestFixture]
    public class AddCellsTests
    {
        private DataGrid dataGrid;
        private NormalRowList normalRowList;

        [SetUp]
        public void SetUp()
        {
            dataGrid = new DataGrid();
            normalRowList = new NormalRowList(dataGrid);
        }

        [Test]
        public void HavingAnEmptyNormalRowList_WhenThreeCellsAreAdded_ThenRowCountIs1()
        {
            NormalCell normalCell1 = new NormalCell();
            NormalCell normalCell2 = new NormalCell();
            NormalCell normalCell3 = new NormalCell();

            normalRowList.Add(normalCell1, normalCell2, normalCell3);

            Assert.That(normalRowList.Count, Is.EqualTo(1));
        }

        [Test]
        public void HavingAnEmptyNormalRowList_WhenThreeCellsAreAdded_ThenRowContainsTheThreeCells()
        {
            NormalCell normalCell1 = new NormalCell();
            NormalCell normalCell2 = new NormalCell();
            NormalCell normalCell3 = new NormalCell();

            normalRowList.Add(normalCell1, normalCell2, normalCell3);

            List<NormalCell> expected = new List<NormalCell>
            {
                normalCell1,
                normalCell2,
                normalCell3
            };
            Assert.That(normalRowList[0], Is.EqualTo(expected));
        }

        [Test]
        public void HavingAnEmptyNormalRowList_WhenNullDataCellIsAdded_ThenRowContainsEmptyCell()
        {
            normalRowList.Add((NormalCell)null);

            Assert.That(normalRowList[0][0].IsEmpty, Is.True);
        }
    }
}