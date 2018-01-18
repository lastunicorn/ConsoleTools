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
    public class LinesBetweenRowsTests
    {
        [Test]
        public void three_rows_without_lines_between_them()
        {
            DataGrid dataGrid = new DataGrid();
            dataGrid.Title = "My Title";
            dataGrid.Rows.Add(new[] { "one", "ichi", "eins" });
            dataGrid.Rows.Add(new[] { "two", "ni", "zwei" });
            dataGrid.Rows.Add(new[] { "three", "san", "drei" });

            string expected =
@"+---------------------+
| My Title            |
+-------+------+------+
| one   | ichi | eins |
| two   | ni   | zwei |
| three | san  | drei |
+-------+------+------+
";

            CustomAssert.TableRender(dataGrid, expected);
        }

        [Test]
        public void three_rows_with_lines_between_them()
        {
            DataGrid dataGrid = new DataGrid();
            dataGrid.Title = "My Title";
            dataGrid.DisplayBorderBetweenRows = true;

            dataGrid.Rows.Add(new[] { "one", "ichi", "eins" });
            dataGrid.Rows.Add(new[] { "two", "ni", "zwei" });
            dataGrid.Rows.Add(new[] { "three", "san", "drei" });

            string expected =
@"+---------------------+
| My Title            |
+-------+------+------+
| one   | ichi | eins |
+-------+------+------+
| two   | ni   | zwei |
+-------+------+------+
| three | san  | drei |
+-------+------+------+
";

            CustomAssert.TableRender(dataGrid, expected);
        }
    }
}
