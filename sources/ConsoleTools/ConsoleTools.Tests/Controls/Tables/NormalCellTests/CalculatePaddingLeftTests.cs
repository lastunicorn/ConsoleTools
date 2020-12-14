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
            NormalCell normalCell = new NormalCell();

            int actual = normalCell.CalculatePaddingLeft();

            Assert.That(actual, Is.EqualTo(1));
        }

        [Test]
        public void HavingCellWithPaddingLeft5_WhenCalculatingPaddingLeft_ThenPaddingLeftIs5()
        {
            NormalCell normalCell = new NormalCell
            {
                PaddingLeft = 5
            };

            int actual = normalCell.CalculatePaddingLeft();

            Assert.That(actual, Is.EqualTo(5));
        }

        [Test]
        public void HavingRowWithPaddingLeft5_WhenCalculatingPaddingLeft_ThenPaddingLeftIs5()
        {
            NormalCell normalCell = new NormalCell();
            NormalRow normalRow = new NormalRow
            {
                CellPaddingLeft = 5
            };
            normalRow.AddCell(normalCell);

            int actual = normalCell.CalculatePaddingLeft();

            Assert.That(actual, Is.EqualTo(5));
        }

        [Test]
        public void HavingDataGridWithPaddingLeft5_WhenCalculatingPaddingLeft_ThenPaddingLeftIs5()
        {
            NormalCell normalCell = new NormalCell();

            NormalRow normalRow = new NormalRow();
            normalRow.AddCell(normalCell);

            DataGrid dataGrid = new DataGrid
            {
                CellPaddingLeft = 5
            };
            dataGrid.Rows.Add(normalRow);

            int actual = normalCell.CalculatePaddingLeft();

            Assert.That(actual, Is.EqualTo(5));
        }

        [Test]
        public void HavingColumnWithPaddingLeft5_WhenCalculatingPaddingLeft_ThenPaddingLeftIs5()
        {
            NormalCell normalCell = new NormalCell();

            NormalRow normalRow = new NormalRow();
            normalRow.AddCell(normalCell);

            DataGrid dataGrid = new DataGrid();
            dataGrid.Rows.Add(normalRow);

            Column column = new Column
            {
                CellPaddingLeft = 5
            };
            dataGrid.Columns.Add(column);

            int actual = normalCell.CalculatePaddingLeft();

            Assert.That(actual, Is.EqualTo(5));
        }
    }
}