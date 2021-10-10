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
            DataCell dataCell = new DataCell();

            int actual = dataCell.CalculatePaddingRight();

            Assert.That(actual, Is.EqualTo(1));
        }

        [Test]
        public void HavingCellWithPaddingRight5_WhenCalculatingPaddingRight_ThenPaddingRightIs5()
        {
            DataCell dataCell = new DataCell
            {
                PaddingRight = 5
            };

            int actual = dataCell.CalculatePaddingRight();

            Assert.That(actual, Is.EqualTo(5));
        }

        [Test]
        public void HavingRowWithPaddingRight5_WhenCalculatingPaddingRight_ThenPaddingRightIs5()
        {
            DataCell dataCell = new DataCell();
            DataRow dataRow = new DataRow
            {
                CellPaddingRight = 5
            };
            dataRow.AddCell(dataCell);

            int actual = dataCell.CalculatePaddingRight();

            Assert.That(actual, Is.EqualTo(5));
        }

        [Test]
        public void HavingDataGridWithPaddingRight5_WhenCalculatingPaddingRight_ThenPaddingRightIs5()
        {
            DataCell dataCell = new DataCell();

            DataRow dataRow = new DataRow();
            dataRow.AddCell(dataCell);

            DataGrid dataGrid = new DataGrid
            {
                CellPaddingRight = 5
            };
            dataGrid.Rows.Add(dataRow);

            int actual = dataCell.CalculatePaddingRight();

            Assert.That(actual, Is.EqualTo(5));
        }

        [Test]
        public void HavingColumnWithPaddingRight5_WhenCalculatingPaddingRight_ThenPaddingRightIs5()
        {
            DataCell dataCell = new DataCell();

            DataRow dataRow = new DataRow();
            dataRow.AddCell(dataCell);

            DataGrid dataGrid = new DataGrid();
            dataGrid.Rows.Add(dataRow);

            Column column = new Column
            {
                CellPaddingRight = 5
            };
            dataGrid.Columns.Add(column);

            int actual = dataCell.CalculatePaddingRight();

            Assert.That(actual, Is.EqualTo(5));
        }
    }
}