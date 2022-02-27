using DustInTheWind.ConsoleTools.Controls.Tables;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.NormalCellTests
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