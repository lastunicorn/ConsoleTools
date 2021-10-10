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
using System.Linq;
using DustInTheWind.ConsoleTools.Controls.Tables;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.NormalRowListTests
{
    [TestFixture]
    public class AddStringEnumerationTests
    {
        private DataGrid dataGrid;
        private DataRowList dataRowList;

        [SetUp]
        public void SetUp()
        {
            dataGrid = new DataGrid();
            dataRowList = new DataRowList(dataGrid);
        }

        [Test]
        public void HavingAnEmptyNormalRowList_WhenThreeStringsAreAdded_ThenRowCountIs1()
        {
            IEnumerable<string> values = new List<string>
            {
                "value 1", "value 2", "value 3"
            };

            dataRowList.Add(values);

            Assert.That(dataRowList.Count, Is.EqualTo(1));
        }

        [Test]
        public void HavingAnEmptyNormalRowList_WhenThreeStringsAreAdded_ThenRowContainsThreeCellsWithCorrectValues()
        {
            IEnumerable<string> values = new List<string>
            {
                "value 1", "value 2", "value 3"
            };

            dataRowList.Add(values);

            IEnumerable<string> actual = dataRowList[0]
                .Select(x => x.Content.ToString());
            Assert.That(actual, Is.EqualTo(values));
        }

        [Test]
        public void HavingAnEmptyNormalRowList_WhenNullStringEnumerationIsAdded_ThenThrows()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                dataRowList.Add((IEnumerable<string>)null);
            });
        }
    }
}