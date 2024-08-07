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
public class CellMultilineTests : TestsBase
{
    [Test]
    public void HavingMultipleLinesSeparatedByLF_WhenRendered_ThenCellContainsMultipleTextLines()
    {
        DataGrid dataGrid = new();
        dataGrid.Title = "Multiline cell tests";
        dataGrid.Rows.Add("first line\nsecond line", "single line", "one\ntwo\nthree");

        string expected = GetResourceFileContent("01-lf.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingMultipleLinesSeparatedByCR_WhenRendered_ThenCellContainsMultipleTextLines()
    {
        DataGrid dataGrid = new();
        dataGrid.Title = "Multiline cell tests";
        dataGrid.Rows.Add("first line\rsecond line", "single line", "one\rtwo\rthree");

        string expected = GetResourceFileContent("02-cr.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingMultipleLinesSeparatedByCRLF_WhenRendered_ThenCellContainsMultipleTextLines()
    {
        DataGrid dataGrid = new();
        dataGrid.Title = "Multiline cell tests";
        dataGrid.Rows.Add("first line\r\nsecond line", "single line", "one\r\ntwo\r\nthree");

        string expected = GetResourceFileContent("03-crlf.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void HavingMultilineTextInstances_WhenRendered_ThenCellContainsMultipleTextLines()
    {
        DataGrid dataGrid = new();
        dataGrid.Title = "Multiline cell tests";
        dataGrid.Rows.Add(new MultilineText("first line", "second line"), new MultilineText("single line"), new MultilineText("one", "two", "three"));

        string expected = GetResourceFileContent("04-multiline-objects.txt");
        dataGrid.IsEqualTo(expected);
    }
}