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

namespace DustInTheWind.ConsoleTools.Tests.TabularData.DataCellTests
{
    [TestFixture]
    public class RenderLineTests
    {
        [Test]
        public void content_is_shorter_than_required_width()
        {
            DataCell cell = new DataCell("text");

            string actual = cell.RenderLine(0, 10);

            Assert.That(actual, Is.EqualTo("text      "));
        }

        [Test]
        public void content_is_longer_than_required_width()
        {
            DataCell cell = new DataCell("some long text");

            string actual = cell.RenderLine(0, 10);

            Assert.That(actual, Is.EqualTo("some long text"));
        }

        [Test]
        public void cell_has_padding_left()
        {
            Table table = new Table();
            DataRow row = new DataRow();
            table.Rows.Add(row);
            DataCell cell = new DataCell("text")
            {
                PaddingLeft = 2,
                PaddingRight = 0
            };
            row.AddCell(cell);

            string actual = cell.RenderLine(0, 10);

            Assert.That(actual, Is.EqualTo("  text    "));
        }

        [Test]
        public void parent_column_has_padding_left()
        {
            Table table = new Table();
            Column column = new Column(string.Empty)
            {
                PaddingLeft = 2,
                PaddingRight = 0
            };
            table.Columns.Add(column);
            DataRow row = new DataRow();
            table.Rows.Add(row);
            DataCell cell = new DataCell("text");
            row.AddCell(cell);

            string actual = cell.RenderLine(0, 10);

            Assert.That(actual, Is.EqualTo("  text    "));
        }

        [Test]
        public void parent_row_has_padding_left()
        {
            Table table = new Table();
            DataRow row = new DataRow
            {
                PaddingLeft = 2,
                PaddingRight = 0
            };
            table.Rows.Add(row);
            DataCell cell = new DataCell("text");
            row.AddCell(cell);

            string actual = cell.RenderLine(0, 10);

            Assert.That(actual, Is.EqualTo("  text    "));
        }

        [Test]
        public void parent_table_has_padding_left()
        {
            Table table = new Table
            {
                PaddingLeft = 2,
                PaddingRight = 0
            };
            DataRow row = new DataRow();
            table.Rows.Add(row);
            DataCell cell = new DataCell("text");
            row.AddCell(cell);
            
            string actual = cell.RenderLine(0, 10);

            Assert.That(actual, Is.EqualTo("  text    "));
        }

        [Test]
        public void cell_has_padding_right()
        {
            Table table = new Table();
            DataRow row = new DataRow();
            table.Rows.Add(row);
            DataCell cell = new DataCell("text")
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                PaddingLeft = 0,
                PaddingRight = 2
            };
            row.AddCell(cell);
            
            string actual = cell.RenderLine(0, 10);

            Assert.That(actual, Is.EqualTo("    text  "));
        }

        [Test]
        public void parent_column_has_padding_right()
        {
            Table table = new Table();
            Column column = new Column(string.Empty)
            {
                PaddingLeft = 0,
                PaddingRight = 2
            };
            table.Columns.Add(column);
            DataRow row = new DataRow();
            table.Rows.Add(row);
            DataCell cell = new DataCell("text")
            {
                HorizontalAlignment = HorizontalAlignment.Right
            };
            row.AddCell(cell);
            
            string actual = cell.RenderLine(0, 10);

            Assert.That(actual, Is.EqualTo("    text  "));
        }

        [Test]
        public void parent_row_has_padding_right()
        {
            Table table = new Table();
            DataRow row = new DataRow
            {
                PaddingLeft = 0,
                PaddingRight = 2
            };
            table.Rows.Add(row);
            DataCell cell = new DataCell("text")
            {
                HorizontalAlignment = HorizontalAlignment.Right
            };
            row.AddCell(cell);
            
            string actual = cell.RenderLine(0, 10);

            Assert.That(actual, Is.EqualTo("    text  "));
        }

        [Test]
        public void parent_table_has_padding_right()
        {
            Table table = new Table
            {
                PaddingLeft = 0,
                PaddingRight = 2
            };
            DataRow row = new DataRow();
            table.Rows.Add(row);
            DataCell cell = new DataCell("text")
            {
                HorizontalAlignment = HorizontalAlignment.Right
            };
            row.AddCell(cell);
            
            string actual = cell.RenderLine(0, 10);

            Assert.That(actual, Is.EqualTo("    text  "));
        }
    }
}
