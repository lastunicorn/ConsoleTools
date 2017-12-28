// ConsoleTools
// Copyright (C) 2017 Dust in the Wind
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
    public class NoBorderTests
    {
        [Test]
        public void render_simple_table_without_border()
        {
            Table table = new Table();
            table.DisplayBorder = false;
            table.AddRow(new[] { "one", "ichi", "eins" });
            table.AddRow(new[] { "two", "ni", "zwei" });
            table.AddRow(new[] { "three", "san", "drei" });

            string expected =
@" one    ichi  eins 
 two    ni    zwei 
 three  san   drei 
";

            CustomAssert.TableRender(table, expected);
        }

        [Test]
        public void render_table_with_title_and_no_border()
        {
            Table table = new Table();
            table.DisplayBorder = false;
            table.Title = "My Title";
            table.AddRow(new[] { "one", "ichi", "eins" });
            table.AddRow(new[] { "two", "ni", "zwei" });
            table.AddRow(new[] { "three", "san", "drei" });

            string expected =
@" My Title          
 one    ichi  eins 
 two    ni    zwei 
 three  san   drei 
";

            CustomAssert.TableRender(table, expected);
        }
    }
}
