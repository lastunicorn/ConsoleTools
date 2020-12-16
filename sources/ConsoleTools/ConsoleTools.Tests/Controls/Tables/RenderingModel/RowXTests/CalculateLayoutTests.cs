using System.Collections.Generic;
using DustInTheWind.ConsoleTools.Controls.Tables;
using DustInTheWind.ConsoleTools.Controls.Tables.RenderingModel;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.RenderingModel.RowXTests
{
    [TestFixture]
    public class CalculateLayoutTests
    {
        [Test]
        public void HavingARowContainingACellAndNoBorder_WhenCalculatingLayout_ThenRowSizeIsEqualToTheCellSize()
        {
            CellX cellX1 = new CellX
            {
                Content = "value 1"
            };
            cellX1.CalculateLayout();
            RowX rowX = new RowX
            {
                Cells = new List<CellX>
                {
                    cellX1
                }
            };

            rowX.CalculateLayout();

            Size expected = new Size(7, 1);
            Assert.AreEqual(expected, rowX.Size);
        }

        [Test]
        public void HavingARowContainingACellAndBorder_WhenCalculatingLayout_ThenRowSizeIsEqualToTheCellSizePlusBorder()
        {
            CellX cellX1 = new CellX
            {
                Content = "value 1"
            };
            cellX1.CalculateLayout();
            DataGridBorderX dataGridBorderX = new DataGridBorderX
            {
                Template = BorderTemplate.PlusMinusBorderTemplate
            };
            RowX rowX = new RowX
            {
                Cells = new List<CellX>
                {
                    cellX1
                },
                Border = dataGridBorderX
            };

            rowX.CalculateLayout();

            Size expected = new Size(9, 1);
            Assert.AreEqual(expected, rowX.Size);
        }

        [Test]
        public void HavingARowContainingBorderAndNoCell_WhenCalculatingLayout_ThenRowSizeIsEqualToTwiceTheBorderWidth()
        {
            DataGridBorderX dataGridBorderX = new DataGridBorderX
            {
                Template = BorderTemplate.PlusMinusBorderTemplate
            };
            RowX rowX = new RowX
            {
                Cells = new List<CellX>(),
                Border = dataGridBorderX
            };

            rowX.CalculateLayout();

            Size expected = new Size(2, 0);
            Assert.AreEqual(expected, rowX.Size);
        }

        [Test]
        public void HavingARowContainingBorderAndNoCellList_WhenCalculatingLayout_ThenRowSizeIsEqualToTwiceTheBorderWidth()
        {
            DataGridBorderX dataGridBorderX = new DataGridBorderX
            {
                Template = BorderTemplate.PlusMinusBorderTemplate
            };
            RowX rowX = new RowX
            {
                Border = dataGridBorderX
            };

            rowX.CalculateLayout();

            Size expected = new Size(2, 0);
            Assert.AreEqual(expected, rowX.Size);
        }
    }
}