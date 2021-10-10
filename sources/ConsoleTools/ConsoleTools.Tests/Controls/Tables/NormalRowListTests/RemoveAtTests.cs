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

using DustInTheWind.ConsoleTools.Controls.Tables;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.NormalRowListTests
{
    [TestFixture]
    public class RemoveAtTests
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
        public void HavingANormalRowListWithOneRow_WhenRemoveTheRow_ThenCountIs0()
        {
            DataRow dataRow = new DataRow("value 1");
            dataRowList.Add(dataRow);

            dataRowList.RemoveAt(0);

            Assert.That(dataRowList.Count, Is.EqualTo(0));
        }

        [Test]
        public void HavingANormalRowListWithTwoRows_WhenRemoveFirstRow_ThenCountIs1()
        {
            DataRow dataRow1 = new DataRow("value 1");
            DataRow dataRow2 = new DataRow("value 2");
            dataRowList.Add(dataRow1);
            dataRowList.Add(dataRow2);

            dataRowList.RemoveAt(0);

            Assert.That(dataRowList.Count, Is.EqualTo(1));
        }

        [Test]
        public void HavingANormalRowListWithTwoRows_WhenRemoveFirstRow_ThenSecondRowIsStillInList()
        {
            DataRow dataRow1 = new DataRow("value 1");
            DataRow dataRow2 = new DataRow("value 2");
            dataRowList.Add(dataRow1);
            dataRowList.Add(dataRow2);

            dataRowList.RemoveAt(0);

            Assert.That(dataRowList[0], Is.EqualTo(dataRow2));
        }

        [Test]
        public void HavingANormalRowListWithTwoRows_WhenRemoveSecondRow_ThenCountIs1()
        {
            DataRow dataRow1 = new DataRow("value 1");
            DataRow dataRow2 = new DataRow("value 2");
            dataRowList.Add(dataRow1);
            dataRowList.Add(dataRow2);

            dataRowList.RemoveAt(1);

            Assert.That(dataRowList.Count, Is.EqualTo(1));
        }

        [Test]
        public void HavingANormalRowListWithTwoRows_WhenRemoveSecondRow_ThenFirstRowIsStillInList()
        {
            DataRow dataRow1 = new DataRow("value 1");
            DataRow dataRow2 = new DataRow("value 2");
            dataRowList.Add(dataRow1);
            dataRowList.Add(dataRow2);

            dataRowList.RemoveAt(1);

            Assert.That(dataRowList[0], Is.EqualTo(dataRow1));
        }
    }
}