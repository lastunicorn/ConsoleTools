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
using DustInTheWind.ConsoleTools.Controls.Tables;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.NormalRowListTests
{
    [TestFixture]
    public class AddNormalRowTests
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
        public void HavingAnEmptyNormalRowList_WhenOneNormalRowIsAdded_ThenRowCountIs1()
        {
            DataRow dataRow = new DataRow();

            dataRowList.Add(dataRow);

            Assert.That(dataRowList.Count, Is.EqualTo(1));
        }

        [Test]
        public void HavingAnEmptyNormalRowList_WhenOneNormalRowIsAdded_ThenListContainsTheNormalRow()
        {
            DataRow dataRow = new DataRow();

            dataRowList.Add(dataRow);

            Assert.That(dataRowList[0], Is.SameAs(dataRow));
        }

        [Test]
        public void HavingAnEmptyNormalRowList_WhenOneNormalRowIsAdded_ThenDataGridIsSetToNormalRow()
        {
            DataRow dataRow = new DataRow();

            dataRowList.Add(dataRow);

            Assert.That(dataRow.ParentDataGrid, Is.SameAs(dataGrid));
        }

        [Test]
        public void HavingAnEmptyNormalRowList_WhenNullNormalRowIsAdded_ThenThrows()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                dataRowList.Add((DataRow)null);
            });
        }
    }
}