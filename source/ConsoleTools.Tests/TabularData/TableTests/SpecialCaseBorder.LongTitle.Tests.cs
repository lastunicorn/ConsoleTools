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
    public class SpecialCaseBorderTests_LongTitle
    {
        [Test]
        public void all()
        {
            Table table = new Table();
            table.BorderTemplate = BorderTemplate.DoubleLineBorderTemplate;
            table.Title = "This is a title longer than the rows";
            table.AddColumn(new Column("Header 1"));
            table.AddColumn(new Column("Header 2"));
            table.AddColumn(new Column("Header 3"));
            table.AddRow(new[] { "one", "ichi", "eins" });
            table.AddRow(new[] { "two", "ni", "zwei" });
            table.AddRow(new[] { "three", "san", "drei" });

            string expected =
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

            CustomAssert.TableRender(table, expected);
        }

        [Test]
        public void no_title()
        {
            Table table = new Table();
            table.BorderTemplate = BorderTemplate.DoubleLineBorderTemplate;
            table.AddColumn(new Column("Header 1"));
            table.AddColumn(new Column("Header 2"));
            table.AddColumn(new Column("Header 3"));
            table.AddRow(new[] { "one", "ichi", "eins" });
            table.AddRow(new[] { "two", "ni", "zwei" });
            table.AddRow(new[] { "three", "san", "drei" });

            string expected =
@"╔══════════╦══════════╦══════════╗
║ Header 1 ║ Header 2 ║ Header 3 ║
╠══════════╬══════════╬══════════╣
║ one      ║ ichi     ║ eins     ║
║ two      ║ ni       ║ zwei     ║
║ three    ║ san      ║ drei     ║
╚══════════╩══════════╩══════════╝
";

            CustomAssert.TableRender(table, expected);
        }

        [Test]
        public void no_header()
        {
            Table table = new Table();
            table.Title = "This is a title longer than the rows";
            table.BorderTemplate = BorderTemplate.DoubleLineBorderTemplate;
            table.AddRow(new[] { "one", "ichi", "eins" });
            table.AddRow(new[] { "two", "ni", "zwei" });
            table.AddRow(new[] { "three", "san", "drei" });

            string expected =
@"╔══════════════════════════════════════╗
║ This is a title longer than the rows ║
╠═════════════╦════════════╦═══════════╣
║ one         ║ ichi       ║ eins      ║
║ two         ║ ni         ║ zwei      ║
║ three       ║ san        ║ drei      ║
╚═════════════╩════════════╩═══════════╝
";

            CustomAssert.TableRender(table, expected);
        }

        [Test]
        public void no_data()
        {
            Table table = new Table();
            table.Title = "This is a title longer than the rows";
            table.BorderTemplate = BorderTemplate.DoubleLineBorderTemplate;
            table.AddColumn(new Column("Header 1"));
            table.AddColumn(new Column("Header 2"));
            table.AddColumn(new Column("Header 3"));

            string expected =
@"╔══════════════════════════════════════╗
║ This is a title longer than the rows ║
╠════════════╦════════════╦════════════╣
║ Header 1   ║ Header 2   ║ Header 3   ║
╚════════════╩════════════╩════════════╝
";

            CustomAssert.TableRender(table, expected);
        }

        [Test]
        public void only_data()
        {
            Table table = new Table();
            table.BorderTemplate = BorderTemplate.DoubleLineBorderTemplate;
            table.AddRow(new[] { "one", "ichi", "eins" });
            table.AddRow(new[] { "two", "ni", "zwei" });
            table.AddRow(new[] { "three", "san", "drei" });

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
        public void only_header()
        {
            Table table = new Table();
            table.BorderTemplate = BorderTemplate.DoubleLineBorderTemplate;
            table.AddColumn(new Column("Header 1"));
            table.AddColumn(new Column("Header 2"));
            table.AddColumn(new Column("Header 3"));

            string expected =
@"╔══════════╦══════════╦══════════╗
║ Header 1 ║ Header 2 ║ Header 3 ║
╚══════════╩══════════╩══════════╝
";

            CustomAssert.TableRender(table, expected);
        }

        [Test]
        public void only_title()
        {
            Table table = new Table();
            table.Title = "This is a title longer than the rows";
            table.BorderTemplate = BorderTemplate.DoubleLineBorderTemplate;

            string expected =
@"╔══════════════════════════════════════╗
║ This is a title longer than the rows ║
╚══════════════════════════════════════╝
";

            CustomAssert.TableRender(table, expected);
        }

        [Test]
        public void no_title_no_header_no_data()
        {
            Table table = new Table();
            table.BorderTemplate = BorderTemplate.DoubleLineBorderTemplate;

            string expected = string.Empty;

            CustomAssert.TableRender(table, expected);
        }
    }
}
