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
            NormalCell normalCell = new NormalCell();

            int actual = normalCell.CalculatePaddingRight();

            Assert.That(actual, Is.EqualTo(1));
        }

        [Test]
        public void HavingCellWithPaddingRight5_WhenCalculatingPaddingRight_ThenPaddingRightIs5()
        {
            NormalCell normalCell = new NormalCell
            {
                PaddingRight = 5
            };

            int actual = normalCell.CalculatePaddingRight();

            Assert.That(actual, Is.EqualTo(5));
        }

        [Test]
        public void HavingRowWithPaddingRight5_WhenCalculatingPaddingRight_ThenPaddingRightIs5()
        {
            NormalCell normalCell = new NormalCell();
            NormalRow normalRow = new NormalRow
            {
                CellPaddingRight = 5
            };
            normalRow.AddCell(normalCell);

            int actual = normalCell.CalculatePaddingRight();

            Assert.That(actual, Is.EqualTo(5));
        }

        [Test]
        public void HavingDataGridWithPaddingRight5_WhenCalculatingPaddingRight_ThenPaddingRightIs5()
        {
            NormalCell normalCell = new NormalCell();

            NormalRow normalRow = new NormalRow();
            normalRow.AddCell(normalCell);

            DataGrid dataGrid = new DataGrid
            {
                CellPaddingRight = 5
            };
            dataGrid.Rows.Add(normalRow);

            int actual = normalCell.CalculatePaddingRight();

            Assert.That(actual, Is.EqualTo(5));
        }

        [Test]
        public void HavingColumnWithPaddingRight5_WhenCalculatingPaddingRight_ThenPaddingRightIs5()
        {
            NormalCell normalCell = new NormalCell();

            NormalRow normalRow = new NormalRow();
            normalRow.AddCell(normalCell);

            DataGrid dataGrid = new DataGrid();
            dataGrid.Rows.Add(normalRow);

            Column column = new Column
            {
                CellPaddingRight = 5
            };
            dataGrid.Columns.Add(column);

            int actual = normalCell.CalculatePaddingRight();

            Assert.That(actual, Is.EqualTo(5));
        }
    }
}