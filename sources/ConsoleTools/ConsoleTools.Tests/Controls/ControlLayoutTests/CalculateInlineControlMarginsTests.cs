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
    public class CalculateInlineControlMarginsTests
    {
        [Test]
        [TestCase(10)]
        [TestCase(8)]
        public void HavingInlineControlWithLeftMargin_WhenCalculating_ThenLeftMarginIsSetCorrectly(int margin)
        {
            ControlLayout controlLayout = new()
            {
                Control = new FakeInlineControl
                {
                    MarginLeft = margin
                }
            };
            controlLayout.Calculate();

            Assert.That(controlLayout.MarginLeft, Is.EqualTo(margin));
        }

        [Test]
        [TestCase(10)]
        [TestCase(8)]
        public void HavingInlineControlWithLeftMargin_WhenCalculating_ThenAllOtherMarginsAreZero(int margin)
        {
            ControlLayout controlLayout = new()
            {
                Control = new FakeInlineControl
                {
                    MarginLeft = margin
                }
            };
            controlLayout.Calculate();

            Assert.That(controlLayout.MarginRight, Is.Zero);
            Assert.That(controlLayout.MarginTop, Is.Zero);
            Assert.That(controlLayout.MarginBottom, Is.Zero);
        }

        [Test]
        [TestCase(10)]
        [TestCase(8)]
        public void HavingInlineControlWithRightMargin_WhenCalculating_ThenLeftMarginIsSetCorrectly(int margin)
        {
            ControlLayout controlLayout = new()
            {
                Control = new FakeInlineControl
                {
                    MarginRight = margin
                }
            };
            controlLayout.Calculate();

            Assert.That(controlLayout.MarginRight, Is.EqualTo(margin));
        }

        [Test]
        [TestCase(10)]
        [TestCase(8)]
        public void HavingInlineControlWithRightMargin_WhenCalculating_ThenAllOtherMarginsAreZero(int margin)
        {
            ControlLayout controlLayout = new()
            {
                Control = new FakeInlineControl
                {
                    MarginRight = margin
                }
            };
            controlLayout.Calculate();

            Assert.That(controlLayout.MarginLeft, Is.Zero);
            Assert.That(controlLayout.MarginTop, Is.Zero);
            Assert.That(controlLayout.MarginBottom, Is.Zero);
        }

        [Test]
        [TestCase(10, 3)]
        [TestCase(2, 8)]
        public void HavingInlineControlWithLeftAndRightMargins_WhenCalculating_ThenLeftAndRightMarginsAreSetCorrectly(int leftMargin, int rightMargin)
        {
            ControlLayout controlLayout = new()
            {
                Control = new FakeInlineControl
                {
                    MarginLeft = leftMargin,
                    MarginRight = rightMargin
                }
            };
            controlLayout.Calculate();

            Assert.That(controlLayout.MarginLeft, Is.EqualTo(leftMargin));
            Assert.That(controlLayout.MarginRight, Is.EqualTo(rightMargin));
        }

        [Test]
        [TestCase(10, 3)]
        [TestCase(2, 8)]
        public void HavingInlineControlWithLeftAndRightMargins_WhenCalculating_ThenTopAnfBottomMarginsAreZero(int leftMargin, int rightMargin)
        {
            ControlLayout controlLayout = new()
            {
                Control = new FakeInlineControl
                {
                    MarginLeft = leftMargin,
                    MarginRight = rightMargin
                }
            };
            controlLayout.Calculate();

            Assert.That(controlLayout.MarginTop, Is.Zero);
            Assert.That(controlLayout.MarginBottom, Is.Zero);
        }
    }
}