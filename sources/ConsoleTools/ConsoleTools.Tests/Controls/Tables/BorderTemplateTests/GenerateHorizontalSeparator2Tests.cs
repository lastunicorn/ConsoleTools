// ConsoleTools
// Copyright (C) 2017-2022 Dust in the Wind
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

using System.Collections.Generic;
using DustInTheWind.ConsoleTools.Controls.Tables;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.BorderTemplateTests
{
    [TestFixture]
    public class GenerateHorizontalSeparator2Tests
    {
        [Test]
        public void returns_string_empty_if_both_column_lists_are_null()
        {
            BorderTemplate borderTemplate = BorderTemplate.SingleLineBorderTemplate;

            string actual = borderTemplate.GenerateHorizontalSeparator(null, null);

            Assert.That(actual, Is.EqualTo(string.Empty));
        }

        [Test]
        public void returns_string_empty_if_top_column_list_is_null()
        {
            BorderTemplate borderTemplate = BorderTemplate.SingleLineBorderTemplate;

            string actual = borderTemplate.GenerateHorizontalSeparator(null, new List<int>());

            Assert.That(actual, Is.EqualTo(string.Empty));
        }

        [Test]
        public void returns_string_empty_if_bottom_column_list_is_null()
        {
            BorderTemplate borderTemplate = BorderTemplate.SingleLineBorderTemplate;

            string actual = borderTemplate.GenerateHorizontalSeparator(new List<int>(), null);

            Assert.That(actual, Is.EqualTo(string.Empty));
        }

        [Test]
        public void returns_string_empty_if_both_column_lists_are_empty()
        {
            BorderTemplate borderTemplate = BorderTemplate.SingleLineBorderTemplate;

            string actual = borderTemplate.GenerateHorizontalSeparator(new List<int>(), new List<int>());

            Assert.That(actual, Is.EqualTo(string.Empty));
        }

        [Test]
        public void one_top_cell_equal_with_one_bottom_cell()
        {
            BorderTemplate borderTemplate = BorderTemplate.SingleLineBorderTemplate;
            List<int> topColumnWidths = new List<int> { 5 };
            List<int> bottomColumnWidths = new List<int> { 5 };

            string actual = borderTemplate.GenerateHorizontalSeparator(topColumnWidths, bottomColumnWidths);

            Assert.That(actual, Is.EqualTo("├─────┤"));
        }

        [Test]
        public void two_top_cells_equal_with_two_bottom_cells()
        {
            BorderTemplate borderTemplate = BorderTemplate.SingleLineBorderTemplate;
            List<int> topColumnWidths = new List<int> { 3, 2 };
            List<int> bottomColumnWidths = new List<int> { 3, 2 };

            string actual = borderTemplate.GenerateHorizontalSeparator(topColumnWidths, bottomColumnWidths);

            Assert.That(actual, Is.EqualTo("├───┼──┤"));
        }

        [Test]
        public void two_top_cells_unequal_with_two_bottom_cells()
        {
            BorderTemplate borderTemplate = BorderTemplate.SingleLineBorderTemplate;
            List<int> topColumnWidths = new List<int> { 3, 2 };
            List<int> bottomColumnWidths = new List<int> { 2, 3 };

            string actual = borderTemplate.GenerateHorizontalSeparator(topColumnWidths, bottomColumnWidths);

            Assert.That(actual, Is.EqualTo("├──┬┴──┤"));
        }

        [Test]
        public void one_top_cell_equal_with_two_bottom_cells()
        {
            BorderTemplate borderTemplate = BorderTemplate.SingleLineBorderTemplate;
            List<int> topColumnWidths = new List<int> { 6 };
            List<int> bottomColumnWidths = new List<int> { 2, 3 };

            string actual = borderTemplate.GenerateHorizontalSeparator(topColumnWidths, bottomColumnWidths);

            Assert.That(actual, Is.EqualTo("├──┬───┤"));
        }

        [Test]
        public void two_top_cells_equal_with_one_bottom_cell()
        {
            BorderTemplate borderTemplate = BorderTemplate.SingleLineBorderTemplate;
            List<int> topColumnWidths = new List<int> { 3, 2 };
            List<int> bottomColumnWidths = new List<int> { 6 };

            string actual = borderTemplate.GenerateHorizontalSeparator(topColumnWidths, bottomColumnWidths);

            Assert.That(actual, Is.EqualTo("├───┴──┤"));
        }

        [Test]
        public void one_top_cell_smaller_than_one_bottom_cell()
        {
            BorderTemplate borderTemplate = BorderTemplate.SingleLineBorderTemplate;
            List<int> topColumnWidths = new List<int> { 3 };
            List<int> bottomColumnWidths = new List<int> { 6 };

            string actual = borderTemplate.GenerateHorizontalSeparator(topColumnWidths, bottomColumnWidths);

            Assert.That(actual, Is.EqualTo("├───┴──┐"));
        }

        [Test]
        public void one_top_cell_greater_than_one_bottom_cell()
        {
            BorderTemplate borderTemplate = BorderTemplate.SingleLineBorderTemplate;
            List<int> topColumnWidths = new List<int> { 6 };
            List<int> bottomColumnWidths = new List<int> { 2 };

            string actual = borderTemplate.GenerateHorizontalSeparator(topColumnWidths, bottomColumnWidths);

            Assert.That(actual, Is.EqualTo("├──┬───┘"));
        }
    }
}