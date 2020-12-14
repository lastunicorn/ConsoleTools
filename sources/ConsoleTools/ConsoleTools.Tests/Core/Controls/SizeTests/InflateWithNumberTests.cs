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

using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Core.Controls.SizeTests
{
    [TestFixture]
    public class InflateWithNumberTests
    {
        [Test]
        public void positive_size_inflated_with_0_remains_the_same()
        {
            Size original = new Size(7, 5);

            Size actual = original.Inflate(0);

            Assert.That(actual, Is.EqualTo(original));
        }

        [Test]
        public void positive_size_inflated_with_positive_number()
        {
            Size original = new Size(7, 5);

            Size actual = original.Inflate(2);

            Assert.That(actual, Is.EqualTo(new Size(9, 7)));
        }

        [Test]
        public void positive_size_inflated_with_negative_number()
        {
            Size original = new Size(7, 5);

            Size actual = original.Inflate(-2);

            Assert.That(actual, Is.EqualTo(new Size(5, 3)));
        }
    }
}