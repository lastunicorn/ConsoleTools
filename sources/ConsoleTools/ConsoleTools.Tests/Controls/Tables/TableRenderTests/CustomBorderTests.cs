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

using DustInTheWind.ConsoleTools.Controls.Tables;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.TableRenderTests;

[TestFixture]
public class CustomBorderTests : TestsBase
{
    [Test]
    public void render_simple_table_with_custom_border()
    {
        DataGrid dataGrid = new();
        dataGrid.BorderTemplate = BorderTemplate.DoubleLineBorderTemplate;
        dataGrid.Rows.Add("one", "ichi", "eins");
        dataGrid.Rows.Add("two", "ni", "zwei");
        dataGrid.Rows.Add("three", "san", "drei");

        string expected = GetResourceFileContent("01-doublelineborder.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void render_table_with_custom_border_and_title()
    {
        DataGrid dataGrid = new();
        dataGrid.Title = "My Title";
        dataGrid.BorderTemplate = BorderTemplate.DoubleLineBorderTemplate;
        dataGrid.Rows.Add("one", "ichi", "eins");
        dataGrid.Rows.Add("two", "ni", "zwei");
        dataGrid.Rows.Add("three", "san", "drei");

        string expected = GetResourceFileContent("02-doublelineborder-title.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void render_table_with_custom_border_and_headers()
    {
        DataGrid dataGrid = new();
        dataGrid.BorderTemplate = BorderTemplate.DoubleLineBorderTemplate;
        dataGrid.HeaderRow.IsVisible = true;
        dataGrid.Columns.Add(new Column("One"));
        dataGrid.Columns.Add(new Column("Two"));
        dataGrid.Columns.Add(new Column("Three"));
        dataGrid.Rows.Add("one", "ichi", "eins");
        dataGrid.Rows.Add("two", "ni", "zwei");
        dataGrid.Rows.Add("three", "san", "drei");

        string expected = GetResourceFileContent("03-doublelineborder-headers.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void render_table_with_custom_border_title_and_headers()
    {
        DataGrid dataGrid = new();
        dataGrid.BorderTemplate = BorderTemplate.DoubleLineBorderTemplate;
        dataGrid.Title = "My Title";
        dataGrid.HeaderRow.IsVisible = true;
        dataGrid.Columns.Add(new Column("One"));
        dataGrid.Columns.Add(new Column("Two"));
        dataGrid.Columns.Add(new Column("Three"));
        dataGrid.Rows.Add("one", "ichi", "eins");
        dataGrid.Rows.Add("two", "ni", "zwei");
        dataGrid.Rows.Add("three", "san", "drei");

        string expected = GetResourceFileContent("04-doublelineborder-title-headers.txt");
        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void render_table_with_digit_and_letter_border()
    {
        DataGrid dataGrid = new();

        dataGrid.BorderTemplate = new BorderTemplate("1234567890abcde");
        dataGrid.Title = "My Title";
        dataGrid.HeaderRow.IsVisible = true;
        dataGrid.CellPaddingLeft = 3;
        dataGrid.CellPaddingRight = 3;

        dataGrid.Columns.Add(new Column("One"));
        dataGrid.Columns.Add(new Column("Two"));
        dataGrid.Columns.Add(new Column("Three"));
        dataGrid.Rows.Add("one", "ichi", "eins");
        dataGrid.Rows.Add("two", "ni", "zwei");
        dataGrid.Rows.Add("three", "san", "drei");

        string expected = GetResourceFileContent("05-customborder-customchars.txt");
        dataGrid.IsEqualTo(expected);
    }
}