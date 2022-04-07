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
    public class CalculateNoControlTests
    {
        private ControlLayout controlLayout;

        [SetUp]
        public void SetUp()
        {
            controlLayout = new ControlLayout
            {
                AvailableWidth = 1024
            };
            controlLayout.Calculate();
        }

        [Test]
        public void HavingAControlLayoutWithoutAControl_WhenCalculating_ThenTopMarginIsZero()
        {
            Assert.That(controlLayout.MarginTop, Is.Zero);
        }

        [Test]
        public void HavingAControlLayoutWithoutAControl_WhenCalculating_ThenBottomMarginIsZero()
        {
            Assert.That(controlLayout.MarginBottom, Is.Zero);
        }

        [Test]
        public void HavingAControlLayoutWithoutAControl_WhenCalculating_ThenLeftMarginIsZero()
        {
            Assert.That(controlLayout.MarginLeft, Is.Zero);
        }

        [Test]
        public void HavingAControlLayoutWithoutAControl_WhenCalculating_ThenRightMarginIsZero()
        {
            Assert.That(controlLayout.MarginRight, Is.Zero);
        }

        [Test]
        public void HavingAControlLayoutWithoutAControl_WhenCalculating_ThenTopPaddingIsZero()
        {
            Assert.That(controlLayout.PaddingTop, Is.Zero);
        }

        [Test]
        public void HavingAControlLayoutWithoutAControl_WhenCalculating_ThenBottomPaddingIsZero()
        {
            Assert.That(controlLayout.PaddingBottom, Is.Zero);
        }

        [Test]
        public void HavingAControlLayoutWithoutAControl_WhenCalculating_ThenLeftPaddingIsZero()
        {
            Assert.That(controlLayout.PaddingLeft, Is.Zero);
        }

        [Test]
        public void HavingAControlLayoutWithoutAControl_WhenCalculating_ThenRightPaddingIsZero()
        {
            Assert.That(controlLayout.PaddingRight, Is.Zero);
        }

        [Test]
        public void HavingAControlLayoutWithoutAControl_WhenCalculating_ThenActualFullWidthIsZero()
        {
            Assert.That(controlLayout.ActualFullWidth, Is.Zero);
        }

        [Test]
        public void HavingAControlLayoutWithoutAControl_WhenCalculating_ThenActualWidthIsZero()
        {
            Assert.That(controlLayout.ActualWidth, Is.Zero);
        }

        [Test]
        public void HavingAControlLayoutWithoutAControl_WhenCalculating_ThenActualClientWidthIsZero()
        {
            Assert.That(controlLayout.ActualClientWidth, Is.Zero);
        }

        [Test]
        public void HavingAControlLayoutWithoutAControl_WhenCalculating_ThenActualContentWidthIsZero()
        {
            Assert.That(controlLayout.ActualContentWidth, Is.Zero);
        }

        [Test]
        public void HavingAControlLayoutWithoutAControl_WhenCalculating_ThenInnerEmptySpaceLeftIsZero()
        {
            Assert.That(controlLayout.InnerEmptySpaceLeft, Is.Zero);
        }

        [Test]
        public void HavingAControlLayoutWithoutAControl_WhenCalculating_ThenInnerEmptySpaceRightIsZero()
        {
            Assert.That(controlLayout.InnerEmptySpaceRight, Is.Zero);
        }

        [Test]
        public void HavingAControlLayoutWithoutAControl_WhenCalculating_ThenOuterEmptySpaceLeftIsZero()
        {
            Assert.That(controlLayout.OuterEmptySpaceLeft, Is.Zero);
        }

        [Test]
        public void HavingAControlLayoutWithoutAControl_WhenCalculating_ThenOuterEmptySpaceRightIsZero()
        {
            Assert.That(controlLayout.OuterEmptySpaceRight, Is.Zero);
        }
    }
}