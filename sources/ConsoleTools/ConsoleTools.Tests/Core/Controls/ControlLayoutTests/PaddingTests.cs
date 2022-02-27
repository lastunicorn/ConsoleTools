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

using DustInTheWind.ConsoleTools.Controls;
using Moq;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Core.Controls.ControlLayoutTests
{
    [TestFixture]
    public class PaddingTests
    {
        private ControlLayout controlLayout;

        [SetUp]
        public void SetUp()
        {
            Mock<BlockControl> control = new Mock<BlockControl>();
            control.Object.Padding = "1 2 3 4";

            controlLayout = new ControlLayout
            {
                Control = control.Object
            };

            controlLayout.Calculate();
        }

        [Test]
        public void PaddingLeft_is_set_to_left_padding_value()
        {
            Assert.That(controlLayout.PaddingLeft, Is.EqualTo(1));
        }

        [Test]
        public void PaddingRight_is_set_to_right_padding_value()
        {
            Assert.That(controlLayout.PaddingRight, Is.EqualTo(3));
        }

        [Test]
        public void PaddingTop_is_set_to_top_padding_value()
        {
            Assert.That(controlLayout.PaddingTop, Is.EqualTo(2));
        }

        [Test]
        public void PaddingBottom_is_set_to_bottom_padding_value()
        {
            Assert.That(controlLayout.PaddingBottom, Is.EqualTo(4));
        }
    }
}