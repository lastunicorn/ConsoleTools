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

// --------------------------------------------------------------------------------
// Bugs or feature requests
// --------------------------------------------------------------------------------
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new/choose

using DustInTheWind.ConsoleTools.Controls.Tables;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.ContentCellTests
{
    [TestFixture]
    public class CalculatePaddingLeftTests
    {
        [Test]
        public void HavingCellWithNoPadding_WhenCalculatingPaddingLeft_ThenPaddingLeftIs1()
        {
            ContentCell contentCell = new ContentCell();

            int actual = contentCell.CalculatePaddingLeft();

            Assert.That(actual, Is.EqualTo(1));
        }

        [Test]
        public void HavingCellWithPaddingLeft5_WhenCalculatingPaddingLeft_ThenPaddingLeftIs5()
        {
            ContentCell contentCell = new ContentCell
            {
                PaddingLeft = 5
            };

            int actual = contentCell.CalculatePaddingLeft();

            Assert.That(actual, Is.EqualTo(5));
        }

        [Test]
        public void HavingRowWithPaddingLeft5_WhenCalculatingPaddingLeft_ThenPaddingLeftIs5()
        {
            ContentCell contentCell = new ContentCell();
            ContentRow contentRow = new ContentRow
            {
                CellPaddingLeft = 5
            };
            contentRow.AddCell(contentCell);

            int actual = contentCell.CalculatePaddingLeft();

            Assert.That(actual, Is.EqualTo(5));
        }

        [Test]
        public void HavingDataGridWithPaddingLeft5_WhenCalculatingPaddingLeft_ThenPaddingLeftIs5()
        {
            ContentCell contentCell = new ContentCell();

            ContentRow contentRow = new ContentRow();
            contentRow.AddCell(contentCell);

            DataGrid dataGrid = new DataGrid
            {
                CellPaddingLeft = 5
            };
            dataGrid.Rows.Add(contentRow);

            int actual = contentCell.CalculatePaddingLeft();

            Assert.That(actual, Is.EqualTo(5));
        }

        [Test]
        public void HavingColumnWithPaddingLeft5_WhenCalculatingPaddingLeft_ThenPaddingLeftIs5()
        {
            ContentCell contentCell = new ContentCell();

            ContentRow contentRow = new ContentRow();
            contentRow.AddCell(contentCell);

            DataGrid dataGrid = new DataGrid();
            dataGrid.Rows.Add(contentRow);

            Column column = new Column
            {
                CellPaddingLeft = 5
            };
            dataGrid.Columns.Add(column);

            int actual = contentCell.CalculatePaddingLeft();

            Assert.That(actual, Is.EqualTo(5));
        }
    }
}