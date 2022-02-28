using DustInTheWind.ConsoleTools.Controls.Tables;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.NormalCellTests
{
    [TestFixture]
    public class CalculatePaddingRightRight
    {
        [Test]
        public void HavingCellWithNoPadding_WhenCalculatingPaddingRight_ThenPaddingRightIs1()
        {
            ContentCell contentCell = new ContentCell();

            int actual = contentCell.CalculatePaddingRight();

            Assert.That(actual, Is.EqualTo(1));
        }

        [Test]
        public void HavingCellWithPaddingRight5_WhenCalculatingPaddingRight_ThenPaddingRightIs5()
        {
            ContentCell contentCell = new ContentCell
            {
                PaddingRight = 5
            };

            int actual = contentCell.CalculatePaddingRight();

            Assert.That(actual, Is.EqualTo(5));
        }

        [Test]
        public void HavingRowWithPaddingRight5_WhenCalculatingPaddingRight_ThenPaddingRightIs5()
        {
            ContentCell contentCell = new ContentCell();
            ContentRow contentRow = new ContentRow
            {
                CellPaddingRight = 5
            };
            contentRow.AddCell(contentCell);

            int actual = contentCell.CalculatePaddingRight();

            Assert.That(actual, Is.EqualTo(5));
        }

        [Test]
        public void HavingDataGridWithPaddingRight5_WhenCalculatingPaddingRight_ThenPaddingRightIs5()
        {
            ContentCell contentCell = new ContentCell();

            ContentRow contentRow = new ContentRow();
            contentRow.AddCell(contentCell);

            DataGrid dataGrid = new DataGrid
            {
                CellPaddingRight = 5
            };
            dataGrid.Rows.Add(contentRow);

            int actual = contentCell.CalculatePaddingRight();

            Assert.That(actual, Is.EqualTo(5));
        }

        [Test]
        public void HavingColumnWithPaddingRight5_WhenCalculatingPaddingRight_ThenPaddingRightIs5()
        {
            ContentCell contentCell = new ContentCell();

            ContentRow contentRow = new ContentRow();
            contentRow.AddCell(contentCell);

            DataGrid dataGrid = new DataGrid();
            dataGrid.Rows.Add(contentRow);

            Column column = new Column
            {
                CellPaddingRight = 5
            };
            dataGrid.Columns.Add(column);

            int actual = contentCell.CalculatePaddingRight();

            Assert.That(actual, Is.EqualTo(5));
        }
    }
}