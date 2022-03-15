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

using System.Collections.Generic;
using DustInTheWind.ConsoleTools.Controls;
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