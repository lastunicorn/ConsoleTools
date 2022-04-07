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

using DustInTheWind.ConsoleTools.Controls;
using Moq;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Controls.ControlLayoutTests
{
    [TestFixture]
    public class CalculateBlockControlPaddingsTests
    {
        [Test]
        [TestCase("10", 10)]
        [TestCase("5 10", 10)]
        [TestCase("1 10 2 3", 10)]
        [TestCase("7", 7)]
        [TestCase("20 7", 7)]
        [TestCase("21 7 22 23", 7)]
        public void HavingControlWithPaddings_WhenCalculating_ThenTopPaddingIsSetCorrectly(string padding, int expectedTopPadding)
        {
            ControlLayout controlLayout = new()
            {
                Control = new FakeBlockControl
                {
                    Padding = padding
                }
            };
            controlLayout.Calculate();

            Assert.That(controlLayout.PaddingTop, Is.EqualTo(expectedTopPadding));
        }

        [Test]
        [TestCase("10", 10)]
        [TestCase("5 10", 10)]
        [TestCase("1 2 3 10", 10)]
        [TestCase("7", 7)]
        [TestCase("20 7", 7)]
        [TestCase("21 22 23 7", 7)]
        public void HavingControlWithPaddings_WhenCalculating_ThenBottomPaddingIsSetCorrectly(string padding, int expectedBottomPadding)
        {
            ControlLayout controlLayout = new()
            {
                Control = new FakeBlockControl
                {
                    Padding = padding
                }
            };
            controlLayout.Calculate();

            Assert.That(controlLayout.PaddingBottom, Is.EqualTo(expectedBottomPadding));
        }

        [Test]
        [TestCase("10", 10)]
        [TestCase("10 2", 10)]
        [TestCase("10 1 2 3", 10)]
        [TestCase("7", 7)]
        [TestCase("7 20", 7)]
        [TestCase("7 21 22 23", 7)]
        public void HavingControlWithPaddings_WhenCalculating_ThenLeftPaddingIsSetCorrectly(string padding, int expectedLeftPadding)
        {
            ControlLayout controlLayout = new()
            {
                Control = new FakeBlockControl
                {
                    Padding = padding
                }
            };
            controlLayout.Calculate();

            Assert.That(controlLayout.PaddingLeft, Is.EqualTo(expectedLeftPadding));
        }

        [Test]
        [TestCase("10", 10)]
        [TestCase("10 2", 10)]
        [TestCase("1 2 10 3", 10)]
        [TestCase("7", 7)]
        [TestCase("7 20", 7)]
        [TestCase("21 22 7 23", 7)]
        public void HavingControlWithPaddings_WhenCalculating_ThenRightPaddingIsSetCorrectly(string padding, int expectedRightPadding)
        {
            ControlLayout controlLayout = new()
            {
                Control = new FakeBlockControl
                {
                    Padding = padding
                }
            };
            controlLayout.Calculate();

            Assert.That(controlLayout.PaddingRight, Is.EqualTo(expectedRightPadding));
        }
    }
}