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

using System;
using System.Collections.Generic;
using DustInTheWind.ConsoleTools.Controls.Tables;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Tables.NormalRowListTests
{
    [TestFixture]
    public class AddCellEnumerationTests
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
            IEnumerable<NormalCell> cells = new List<NormalCell>
            {
                new NormalCell(),
                new NormalCell(),
                new NormalCell()
            };

            normalRowList.Add(cells);

            Assert.That(normalRowList.Count, Is.EqualTo(1));
        }

        [Test]
        public void HavingAnEmptyNormalRowList_WhenThreeCellsAreAdded_ThenRowContainsTheThreeCells()
        {
            IEnumerable<NormalCell> cells = new List<NormalCell>
            {
                new NormalCell(),
                new NormalCell(),
                new NormalCell()
            };

            normalRowList.Add(cells);

            Assert.That(normalRowList[0], Is.EqualTo(cells));
        }

        [Test]
        public void HavingAnEmptyNormalRowList_WhenNullDataCellEnumerationIsAdded_ThenThrows()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                normalRowList.Add((List<NormalCell>)null);
            });
        }
    }
}