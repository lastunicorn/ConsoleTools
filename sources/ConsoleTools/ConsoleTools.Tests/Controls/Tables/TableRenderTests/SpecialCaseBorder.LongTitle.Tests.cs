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
public class SpecialCaseBorderTests_LongTitle
{
    [Test]
    public void all()
    {
        DataGrid dataGrid = new();
        dataGrid.Border.Template = BorderTemplate.DoubleLineBorderTemplate;
        dataGrid.Title = "This is a title longer than the rows";
        dataGrid.Columns.Add(new Column("Header 1"));
        dataGrid.Columns.Add(new Column("Header 2"));
        dataGrid.Columns.Add(new Column("Header 3"));
        dataGrid.Rows.Add("one", "ichi", "eins");
        dataGrid.Rows.Add("two", "ni", "zwei");
        dataGrid.Rows.Add("three", "san", "drei");

        const string expected =
            @"╔══════════════════════════════════════╗
║ This is a title longer than the rows ║
╠════════════╦════════════╦════════════╣
║ Header 1   ║ Header 2   ║ Header 3   ║
╠════════════╬════════════╬════════════╣
║ one        ║ ichi       ║ eins       ║
║ two        ║ ni         ║ zwei       ║
║ three      ║ san        ║ drei       ║
╚════════════╩════════════╩════════════╝
";

        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void no_title()
    {
        DataGrid dataGrid = new();
        dataGrid.Border.Template = BorderTemplate.DoubleLineBorderTemplate;
        dataGrid.Columns.Add(new Column("Header 1"));
        dataGrid.Columns.Add(new Column("Header 2"));
        dataGrid.Columns.Add(new Column("Header 3"));
        dataGrid.Rows.Add("one", "ichi", "eins");
        dataGrid.Rows.Add("two", "ni", "zwei");
        dataGrid.Rows.Add("three", "san", "drei");

        const string expected =
            @"╔══════════╦══════════╦══════════╗
║ Header 1 ║ Header 2 ║ Header 3 ║
╠══════════╬══════════╬══════════╣
║ one      ║ ichi     ║ eins     ║
║ two      ║ ni       ║ zwei     ║
║ three    ║ san      ║ drei     ║
╚══════════╩══════════╩══════════╝
";

        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void no_header()
    {
        DataGrid dataGrid = new();
        dataGrid.Title = "This is a title longer than the rows";
        dataGrid.Border.Template = BorderTemplate.DoubleLineBorderTemplate;
        dataGrid.Rows.Add("one", "ichi", "eins");
        dataGrid.Rows.Add("two", "ni", "zwei");
        dataGrid.Rows.Add("three", "san", "drei");

        string expected =
            @"╔══════════════════════════════════════╗
║ This is a title longer than the rows ║
╠═════════════╦════════════╦═══════════╣
║ one         ║ ichi       ║ eins      ║
║ two         ║ ni         ║ zwei      ║
║ three       ║ san        ║ drei      ║
╚═════════════╩════════════╩═══════════╝
";

        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void no_data()
    {
        DataGrid dataGrid = new();
        dataGrid.Title = "This is a title longer than the rows";
        dataGrid.Border.Template = BorderTemplate.DoubleLineBorderTemplate;
        dataGrid.Columns.Add(new Column("Header 1"));
        dataGrid.Columns.Add(new Column("Header 2"));
        dataGrid.Columns.Add(new Column("Header 3"));

        const string expected =
            @"╔══════════════════════════════════════╗
║ This is a title longer than the rows ║
╠════════════╦════════════╦════════════╣
║ Header 1   ║ Header 2   ║ Header 3   ║
╚════════════╩════════════╩════════════╝
";

        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void only_data()
    {
        DataGrid dataGrid = new();
        dataGrid.Border.Template = BorderTemplate.DoubleLineBorderTemplate;
        dataGrid.Rows.Add("one", "ichi", "eins");
        dataGrid.Rows.Add("two", "ni", "zwei");
        dataGrid.Rows.Add("three", "san", "drei");

        const string expected =
            @"╔═══════╦══════╦══════╗
║ one   ║ ichi ║ eins ║
║ two   ║ ni   ║ zwei ║
║ three ║ san  ║ drei ║
╚═══════╩══════╩══════╝
";

        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void only_header()
    {
        DataGrid dataGrid = new();
        dataGrid.Border.Template = BorderTemplate.DoubleLineBorderTemplate;
        dataGrid.Columns.Add(new Column("Header 1"));
        dataGrid.Columns.Add(new Column("Header 2"));
        dataGrid.Columns.Add(new Column("Header 3"));

        const string expected =
            @"╔══════════╦══════════╦══════════╗
║ Header 1 ║ Header 2 ║ Header 3 ║
╚══════════╩══════════╩══════════╝
";

        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void only_title()
    {
        DataGrid dataGrid = new();
        dataGrid.Title = "This is a title longer than the rows";
        dataGrid.Border.Template = BorderTemplate.DoubleLineBorderTemplate;

        const string expected =
            @"╔══════════════════════════════════════╗
║ This is a title longer than the rows ║
╚══════════════════════════════════════╝
";

        dataGrid.IsEqualTo(expected);
    }

    [Test]
    public void no_title_no_header_no_data()
    {
        DataGrid dataGrid = new();
        dataGrid.Border.Template = BorderTemplate.DoubleLineBorderTemplate;

        string expected = string.Empty;

        dataGrid.IsEqualTo(expected);
    }
}