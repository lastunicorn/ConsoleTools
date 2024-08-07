// ConsoleTools
// Copyright (C) 2017-2024 Dust in the Wind
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

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.RenderingModel.RowXTests;

[TestFixture]
public class CalculateLayoutTests
{
    [Test]
    public void HavingARowContainingACellAndNoBorder_WhenCalculatingLayout_ThenRowSizeIsEqualToTheCellSize()
    {
        CellX cellX1 = new()
        {
            Content = "value 1"
        };
        cellX1.CalculateLayout();
        RowX rowX = new()
        {
            Cells = new List<CellX>
            {
                cellX1
            }
        };

        rowX.CalculateLayout();

        Size expected = new(7, 1);
        Assert.AreEqual(expected, rowX.Size);
    }

    [Test]
    public void HavingARowContainingACellAndBorder_WhenCalculatingLayout_ThenRowSizeIsEqualToTheCellSizePlusBorder()
    {
        CellX cellX1 = new()
        {
            Content = "value 1"
        };
        cellX1.CalculateLayout();
        RowBorderX rowBorderX = new()
        {
            Template = BorderTemplate.PlusMinusBorderTemplate
        };
        RowX rowX = new()
        {
            Cells = new List<CellX>
            {
                cellX1
            },
            Border = rowBorderX
        };

        rowX.CalculateLayout();

        Size expected = new(9, 1);
        Assert.AreEqual(expected, rowX.Size);
    }

    [Test]
    public void HavingARowContainingBorderAndNoCell_WhenCalculatingLayout_ThenRowSizeIsEqualToTwiceTheBorderWidth()
    {
        RowBorderX rowBorderX = new()
        {
            Template = BorderTemplate.PlusMinusBorderTemplate
        };
        RowX rowX = new()
        {
            Cells = new List<CellX>(),
            Border = rowBorderX
        };

        rowX.CalculateLayout();

        Size expected = new(2, 0);
        Assert.AreEqual(expected, rowX.Size);
    }

    [Test]
    public void HavingARowContainingBorderAndNoCellList_WhenCalculatingLayout_ThenRowSizeIsEqualToTwiceTheBorderWidth()
    {
        RowBorderX rowBorderX = new()
        {
            Template = BorderTemplate.PlusMinusBorderTemplate
        };
        RowX rowX = new()
        {
            Border = rowBorderX
        };

        rowX.CalculateLayout();

        Size expected = new(2, 0);
        Assert.AreEqual(expected, rowX.Size);
    }
}