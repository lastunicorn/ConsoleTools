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

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.NormalCellTests
{
    [TestFixture]
    public class ConstructorObjectAndAlignmentTests
    {
        private NormalCell normalCell;

        private class Content
        {
            public override string ToString()
            {
                return "content";
            }
        }

        [SetUp]
        public void SetUp()
        {
            Content content = new Content();
            normalCell = new NormalCell(content, HorizontalAlignment.Center);
        }

        [Test]
        public void Content_is_the_one_provided_on_constructor()
        {
            Assert.That(normalCell.Content, Is.EqualTo(new MultilineText("content")));
        }

        [Test]
        public void IsEmpty_is_false()
        {
            Assert.That(normalCell.IsEmpty, Is.False);
        }

        [Test]
        public void HorizontalAlignment_is_Center()
        {
            Assert.That(normalCell.HorizontalAlignment, Is.EqualTo(HorizontalAlignment.Center));
        }
    }
}