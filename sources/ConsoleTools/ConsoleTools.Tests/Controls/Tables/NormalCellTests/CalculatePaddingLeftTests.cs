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
            DataCell dataCell = new DataCell();

            int actual = dataCell.CalculatePaddingLeft();

            Assert.That(actual, Is.EqualTo(1));
        }

        [Test]
        public void HavingCellWithPaddingLeft5_WhenCalculatingPaddingLeft_ThenPaddingLeftIs5()
        {
            DataCell dataCell = new DataCell
            {
                PaddingLeft = 5
            };

            int actual = dataCell.CalculatePaddingLeft();

            Assert.That(actual, Is.EqualTo(5));
        }

        [Test]
        public void HavingRowWithPaddingLeft5_WhenCalculatingPaddingLeft_ThenPaddingLeftIs5()
        {
            DataCell dataCell = new DataCell();
            DataRow dataRow = new DataRow
            {
                CellPaddingLeft = 5
            };
            dataRow.AddCell(dataCell);

            int actual = dataCell.CalculatePaddingLeft();

            Assert.That(actual, Is.EqualTo(5));
        }

        [Test]
        public void HavingDataGridWithPaddingLeft5_WhenCalculatingPaddingLeft_ThenPaddingLeftIs5()
        {
            DataCell dataCell = new DataCell();

            DataRow dataRow = new DataRow();
            dataRow.AddCell(dataCell);

            DataGrid dataGrid = new DataGrid
            {
                CellPaddingLeft = 5
            };
            dataGrid.Rows.Add(dataRow);

            int actual = dataCell.CalculatePaddingLeft();

            Assert.That(actual, Is.EqualTo(5));
        }

        [Test]
        public void HavingColumnWithPaddingLeft5_WhenCalculatingPaddingLeft_ThenPaddingLeftIs5()
        {
            DataCell dataCell = new DataCell();

            DataRow dataRow = new DataRow();
            dataRow.AddCell(dataCell);

            DataGrid dataGrid = new DataGrid();
            dataGrid.Rows.Add(dataRow);

            Column column = new Column
            {
                CellPaddingLeft = 5
            };
            dataGrid.Columns.Add(column);

            int actual = dataCell.CalculatePaddingLeft();

            Assert.That(actual, Is.EqualTo(5));
        }
    }
}