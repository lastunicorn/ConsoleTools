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

// --------------------------------------------------------------------------------
// Bugs or feature requests
// --------------------------------------------------------------------------------
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new/choose

using System;
using System.Data;
using DustInTheWind.ConsoleTools.Controls.Tables;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.DataGridTests.BuildFromDataTable
{
    [TestFixture]
    public class RowTests
    {
        [Test]
        public void one_row_exists_if_DataTable_has_one_row()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("col1");
            dataTable.Rows.Add("value 1");

            DataGrid actual = DataGrid.BuildFrom(dataTable);

            Assert.That(actual.Rows.Count, Is.EqualTo(1));
        }

        [Test]
        public void first_cell_content_is_equal_to_the_first_cell_value_of_the_DataTable_row__string()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("col1");
            dataTable.Rows.Add("value 1");

            DataGrid actual = DataGrid.BuildFrom(dataTable);

            Assert.That(actual.Rows[0][0].Content, Is.EqualTo(new MultilineText("value 1")));
        }

        [Test]
        public void first_cell_content_is_equal_to_the_first_cell_value_of_the_DataTable_row__int()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("col1");
            dataTable.Rows.Add(15);

            DataGrid actual = DataGrid.BuildFrom(dataTable);

            Assert.That(actual.Rows[0][0].Content, Is.EqualTo(new MultilineText("15")));
        }

        [Test]
        public void first_cell_content_is_equal_to_the_first_cell_value_of_the_DataTable_row__DateTime()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("col1");
            dataTable.Rows.Add(new DateTime(2018, 06, 13));

            DataGrid actual = DataGrid.BuildFrom(dataTable);

            MultilineText expected = new MultilineText(new DateTime(2018, 06, 13));
            Assert.That(actual.Rows[0][0].Content, Is.EqualTo(expected));
        }
    }
}