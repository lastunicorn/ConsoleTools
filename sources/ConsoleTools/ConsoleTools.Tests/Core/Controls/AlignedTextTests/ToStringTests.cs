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

using DustInTheWind.ConsoleTools.Controls;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Core.Controls.AlignedTextTests
{
    [TestFixture]
    public class ToStringTests
    {
        [Test]
        public void if_alignment_is_not_specified_then_text_is_aligend_to_left()
        {
            AlignedText alignedText = new AlignedText
            {
                Text = "some text",
                Width = 15
            };

            string actual = alignedText.ToString();

            Assert.That(actual, Is.EqualTo("some text      "));
        }

        [Test]
        public void text_aligend_to_left()
        {
            AlignedText alignedText = new AlignedText
            {
                Text = "some text",
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 15
            };

            string actual = alignedText.ToString();

            Assert.That(actual, Is.EqualTo("some text      "));
        }

        [Test]
        public void text_aligend_to_center_even_empty_space()
        {
            AlignedText alignedText = new AlignedText
            {
                Text = "some text",
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = 15
            };

            string actual = alignedText.ToString();

            Assert.That(actual, Is.EqualTo("   some text   "));
        }

        [Test]
        public void text_aligend_to_center_odd_empty_space()
        {
            AlignedText alignedText = new AlignedText
            {
                Text = "some text",
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = 16
            };

            string actual = alignedText.ToString();

            Assert.That(actual, Is.EqualTo("   some text    "));
        }

        [Test]
        public void text_aligend_to_right()
        {
            AlignedText alignedText = new AlignedText
            {
                Text = "some text",
                HorizontalAlignment = HorizontalAlignment.Right,
                Width = 15
            };

            string actual = alignedText.ToString();

            Assert.That(actual, Is.EqualTo("      some text"));
        }

        [Test]
        public void text_aligend_to_default()
        {
            AlignedText alignedText = new AlignedText
            {
                Text = "some text",
                HorizontalAlignment = HorizontalAlignment.Default,
                Width = 15
            };

            string actual = alignedText.ToString();

            Assert.That(actual, Is.EqualTo("some text      "));
        }
    }
}