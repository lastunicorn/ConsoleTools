// ConsoleTools
// Copyright (C) 2017-2018 Dust in the Wind
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

using DustInTheWind.ConsoleTools.TabularData;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.TabularData.TableTests
{
    [TestFixture]
    public class CustomBorderTests
    {
        [Test]
        public void render_simple_table_with_custom_border()
        {
            Table table = new Table();
            table.BorderTemplate = BorderTemplate.DoubleLineBorderTemplate;
            table.Rows.Add(new[] { "one", "ichi", "eins" });
            table.Rows.Add(new[] { "two", "ni", "zwei" });
            table.Rows.Add(new[] { "three", "san", "drei" });

            string expected =
@"╔═══════╦══════╦══════╗
║ one   ║ ichi ║ eins ║
║ two   ║ ni   ║ zwei ║
║ three ║ san  ║ drei ║
╚═══════╩══════╩══════╝
";

            CustomAssert.TableRender(table, expected);
        }

        [Test]
        public void render_table_with_custom_border_and_title()
        {
            Table table = new Table();
            table.Title = "My Title";
            table.BorderTemplate = BorderTemplate.DoubleLineBorderTemplate;
            table.Rows.Add(new[] { "one", "ichi", "eins" });
            table.Rows.Add(new[] { "two", "ni", "zwei" });
            table.Rows.Add(new[] { "three", "san", "drei" });

            string expected =
@"╔═════════════════════╗
║ My Title            ║
╠═══════╦══════╦══════╣
║ one   ║ ichi ║ eins ║
║ two   ║ ni   ║ zwei ║
║ three ║ san  ║ drei ║
╚═══════╩══════╩══════╝
";

            CustomAssert.TableRender(table, expected);
        }

        [Test]
        public void render_table_with_custom_border_and_headers()
        {
            Table table = new Table();
            table.BorderTemplate = BorderTemplate.DoubleLineBorderTemplate;
            table.DisplayColumnHeaders = true;
            table.Columns.Add(new Column("One"));
            table.Columns.Add(new Column("Two"));
            table.Columns.Add(new Column("Three"));
            table.Rows.Add(new[] { "one", "ichi", "eins" });
            table.Rows.Add(new[] { "two", "ni", "zwei" });
            table.Rows.Add(new[] { "three", "san", "drei" });

            string expected =
@"╔═══════╦══════╦═══════╗
║ One   ║ Two  ║ Three ║
╠═══════╬══════╬═══════╣
║ one   ║ ichi ║ eins  ║
║ two   ║ ni   ║ zwei  ║
║ three ║ san  ║ drei  ║
╚═══════╩══════╩═══════╝
";

            CustomAssert.TableRender(table, expected);
        }

        [Test]
        public void render_table_with_custom_border_title_and_headers()
        {
            Table table = new Table();
            table.BorderTemplate = BorderTemplate.DoubleLineBorderTemplate;
            table.Title = "My Title";
            table.DisplayColumnHeaders = true;
            table.Columns.Add(new Column("One"));
            table.Columns.Add(new Column("Two"));
            table.Columns.Add(new Column("Three"));
            table.Rows.Add(new[] { "one", "ichi", "eins" });
            table.Rows.Add(new[] { "two", "ni", "zwei" });
            table.Rows.Add(new[] { "three", "san", "drei" });

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

            CustomAssert.TableRender(table, expected);
        }

        [Test]
        public void render_table_with_digit_and_letter_border()
        {
            Table table = new Table();
            table.BorderTemplate = new BorderTemplate("1234567890abcde");
            table.Title = "My Title";
            table.DisplayColumnHeaders = true;
            table.PaddingLeft = 3;
            table.PaddingRight = 3;
            table.Columns.Add(new Column("One"));
            table.Columns.Add(new Column("Two"));
            table.Columns.Add(new Column("Three"));
            table.Rows.Add(new[] { "one", "ichi", "eins" });
            table.Rows.Add(new[] { "two", "ni", "zwei" });
            table.Rows.Add(new[] { "three", "san", "drei" });

            string expected =
@"122222222222222222222222222222222223
8   My Title                       4
beeeeeeeeeee9eeeeeeeeee9eeeeeeeeeee0
8   One     d   Two    d   Three   4
beeeeeeeeeeeceeeeeeeeeeceeeeeeeeeee0
8   one     d   ichi   d   eins    4
8   two     d   ni     d   zwei    4
8   three   d   san    d   drei    4
766666666666a6666666666a666666666665
";

            CustomAssert.TableRender(table, expected);
        }
    }
}
