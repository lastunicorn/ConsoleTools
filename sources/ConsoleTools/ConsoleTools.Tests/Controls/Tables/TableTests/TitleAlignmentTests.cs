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

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.TableTests
{
    [TestFixture]
    public class TitleAlignmentTests
    {
        [Test]
        public void by_default_title_content_is_aligned_to_left()
        {
            DataGrid dataGrid = new DataGrid("Title");
            dataGrid.Rows.Add("Cell 0,0", "Cell 0,1", "Cell 0,2");

            string expected =
                @"+--------------------------------+
| Title                          |
+----------+----------+----------+
| Cell 0,0 | Cell 0,1 | Cell 0,2 |
+----------+----------+----------+
";
            CustomAssert.TableRender(dataGrid, expected);
        }

        [Test]
        public void title_alignment_Default()
        {
            DataGrid dataGrid = new DataGrid("Title");
            dataGrid.Rows.Add("Cell 0,0", "Cell 0,1", "Cell 0,2");
            dataGrid.TitleRow.CellHorizontalAlignment = HorizontalAlignment.Default;

            string expected =
                @"+--------------------------------+
| Title                          |
+----------+----------+----------+
| Cell 0,0 | Cell 0,1 | Cell 0,2 |
+----------+----------+----------+
";
            CustomAssert.TableRender(dataGrid, expected);
        }

        [Test]
        public void title_alignment_Left()
        {
            DataGrid dataGrid = new DataGrid("Title");
            dataGrid.Rows.Add("Cell 0,0", "Cell 0,1", "Cell 0,2");
            dataGrid.TitleRow.CellHorizontalAlignment = HorizontalAlignment.Left;

            string expected =
                @"+--------------------------------+
| Title                          |
+----------+----------+----------+
| Cell 0,0 | Cell 0,1 | Cell 0,2 |
+----------+----------+----------+
";
            CustomAssert.TableRender(dataGrid, expected);
        }

        [Test]
        public void title_alignment_Center()
        {
            DataGrid dataGrid = new DataGrid("Title");
            dataGrid.Rows.Add("Cell 0,0", "Cell 0,1", "Cell 0,2");
            dataGrid.TitleRow.CellHorizontalAlignment = HorizontalAlignment.Center;

            string expected =
                @"+--------------------------------+
|             Title              |
+----------+----------+----------+
| Cell 0,0 | Cell 0,1 | Cell 0,2 |
+----------+----------+----------+
";
            CustomAssert.TableRender(dataGrid, expected);
        }

        [Test]
        public void title_alignment_Right()
        {
            DataGrid dataGrid = new DataGrid("Title");
            dataGrid.Rows.Add("Cell 0,0", "Cell 0,1", "Cell 0,2");
            dataGrid.TitleRow.CellHorizontalAlignment = HorizontalAlignment.Right;

            string expected =
                @"+--------------------------------+
|                          Title |
+----------+----------+----------+
| Cell 0,0 | Cell 0,1 | Cell 0,2 |
+----------+----------+----------+
";
            CustomAssert.TableRender(dataGrid, expected);
        }
    }
}