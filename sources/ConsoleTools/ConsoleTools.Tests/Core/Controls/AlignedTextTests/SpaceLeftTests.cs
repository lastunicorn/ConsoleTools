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

namespace DustInTheWind.ConsoleTools.Tests.Core.Controls.AlignedTextTests
{
    [TestFixture]
    public class SpaceLeftTests
    {
        [Test]
        public void if_alignment_is_not_specified_return_0()
        {
            AlignedText alignedText = new AlignedText
            {
                Text = "some text",
                Width = 15
            };

            Assert.That(alignedText.SpaceLeftCount, Is.EqualTo(0));
        }

        [Test]
        public void text_aligend_to_left_returns_0()
        {
            AlignedText alignedText = new AlignedText
            {
                Text = "some text",
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 15
            };

            Assert.That(alignedText.SpaceLeftCount, Is.EqualTo(0));
        }

        [Test]
        public void text_with_length_9_aligend_to_center_in_width_15_returns_3()
        {
            AlignedText alignedText = new AlignedText
            {
                Text = "some text",
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = 15
            };

            Assert.That(alignedText.SpaceLeftCount, Is.EqualTo(3));
        }

        [Test]
        public void text_with_length_9_aligend_to_center_in_width_16_returns_3()
        {
            AlignedText alignedText = new AlignedText
            {
                Text = "some text",
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = 16
            };

            Assert.That(alignedText.SpaceLeftCount, Is.EqualTo(3));
        }

        [Test]
        public void text_with_length_9_aligend_to_right_in_width_15_returns_6()
        {
            AlignedText alignedText = new AlignedText
            {
                Text = "some text",
                HorizontalAlignment = HorizontalAlignment.Right,
                Width = 15
            };

            Assert.That(alignedText.SpaceLeftCount, Is.EqualTo(6));
        }

        [Test]
        public void text_with_length_9_aligend_to_default_in_width_15_returns_0()
        {
            AlignedText alignedText = new AlignedText
            {
                Text = "some text",
                HorizontalAlignment = HorizontalAlignment.Default,
                Width = 15
            };

            Assert.That(alignedText.SpaceLeftCount, Is.EqualTo(0));
        }
    }
}