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

using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Controls.Tables;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.RenderingTests;

[TestFixture]
public class CellAlignmentPerColumnTests : TestsBase
{
    [Test]
    public void column_0_is_aligned_to_Right()
    {
        DataGrid dataGrid = new("This is a cell alignment test")
        {
            HeaderRow =
            {
                IsVisible = false
            }
        };

        dataGrid.Columns.Add(new Column("Col 0")
        {
            CellHorizontalAlignment = HorizontalAlignment.Right
        });

        dataGrid.Columns.Add(new Column("Col 1"));

        dataGrid.Columns.Add(new Column("Col 2"));

        dataGrid.Rows.Add("0,0", "0,1", "0,2");
        dataGrid.Rows.Add("1,0", "1,1", "1,2");
        dataGrid.Rows.Add("2,0", "2,1", "2,2");

        string expected = GetResourceFileContent("01-column-0-right.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void column_0_is_aligned_to_Right_and_cell_1_0_is_aligned_to_Left()
    {
        DataGrid dataGrid = new("This is a cell alignment test")
        {
            HeaderRow =
            {
                IsVisible = false
            }
        };

        dataGrid.Columns.Add(new Column("Col 0")
        {
            CellHorizontalAlignment = HorizontalAlignment.Right
        });

        dataGrid.Columns.Add(new Column("Col 1"));

        dataGrid.Columns.Add(new Column("Col 2"));

        dataGrid.Rows.Add("0,0", "0,1", "0,2");
        dataGrid.Rows.Add(new ContentCell[] { new("1,0", HorizontalAlignment.Left), "1,1", "1,2" });
        dataGrid.Rows.Add("2,0", "2,1", "2,2");

        string expected = GetResourceFileContent("02-column-0-right-cell-1-0-left.txt");
        dataGrid.IsEqualTo(expected);
    }
}