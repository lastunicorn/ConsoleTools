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

namespace DustInTheWind.ConsoleTools.Tests.Tables.NormalRowListTests
{
    [TestFixture]
    public class RemoveAtTests
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
        public void HavingANormalRowListWithOneRow_WhenRemoveTheRow_ThenCountIs0()
        {
            NormalRow normalRow = new NormalRow("value 1");
            normalRowList.Add(normalRow);

            normalRowList.RemoveAt(0);

            Assert.That(normalRowList.Count, Is.EqualTo(0));
        }

        [Test]
        public void HavingANormalRowListWithTwoRows_WhenRemoveFirstRow_ThenCountIs1()
        {
            NormalRow normalRow1 = new NormalRow("value 1");
            NormalRow normalRow2 = new NormalRow("value 2");
            normalRowList.Add(normalRow1);
            normalRowList.Add(normalRow2);

            normalRowList.RemoveAt(0);

            Assert.That(normalRowList.Count, Is.EqualTo(1));
        }

        [Test]
        public void HavingANormalRowListWithTwoRows_WhenRemoveFirstRow_ThenSecondRowIsStillInList()
        {
            NormalRow normalRow1 = new NormalRow("value 1");
            NormalRow normalRow2 = new NormalRow("value 2");
            normalRowList.Add(normalRow1);
            normalRowList.Add(normalRow2);

            normalRowList.RemoveAt(0);

            Assert.That(normalRowList[0], Is.EqualTo(normalRow2));
        }

        [Test]
        public void HavingANormalRowListWithTwoRows_WhenRemoveSecondRow_ThenCountIs1()
        {
            NormalRow normalRow1 = new NormalRow("value 1");
            NormalRow normalRow2 = new NormalRow("value 2");
            normalRowList.Add(normalRow1);
            normalRowList.Add(normalRow2);

            normalRowList.RemoveAt(1);

            Assert.That(normalRowList.Count, Is.EqualTo(1));
        }

        [Test]
        public void HavingANormalRowListWithTwoRows_WhenRemoveSecondRow_ThenFirstRowIsStillInList()
        {
            NormalRow normalRow1 = new NormalRow("value 1");
            NormalRow normalRow2 = new NormalRow("value 2");
            normalRowList.Add(normalRow1);
            normalRowList.Add(normalRow2);

            normalRowList.RemoveAt(1);

            Assert.That(normalRowList[0], Is.EqualTo(normalRow1));
        }
    }
}