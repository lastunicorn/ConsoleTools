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

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.NormalRowListTests
{
    [TestFixture]
    public class RemoveAtTests
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
        public void HavingANormalRowListWithOneRow_WhenRemoveTheRow_ThenCountIs0()
        {
            ContentRow contentRow = new ContentRow("value 1");
            contentRowList.Add(contentRow);

            contentRowList.RemoveAt(0);

            Assert.That(contentRowList.Count, Is.EqualTo(0));
        }

        [Test]
        public void HavingANormalRowListWithTwoRows_WhenRemoveFirstRow_ThenCountIs1()
        {
            ContentRow contentRow1 = new ContentRow("value 1");
            ContentRow contentRow2 = new ContentRow("value 2");
            contentRowList.Add(contentRow1);
            contentRowList.Add(contentRow2);

            contentRowList.RemoveAt(0);

            Assert.That(contentRowList.Count, Is.EqualTo(1));
        }

        [Test]
        public void HavingANormalRowListWithTwoRows_WhenRemoveFirstRow_ThenSecondRowIsStillInList()
        {
            ContentRow contentRow1 = new ContentRow("value 1");
            ContentRow contentRow2 = new ContentRow("value 2");
            contentRowList.Add(contentRow1);
            contentRowList.Add(contentRow2);

            contentRowList.RemoveAt(0);

            Assert.That(contentRowList[0], Is.EqualTo(contentRow2));
        }

        [Test]
        public void HavingANormalRowListWithTwoRows_WhenRemoveSecondRow_ThenCountIs1()
        {
            ContentRow contentRow1 = new ContentRow("value 1");
            ContentRow contentRow2 = new ContentRow("value 2");
            contentRowList.Add(contentRow1);
            contentRowList.Add(contentRow2);

            contentRowList.RemoveAt(1);

            Assert.That(contentRowList.Count, Is.EqualTo(1));
        }

        [Test]
        public void HavingANormalRowListWithTwoRows_WhenRemoveSecondRow_ThenFirstRowIsStillInList()
        {
            ContentRow contentRow1 = new ContentRow("value 1");
            ContentRow contentRow2 = new ContentRow("value 2");
            contentRowList.Add(contentRow1);
            contentRowList.Add(contentRow2);

            contentRowList.RemoveAt(1);

            Assert.That(contentRowList[0], Is.EqualTo(contentRow1));
        }
    }
}