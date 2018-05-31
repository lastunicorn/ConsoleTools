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

using System.Collections.Generic;
using DustInTheWind.ConsoleTools.TabularData;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.TabularData.DataGridTests.BuildFromList
{
    [TestFixture]
    public class RowTests
    {
        private class CustomClass
        {
            public int Number { get; set; }

            public string Text { get; set; }
        }

        [Test]
        public void one_row_is_generated_if_one_item_in_list()
        {
            List<CustomClass> data = new List<CustomClass>
            {
                new CustomClass()
            };

            DataGrid actual = DataGrid.BuildFrom(data);

            Assert.That(actual.Rows.Count, Is.EqualTo(1));
        }

        [Test]
        public void row_contains_two_cells()
        {
            List<CustomClass> data = new List<CustomClass>
            {
                new CustomClass()
            };

            DataGrid actual = DataGrid.BuildFrom(data);

            Assert.That(actual.Rows[0].CellCount, Is.EqualTo(2));
        }

        [Test]
        public void first_cell_contains_the_value_of_first_property()
        {
            List<CustomClass> data = new List<CustomClass>
            {
                new CustomClass {Number = 4, Text = "bla"}
            };

            DataGrid actual = DataGrid.BuildFrom(data);

            Assert.That(actual.Rows[0][0].Content, Is.EqualTo(new MultilineText("4")));
        }

        [Test]
        public void second_cell_contains_the_value_of_second_property()
        {
            List<CustomClass> data = new List<CustomClass>
            {
                new CustomClass {Number = 4, Text = "bla"}
            };

            DataGrid actual = DataGrid.BuildFrom(data);

            Assert.That(actual.Rows[0][1].Content, Is.EqualTo(new MultilineText("bla")));
        }
    }
}