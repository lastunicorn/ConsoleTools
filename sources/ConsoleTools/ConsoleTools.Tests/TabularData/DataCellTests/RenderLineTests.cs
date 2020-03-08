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

using System.Collections.Generic;
using System.Linq;
using DustInTheWind.ConsoleTools.Controls.Tables;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.TabularData.DataCellTests
{
    [TestFixture]
    public class RenderLineTests
    {
        [Test]
        public void content_is_shorter_than_required_width()
        {
            DataCell cell = new DataCell("text");
            Size size = new Size(10, 1);

            List<string> actual = cell.Render(size).ToList();

            Assert.That(actual, Is.EqualTo(new List<string> { "text      " }));
        }

        [Test]
        public void content_is_longer_than_required_width()
        {
            DataCell cell = new DataCell("some long text");
            Size size = new Size(10, 1);

            List<string> actual = cell.Render(size).ToList();

            Assert.That(actual, Is.EqualTo(new List<string> { "some long text" }));
        }

        [Test]
        public void content_is_shorter_than_required_height()
        {
            DataCell cell = new DataCell("text");
            Size size = new Size(10, 2);

            List<string> actual = cell.Render(size).ToList();

            Assert.That(actual, Is.EqualTo(new List<string>
            {
                "text      ",
                "          "
            }));
        }

        [Test]
        public void content_is_longer_than_required_height()
        {
            DataCell cell = new DataCell(new MultilineText(new[] { "line1", "line2", "line3" }));
            Size size = new Size(10, 2);

            List<string> actual = cell.Render(size).ToList();

            Assert.That(actual, Is.EqualTo(new List<string>
            {
                "line1     ",
                "line2     "
            }));
        }

        [Test]
        public void cell_has_padding_left()
        {
            DataGrid dataGrid = new DataGrid();
            DataRow row = new DataRow();
            dataGrid.Rows.Add(row);
            DataCell cell = new DataCell("text")
            {
                PaddingLeft = 2,
                PaddingRight = 0
            };
            row.AddCell(cell);
            Size size = new Size(10, 1);

            List<string> actual = cell.Render(size).ToList();

            Assert.That(actual, Is.EqualTo(new List<string> { "  text    " }));
        }

        [Test]
        public void parent_column_has_padding_left()
        {
            DataGrid dataGrid = new DataGrid();
            Column column = new Column(string.Empty)
            {
                PaddingLeft = 2,
                PaddingRight = 0
            };
            dataGrid.Columns.Add(column);
            DataRow row = new DataRow();
            dataGrid.Rows.Add(row);
            DataCell cell = new DataCell("text");
            row.AddCell(cell);
            Size size = new Size(10, 1);

            List<string> actual = cell.Render(size).ToList();

            Assert.That(actual, Is.EqualTo(new List<string> { "  text    " }));
        }

        [Test]
        public void parent_row_has_padding_left()
        {
            DataGrid dataGrid = new DataGrid();
            DataRow row = new DataRow
            {
                PaddingLeft = 2,
                PaddingRight = 0
            };
            dataGrid.Rows.Add(row);
            DataCell cell = new DataCell("text");
            row.AddCell(cell);
            Size size = new Size(10, 1);

            List<string> actual = cell.Render(size).ToList();

            Assert.That(actual, Is.EqualTo(new List<string> { "  text    " }));
        }

        [Test]
        public void parent_table_has_padding_left()
        {
            DataGrid dataGrid = new DataGrid
            {
                PaddingLeft = 2,
                PaddingRight = 0
            };
            DataRow row = new DataRow();
            dataGrid.Rows.Add(row);
            DataCell cell = new DataCell("text");
            row.AddCell(cell);
            Size size = new Size(10, 1);

            List<string> actual = cell.Render(size).ToList();

            Assert.That(actual, Is.EqualTo(new List<string> { "  text    " }));
        }

        [Test]
        public void cell_has_padding_right()
        {
            DataGrid dataGrid = new DataGrid();
            DataRow row = new DataRow();
            dataGrid.Rows.Add(row);
            DataCell cell = new DataCell("text")
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                PaddingLeft = 0,
                PaddingRight = 2
            };
            row.AddCell(cell);
            Size size = new Size(10, 1);

            List<string> actual = cell.Render(size).ToList();

            Assert.That(actual, Is.EqualTo(new List<string> { "    text  " }));
        }

        [Test]
        public void parent_column_has_padding_right()
        {
            DataGrid dataGrid = new DataGrid();
            Column column = new Column(string.Empty)
            {
                PaddingLeft = 0,
                PaddingRight = 2
            };
            dataGrid.Columns.Add(column);
            DataRow row = new DataRow();
            dataGrid.Rows.Add(row);
            DataCell cell = new DataCell("text")
            {
                HorizontalAlignment = HorizontalAlignment.Right
            };
            row.AddCell(cell);
            Size size = new Size(10, 1);

            List<string> actual = cell.Render(size).ToList();

            Assert.That(actual, Is.EqualTo(new List<string> { "    text  " }));
        }

        [Test]
        public void parent_row_has_padding_right()
        {
            DataGrid dataGrid = new DataGrid();
            DataRow row = new DataRow
            {
                PaddingLeft = 0,
                PaddingRight = 2
            };
            dataGrid.Rows.Add(row);
            DataCell cell = new DataCell("text")
            {
                HorizontalAlignment = HorizontalAlignment.Right
            };
            row.AddCell(cell);
            Size size = new Size(10, 1);

            List<string> actual = cell.Render(size).ToList();

            Assert.That(actual, Is.EqualTo(new List<string> { "    text  " }));
        }

        [Test]
        public void parent_table_has_padding_right()
        {
            DataGrid dataGrid = new DataGrid
            {
                PaddingLeft = 0,
                PaddingRight = 2
            };
            DataRow row = new DataRow();
            dataGrid.Rows.Add(row);
            DataCell cell = new DataCell("text")
            {
                HorizontalAlignment = HorizontalAlignment.Right
            };
            row.AddCell(cell);
            Size size = new Size(10, 1);

            List<string> actual = cell.Render(size).ToList();

            Assert.That(actual, Is.EqualTo(new List<string> { "    text  " }));
        }
    }
}