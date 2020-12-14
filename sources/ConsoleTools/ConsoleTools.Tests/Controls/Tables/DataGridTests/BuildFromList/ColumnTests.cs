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

using System.Collections.Generic;
using DustInTheWind.ConsoleTools.Controls.Tables;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.DataGridTests.BuildFromList
{
    [TestFixture]
    public class ColumnTests
    {
        private class GetSetProperty
        {
            public int Number { get; set; }
        }

        [Test]
        public void public_get_set_property_generates_one_column()
        {
            List<GetSetProperty> data = new List<GetSetProperty>();

            DataGrid actual = DataGrid.BuildFrom(data);

            Assert.That(actual.Columns.Count, Is.EqualTo(1));
            Assert.That(actual.Columns[0].HeaderCell.Content, Is.EqualTo(new MultilineText("Number")));
        }

        private class GetOnlyProperty
        {
            public int Number { get; }
        }

        [Test]
        public void public_get_only_property_generates_one_column()
        {
            List<GetOnlyProperty> data = new List<GetOnlyProperty>();

            DataGrid actual = DataGrid.BuildFrom(data);

            Assert.That(actual.Columns.Count, Is.EqualTo(1));
            Assert.That(actual.Columns[0].HeaderCell.Content, Is.EqualTo(new MultilineText("Number")));
        }

        private class SetOnlyProperty
        {
            private int number;

            public int Number
            {
                set => number = value;
            }
        }

        [Test]
        public void public_set_only_property_generates_no_column()
        {
            List<SetOnlyProperty> data = new List<SetOnlyProperty>();

            DataGrid actual = DataGrid.BuildFrom(data);

            Assert.That(actual.Columns.Count, Is.EqualTo(0));
        }

        private class PrivateGetSetProperty
        {
            private int Number { get; set; }
        }

        [Test]
        public void private_get_set_property_generates_no_column()
        {
            List<PrivateGetSetProperty> data = new List<PrivateGetSetProperty>();

            DataGrid actual = DataGrid.BuildFrom(data);

            Assert.That(actual.Columns.Count, Is.EqualTo(0));
        }

        private class ReadWriteField
        {
            public int number;
        }

        [Test]
        public void public_read_write_field_generates_one_column()
        {
            List<ReadWriteField> data = new List<ReadWriteField>();

            DataGrid actual = DataGrid.BuildFrom(data);

            Assert.That(actual.Columns.Count, Is.EqualTo(1));
            Assert.That(actual.Columns[0].HeaderCell.Content, Is.EqualTo(new MultilineText("number")));
        }

        private class ReadOnlyField
        {
            public readonly int number;
        }

        [Test]
        public void public_read_only_field_generates_one_column()
        {
            List<ReadOnlyField> data = new List<ReadOnlyField>();

            DataGrid actual = DataGrid.BuildFrom(data);

            Assert.That(actual.Columns.Count, Is.EqualTo(1));
            Assert.That(actual.Columns[0].HeaderCell.Content, Is.EqualTo(new MultilineText("number")));
        }

        private class PrivateField
        {
            private int number;
        }

        [Test]
        public void private_field_generates_no_column()
        {
            List<PrivateField> data = new List<PrivateField>();

            DataGrid actual = DataGrid.BuildFrom(data);

            Assert.That(actual.Columns.Count, Is.EqualTo(0));
        }

        private class PublicMethod
        {
            public int GetNumber()
            {
                return 5;
            }
        }

        [Test]
        public void public_method_generates_no_column()
        {
            List<PublicMethod> data = new List<PublicMethod>();

            DataGrid actual = DataGrid.BuildFrom(data);

            Assert.That(actual.Columns.Count, Is.EqualTo(0));
        }
    }
}