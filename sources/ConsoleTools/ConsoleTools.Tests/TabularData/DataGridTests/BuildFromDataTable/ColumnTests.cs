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

// --------------------------------------------------------------------------------
// Bugs or feature requests
// --------------------------------------------------------------------------------
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new

using System.Data;
using DustInTheWind.ConsoleTools.TabularData;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.TabularData.DataGridTests.BuildFromDataTable
{
    [TestFixture]
    public class ColumnTests
    {
        [Test]
        public void one_column_exists_if_DataTable_has_one_column()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("col1");

            DataGrid actual = DataGrid.BuildFrom(dataTable);

            Assert.That(actual.Columns.Count, Is.EqualTo(1));
        }

        [Test]
        public void column_header_content_is_equal_to_DataTable_column_name_if_caption_is_not_set()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("col1");

            DataGrid actual = DataGrid.BuildFrom(dataTable);

            Assert.That(actual.Columns[0].HeaderCell.Content, Is.EqualTo(new MultilineText("col1")));
        }

        [Test]
        public void column_header_content_is_equal_to_DataTable_column_name_if_caption_is_set_to_null()
        {
            DataTable dataTable = new DataTable();
            DataColumn dataColumn = new DataColumn("col1")
            {
                Caption = null
            };
            dataTable.Columns.Add(dataColumn);

            DataGrid actual = DataGrid.BuildFrom(dataTable);

            Assert.That(actual.Columns[0].HeaderCell.Content, Is.EqualTo(new MultilineText("col1")));
        }

        [Test]
        public void column_header_content_is_equal_to_DataTable_column_name_if_caption_is_set_to_string_empty()
        {
            DataTable dataTable = new DataTable();
            DataColumn dataColumn = new DataColumn("col1")
            {
                Caption = string.Empty
            };
            dataTable.Columns.Add(dataColumn);

            DataGrid actual = DataGrid.BuildFrom(dataTable);

            Assert.That(actual.Columns[0].HeaderCell.Content, Is.EqualTo(new MultilineText("col1")));
        }

        [Test]
        public void column_header_content_is_equal_to_DataTable_column_caption_if_it_is_set()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn
            {
                Caption = "Column 1"
            });

            DataGrid actual = DataGrid.BuildFrom(dataTable);

            Assert.That(actual.Columns[0].HeaderCell.Content, Is.EqualTo(new MultilineText("Column 1")));
        }
    }
}