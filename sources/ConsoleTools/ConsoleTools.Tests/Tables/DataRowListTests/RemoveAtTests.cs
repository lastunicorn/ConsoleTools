using DustInTheWind.ConsoleTools.Controls.Tables;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Tables.DataRowListTests
{
    [TestFixture]
    public class RemoveAtTests
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
        public void HavingADataRowListWithOneRow_WhenRemoveTheRow_ThenCountIs0()
        {
            DataRow dataRow = new DataRow("value 1");
            dataRowList.Add(dataRow);

            dataRowList.RemoveAt(0);

            Assert.That(dataRowList.Count, Is.EqualTo(0));
        }

        [Test]
        public void HavingADataRowListWithTwoRows_WhenRemoveFirstRow_ThenCountIs1()
        {
            DataRow dataRow1 = new DataRow("value 1");
            DataRow dataRow2 = new DataRow("value 2");
            dataRowList.Add(dataRow1);
            dataRowList.Add(dataRow2);

            dataRowList.RemoveAt(0);

            Assert.That(dataRowList.Count, Is.EqualTo(1));
        }

        [Test]
        public void HavingADataRowListWithTwoRows_WhenRemoveFirstRow_ThenSecondRowIsStillInList()
        {
            DataRow dataRow1 = new DataRow("value 1");
            DataRow dataRow2 = new DataRow("value 2");
            dataRowList.Add(dataRow1);
            dataRowList.Add(dataRow2);

            dataRowList.RemoveAt(0);

            Assert.That(dataRowList[0], Is.EqualTo(dataRow2));
        }

        [Test]
        public void HavingADataRowListWithTwoRows_WhenRemoveSecondRow_ThenCountIs1()
        {
            DataRow dataRow1 = new DataRow("value 1");
            DataRow dataRow2 = new DataRow("value 2");
            dataRowList.Add(dataRow1);
            dataRowList.Add(dataRow2);

            dataRowList.RemoveAt(1);

            Assert.That(dataRowList.Count, Is.EqualTo(1));
        }

        [Test]
        public void HavingADataRowListWithTwoRows_WhenRemoveSecondRow_ThenFirstRowIsStillInList()
        {
            DataRow dataRow1 = new DataRow("value 1");
            DataRow dataRow2 = new DataRow("value 2");
            dataRowList.Add(dataRow1);
            dataRowList.Add(dataRow2);

            dataRowList.RemoveAt(1);

            Assert.That(dataRowList[0], Is.EqualTo(dataRow1));
        }
    }
}