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
    public class HeaderAlignmentTests
    {
        [Test]
        public void by_default_header_cell_content_is_aligned_to_left()
        {
            DataGrid dataGrid = new DataGrid("This is a header cell alignment test");
            dataGrid.Columns.Add(new Column("Header 1"));
            dataGrid.Columns.Add(new Column("Header 2"));
            dataGrid.Columns.Add(new Column("Header 3"));
            dataGrid.Rows.Add("Cell Content 0,0", "Cell Content 0,1", "Cell Content 0,2");
            dataGrid.Rows.Add("Cell Content 1,0", "Cell Content 1,1", "Cell Content 1,2");
            dataGrid.Rows.Add("Cell Content 2,0", "Cell Content 2,1", "Cell Content 2,2");

            string expected =
                @"+--------------------------------------------------------+
| This is a header cell alignment test                   |
+------------------+------------------+------------------+
| Header 1         | Header 2         | Header 3         |
+------------------+------------------+------------------+
| Cell Content 0,0 | Cell Content 0,1 | Cell Content 0,2 |
| Cell Content 1,0 | Cell Content 1,1 | Cell Content 1,2 |
| Cell Content 2,0 | Cell Content 2,1 | Cell Content 2,2 |
+------------------+------------------+------------------+
";
            CustomAssert.TableRender(dataGrid, expected);
        }

        [Test]
        public void header_cell_1_is_alignment_Default_after_cell_creation()
        {
            DataGrid dataGrid = new DataGrid("This is a header cell alignment test");
            dataGrid.Columns.Add(new Column("Header 1"));
            dataGrid.Columns.Add(new Column("Header 2"));
            dataGrid.Columns.Add(new Column("Header 3"));
            dataGrid.Rows.Add("Cell Content 0,0", "Cell Content 0,1", "Cell Content 0,2");
            dataGrid.Rows.Add("Cell Content 1,0", "Cell Content 1,1", "Cell Content 1,2");
            dataGrid.Rows.Add("Cell Content 2,0", "Cell Content 2,1", "Cell Content 2,2");
            dataGrid.Columns[1].HorizontalAlignment = HorizontalAlignment.Default;

            string expected =
                @"+--------------------------------------------------------+
| This is a header cell alignment test                   |
+------------------+------------------+------------------+
| Header 1         | Header 2         | Header 3         |
+------------------+------------------+------------------+
| Cell Content 0,0 | Cell Content 0,1 | Cell Content 0,2 |
| Cell Content 1,0 | Cell Content 1,1 | Cell Content 1,2 |
| Cell Content 2,0 | Cell Content 2,1 | Cell Content 2,2 |
+------------------+------------------+------------------+
";
            CustomAssert.TableRender(dataGrid, expected);
        }

        [Test]
        public void header_cell_1_is_alignment_Left_after_cell_creation()
        {
            DataGrid dataGrid = new DataGrid("This is a header cell alignment test");
            dataGrid.Columns.Add(new Column("Header 1"));
            dataGrid.Columns.Add(new Column("Header 2"));
            dataGrid.Columns.Add(new Column("Header 3"));
            dataGrid.Rows.Add("Cell Content 0,0", "Cell Content 0,1", "Cell Content 0,2");
            dataGrid.Rows.Add("Cell Content 1,0", "Cell Content 1,1", "Cell Content 1,2");
            dataGrid.Rows.Add("Cell Content 2,0", "Cell Content 2,1", "Cell Content 2,2");
            dataGrid.Columns[1].HorizontalAlignment = HorizontalAlignment.Left;

            string expected =
                @"+--------------------------------------------------------+
| This is a header cell alignment test                   |
+------------------+------------------+------------------+
| Header 1         | Header 2         | Header 3         |
+------------------+------------------+------------------+
| Cell Content 0,0 | Cell Content 0,1 | Cell Content 0,2 |
| Cell Content 1,0 | Cell Content 1,1 | Cell Content 1,2 |
| Cell Content 2,0 | Cell Content 2,1 | Cell Content 2,2 |
+------------------+------------------+------------------+
";
            CustomAssert.TableRender(dataGrid, expected);
        }

        [Test]
        public void header_cell_1_is_alignment_Center_after_cell_creation()
        {
            DataGrid dataGrid = new DataGrid("This is a header cell alignment test");
            dataGrid.Columns.Add(new Column("Header 1"));
            dataGrid.Columns.Add(new Column("Header 2"));
            dataGrid.Columns.Add(new Column("Header 3"));
            dataGrid.Rows.Add("Cell Content 0,0", "Cell Content 0,1", "Cell Content 0,2");
            dataGrid.Rows.Add("Cell Content 1,0", "Cell Content 1,1", "Cell Content 1,2");
            dataGrid.Rows.Add("Cell Content 2,0", "Cell Content 2,1", "Cell Content 2,2");
            dataGrid.Columns[1].HorizontalAlignment = HorizontalAlignment.Center;

            string expected =
                @"+--------------------------------------------------------+
| This is a header cell alignment test                   |
+------------------+------------------+------------------+
| Header 1         |     Header 2     | Header 3         |
+------------------+------------------+------------------+
| Cell Content 0,0 | Cell Content 0,1 | Cell Content 0,2 |
| Cell Content 1,0 | Cell Content 1,1 | Cell Content 1,2 |
| Cell Content 2,0 | Cell Content 2,1 | Cell Content 2,2 |
+------------------+------------------+------------------+
";
            CustomAssert.TableRender(dataGrid, expected);
        }

        [Test]
        public void header_cell_1_is_alignment_Right_after_cell_creation()
        {
            DataGrid dataGrid = new DataGrid("This is a header cell alignment test");
            dataGrid.Columns.Add(new Column("Header 1"));
            dataGrid.Columns.Add(new Column("Header 2"));
            dataGrid.Columns.Add(new Column("Header 3"));
            dataGrid.Rows.Add("Cell Content 0,0", "Cell Content 0,1", "Cell Content 0,2");
            dataGrid.Rows.Add("Cell Content 1,0", "Cell Content 1,1", "Cell Content 1,2");
            dataGrid.Rows.Add("Cell Content 2,0", "Cell Content 2,1", "Cell Content 2,2");
            dataGrid.Columns[1].HorizontalAlignment = HorizontalAlignment.Right;

            string expected =
                @"+--------------------------------------------------------+
| This is a header cell alignment test                   |
+------------------+------------------+------------------+
| Header 1         |         Header 2 | Header 3         |
+------------------+------------------+------------------+
| Cell Content 0,0 | Cell Content 0,1 | Cell Content 0,2 |
| Cell Content 1,0 | Cell Content 1,1 | Cell Content 1,2 |
| Cell Content 2,0 | Cell Content 2,1 | Cell Content 2,2 |
+------------------+------------------+------------------+
";
            CustomAssert.TableRender(dataGrid, expected);
        }
    }
}