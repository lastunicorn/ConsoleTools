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

using System.Linq;
using DustInTheWind.ConsoleTools.Controls;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Core.Controls.MultilineTextTests
{
    [TestFixture]
    public class GetLinesTest
    {
        [Test]
        public void single_line_infinite_width()
        {
            MultilineText multilineText = new MultilineText("1234567890");

            string[] lines = multilineText.GetLines().ToArray();

            Assert.That(lines, Is.EqualTo(new[] { "1234567890" }));
        }

        [Test]
        public void single_line_width_equal_to_line()
        {
            MultilineText multilineText = new MultilineText("1234567890");

            string[] lines = multilineText.GetLines(10).ToArray();

            Assert.That(lines, Is.EqualTo(new[] { "1234567890" }));
        }

        [Test]
        public void single_line_width_smaller_than_line()
        {
            MultilineText multilineText = new MultilineText("1234567890");

            string[] lines = multilineText.GetLines(7).ToArray();

            Assert.That(lines, Is.EqualTo(new[] { "1234567", "890" }));
        }

        [Test]
        public void two_lines_infinite_width()
        {
            MultilineText multilineText = new MultilineText(new[]
            {
                "1234567890",
                "abcdefg"
            });

            string[] lines = multilineText.GetLines().ToArray();

            Assert.That(lines, Is.EqualTo(new[] { "1234567890", "abcdefg" }));
        }

        [Test]
        public void two_lines_width_equal_to_biggest_line()
        {
            MultilineText multilineText = new MultilineText(new[]
            {
                "1234567890",
                "abcdefg"
            });

            string[] lines = multilineText.GetLines(10).ToArray();

            Assert.That(lines, Is.EqualTo(new[] { "1234567890", "abcdefg" }));
        }

        [Test]
        public void two_lines_width_equal_smaller_than_biggest_line()
        {
            MultilineText multilineText = new MultilineText(new[]
            {
                "1234567890",
                "abcdefg"
            });

            string[] lines = multilineText.GetLines(8).ToArray();

            Assert.That(lines, Is.EqualTo(new[] { "12345678", "90", "abcdefg" }));
        }

        [Test]
        public void two_lines_width_equal_smaller_than_smallest_line()
        {
            MultilineText multilineText = new MultilineText(new[]
            {
                "1234567890",
                "abcdefg"
            });

            string[] lines = multilineText.GetLines(5).ToArray();

            Assert.That(lines, Is.EqualTo(new[] { "12345", "67890", "abcde", "fg" }));
        }

        [Test]
        public void if_maxWidth_is_0_no_lines_are_returned()
        {
            MultilineText multilineText = new MultilineText(new[]
            {
                "1234567890",
                "abcdefg"
            });

            string[] lines = multilineText.GetLines(0).ToArray();

            Assert.That(lines, Is.Empty);
        }
    }
}