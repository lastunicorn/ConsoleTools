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

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.TableRenderTests;

[TestFixture]
public class CellAlignmentPerCellTests : TestsBase
{
    [Test]
    public void by_default_cell_content_is_aligned_to_left()
    {
        DataGrid dataGrid = new("This is a cell alignment test");
        dataGrid.Rows.Add("0,0", "0,1", "0,2");

        string expected = GetResourceFileContent("01-no-alignment.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void cell_0_1_is_alignment_Default_after_cell_creation()
    {
        DataGrid dataGrid = new("This is a cell alignment test");
        dataGrid.Rows.Add("0,0", "0,1", "0,2");
        dataGrid[0][1].HorizontalAlignment = HorizontalAlignment.Default;

        string expected = GetResourceFileContent("02-alignment-default-after.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void cell_0_1_is_alignment_Left_after_cell_creation()
    {
        DataGrid dataGrid = new("This is a cell alignment test");
        dataGrid.Rows.Add("0,0", "0,1", "0,2");
        dataGrid[0][1].HorizontalAlignment = HorizontalAlignment.Left;

        string expected = GetResourceFileContent("03-alignment-left-after.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void cell_0_1_is_alignment_Center_after_cell_creation()
    {
        DataGrid dataGrid = new("This is a cell alignment test");
        dataGrid.Rows.Add("0,0", "0,1", "0,2");
        dataGrid[0][1].HorizontalAlignment = HorizontalAlignment.Center;

        string expected = GetResourceFileContent("04-alignment-center-after.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void cell_0_1_is_alignment_Right_after_cell_creation()
    {
        DataGrid dataGrid = new("This is a cell alignment test");
        dataGrid.Rows.Add("0,0", "0,1", "0,2");
        dataGrid[0][1].HorizontalAlignment = HorizontalAlignment.Right;

        string expected = GetResourceFileContent("05-alignment-right-after.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void cell_1_1_is_alignment_Default_at_cell_creation()
    {
        DataGrid dataGrid = new("This is a cell alignment test");
        dataGrid.Rows.Add("0,0", "0,1", "0,2");
        dataGrid.Rows.Add(new ContentCell[] { new("1,0", HorizontalAlignment.Default), "1,1", "1,2" });
        dataGrid.Rows.Add("2,0", "2,1", "2,2");

        string expected = GetResourceFileContent("06-alignment-default-atcreation.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void cell_1_1_is_alignment_Left_at_cell_creation()
    {
        DataGrid dataGrid = new("This is a cell alignment test");
        dataGrid.Rows.Add("0,0", "0,1", "0,2");
        dataGrid.Rows.Add(new ContentCell[] { new("1,0", HorizontalAlignment.Left), "1,1", "1,2" });
        dataGrid.Rows.Add("2,0", "2,1", "2,2");

        string expected = GetResourceFileContent("07-alignment-left-atcreation.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void cell_1_1_is_alignment_Center_at_cell_creation()
    {
        DataGrid dataGrid = new("This is a cell alignment test");
        dataGrid.Rows.Add("0,0", "0,1", "0,2");
        dataGrid.Rows.Add(new ContentCell[] { new("1,0", HorizontalAlignment.Center), "1,1", "1,2" });
        dataGrid.Rows.Add("2,0", "2,1", "2,2");

        string expected = GetResourceFileContent("08-alignment-center-atcreation.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void cell_1_1_is_alignment_Right_at_cell_creation()
    {
        DataGrid dataGrid = new("This is a cell alignment test");
        dataGrid.Rows.Add("0,0", "0,1", "0,2");
        dataGrid.Rows.Add(new ContentCell[] { new("1,0", HorizontalAlignment.Right), "1,1", "1,2" });
        dataGrid.Rows.Add("2,0", "2,1", "2,2");

        string expected = GetResourceFileContent("09-alignment-right-atcreation.txt");
        dataGrid.IsEqualTo(expected);
    }
}