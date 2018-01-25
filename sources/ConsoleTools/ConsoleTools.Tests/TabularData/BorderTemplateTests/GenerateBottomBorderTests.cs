﻿// ConsoleTools
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

namespace DustInTheWind.ConsoleTools.Tests.TabularData.BorderTemplateTests
{
    [TestFixture]
    public class GenerateBottomBorderTests
    {
        [Test]
        public void returns_string_empty_if_no_column_is_provided()
        {
            BorderTemplate borderTemplate = BorderTemplate.SingleLineBorderTemplate;

            string actual = borderTemplate.GenerateBottomBorder();

            Assert.That(actual, Is.EqualTo(string.Empty));
        }

        [Test]
        public void one_column_with_length_0()
        {
            BorderTemplate borderTemplate = BorderTemplate.SingleLineBorderTemplate;

            string actual = borderTemplate.GenerateBottomBorder(0);

            Assert.That(actual, Is.EqualTo("└┘"));
        }

        [Test]
        public void one_column_with_length_5()
        {
            BorderTemplate borderTemplate = BorderTemplate.SingleLineBorderTemplate;

            string actual = borderTemplate.GenerateBottomBorder(5);

            Assert.That(actual, Is.EqualTo("└─────┘"));
        }

        [Test]
        public void two_columns_with_length_0()
        {
            BorderTemplate borderTemplate = BorderTemplate.SingleLineBorderTemplate;

            string actual = borderTemplate.GenerateBottomBorder(0, 0);

            Assert.That(actual, Is.EqualTo("└┴┘"));
        }

        [Test]
        public void two_columns_with_length_0_and_5()
        {
            BorderTemplate borderTemplate = BorderTemplate.SingleLineBorderTemplate;

            string actual = borderTemplate.GenerateBottomBorder(0, 5);

            Assert.That(actual, Is.EqualTo("└┴─────┘"));
        }

        [Test]
        public void two_columns_with_length_5_and_0()
        {
            BorderTemplate borderTemplate = BorderTemplate.SingleLineBorderTemplate;

            string actual = borderTemplate.GenerateBottomBorder(5, 0);

            Assert.That(actual, Is.EqualTo("└─────┴┘"));
        }

        [Test]
        public void two_columns_with_length_5_and_5()
        {
            BorderTemplate borderTemplate = BorderTemplate.SingleLineBorderTemplate;

            string actual = borderTemplate.GenerateBottomBorder(5, 5);

            Assert.That(actual, Is.EqualTo("└─────┴─────┘"));
        }

        [Test]
        public void two_columns_with_length_5_and_5_List()
        {
            BorderTemplate borderTemplate = BorderTemplate.SingleLineBorderTemplate;

            string actual = borderTemplate.GenerateBottomBorder(new List<int> { 5, 5});

            Assert.That(actual, Is.EqualTo("└─────┴─────┘"));
        }
    }
}