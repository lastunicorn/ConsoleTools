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

using DustInTheWind.ConsoleTools.Controls.Tables;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.ContentCellTests;

[TestFixture]
public class CalculatePaddingRightRight
{
    [Test]
    public void HavingCellWithNoPadding_WhenCalculatingPaddingRight_ThenPaddingRightIs1()
    {
        ContentCell contentCell = new();

        int actual = contentCell.CalculatePaddingRight();

        Assert.That(actual, Is.EqualTo(1));
    }

    [Test]
    public void HavingCellWithPaddingRight5_WhenCalculatingPaddingRight_ThenPaddingRightIs5()
    {
        ContentCell contentCell = new()
        {
            PaddingRight = 5
        };

        int actual = contentCell.CalculatePaddingRight();

        Assert.That(actual, Is.EqualTo(5));
    }

    [Test]
    public void HavingRowWithPaddingRight5_WhenCalculatingPaddingRight_ThenPaddingRightIs5()
    {
        ContentCell contentCell = new();
        ContentRow contentRow = new()
        {
            CellPaddingRight = 5
        };
        contentRow.AddCell(contentCell);

        int actual = contentCell.CalculatePaddingRight();

        Assert.That(actual, Is.EqualTo(5));
    }

    [Test]
    public void HavingDataGridWithPaddingRight5_WhenCalculatingPaddingRight_ThenPaddingRightIs5()
    {
        ContentCell contentCell = new();

        ContentRow contentRow = new();
        contentRow.AddCell(contentCell);

        DataGrid dataGrid = new()
        {
            CellPaddingRight = 5
        };
        dataGrid.Rows.Add(contentRow);

        int actual = contentCell.CalculatePaddingRight();

        Assert.That(actual, Is.EqualTo(5));
    }

    [Test]
    public void HavingColumnWithPaddingRight5_WhenCalculatingPaddingRight_ThenPaddingRightIs5()
    {
        ContentCell contentCell = new();

        ContentRow contentRow = new();
        contentRow.AddCell(contentCell);

        DataGrid dataGrid = new();
        dataGrid.Rows.Add(contentRow);

        Column column = new()
        {
            CellPaddingRight = 5
        };
        dataGrid.Columns.Add(column);

        int actual = contentCell.CalculatePaddingRight();

        Assert.That(actual, Is.EqualTo(5));
    }
}