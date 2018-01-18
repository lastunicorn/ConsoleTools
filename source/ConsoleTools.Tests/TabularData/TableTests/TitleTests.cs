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

using System.Collections.Generic;
using DustInTheWind.ConsoleTools.TabularData;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.TabularData.TableTests
{
    [TestFixture]
    public class TitleTests
    {
        [Test]
        public void title_is_shorter_than_row()
        {
            DataGrid dataGrid = new DataGrid();
            dataGrid.Title = "My Title";
            dataGrid.Rows.Add(new[] { "asd", "qwe", "zxczxc" });

            string actual = dataGrid.ToString();

            string expected =
@"+--------------------+
| My Title           |
+-----+-----+--------+
| asd | qwe | zxczxc |
+-----+-----+--------+
";
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void title_is_longer_than_row()
        {
            DataGrid dataGrid = new DataGrid();
            dataGrid.Title = "My Title My Title My Title My Title";
            dataGrid.Rows.Add(new[] { "asd", "qwe", "zxczxc" });

            string expected =
@"+-------------------------------------+
| My Title My Title My Title My Title |
+-----------+-----------+-------------+
| asd       | qwe       | zxczxc      |
+-----------+-----------+-------------+
";

            CustomAssert.TableRender(dataGrid, expected);
        }

        [Test]
        public void multiline_title()
        {
            DataGrid dataGrid = new DataGrid();
            dataGrid.Title = new List<string> { "My Title1", "My Title2", "My Title3", "My Title4" };
            dataGrid.Rows.Add(new[] { "asd", "qwe", "zxczxc" });

            string expected =
@"+--------------------+
| My Title1          |
| My Title2          |
| My Title3          |
| My Title4          |
+-----+-----+--------+
| asd | qwe | zxczxc |
+-----+-----+--------+
";

            CustomAssert.TableRender(dataGrid, expected);
        }
    }
}
