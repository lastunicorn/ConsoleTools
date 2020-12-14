// ConsoleTools
// Copyright (C) 2017-2020 Dust in the Wind
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

namespace DustInTheWind.ConsoleTools.Tests.Tables.TableTests
{
    [TestFixture]
    public class CustomBorderTests
    {
        [Test]
        public void render_simple_table_with_custom_border()
        {
            DataGrid dataGrid = new DataGrid();
            dataGrid.Border.Template = BorderTemplate.DoubleLineBorderTemplate;
            dataGrid.Rows.Add("one", "ichi", "eins");
            dataGrid.Rows.Add("two", "ni", "zwei");
            dataGrid.Rows.Add("three", "san", "drei");

            string expected =
                @"╔═══════╦══════╦══════╗
║ one   ║ ichi ║ eins ║
║ two   ║ ni   ║ zwei ║
║ three ║ san  ║ drei ║
╚═══════╩══════╩══════╝
";

            CustomAssert.TableRender(dataGrid, expected);
        }

        [Test]
        public void render_table_with_custom_border_and_title()
        {
            DataGrid dataGrid = new DataGrid();
            dataGrid.Title = "My Title";
            dataGrid.Border.Template = BorderTemplate.DoubleLineBorderTemplate;
            dataGrid.Rows.Add("one", "ichi", "eins");
            dataGrid.Rows.Add("two", "ni", "zwei");
            dataGrid.Rows.Add("three", "san", "drei");

            string expected =
                @"╔═════════════════════╗
║ My Title            ║
╠═══════╦══════╦══════╣
║ one   ║ ichi ║ eins ║
║ two   ║ ni   ║ zwei ║
║ three ║ san  ║ drei ║
╚═══════╩══════╩══════╝
";

            CustomAssert.TableRender(dataGrid, expected);
        }

        [Test]
        public void render_table_with_custom_border_and_headers()
        {
            DataGrid dataGrid = new DataGrid();
            dataGrid.Border.Template = BorderTemplate.DoubleLineBorderTemplate;
            dataGrid.HeaderRow.IsVisible = true;
            dataGrid.Columns.Add(new Column("One"));
            dataGrid.Columns.Add(new Column("Two"));
            dataGrid.Columns.Add(new Column("Three"));
            dataGrid.Rows.Add("one", "ichi", "eins");
            dataGrid.Rows.Add("two", "ni", "zwei");
            dataGrid.Rows.Add("three", "san", "drei");

            string expected =
                @"╔═══════╦══════╦═══════╗
║ One   ║ Two  ║ Three ║
╠═══════╬══════╬═══════╣
║ one   ║ ichi ║ eins  ║
║ two   ║ ni   ║ zwei  ║
║ three ║ san  ║ drei  ║
╚═══════╩══════╩═══════╝
";

            CustomAssert.TableRender(dataGrid, expected);
        }

        [Test]
        public void render_table_with_custom_border_title_and_headers()
        {
            DataGrid dataGrid = new DataGrid();
            dataGrid.Border.Template = BorderTemplate.DoubleLineBorderTemplate;
            dataGrid.Title = "My Title";
            dataGrid.HeaderRow.IsVisible = true;
            dataGrid.Columns.Add(new Column("One"));
            dataGrid.Columns.Add(new Column("Two"));
            dataGrid.Columns.Add(new Column("Three"));
            dataGrid.Rows.Add("one", "ichi", "eins");
            dataGrid.Rows.Add("two", "ni", "zwei");
            dataGrid.Rows.Add("three", "san", "drei");

            string expected =
                @"╔══════════════════════╗
║ My Title             ║
╠═══════╦══════╦═══════╣
║ One   ║ Two  ║ Three ║
╠═══════╬══════╬═══════╣
║ one   ║ ichi ║ eins  ║
║ two   ║ ni   ║ zwei  ║
║ three ║ san  ║ drei  ║
╚═══════╩══════╩═══════╝
";

            CustomAssert.TableRender(dataGrid, expected);
        }

        [Test]
        public void render_table_with_digit_and_letter_border()
        {
            DataGrid dataGrid = new DataGrid();
            dataGrid.Border.Template = new BorderTemplate("1234567890abcde");
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

            string expected =
                @"122222222222222222222222222222222223
8 My Title                         4
beeeeeeeeeee9eeeeeeeeee9eeeeeeeeeee0
8   One     d   Two    d   Three   4
beeeeeeeeeeeceeeeeeeeeeceeeeeeeeeee0
8   one     d   ichi   d   eins    4
8   two     d   ni     d   zwei    4
8   three   d   san    d   drei    4
766666666666a6666666666a666666666665
";

            CustomAssert.TableRender(dataGrid, expected);
        }
    }
}