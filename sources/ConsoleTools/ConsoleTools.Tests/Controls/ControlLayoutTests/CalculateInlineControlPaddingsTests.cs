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
    public class CalculateInlineControlPaddingsTests
    {
        private Mock<IDisplay> display;

        [SetUp]
        public void SetUp()
        {
            display = new Mock<IDisplay>();
            display
                .Setup(x => x.AvailableWidth)
                .Returns(1024);
        }

        [Test]
        [TestCase(10)]
        [TestCase(8)]
        public void HavingInlineControlWithLeftPadding_WhenCalculating_ThenLeftPaddingIsSetCorrectly(int padding)
        {
            ControlLayout controlLayout = new()
            {
                Control = new FakeInlineControl
                {
                    PaddingLeft = padding
                },
                Display = display.Object
            };
            controlLayout.Calculate();

            Assert.That(controlLayout.PaddingLeft, Is.EqualTo(padding));
        }

        [Test]
        [TestCase(10)]
        [TestCase(8)]
        public void HavingInlineControlWithLeftPadding_WhenCalculating_ThenAllOtherPaddingsAreZero(int padding)
        {
            ControlLayout controlLayout = new()
            {
                Control = new FakeInlineControl
                {
                    PaddingLeft = padding
                },
                Display = display.Object
            };
            controlLayout.Calculate();

            Assert.That(controlLayout.PaddingRight, Is.Zero);
            Assert.That(controlLayout.PaddingTop, Is.Zero);
            Assert.That(controlLayout.PaddingBottom, Is.Zero);
        }

        [Test]
        [TestCase(10)]
        [TestCase(8)]
        public void HavingInlineControlWithRightPadding_WhenCalculating_ThenLeftPaddingIsSetCorrectly(int padding)
        {
            ControlLayout controlLayout = new()
            {
                Control = new FakeInlineControl
                {
                    PaddingRight = padding
                },
                Display = display.Object
            };
            controlLayout.Calculate();

            Assert.That(controlLayout.PaddingRight, Is.EqualTo(padding));
        }

        [Test]
        [TestCase(10)]
        [TestCase(8)]
        public void HavingInlineControlWithRightPadding_WhenCalculating_ThenAllOtherPaddingsAreZero(int padding)
        {
            ControlLayout controlLayout = new()
            {
                Control = new FakeInlineControl
                {
                    PaddingRight = padding
                },
                Display = display.Object
            };
            controlLayout.Calculate();

            Assert.That(controlLayout.PaddingLeft, Is.Zero);
            Assert.That(controlLayout.PaddingTop, Is.Zero);
            Assert.That(controlLayout.PaddingBottom, Is.Zero);
        }

        [Test]
        [TestCase(10, 3)]
        [TestCase(2, 8)]
        public void HavingInlineControlWithLeftAndRightPaddings_WhenCalculating_ThenLeftAndRightPaddingsAreSetCorrectly(int leftPadding, int rightPadding)
        {
            ControlLayout controlLayout = new()
            {
                Control = new FakeInlineControl
                {
                    PaddingLeft = leftPadding,
                    PaddingRight = rightPadding
                },
                Display = display.Object
            };
            controlLayout.Calculate();

            Assert.That(controlLayout.PaddingLeft, Is.EqualTo(leftPadding));
            Assert.That(controlLayout.PaddingRight, Is.EqualTo(rightPadding));
        }

        [Test]
        [TestCase(10, 3)]
        [TestCase(2, 8)]
        public void HavingInlineControlWithLeftAndRightPaddings_WhenCalculating_ThenTopAnfBottomPaddingsAreZero(int leftPadding, int rightPadding)
        {
            ControlLayout controlLayout = new()
            {
                Control = new FakeInlineControl
                {
                    PaddingLeft = leftPadding,
                    PaddingRight = rightPadding
                },
                Display = display.Object
            };
            controlLayout.Calculate();

            Assert.That(controlLayout.PaddingTop, Is.Zero);
            Assert.That(controlLayout.PaddingBottom, Is.Zero);
        }
    }
}