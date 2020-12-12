using DustInTheWind.ConsoleTools.Controls.Tables;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Tables.DataRowListTests
{
    [TestFixture]
    public class ClearTests
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
        public void HavingAnEmptyDataRowList_WhenClear_ThenCountIs0()
        {
            dataRowList.Clear();

            Assert.That(dataRowList.Count, Is.EqualTo(0));
        }

        [Test]
        public void HavingADataRowListWithOneRow_WhenClear_ThenCountIs0()
        {
            dataRowList.Add(new DataRow());

            dataRowList.Clear();

            Assert.That(dataRowList.Count, Is.EqualTo(0));
        }

        [Test]
        public void HavingADataRowListWithTwoRows_WhenClear_ThenCountIs0()
        {
            dataRowList.Add(new DataRow());
            dataRowList.Add(new DataRow());

            dataRowList.Clear();

            Assert.That(dataRowList.Count, Is.EqualTo(0));
        }
    }
}