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

namespace DustInTheWind.ConsoleTools.Tests.SizeTests
{
    [TestFixture]
    public class ConstructorTests
    {
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(154)]
        [TestCase(-1250)]
        public void Width_is_initialized_with_the_value_provided_on_the_constructor(int width)
        {
            Size size = new Size(width, 0);

            Assert.That(size.Width, Is.EqualTo(width));
        }

        [TestCase(0)]
        [TestCase(7)]
        [TestCase(20)]
        [TestCase(-101)]
        public void Height_is_initialized_with_the_value_provided_on_the_constructor(int height)
        {
            Size size = new Size(0, height);

            Assert.That(size.Height, Is.EqualTo(height));
        }
    }
}